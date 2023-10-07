using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders.Protocol;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        public readonly ConcurrentDictionary<string, ProtocolMessage> ClassMessages;
        public readonly ConcurrentDictionary<string, EnumProtocolMessage> EnumMessages;
        public readonly List<ProtocolService> Services;

        private readonly AssemblyMetaData _assemblyMetaData;
        public static Config Config;

        public Builder(AssemblyMetaData assemblyMetaData, Config config)
        {
            _assemblyMetaData = assemblyMetaData;
            Config = config;

            ClassMessages = new ConcurrentDictionary<string, ProtocolMessage>();
            EnumMessages = new ConcurrentDictionary<string, EnumProtocolMessage>();
            Services = new List<ProtocolService>();

            //枚举
            CreateEnumMessageList(assemblyMetaData.EnumMetaDataDictionary.Select(d => d.Value).ToList());

            //类
            CreateClassMessageList(assemblyMetaData.ClassMetaDataDictionary.Select(d => d.Value).ToList());

            //接口
            CreateProtoServiceList(assemblyMetaData.InterfaceMetaDataDictionary.Select(d => d.Value).ToList());
        }

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

                if (File.Exists(Config.Server.GetProgramFilePath()))
                {
                    serverProgramString = "/*" + Environment.NewLine + serverProgramString + Environment.NewLine + "*/";
                    serverProgramFileName += "New";
                }

                await BuilderPath.CreateServerRootFile(serverProgramFileName + ".cs", serverProgramString);


                // server csproj
                var serverCsprojString = await ScribanHelper.Render(new { config = Config, servers = Services }, "Server.csproj");
                var serverCsprojFileName = Config.Server.ProjectName + ".csproj";
                if (File.Exists(Config.Server.GetCsprojFilePath()))
                {
                    serverCsprojFileName += "New";
                }

                await BuilderPath.CreateServerRootFile(serverCsprojFileName, serverCsprojString);
            }

            // proto csproj
            var protoCsprojString = await ScribanHelper.Render(new { config = Config, servers = Services }, "Protocol.csproj");
            var protoCsprojFileName = Config.Proto.ProjectName + ".csproj";
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





        //public ProtoMessage CreateProtoMessage(List<ParamMetaData> paramList)
        //{
        //    if (paramList == null || !paramList.Any())
        //        return new EmptyProtoMessage();

        //    paramList = paramList.OrderBy(d => d.FullName).ToList();
        //    var key = string.Join("_", paramList.Select(d => d.Name));

        //    var message = _classMessage.GetOrAdd(key, k =>
        //    {
        //        //1、单个参数
        //        // 1.1 非数组
        //        //     值类型 创建对象 单层
        //        //     字典   创建对象 单层 repeated
        //        //     枚举   创建对象 单层
        //        //     类     类对象   单层
        //        // 1.2 数组
        //        //     值类型 创建对象 单层 repeated
        //        //     字典   创建对象 两层 repeated
        //        //     枚举   创建对象      repeated
        //        //     类     创建对象      repeated
        //        //
        //        //2、多个参数 创建对象
        //        //

        //        if (paramList.Count > 1)
        //            return null;





        //        return new ProtoMessage();
        //    });

        //    return message;
        //}



        private void CreateEnumMessageList(List<EnumMetaData> enumMetaList)
        {
            foreach (var enumMeta in enumMetaList)
            {
                CreateEnumMessage(enumMeta);
            }
        }

        private void CreateClassMessageList(List<ClassMetaData> classMetaList)
        {
            foreach (var classMeta in classMetaList)
            {
                var dependency = new List<string>();
                var msg = CreateClassMessage(classMeta, null, ref dependency);
                msg?.SetClassDependency(ClassMessages
                    .Where(d => dependency.Where(t => t != classMeta.FullName).Contains(d.Key)).Select(d => d.Value)
                    .ToList());
            }
        }

        /// <summary>
        /// 处理方法名称、传入传出参数、包装返回值等
        /// </summary>
        /// <param name="interfaceMetaList"></param>
        private void CreateProtoServiceList(List<InterfaceMetaData> interfaceMetaList)
        {
            foreach (var interfaceMetaData in interfaceMetaList.OrderBy(d => d.Name))
            {
                var inIndex = 1;
                var outIndex = 1;
                var items = new List<ProtoServiceItem>();
                foreach (var grouping in interfaceMetaData.MethodMetaDataList.GroupBy(d => d.GrpcMethodName).OrderBy(d => d.Key))
                {
                    //方法重载，添加序号 1 2 3...
                    if (grouping.Count() > 1)
                    {
                        var methodIndex = 1;

                        foreach (var methodMetaData in grouping)
                        {
                            items.Add(new ProtoServiceItem()
                            {
                                Index = methodMetaData.Index,
                                Name = $"{methodMetaData.GrpcMethodName}_{methodIndex}",
                                InParam = CreateMethodParam(interfaceMetaData, methodMetaData, methodMetaData.InParamMetaDataList, "Request", ref inIndex),
                                OutParam = ProtoMessageWrapper(interfaceMetaData, CreateMethodParam(interfaceMetaData, methodMetaData, methodMetaData.OutParamMetaDataList, "Response", ref outIndex)),
                                MethodMetaData = methodMetaData
                            });

                            methodIndex++;
                        }
                    }
                    else
                    {
                        var methodMetaData = grouping.First();
                        items.Add(new ProtoServiceItem()
                        {
                            Index = methodMetaData.Index,
                            Name = $"{methodMetaData.GrpcMethodName}",
                            InParam = CreateMethodParam(interfaceMetaData, methodMetaData, methodMetaData.InParamMetaDataList, "Request", ref inIndex),
                            OutParam = ProtoMessageWrapper(interfaceMetaData, CreateMethodParam(interfaceMetaData, methodMetaData, methodMetaData.OutParamMetaDataList, "Response", ref outIndex)),
                            MethodMetaData = methodMetaData
                        });
                    }
                }

                Services.Add(new ProtocolService(interfaceMetaData.Name, items, interfaceMetaData));
            }
        }

        /// <summary>
        /// 包装返回结果，主要给Restful api 使用
        /// </summary>
        /// <param name="interfaceMetaData"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private ProtocolMessage ProtoMessageWrapper(InterfaceMetaData interfaceMetaData, ProtocolMessage msg)
        {
            if (Config.JsonTranscoding.UseResultWrapper == false)
                return msg;

            //空
            if (msg.IsEmpty)
            {
                var wrapper = new ProtocolMessage($"{interfaceMetaData.FormatServiceName()}_EmptyWrapper",
                    new List<ProtocolItemMessage>(),
                    new List<ProtocolMessage>() { msg },
                    new List<EnumProtocolMessage>(),
                    false,
                    false,
                    null);

                wrapper.Items.Insert(0, new ProtocolItemMessage(typeof(int), false, false, "Code", "int32", "int32", 0, null, null, null));
                wrapper.Items.Insert(1, new ProtocolItemMessage(typeof(string), true, false, "Message", "google.protobuf.StringValue", "google.protobuf.StringValue", 0, null, null, null));

                return wrapper;
            }

            //原始类
            if (msg.IsOriginalClass)
            {
                var wrapper = new ProtocolMessage(interfaceMetaData.FormatServiceName() + "_" + msg.GetGRpcName() + "Wrapper",
                    new List<ProtocolItemMessage>(),
                    new List<ProtocolMessage>() { msg },
                    new List<EnumProtocolMessage>(),
                    false,
                    false,
                    null);

                wrapper.Items.Insert(0, new ProtocolItemMessage(typeof(int), false, false, "Code", "int32", "int32", 0, null, null, null));
                wrapper.Items.Insert(1, new ProtocolItemMessage(typeof(string), true, false, "Message", "google.protobuf.StringValue", "google.protobuf.StringValue", 0, null, null, null));
                wrapper.Items.Insert(2, new ProtocolItemMessage(msg.ClassMetaData.TypeWrapper.Type, true, false, "Data", msg.GetGRpcName(), "google.protobuf.StringValue", 0, msg.ClassMetaData, msg.ClassMetaData, msg.EnumMetaData));

                return wrapper;
            }

            msg.Items.Insert(0, new ProtocolItemMessage(typeof(int), false, false, "Code", "int32", "int32", 0, null, null, null));
            msg.Items.Insert(1, new ProtocolItemMessage(typeof(string), true, false, "Message", "google.protobuf.StringValue", "google.protobuf.StringValue", 0, null, null, null));

            return msg;
        }

        private ProtocolMessage CreateMethodParam(InterfaceMetaData interfaceMetaData, MethodMetaData methodMetaData, List<PropertyMetaData> paramList, string append, ref int index)
        {
            if (paramList.Count == 0)
            {
                return new EmptyProtocolMessage();
            }

            ProtocolMessage GetMessageFromClassMetaData(ClassMetaData meta)
            {
                if (meta == null) return null;
                return ClassMessages.TryGetValue(meta.FullName, out ProtocolMessage msg) ? msg : null;
            }

            if (paramList.Count == 1 &&
                paramList.All(d => d.ClassMetaData != null) &&
                paramList.All(d => d.TypeWrapper.IsArray == false))
            {
                //只有一个参数，并且参数是类，并且不是数组 此时不需要创建新对象，直接用类对象
                var className = paramList.First().ClassMetaData.FullName;
                var msg = GetMessageFromClassMetaData(paramList.First().ClassMetaData);
                if (msg != null)
                {
                    return msg;
                }

                throw new Exception($"未找到对应的类型：{className}");
            }

            //新建类，不加入字典，就放在当前服务内
            var newName = $"{interfaceMetaData.FormatServiceName()}_{methodMetaData.Name}_{append}{index}";

            //重新生成名称
            if (paramList.Count > 0)
            {
                var reName = Config.Proto?.MethodInOutParamNameFunc?.Invoke(methodMetaData, paramList);
                if (!string.IsNullOrWhiteSpace(reName))
                {
                    newName = reName;
                }
                else
                {
                    index++;
                }
            }

            var paramMessage = new ProtocolMessage(newName, new List<ProtocolItemMessage>(), null);

            foreach (var param in paramList)
            {
                if (param.ClassMetaData == null && param.EnumMetaData == null)
                {
                    paramMessage.Items.Add(CreateSampleMessageItem(param));
                }
                else
                {
                    paramMessage.Items.Add(CreateClassMessageItem(param));

                    //添加对枚举的依赖
                    if (param.EnumMetaData != null)
                    {
                        var enumMetaData = GetEnumProtoMessage(param.EnumMetaData.FullName);
                        if (enumMetaData != null)
                        {
                            paramMessage.EnumDependency.Add(enumMetaData);
                        }
                    }

                    var currentMsg = GetMessageFromClassMetaData(param.ClassMetaData);
                    paramMessage.AddClassDependency(currentMsg?.ClassDependency.Concat(new List<ProtocolMessage>() { currentMsg }).ToList());
                }
            }

            return paramMessage;
        }

        private ProtocolMessage CreateClassMessage(ClassMetaData classMetaData, List<ProtocolMessage> result, ref List<string> parents)
        {
            result ??= new List<ProtocolMessage>();
            parents ??= new List<string>();

            if (classMetaData == null)
                return null;

            var key = classMetaData.FullName;
            if (parents.Contains(key))
                return null;

            parents.Add(key);

            if (ClassMessages.ContainsKey(key))
                return null;

            var enumDependency = new List<EnumProtocolMessage>();
            var items = new List<ProtocolItemMessage>();
            foreach (var prop in classMetaData.PropertyMetaDataList)
            {
                if (prop.ClassMetaData == null && prop.EnumMetaData == null)
                {
                    items.Add(CreateSampleMessageItem(prop));
                }
                else
                {
                    items.Add(CreateClassMessageItem(prop));
                }

                //添加对枚举的依赖
                if (prop.EnumMetaData != null)
                {
                    var enumMetaData = GetEnumProtoMessage(prop.EnumMetaData.FullName);
                    if (enumMetaData != null)
                    {
                        enumDependency.Add(enumMetaData);
                    }
                }

                CreateClassMessage(prop.ClassMetaData, result, ref parents);
            }

            var current = new ProtocolMessage(classMetaData.Name, items, null, enumDependency, true, false, classMetaData);

            result.Add(current);

            ClassMessages.TryAdd(key, current);

            return current;
        }

        private EnumProtocolMessage CreateEnumMessage(EnumMetaData enumMetaData)
        {
            if (enumMetaData == null)
                return null;

            //proto 枚举必须是从0开始，如果不满足，则不生成枚举
            if (enumMetaData.Members.Min(d => d.Value) > 0)
                return null;

            var key = enumMetaData.FullName;

            if (EnumMessages.ContainsKey(key))
                return null;

            var items = new List<ProtocolItemMessage>();
            foreach (var member in enumMetaData.Members)
            {
                items.Add(CreateEnumMessageItem(member));
            }

            var current = new EnumProtocolMessage(enumMetaData.Name, items, enumMetaData);

            EnumMessages.TryAdd(key, current);

            return current;
        }

        private ProtocolItemMessage CreateSampleMessageItem(PropertyMetaData prop)
        {
            if (prop == null || prop.ClassMetaData != null || prop.EnumMetaData != null)
                return null;

            return new ProtocolItemMessage(prop.TypeWrapper.Type, prop.TypeWrapper.IsNullable, prop.TypeWrapper.IsArray, prop.Name, null,
                prop.TypeWrapper.Type.ToProtobufString(prop.TypeWrapper.IsNullable), 0, prop,
                prop.ClassMetaData, null);
        }

        private ProtocolItemMessage CreateClassMessageItem(PropertyMetaData prop)
        {
            if (prop?.ClassMetaData == null && prop?.EnumMetaData == null)
                return null;

            //特殊处理枚举，转换成 int
            if (prop.EnumMetaData != null)
            {
                return new ProtocolItemMessage(typeof(int), prop.TypeWrapper.IsNullable, prop.TypeWrapper.IsArray, prop.Name, null, typeof(int).ToProtobufString(prop.TypeWrapper.IsNullable), 0, prop, prop.ClassMetaData, prop.EnumMetaData);
            }

            var type = prop.ClassMetaData?.Name ?? prop.TypeWrapper.Type.Name;

            return new ProtocolItemMessage(prop.TypeWrapper.Type, prop.TypeWrapper.IsNullable, prop.TypeWrapper.IsArray, prop.Name, type.FormatMessageName(), type, 0, prop, prop.ClassMetaData, prop.EnumMetaData);
        }

        private ProtocolItemMessage CreateEnumMessageItem(EnumMemberMetaData member)
        {
            return new ProtocolItemMessage(typeof(int), false, false, member.Name, null, "", member.Value, new CommentMetaData(member.Name, member.Name, member.AttributeMetaDataList), null, null);
        }

        private EnumProtocolMessage GetEnumProtoMessage(string name)
        {
            return EnumMessages.TryGetValue(name, out EnumProtocolMessage msg) ? msg : null;
        }
    }
}
