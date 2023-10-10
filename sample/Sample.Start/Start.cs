using GRpcProtocolGenerator;
using GRpcProtocolGenerator.Common;
using GRpcProtocolGenerator.Renders;
using Sample.Services.Models;

namespace Sample.Start
{
    public class Start
    {
        public static async Task Generator(string basePath)
        {
            //引用目标项目中的一个对象，确保反射能找到目标程序集
            new SampleClass();

            var project = "Sample";

            await new GeneratorHandler(basePath, builder =>
            {
                //配置目标程序集名称
                builder.SetAssembly($"{project}.Services.dll");

                //配置 protocol
                builder.SetProtocolConfig(proto =>
                {
                    proto.Output = $"../{project}.Protocol";
                    proto.ProtoDirectory = "protos";
                    proto.PackageNameFunc = meta => "_grpc";
                    proto.CSharpNamespaceFunc = meta => $"{project}.GRpc.Protocol";
                    proto.PropertyDescriptionFunc = meta => meta?.Display ?? meta?.DisplayName ?? meta?.Description;
                    proto.ServiceNameFunc = meta => "GRpc" + meta.Name.TrimStart('I');
                    proto.UseProtoDirectoryWhenImportPackage = true;
                    proto.MethodNameFunc = meta =>
                    {
                        if (meta.Name.EndsWith("Async"))
                        {
                            return meta.Name.Substring(0, meta.Name.Length - 5);
                        }

                        return meta.Name;
                    };
                    proto.MethodInOutParamNameFunc = (method, props) =>
                    {
                        //传入
                        if (props.Count == 1 && !props[0].TypeWrapper.IsArray &&
                            props[0].TypeWrapper.Type == typeof(string) && props[0].Name == "ids")
                        {
                            return $"{method.InterfaceMetaData.FormatServiceName()}IdsRequest";
                        }

                        if (props.Count == 1 && !props[0].TypeWrapper.IsArray &&
                            props[0].TypeWrapper.Type == typeof(object) && props[0].Name == "id")
                        {
                            return $"{method.InterfaceMetaData.FormatServiceName()}IdRequest";
                        }

                        //传出
                        if (props.Count == 1 && string.IsNullOrWhiteSpace(props[0].Name) &&
                            !props[0].TypeWrapper.IsArray && props[0].TypeWrapper.Type == typeof(string))
                        {
                            return $"{method.InterfaceMetaData.FormatServiceName()}StringResponse";
                        }

                        if (props.Count == 1 && string.IsNullOrWhiteSpace(props[0].Name) &&
                            props[0].TypeWrapper.IsArray)
                        {
                            if (props[0].TypeWrapper.IsNullable)
                                return $"{method.InterfaceMetaData.FormatServiceName()}ListNullable{props[0].TypeWrapper.Type.Name}Response";

                            return $"{method.InterfaceMetaData.FormatServiceName()}List{props[0].TypeWrapper.Type.Name}Response";
                        }

                        return null;
                    };
                    proto.OriginalClassNameFunc = name => "GRpc" + name.Replace("`1", "").Replace("`", "");
                });

                //配置 service 实现
                builder.SetServerConfig(server =>
                {
                    server.Output = $"../{project}.Server";
                    server.ImplementsDirectory = "Implements";
                    server.NamespaceFunc = meta => $"{project}.Server.Implements";
                });

                ////配置json 转码，生成 Restful API
                //builder.SetJsonTranscoding(json =>
                //{
                //    json.UseJsonTranscoding = true;
                //    json.UseResultWrapper = true;
                //    json.UseJwtAuthentication = true;
                //    json.SuccessCode = 1;
                //    json.ErrorCode = 2;
                //    json.RouteFunc = route => $"{project}/api/v1/{route}";
                //    json.Swagger = new SwaggerConfig()
                //    {
                //        SwaggerConfigType = SwaggerConfigType.IdentityLogin,
                //        Name = "GRpc Server + Restful api",
                //        Title = "gRPC transcoding",
                //        Audience = "attendancesystem",
                //        Scope = new[] { "gateway" },
                //        ClientId = "64057d47d3b24a0001470082",
                //        ClientSecret = "secret",
                //        IdentityUrl = "https://192.168.1.20:8443",
                //        Version = "v1",
                //        DocumentXml = new[]
                //        {
                //            $"{project}.Server.xml",
                //            $"{project}.Protocol.xml"
                //        }
                //    };
                //});

                // 生成客户端包装
                builder.SetClientWrapper(client =>
                {
                    client.Output = $"../{project}.ClientWrapper";
                });

                // 生成控制器配置
                builder.SetController(controller =>
                {
                    controller.Output = $"../{project}.Gateway";
                    controller.ControllerDirectory = "Controllers//v1";

                    //基类
                    //controller.BaseController = "SaiLing.AspNetCore.Controllers.ApiControllerBase";
                    controller.BaseController = "ControllerBase";

                    //路由
                    controller.Route = $"{project}/api/v1/[controller]";

                    //控制器返回结果包装，返回给前端
                    //controller.ReturnMethodName = "Success";

                    controller.AppendAttributeToController = new List<string>() {  };

                    controller.Swagger = new SwaggerConfig()
                    {
                        SwaggerConfigType = SwaggerConfigType.IdentityLogin,
                        Name = "GRpc Client + Restful api",
                        Title = "gRPC transcoding",
                        Audience = "attendancesystem",
                        Scope = new[] { "gateway" },
                        ClientId = "64057d47d3b24a0001470082",
                        ClientSecret = "secret",
                        IdentityUrl = "https://192.168.1.20:8443",
                        Version = "v1",
                        DocumentXml = new[]
                        {
                            $"{project}.Gateway.xml",
                            $"{project}.Protocol.xml"
                        }
                    };
                });

                builder.SetFilter(filter =>
                {
                    filter.InterfaceFilterFunc = meta => true;
                    filter.MethodFilterFunc = (interfaceMeta, methodMeta) => true;
                });

            }).GeneratorAsync();
        }
    }
}
