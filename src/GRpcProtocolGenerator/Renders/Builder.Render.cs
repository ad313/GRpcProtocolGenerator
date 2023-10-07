﻿using GRpcProtocolGenerator.Renders.Protocol;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GRpcProtocolGenerator.Renders
{
    /// <summary>
    /// 生成代码
    /// </summary>
    public partial class Builder
    {
        /// <summary>
        /// 生成代码
        /// </summary>
        /// <returns></returns>
        public async Task RenderAsync()
        {
            BuilderPath.Init(Config);

            foreach (var service in Services.OrderBy(d => d.InterfaceMetaData.FullName))
            {
                //proto
                var protoContent = new ProtocolContent(service, Config);
                var content = protoContent.ToContent();
                await BuilderPath.CreateFile(service.InterfaceMetaData.FormatServiceProtoFileName(), content);
                Console.WriteLine(service.InterfaceMetaData.FormatServiceProtoFileName());
                Console.WriteLine(content);

                //server
                if (Config.HasServer)
                {
                    var server = new ProtocolServiceImpl(protoContent);
                    var serverContent = server.ToContent();
                    await BuilderPath.CreateServerFile(server.ServerName, serverContent);
                    Console.WriteLine(serverContent);
                }
            }

            if (Config.HasServer)
            {
                //mapper
                var mapperString = await ScribanHelper.Render(new { data = Config.Server.ProjectName }, "Server.MapperRegister");
                await BuilderPath.CreateServerMapperFile("DefaultMapperConfig", mapperString);

                // program
                var servers = Services.Select(d => new ProtocolServiceImpl(new ProtocolContent(d, Config))).ToList();
                var serverProgramString = await ScribanHelper.Render(new { config = Config, servers = servers }, "Server.Program");
                var serverProgramFileName = "Program";

                // 如果 program.cs 已存在，则生成 programNew.cs，不覆盖原来的 program.cs
                if (File.Exists(Config.Server.GetProgramFilePath()))
                {
                    serverProgramString = "/*" + Environment.NewLine + serverProgramString + Environment.NewLine + "*/";
                    serverProgramFileName += "New";
                }

                await BuilderPath.CreateServerRootFile(serverProgramFileName + ".cs", serverProgramString);


                // server csproj
                var serverCsprojString = await ScribanHelper.Render(new { config = Config, servers = Services }, "Server.csproj");
                var serverCsprojFileName = Config.Server.ProjectName + ".csproj";

                // 如果 csproj 已存在，则生成 csprojNew，不覆盖原来的 csproj
                if (File.Exists(Config.Server.GetCsprojFilePath()))
                {
                    serverCsprojFileName += "New";
                }

                await BuilderPath.CreateServerRootFile(serverCsprojFileName, serverCsprojString);
            }

            // proto csproj
            var protoCsprojString = await ScribanHelper.Render(new { config = Config, servers = Services }, "Protocol.csproj");
            var protoCsprojFileName = Config.Proto.ProjectName + ".csproj";

            // 如果 csproj 已存在，则生成 csprojNew，不覆盖原来的 csproj
            if (File.Exists(Config.Proto.GetCsprojFilePath()))
            {
                protoCsprojFileName += "New";
            }

            await BuilderPath.CreateProtoRootFile(protoCsprojFileName, protoCsprojString);

            //google api
            if (Config.JsonTranscoding.UseJsonTranscoding)
            {
                var annotations = await ScribanHelper.Render(new { }, "google.api.annotations");
                var http = await ScribanHelper.Render(new { }, "google.api.http");

                await BuilderPath.CreateProtoGoogleApiFile("annotations.proto", annotations);
                await BuilderPath.CreateProtoGoogleApiFile("http.proto", http);

                //swagger
                var swaggerProgramString = await ScribanHelper.Render(new { config = Config }, "Server.Swagger");
                var swaggerProgramFileName = "SwaggerExtensions";
                await BuilderPath.CreateServerRootFile(swaggerProgramFileName + ".cs", swaggerProgramString);
            }

            //不支持的方法
            Console.WriteLine($"不支持的方法：{_assemblyMetaData.NotSupportMethodList.Count}");
            foreach (var method in _assemblyMetaData.NotSupportMethodList.OrderBy(d => d.Name))
            {
                Console.WriteLine(method.DeclaringType?.FullName + "." + method.Name);
            }

            Console.WriteLine("生成完毕");
        }
    }
}
