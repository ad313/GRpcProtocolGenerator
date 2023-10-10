using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Renders.Protocol;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            BuilderPath.Init(Config.ConfigInstance);

            foreach (var service in Services.OrderBy(d => d.InterfaceMetaData.FullName))
            {
                //proto
                var protoContent = new ProtocolContent(service);
                var content = protoContent.ToContent();
                await BuilderPath.CreateFile(service.InterfaceMetaData.FormatServiceProtoFileName(), content);
                Console.WriteLine(service.InterfaceMetaData.FormatServiceProtoFileName());
                Console.WriteLine(content);

                //server
                if (Config.ConfigInstance.HasServer)
                {
                    var server = new ServerServiceImpl(protoContent);
                    var serverContent = server.ToContent();
                    await BuilderPath.CreateServerFile(server.ServerName, serverContent);
                    Console.WriteLine(serverContent);
                }
            }

            if (Config.ConfigInstance.HasServer)
            {
                //mapper
                var mapperString = await ScribanHelper.Render(new { data = Config.ConfigInstance.Server.ProjectName }, "Server.MapperRegister");
                await BuilderPath.CreateServerMapperFile("DefaultMapperConfig", mapperString);

                // program
                var servers = Services.Select(d => new ServerServiceImpl(new ProtocolContent(d))).ToList();
                var serverProgramString = await ScribanHelper.Render(new { config = Config.ConfigInstance, servers = servers }, "Server.Program");
                var serverProgramFileName = "Program";

                // 如果 program.cs 已存在，则生成 programNew.cs，不覆盖原来的 program.cs
                if (File.Exists(Config.ConfigInstance.Server.GetProgramFilePath()))
                {
                    serverProgramString = "/*" + Environment.NewLine + serverProgramString + Environment.NewLine + "*/";
                    serverProgramFileName += "New";
                }

                await BuilderPath.CreateServerRootFile(serverProgramFileName + ".cs", serverProgramString);


                // server csproj
                var serverCsprojString = await ScribanHelper.Render(new { config = Config.ConfigInstance, servers = Services }, "Server.csproj");
                var serverCsprojFileName = Config.ConfigInstance.Server.ProjectName + ".csproj";

                // 如果 csproj 已存在，则生成 csprojNew，不覆盖原来的 csproj
                if (File.Exists(Config.ConfigInstance.Server.GetCsprojFilePath()))
                {
                    serverCsprojFileName += "New";
                }

                await BuilderPath.CreateServerRootFile(serverCsprojFileName, serverCsprojString);


                //appsettings.json
                var appSettingsJson = await ScribanHelper.Render(new { config = Config.ConfigInstance }, "Server.AppSettings");
                var appSettingsJsonFileName = "appsettings.json";
                await BuilderPath.CreateFileAsync(Config.ConfigInstance.Server.OutputFullPath, appSettingsJsonFileName, appSettingsJson, false);
            }

            // proto csproj
            var protoCsprojString = await ScribanHelper.Render(new { config = Config.ConfigInstance, servers = Services }, "Protocol.csproj");
            var protoCsprojFileName = Config.ConfigInstance.Proto.ProjectName + ".csproj";

            // 如果 csproj 已存在，则生成 csprojNew，不覆盖原来的 csproj
            if (File.Exists(Config.ConfigInstance.Proto.GetCsprojFilePath()))
            {
                protoCsprojFileName += "New";
            }

            await BuilderPath.CreateProtoRootFile(protoCsprojFileName, protoCsprojString);

            //google api
            if (Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding)
            {
                var annotations = await ScribanHelper.Render(new { }, "google.api.annotations");
                var http = await ScribanHelper.Render(new { }, "google.api.http");

                await BuilderPath.CreateProtoGoogleApiFile("annotations.proto", annotations);
                await BuilderPath.CreateProtoGoogleApiFile("http.proto", http);

                //swagger
                var swaggerProgramString = await ScribanHelper.Render(new { config = Config.ConfigInstance }, "Server.Swagger");
                var swaggerProgramFileName = "SwaggerExtensions";
                await BuilderPath.CreateServerRootFile(swaggerProgramFileName + ".cs", swaggerProgramString);
            }

            //不支持的方法
            Console.WriteLine($"不支持的方法：{_assemblyMetaData.NotSupportMethodList.Count}");
            foreach (var method in _assemblyMetaData.NotSupportMethodList.OrderBy(d => d.Name))
            {
                Console.WriteLine(method.DeclaringType?.FullName + "." + method.Name);
            }

            // 生成客户端包装
            await RenderClientWrapper();

            // 生成控制器
            await RenderController();

            Console.WriteLine("生成完毕");
        }

        /// <summary>
        /// 生成客户端包装
        /// </summary>
        /// <returns></returns>
        private async Task RenderClientWrapper()
        {
            if (Config.ConfigInstance.ClientWrapper == null)
                return;

            var interfaces = _assemblyMetaData.InterfaceMetaDataDictionary.Select(d => d.Value).ToList();

            //生成 GRpcClientExtensions.cs
            var extensionString = await ScribanHelper.Render(new { config = Config.ConfigInstance, servers = interfaces }, "Client.Extensions");
            await BuilderPath.CreateFileAsync(Config.ConfigInstance.ClientWrapper.OutputFullPath, "GRpcClientExtensions.cs", extensionString, true);

            //生成 GRpcClientProvider.cs
            var providerString = await ScribanHelper.Render(new { config = Config.ConfigInstance, servers = interfaces }, "Client.Provider");
            await BuilderPath.CreateFileAsync(Config.ConfigInstance.ClientWrapper.OutputFullPath, "GRpcClientProvider.cs", providerString, true);

            //生成服务代理
            foreach (var service in Services)
            {
                var name = $"GRpc{service.Name.TrimStart('I')}Client";

                var str = await ScribanHelper.Render(new
                {
                    config = Config.ConfigInstance,
                    data = new
                    {
                        service = service,
                        name = name,
                        protoNamespace= Config.ConfigInstance.Proto.GetCSharpNamespace(service.InterfaceMetaData),
                        description= Config.ConfigInstance.Proto.PropertyDescriptionFunc(service.InterfaceMetaData),
                        gRpcServiceName =service.InterfaceMetaData.FormatServiceName(),
                    }
                }, "Client.Client");
                
                await BuilderPath.CreateFileAsync(Config.ConfigInstance.ClientWrapper.OutputFullPath, $"I{name}.cs", str, true);
            }

            // csproj
            var csprojString = await ScribanHelper.Render(new { config = Config.ConfigInstance }, "Client.csproj");
            var csprojFileName = Config.ConfigInstance.ClientWrapper.ProjectName + ".csproj";

            // 如果 csproj 已存在，则生成 csprojNew，不覆盖原来的 csproj
            if (File.Exists(Config.ConfigInstance.ClientWrapper.GetCsprojFilePath()))
            {
                csprojFileName += "New";
            }

            await BuilderPath.CreateFileAsync(Config.ConfigInstance.ClientWrapper.OutputFullPath, csprojFileName, csprojString, true);
        }

        /// <summary>
        /// 生成控制器
        /// </summary>
        /// <returns></returns>
        private async Task RenderController()
        {
            if (Config.ConfigInstance.Controller == null)
                return;

            var interfaces = _assemblyMetaData.InterfaceMetaDataDictionary.Select(d => d.Value).ToList();

            //生成服务代理
            foreach (var service in Services)
            {
                var name = service.Name.FormatGRpcClientName();

                var str = await ScribanHelper.Render(new
                {
                    config = Config.ConfigInstance,
                    data = new
                    {
                        service = new ControllerImpl(service),
                        name_space = Config.ConfigInstance.Controller.GetControllerNamespace(),
                        name = name,
                        protoNamespace = Config.ConfigInstance.Proto.GetCSharpNamespace(service.InterfaceMetaData),
                        description = Config.ConfigInstance.Proto.PropertyDescriptionFunc(service.InterfaceMetaData) ?? service.InterfaceMetaData.FullName,
                        gRpcServiceName = service.InterfaceMetaData.FormatServiceName(),
                    }
                }, "Controller.Controller");

                await BuilderPath.CreateFileAsync(Config.ConfigInstance.Controller.GetControllerFileOutputPath(), $"{service.Name.TrimStart('I')}Controller.cs", str, true);
            }

            // csproj
            var csprojString = await ScribanHelper.Render(new { config = Config.ConfigInstance }, "Controller.csproj");
            var csprojFileName = Config.ConfigInstance.Controller.ProjectName + ".csproj";

            // 如果 csproj 已存在，则生成 csprojNew，不覆盖原来的 csproj
            if (File.Exists(Config.ConfigInstance.Controller.GetCsprojFilePath()))
            {
                csprojFileName += "New";
            }

            await BuilderPath.CreateFileAsync(Config.ConfigInstance.Controller.OutputFullPath, csprojFileName, csprojString, true);




            // program
            var controllerProgramString = await ScribanHelper.Render(new { config = Config.ConfigInstance }, "Controller.Program");
            var controllerProgramFileName = "Program.cs";

            // 如果 program.cs 已存在，则生成 programNew.cs，不覆盖原来的 program.cs
            if (File.Exists(Config.ConfigInstance.Controller.GetProgramFilePath()))
            {
                controllerProgramString = "/*" + Environment.NewLine + controllerProgramString + Environment.NewLine + "*/";
                controllerProgramFileName += "New";
            }

            await BuilderPath.CreateFileAsync(Config.ConfigInstance.Controller.OutputFullPath, controllerProgramFileName, controllerProgramString, true);



            //swagger
            if (Config.ConfigInstance.Controller.Swagger != null)
            {
                var swaggerProgramString = await ScribanHelper.Render(new { config = Config.ConfigInstance }, "Controller.Swagger");
                var swaggerProgramFileName = "SwaggerExtensions.cs";
                await BuilderPath.CreateFileAsync(Config.ConfigInstance.Controller.OutputFullPath, swaggerProgramFileName, swaggerProgramString, true);
            }


            //appsettings.json
            var appSettingsJson = await ScribanHelper.Render(new { config = Config.ConfigInstance }, "Controller.AppSettings");
            var appSettingsJsonFileName = "appsettings.json";
            await BuilderPath.CreateFileAsync(Config.ConfigInstance.Controller.OutputFullPath, appSettingsJsonFileName, appSettingsJson, false);
        }
    }
}
