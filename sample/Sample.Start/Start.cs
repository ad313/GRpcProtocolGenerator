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

            //指定项目名称
            var project = "Sample";

            await new GeneratorHandler(basePath, builder =>
            {
                //配置目标程序集名称
                builder.SetAssembly($"{project}.Services.dll");

                //配置 protocol
                builder.SetProtocolConfig(proto =>
                {
                    //指定输出的根目录
                    proto.Output = $"../{project}.Protocol";

                    //指定 proto 文件存放目录
                    proto.ProtoDirectory = "protos";

                    //指定 Package 名称
                    proto.PackageNameFunc = meta => "_grpc";

                    //指定 C# 对应的命名空间
                    proto.CSharpNamespaceFunc = meta => $"{project}.GRpc.Protocol";

                    //获取注释
                    proto.PropertyDescriptionFunc = meta => meta?.Display ?? meta?.DisplayName ?? meta?.Description;

                    //处理 proto 中 service 名称
                    proto.ServiceNameFunc = meta => "GRpc" + meta.Name.TrimStart('I');

                    //当引用别的proto文件时，是否带上路径；注意，如果你把proto目录设为了根目库，那引用包不需要路径，这里设为false，否则是true
                    proto.UseProtoDirectoryWhenImportPackage = true;

                    //自定义类型映射 C# => protoBuf
                    proto.CSharpTypeToProtobufString = (type, nullable) =>
                    {
                        if (type == typeof(byte[]))
                            return "google.protobuf.StringValue";

                        if (type == typeof(object))
                            return "google.protobuf.StringValue";

                        return null;
                    };

                    // 给 csproj 附加 包引用
                    proto.AppendPackageToCsproj = new List<string>() { };

                    //存放 生成的客户端接口 的文件夹
                    proto.ClientsDirectory = "Clients";

                    //处理方法名称，比如去掉 Async 等
                    proto.MethodNameFunc = meta =>
                    {
                        if (meta.Name.EndsWith("Async"))
                        {
                            return meta.Name.Substring(0, meta.Name.Length - 5);
                        }

                        return meta.Name;
                    };

                    //对输入输出参数包装的类型名称进行处理，让名称可读，否则就自动生成
                    proto.MethodInOutParamNameFunc = (method, props) =>
                    {
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

                    //处理原始类名称，如果是泛型类，就会有 `1、`2 等，则去掉
                    proto.OriginalClassNameFunc = name => "GRpc" + name.Replace("`1", "").Replace("`", "");
                });

                //配置 service 实现
                builder.SetServerConfig(server =>
                {
                    //指定输出的根目录
                    server.Output = $"../{project}.Server";

                    //指定端口
                    server.Port = 6010;

                    //指定 service 实现文件存放目录
                    server.ImplementsDirectory = "Implements";

                    //给service实现附加特性,比如自动扫描接口等
                    server.AppendAttributeToServer = new List<string>() { };

                    // 给 csproj 附加 包引用
                    server.AppendPackageToCsproj = new List<string>() { };
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

                // 生成控制器配置
                builder.SetController(controller =>
                {
                    //指定输出的根目录
                    controller.Output = $"../{project}.Gateway";

                    //指定控制器文件存放路径
                    controller.ControllerDirectory = "Controllers//v1";

                    //指定端口
                    controller.Port = 6011;

                    //设置控制器基类，默认是 ControllerBase
                    controller.BaseController = "ControllerBase";

                    //设置路由
                    controller.Route = $"{project}/api/v1/[controller]";

                    // 指定一个函数名称，控制器返回结果包装，返回给前端，比如 指定 Success，则结果是 return Success(result)；默认是 return Ok(result)
                    controller.ReturnMethodName = "Ok";

                    // 给控制器附加 attribute [xxxAttribute]
                    controller.AppendAttributeToController = new List<string>() { };

                    // 给 csproj 附加 包引用
                    controller.AppendPackageToCsproj = new List<string>() { };

                    //指定swagger参数
                    controller.Swagger = new SwaggerConfig()
                    {
                        SwaggerConfigType = SwaggerConfigType.IdentityLogin,
                        Name = "GRpc Client + Restful api",
                        Title = "GRpc Client + Restful api",
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

                //对元数据进行过滤
                builder.SetFilter(filter =>
                {
                    filter.InterfaceFilterFunc = meta => true;
                    filter.MethodFilterFunc = (interfaceMeta, methodMeta) => true;
                });

            }).GeneratorAsync();
        }
    }
}
