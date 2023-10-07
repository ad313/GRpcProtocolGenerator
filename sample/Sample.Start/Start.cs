using GRpcProtocolGenerator;
using GRpcProtocolGenerator.Common;
using GRpcProtocolGenerator.Renders;
using GRpcProtocolGenerator.Resolve.Configs;
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

            await new GeneratorHandler(new Config(basePath)
            {
                Assemblies = $"{project}.Services",
                Proto = new ProtocolConfig()
                {
                    Output = $"../{project}.Protocol",
                    ProtoDirectory = "protos",
                    PackageNameFunc = meta => "_grpc",
                    CSharpNamespaceFunc = meta => $"{project}.Grpc.Protocol",
                    PropertyDescriptionFunc = meta => meta?.Display ?? meta?.DisplayName ?? meta?.Description,
                    ServiceNameFunc = meta => "GRpc" + meta.Name.TrimStart('I'),
                    UseProtoDirectoryWhenImportPackage = true,
                    MethodNameFunc = meta =>
                    {
                        if (meta.Name.EndsWith("Async"))
                        {
                            return meta.Name.Substring(0, meta.Name.Length - 5);
                            //return meta.Name + "Method";
                        }

                        return meta.Name;
                    },
                    MethodInOutParamNameFunc = (method, props) =>
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
                        if (props.Count == 1 && string.IsNullOrWhiteSpace(props[0].Name) && !props[0].TypeWrapper.IsArray && props[0].TypeWrapper.Type == typeof(string))
                        {
                            return $"{method.InterfaceMetaData.FormatServiceName()}StringResponse";
                        }

                        if (props.Count == 1 && string.IsNullOrWhiteSpace(props[0].Name) && props[0].TypeWrapper.IsArray)
                        {
                            if (props[0].TypeWrapper.IsNullable)
                                return $"{method.InterfaceMetaData.FormatServiceName()}ListNullable{props[0].TypeWrapper.Type.Name}Response";

                            return $"{method.InterfaceMetaData.FormatServiceName()}List{props[0].TypeWrapper.Type.Name}Response";
                        }

                        return null;
                    },
                    OriginalClassNameFunc = name => "Grpc" + name.Replace("`1", "").Replace("`", "")
                },
                Server = new ServerConfig()
                {
                    Output = $"../{project}.Server",
                    ServerDirectory = "Implements",
                    NamespaceFunc = meta => $"{project}.Server.Implements",
                    //AppendAttributeToServer = new List<string>() { "[SaiLing.Grpc.Server.GrpcServer]" }
                },
                Filter = new Filter()
                {
                    InterfaceFilterFunc = meta => true,
                    MethodFilterFunc = (interfaceMeta, methodMeta) => true
                },
                JsonTranscoding = new JsonTranscodingConfig()
                {
                    UseJsonTranscoding = true,
                    UseResultWrapper = true,
                    UseJwtAuthentication = true,
                    SuccessCode = 1,
                    ErrorCode = 2,
                    //RouteFunc = route => $"{project}/api/v1/{route}"
                    Swagger = new SwaggerConfig()
                    {
                        SwaggerConfigType = SwaggerConfigType.IdentityLogin,
                        Name = "GrpcServer + Restful api",
                        Title = "gRPC transcoding",
                        Audience = "attendancesystem",
                        Scope = new[] { "gateway" },
                        ClientId = "64057d47d3b24a0001470082",
                        ClientSecret = "secret",
                        IdentityUrl = "https://192.168.1.20:8443",
                        Version = "v1",
                        DocumentXml = new[]
                                    {
                            $"{project}.Server.xml",
                            $"{project}.Protocol.xml"
                        }
                    }
                }
            }, () => true).GeneratorAsync();
        }
    }
}
