using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Renders.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GRpcProtocolGenerator.Models.Abstracts;
using GRpcProtocolGenerator.Types;
using System.Text.RegularExpressions;

namespace GRpcProtocolGenerator.Renders
{
    public class GoGRpcServerIml
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsInEmpty { get; set; }

        public bool IsOutEmpty { get; set; }

        public string InParam { get; set; }

        public string OutParam { get; set; }

        public string StoreInParam { get; set; }

        public string StoreOutParam { get; set; }

        public GoGRpcServerIml(ProtoServiceItem item)
        {
            IsInEmpty = item.MethodMetaData.InParamMetaDataList.Count == 0;
            IsOutEmpty = item.MethodMetaData.OutParamMetaDataList.Count == 0;

            Name = item.Name;
            Description = Config.ConfigInstance.Proto.PropertyDescriptionFunc(item.MethodMetaData);

            if (IsInEmpty)
            {
                InParam = "emptypb.Empty";
                StoreInParam = "ctx";
            }
            else
            {
                InParam = $"{Config.ConfigInstance.GoServer.ProtoPackageName}.{item.InParam.GetGRpcName()}";
                StoreInParam = "ctx, request";
            }

            if (IsOutEmpty)
            {
                OutParam = "emptypb.Empty";
                StoreOutParam = "err";
            }
            else
            {
                OutParam = $"{Config.ConfigInstance.GoServer.ProtoPackageName}.{item.OutParam.GetGRpcName()}";
                StoreOutParam = "data, err";
            }
        }
    }

    /// <summary>
    /// 生成代码
    /// </summary>
    public partial class Builder
    {
        /// <summary>
        /// 生成代码 
        /// </summary>
        /// <returns></returns>
        public async Task RenderGoLangAsync()
        {
            BuilderPath.Init(Config.ConfigInstance);

            foreach (var service in Services.OrderBy(d => d.InterfaceMetaData.FullName))
            {
                //proto
                var protoContent = new ProtocolContent(service);
                var content = protoContent.ToContent();
                await BuilderPath.CreateFile(service.InterfaceMetaData.FormatServiceProtoFileName().Replace("_service", ""), content);
                Console.WriteLine(service.InterfaceMetaData.FormatServiceProtoFileName());
                Console.WriteLine(content);

                //server
                Config.ConfigInstance.GoServer.ServerName = Config.ConfigInstance.GoServer.GetServerName(service.InterfaceMetaData);
                Config.ConfigInstance.GoServer.Description = Config.ConfigInstance.Proto.PropertyDescriptionFunc(service.InterfaceMetaData);
                Config.ConfigInstance.GoServer.ServiceServerName = service.InterfaceMetaData.FormatServiceName() + "Server";
                Config.ConfigInstance.GoServer.ProtoNamespace = Config.ConfigInstance.Proto.GetGoPackageName(service.InterfaceMetaData);
                Config.ConfigInstance.GoServer.ProtoPackageName = Config.ConfigInstance.Proto.PackageNameFunc(service.InterfaceMetaData);
                Config.ConfigInstance.GoServer.Lite = service.Name.TrimStart('I').Replace("Service", "");

                var last = Config.ConfigInstance.GoServer.ProtoNamespace.Split('/').Last();
                Config.ConfigInstance.GoServer.ProtoNamespace = Config.ConfigInstance.GoServer.ProtoNamespace.Replace($"/{last}", "");

                var items = new List<GoGRpcServerIml>();
                foreach (var serviceItem in service.Items)
                {
                    var item = new GoGRpcServerIml(serviceItem);
                    items.Add(item);
                }

                //server
                var str = await ScribanHelper.Render(new { config = Config.ConfigInstance, data = items }, "go.server");
                await BuilderPath.CreateFileAsync(Config.ConfigInstance.GoServer.OutputFullPath, Config.ConfigInstance.GoServer.GetServerFileName(service.InterfaceMetaData), str, true);
                
                //dto
                var dtoString = protoContent.ToGoStructContent();
                await BuilderPath.CreateFileAsync(Config.ConfigInstance.GoStruct.OutputFullPath, $"{Config.ConfigInstance.GoServer.Lite.ToSnakeString()}.go", dtoString, true);

                //if (Config.ConfigInstance.HasServer)
                //{
                //    var server = new GoServerServiceImpl(protoContent);
                //    var serverContent = server.ToContent();
                //    await BuilderPath.CreateServerFile(server.ServerName.ToSnakeString(), serverContent);
                //    Console.WriteLine(serverContent);
                //}



                var controller = new ControllerAbstract()
                {
                    Name = service.InterfaceMetaData.Name.TrimStart('I').Replace("Service", ""),
                    Description = Config.ConfigInstance.Proto.PropertyDescriptionFunc(service.InterfaceMetaData),
                    NamespaceOrPackage = Config.ConfigInstance.GoController.NamespaceOrPackage,
                    Dependency = Config.ConfigInstance.GoController.Dependency ?? new List<string>(),
                    Version = Config.ConfigInstance.GoController.Version,
                    Items = new List<ControllerItemAbstract>()
                };

                foreach (var item in service.Items)
                {
                    var inParam = item.MethodMetaData.InParamMetaDataListFilter();
                    var outParam = item.MethodMetaData.OutParamMetaDataList;

                    var param = new ControllerItemAbstract()
                    {
                        MethodName = item.Name,
                        BaseRoute = Config.ConfigInstance.GoController.Route,
                        Route = item.HttpOption?.Route.TrimStart('/') ?? "",
                        HttpMethod = item.HttpOption?.HttpMethod,
                        Http = item.HttpOption?.HttpMethod.ToString().ToUpper(),
                        Description = FilterFunctions.GetMethodDescription(item),
                        DtoInputTypeDescription = Config.ConfigInstance.Proto.PropertyDescriptionFunc(item.InParam.ClassMetaData),
                        DtoOutTypeDescription = Config.ConfigInstance.Proto.PropertyDescriptionFunc(item.OutParam.ClassMetaData),
                    };

                    param.OriginalRoute = param.Route;

                    if (param.Route?.Contains("{") == true)
                    {
                        param.RouteParam = param.Route.Split('{')[1].Split('}')[0];
                        param.RouteParamUpper = param.RouteParam?.ToFirstUpString();
                    }

                    param.Route = Regex.Replace(param.Route, "\\{", ":").Replace("}", "");

                    if (inParam.Count > 0)
                    {
                        param.GRpcInputType = item.InParam.IsOriginalClass ? Config.ConfigInstance.Proto.GetPackageName(null) + "." + item.InParam.GetGRpcName() : null;
                        param.DtoInputType = item.InParam.IsOriginalClass ? Config.ConfigInstance.GoStruct.GetPackageName(null) + "." + item.InParam.Name.FormatGoStructName() : null;
                    }

                    // 当有路径参数，并且输入参数模型中有且仅有这个输入参数，则去除输入模型
                    if (!string.IsNullOrWhiteSpace(param.RouteParam) && inParam.Count == 1 && (inParam.First().Name.Equals(param.RouteParam, StringComparison.OrdinalIgnoreCase) || inParam.First().ClassMetaData?.PropertyMetaDataList.All(d => d.Name.Equals(param.RouteParam, StringComparison.OrdinalIgnoreCase)) == true))
                    {
                        param.HasInputParam = false;
                    }
                    else
                    {
                        param.HasInputParam = true;
                    }

                    if (outParam.Count > 0)
                    {
                        param.GRpcOutType = item.OutParam.IsOriginalClass ? Config.ConfigInstance.Proto.GetPackageName(null) + "." + item.OutParam.GetGRpcName() : null;
                        param.DtoOutType = item.OutParam.IsOriginalClass ? Config.ConfigInstance.GoStruct.GetPackageName(null) + "." + item.OutParam.Name.FormatGoStructName() : null;

                        if (item.OutParam.ClassMetaData?.PropertyMetaDataList.Count == 1)
                        {
                            param.Return = 1;

                            var first = item.OutParam.ClassMetaData.PropertyMetaDataList.First();

                            param.ReturnClass = first.ClassMetaData != null;
                            param.IsArray = first.TypeWrapper.IsArray;

                            if (param.ReturnClass)
                            {
                                param.ReturnType = "*" + Config.ConfigInstance.GoStruct.GetPackageName(null) + "." + first.ClassMetaData?.Name.FormatGoStructName() ?? "";
                            }
                            else
                            {
                                param.ReturnType = first.TypeWrapper.Type.ToGoStructString(true);
                            }

                            if (param.IsArray)
                            {
                                param.ReturnType = "[]" + param.ReturnType;
                            }

                            param.ReturnName = first.Name;
                        }
                        else
                        {
                            param.Return = 2;
                            param.ReturnType = Config.ConfigInstance.GoStruct.GetPackageName(null) + "." + item.OutParam.ClassMetaData?.Name.FormatGoStructName() ?? "";
                        }
                    }
                    else
                    {
                        param.Return = 0;
                    }

                    controller.Items.Add(param);
                    //pa.ReturnType = "emptypb.Empty";
                }

                //Controller
                var controllerString = await ScribanHelper.Render(new { config = Config.ConfigInstance, data = controller }, "Controller.GoController");
                await BuilderPath.CreateFileAsync(Config.ConfigInstance.GoController.OutputFullPath, $"{Config.ConfigInstance.GoServer.Lite.ToSnakeString()}.controller.go", controllerString, true);
            }

            //if (Config.ConfigInstance.HasServer)
            //{
            //    //mapper
            //    var mapperString = await ScribanHelper.Render(new { data = Config.ConfigInstance.Server.ProjectName }, "Server.MapperRegister");
            //    await BuilderPath.CreateServerMapperFile("DefaultMapperConfig", mapperString);

            //    // program
            //    var servers = Services.Select(d => new ServerServiceImpl(new ProtocolContent(d))).ToList();
            //    var serverProgramString = await ScribanHelper.Render(new { config = Config.ConfigInstance, servers = servers }, "Server.Program");
            //    var serverProgramFileName = "Program";

            //    // 如果 program.cs 已存在，则生成 programNew.cs，不覆盖原来的 program.cs
            //    if (File.Exists(Config.ConfigInstance.Server.GetProgramFilePath()))
            //    {
            //        serverProgramString = "/*" + Environment.NewLine + serverProgramString + Environment.NewLine + "*/";
            //        serverProgramFileName += ".g";
            //    }

            //    await BuilderPath.CreateServerRootFile(serverProgramFileName + ".cs", serverProgramString);


            //    // server csproj
            //    var serverCsprojString = await ScribanHelper.Render(new { config = Config.ConfigInstance, servers = Services }, "Server.csproj");
            //    var serverCsprojFileName = Config.ConfigInstance.Server.ProjectName + ".csproj";

            //    // 如果 csproj 已存在，则跳过
            //    if (!File.Exists(Config.ConfigInstance.Server.GetCsprojFilePath()))
            //    {
            //        await BuilderPath.CreateServerRootFile(serverCsprojFileName, serverCsprojString);
            //    }

            //    ////appsettings.json
            //    //var appSettingsJson = await ScribanHelper.Render(new { config = Config.ConfigInstance }, "Server.AppSettings");
            //    //var appSettingsJsonFileName = "appsettings.json";
            //    //await BuilderPath.CreateFileAsync(Config.ConfigInstance.Server.OutputFullPath, appSettingsJsonFileName, appSettingsJson, false);
            //}

            //// proto csproj
            //var protoCsprojString = await ScribanHelper.Render(new { config = Config.ConfigInstance, servers = Services }, "Protocol.csproj");
            //var protoCsprojFileName = Config.ConfigInstance.Proto.ProjectName + ".csproj";

            //// 如果 csproj 已存在，则跳过
            //if (!File.Exists(Config.ConfigInstance.Proto.GetCsprojFilePath()))
            //{
            //    await BuilderPath.CreateProtoRootFile(protoCsprojFileName, protoCsprojString);
            //}

            ////google api
            //if (Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding)
            //{
            //    var annotations = await ScribanHelper.Render(new { }, "google.api.annotations");
            //    var http = await ScribanHelper.Render(new { }, "google.api.http");

            //    await BuilderPath.CreateProtoGoogleApiFile("annotations.proto", annotations);
            //    await BuilderPath.CreateProtoGoogleApiFile("http.proto", http);

            //    //swagger
            //    var swaggerProgramString = await ScribanHelper.Render(new { config = Config.ConfigInstance }, "Server.Swagger");
            //    var swaggerProgramFileName = "SwaggerExtensions";
            //    await BuilderPath.CreateServerRootFile(swaggerProgramFileName + ".cs", swaggerProgramString);
            //}

            ////不支持的方法
            //Console.WriteLine($"不支持的方法：{_assemblyMetaData.NotSupportMethodList.Count}");
            //foreach (var method in _assemblyMetaData.NotSupportMethodList.OrderBy(d => d.Name))
            //{
            //    Console.WriteLine(method.DeclaringType?.FullName + "." + method.Name);
            //}

            //// 生成客户端包装
            //await RenderClientWrapper();

            //// 生成控制器
            //await RenderController();

            //// 生成Ui
            //await RenderUi();

            Console.WriteLine("生成完毕");
        }

        //public static string ReplaceBracesWithColon(string input)
        //{
        //    if (string.IsNullOrWhiteSpace(input)) return input;

        //    //var pattern = @"\{([^}]+)\}";
        //    //var replacement = ":$1";
        //    //return Regex.Replace(input, pattern, replacement);

        //    return Regex.Replace(input, "/\\{", ":").Replace("}", "");
        //}

    }
}
