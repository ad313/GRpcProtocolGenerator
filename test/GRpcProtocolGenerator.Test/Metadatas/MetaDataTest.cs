using GRpcProtocolGenerator.Common;
using GRpcProtocolGenerator.Common.Attributes;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders;
using Sample.Services;
using Sample.Services.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GRpcProtocolGenerator.Test.Metadatas
{
    public class MetaDataTest
    {
        private AssemblyMetaData _assemblyMetaData { get; set; }

        public GeneratorHandler Handler;
        
        public MetaDataTest()
        {
            var c = new SampleClass();

            var basePath = "123";
            var project = "Sample";

            Handler = new GeneratorHandler(basePath, builder =>
            {
                //配置目标程序集名称
                builder.SetAssembly($"{project}.Services");

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
                                return
                                    $"{method.InterfaceMetaData.FormatServiceName()}ListNullable{props[0].TypeWrapper.Type.Name}Response";

                            return
                                $"{method.InterfaceMetaData.FormatServiceName()}List{props[0].TypeWrapper.Type.Name}Response";
                        }

                        return null;
                    };
                    proto.OriginalClassNameFunc = name => "GRpc" + name.Replace("`1", "").Replace("`", "");
                });

                builder.SetServerConfig(server =>
                {
                    server.Output = $"../{project}.Server";
                    server.ImplementsDirectory = "Implements";
                });

                builder.SetJsonTranscoding(json =>
                {
                    json.UseJsonTranscoding = true;
                    json.UseResultWrapper = true;
                    json.UseJwtAuthentication = true;
                    json.SuccessCode = 1;
                    json.ErrorCode = 2;
                    json.RouteFunc = route => $"{project}/api/v1/{route}";
                    json.Swagger = new SwaggerConfig()
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
                    };
                });

                builder.SetFilter(filter =>
                {
                    filter.InterfaceFilterFunc = meta => true;
                    filter.MethodFilterFunc = (interfaceMeta, methodMeta) => true;
                });
            });
            
            _assemblyMetaData = Handler.AssemblyMetaData;
        }


        [Fact]
        public void ResolverSuccess()
        {
            Assert.NotNull(_assemblyMetaData);
        }

        [Fact]
        public void InterfaceTest()
        {
            var type = typeof(IServiceTest);

            var metaData = _assemblyMetaData.InterfaceMetaDataDictionary.FirstOrDefault(d => d.Value.Type == type).Value;
            Assert.NotNull(metaData);

            Assert.True(metaData.Key == type.FullName);
            Assert.True(metaData.Name == type.Name);
            Assert.True(metaData.FullName == type.FullName);
            Assert.True(metaData.Description == "desc from Description");
            Assert.True(metaData.AttributeMetaDataList.Count == 2);

            Assert.Contains(typeof(DescriptionAttribute), metaData.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(GRpcGeneratorAttribute), metaData.AttributeMetaDataList.Select(d => d.Type));

            //method
            Assert.NotNull(metaData.MethodMetaDataList);
            Assert.True(metaData.MethodMetaDataList.Any());


            //无返回值 同步
            SyncMethodWithNoReturnTest(metaData);

            //无返回值 异步
            AsyncMethodWithNoReturnTest(metaData);

            //有返回值 同步
            SyncMethodWithReturnTest(metaData);

            //有返回值 异步
            AsyncMethodWithReturnTest(metaData);

            //不支持的方法
            NotSupportMethodTest(metaData);

            //其他
            OtherTest(metaData);
        }

        /// <summary>
        /// 无返回值 同步
        /// </summary>
        /// <param name="metaData"></param>
        private void SyncMethodWithNoReturnTest(InterfaceMetaData metaData)
        {
            var name = "VoidMethodSync";
            var voidMethodSync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(voidMethodSync);
            Assert.False(voidMethodSync.IsTask);
            Assert.False(voidMethodSync.InParamMetaDataList.Any());
            Assert.False(voidMethodSync.OutParamMetaDataList.Any());

            //attribute
            Assert.Equal("title from Description attr", voidMethodSync.Description);
            Assert.Equal("title from Display attr", voidMethodSync.Display);
            Assert.Equal("title from DisplayName attr", voidMethodSync.DisplayName);
            Assert.Contains(typeof(DescriptionAttribute), voidMethodSync.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayNameAttribute), voidMethodSync.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayAttribute), voidMethodSync.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(HttpGetAttribute), voidMethodSync.AttributeMetaDataList.Select(d => d.Type));

            var httpGet = voidMethodSync.AttributeMetaDataList.First(d => d.Type == typeof(HttpGetAttribute));
            Assert.NotNull(httpGet);
            Assert.True(httpGet.ConstructorDictionary.Count == 1);
            Assert.True(httpGet.ConstructorDictionary.First().Value.ToString() == name);




            name = "MethodWithInputReturnVoidSync";
            var methodWithInputReturnVoidSync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnVoidSync);
            Assert.False(methodWithInputReturnVoidSync.IsTask);
            Assert.True(methodWithInputReturnVoidSync.InParamMetaDataList.Count == 1);
            Assert.False(methodWithInputReturnVoidSync.OutParamMetaDataList.Any());

            var inParam = methodWithInputReturnVoidSync.InParamMetaDataList.First();
            Assert.True(inParam.TypeWrapper.Type == typeof(int));
            Assert.True(inParam.Name == "a");




            name = "MethodWithInputReturnVoid2Sync";
            var methodWithInputReturnVoid2Sync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnVoid2Sync);
            Assert.False(methodWithInputReturnVoid2Sync.IsTask);
            Assert.True(methodWithInputReturnVoid2Sync.InParamMetaDataList.Count == 2);
            Assert.False(methodWithInputReturnVoid2Sync.OutParamMetaDataList.Any());

            var inParam1 = methodWithInputReturnVoid2Sync.InParamMetaDataList.First();
            Assert.True(inParam1.TypeWrapper.Type == typeof(int));
            Assert.True(inParam1.Name == "a");

            var inParam2 = methodWithInputReturnVoid2Sync.InParamMetaDataList.Last();
            Assert.True(inParam2.TypeWrapper.Type == typeof(string));
            Assert.True(inParam2.Name == "b");




            name = "MethodWithInputReturnVoid3Sync";
            var methodWithInputReturnVoid3Sync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnVoid3Sync);
            Assert.False(methodWithInputReturnVoid3Sync.IsTask);
            Assert.True(methodWithInputReturnVoid3Sync.InParamMetaDataList.Count == 1);
            Assert.False(methodWithInputReturnVoid3Sync.OutParamMetaDataList.Any());

            inParam = methodWithInputReturnVoid3Sync.InParamMetaDataList.First();
            Assert.True(inParam.TypeWrapper.Type == typeof(SampleClass));
            Assert.True(inParam.Name == "a");

            //class
            ClassMetaDataTest(inParam.ClassMetaData);



            name = "MethodWithInputReturnVoid4Sync";
            var methodWithInputReturnVoid4Sync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnVoid4Sync);
            Assert.False(methodWithInputReturnVoid4Sync.IsTask);
            Assert.True(methodWithInputReturnVoid4Sync.InParamMetaDataList.Count == 1);
            Assert.False(methodWithInputReturnVoid4Sync.OutParamMetaDataList.Any());

            inParam = methodWithInputReturnVoid4Sync.InParamMetaDataList.First();
            Assert.True(inParam.TypeWrapper.Type == typeof(int));
            Assert.True(inParam.TypeWrapper.IsArray);
            Assert.True(inParam.Name == "a");





            name = "MethodWithInputEnumReturnVoid4Sync";
            var methodWithInputEnumReturnVoid4Sync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputEnumReturnVoid4Sync);
            Assert.False(methodWithInputEnumReturnVoid4Sync.IsTask);
            Assert.True(methodWithInputEnumReturnVoid4Sync.InParamMetaDataList.Count == 1);
            Assert.False(methodWithInputEnumReturnVoid4Sync.OutParamMetaDataList.Any());

            inParam = methodWithInputEnumReturnVoid4Sync.InParamMetaDataList.First();
            Assert.True(inParam.TypeWrapper.Type == typeof(ApplicationEnumType));
            Assert.True(inParam.Name == "a");


            

            name = "MethodWithInputTreeReturnVoid5Sync";
            var methodWithInputTreeReturnVoid5Sync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputTreeReturnVoid5Sync);
            Assert.False(methodWithInputTreeReturnVoid5Sync.IsTask);
            Assert.True(methodWithInputTreeReturnVoid5Sync.InParamMetaDataList.Count == 4);
            Assert.False(methodWithInputTreeReturnVoid5Sync.OutParamMetaDataList.Any());

            inParam1 = methodWithInputTreeReturnVoid5Sync.InParamMetaDataList[0];
            Assert.True(inParam1.TypeWrapper.Type == typeof(TreeNode<SampleClass>));
            Assert.True(inParam1.Name == "a");

            inParam2 = methodWithInputTreeReturnVoid5Sync.InParamMetaDataList[1];
            Assert.True(inParam2.TypeWrapper.Type == typeof(TreeClass));
            Assert.True(inParam2.Name == "b");

            var inParam3 = methodWithInputTreeReturnVoid5Sync.InParamMetaDataList[2];
            Assert.True(inParam3.TypeWrapper.Type == typeof(ExtClass));
            Assert.True(inParam3.Name == "c");

            var inParam4 = methodWithInputTreeReturnVoid5Sync.InParamMetaDataList[3];
            Assert.True(inParam4.TypeWrapper.Type == typeof(int));
            Assert.True(inParam4.Name == "d");

            //泛型结构
            GenericClassMetaDataTest(inParam1.ClassMetaData);

            //树形结构
            TreeClassMetaDataTest(inParam2.ClassMetaData);
        }

        /// <summary>
        /// 无返回值 异步
        /// </summary>
        /// <param name="metaData"></param>
        private void AsyncMethodWithNoReturnTest(InterfaceMetaData metaData)
        {
            var name = "MethodWithInputEnumReturnVoid4Async";
            var methodWithInputEnumReturnVoid4Async = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputEnumReturnVoid4Async);
            Assert.True(methodWithInputEnumReturnVoid4Async.IsTask);
            Assert.True(methodWithInputEnumReturnVoid4Async.InParamMetaDataList.Count == 1);
            Assert.False(methodWithInputEnumReturnVoid4Async.OutParamMetaDataList.Any());

            var inParam = methodWithInputEnumReturnVoid4Async.InParamMetaDataList.First();
            Assert.True(inParam.TypeWrapper.Type == typeof(ApplicationEnumType));
            Assert.True(inParam.Name == "a");
        }

        /// <summary>
        /// 有返回值 同步
        /// </summary>
        /// <param name="metaData"></param>
        private void SyncMethodWithReturnTest(InterfaceMetaData metaData)
        {
            var name = "MethodWithInputReturnIntSync";
            var methodWithInputReturnIntSync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnIntSync);
            Assert.False(methodWithInputReturnIntSync.IsTask);
            Assert.True(methodWithInputReturnIntSync.InParamMetaDataList.Count == 1);
            Assert.True(methodWithInputReturnIntSync.OutParamMetaDataList.Count == 1);

            var inParam = methodWithInputReturnIntSync.InParamMetaDataList.First();
            Assert.True(inParam.TypeWrapper.Type == typeof(int));
            Assert.True(inParam.Name == "a");

            var outParam = methodWithInputReturnIntSync.OutParamMetaDataList.First();
            Assert.True(outParam.TypeWrapper.Type == typeof(int));




            name = "MethodWithInputReturnStringSync";
            var methodWithInputReturnStringSync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnStringSync);
            Assert.False(methodWithInputReturnStringSync.IsTask);
            Assert.True(methodWithInputReturnStringSync.InParamMetaDataList.Count == 2);
            Assert.True(methodWithInputReturnStringSync.OutParamMetaDataList.Count == 1);

            var inParam1 = methodWithInputReturnStringSync.InParamMetaDataList.First();
            Assert.True(inParam1.TypeWrapper.Type == typeof(int));
            Assert.True(inParam1.Name == "a");

            var inParam2 = methodWithInputReturnStringSync.InParamMetaDataList.Last();
            Assert.True(inParam2.TypeWrapper.Type == typeof(string));
            Assert.True(inParam2.Name == "b");

            outParam = methodWithInputReturnStringSync.OutParamMetaDataList.First();
            Assert.True(outParam.TypeWrapper.Type == typeof(string));




            name = "MethodWithInputReturnClassSync";
            var methodWithInputReturnClassSync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnClassSync);
            Assert.False(methodWithInputReturnClassSync.IsTask);
            Assert.True(methodWithInputReturnClassSync.InParamMetaDataList.Count == 2);
            Assert.True(methodWithInputReturnClassSync.OutParamMetaDataList.Count == 1);

            inParam1 = methodWithInputReturnClassSync.InParamMetaDataList.First();
            Assert.True(inParam1.TypeWrapper.Type == typeof(int));
            Assert.True(inParam1.Name == "a");

            inParam2 = methodWithInputReturnClassSync.InParamMetaDataList.Last();
            Assert.True(inParam2.TypeWrapper.Type == typeof(string));
            Assert.True(inParam2.Name == "b");

            outParam = methodWithInputReturnClassSync.OutParamMetaDataList.First();
            Assert.True(outParam.TypeWrapper.Type == typeof(SampleClass));





            name = "MethodWithInputReturnListClassSync";
            var methodWithInputReturnListClassSync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnListClassSync);
            Assert.False(methodWithInputReturnListClassSync.IsTask);
            Assert.True(methodWithInputReturnListClassSync.InParamMetaDataList.Count == 1);
            Assert.True(methodWithInputReturnListClassSync.OutParamMetaDataList.Count == 1);

            inParam1 = methodWithInputReturnListClassSync.InParamMetaDataList.First();
            Assert.True(inParam1.TypeWrapper.Type == typeof(SampleClass));
            Assert.True(inParam1.Name == "a");

            outParam = methodWithInputReturnListClassSync.OutParamMetaDataList.First();
            Assert.True(outParam.TypeWrapper.Type == typeof(SampleClass));
            Assert.True(outParam.TypeWrapper.IsArray);




            name = "MethodWithInputReturnListIntSync";
            var methodWithInputReturnListIntSync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnListIntSync);
            Assert.False(methodWithInputReturnListIntSync.IsTask);
            Assert.True(methodWithInputReturnListIntSync.InParamMetaDataList.Count == 1);
            Assert.True(methodWithInputReturnListIntSync.OutParamMetaDataList.Count == 1);

            inParam1 = methodWithInputReturnListIntSync.InParamMetaDataList.First();
            Assert.True(inParam1.TypeWrapper.Type == typeof(SampleClass));
            Assert.True(inParam1.Name == "a");

            outParam = methodWithInputReturnListIntSync.OutParamMetaDataList.First();
            Assert.True(outParam.TypeWrapper.Type == typeof(int));
            Assert.True(outParam.TypeWrapper.IsArray);




            name = "MethodWithInputReturnEnumSync";
            var methodWithInputReturnEnumSync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnEnumSync);
            Assert.False(methodWithInputReturnEnumSync.IsTask);
            Assert.True(methodWithInputReturnEnumSync.InParamMetaDataList.Count == 1);
            Assert.True(methodWithInputReturnEnumSync.OutParamMetaDataList.Count == 1);

            inParam1 = methodWithInputReturnEnumSync.InParamMetaDataList.First();
            Assert.True(inParam1.TypeWrapper.Type == typeof(ApplicationEnumType));
            Assert.True(inParam1.Name == "a");

            outParam = methodWithInputReturnEnumSync.OutParamMetaDataList.First();
            Assert.True(outParam.TypeWrapper.Type == typeof(ApplicationEnumType));


        }

        /// <summary>
        /// 有返回值 异步
        /// </summary>
        /// <param name="metaData"></param>
        private void AsyncMethodWithReturnTest(InterfaceMetaData metaData)
        {
            var name = "MethodWithInputReturnListClassAsync";
            var methodWithInputReturnListClassAsync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnListClassAsync);
            Assert.True(methodWithInputReturnListClassAsync.IsTask);
            Assert.True(methodWithInputReturnListClassAsync.InParamMetaDataList.Count == 1);
            Assert.True(methodWithInputReturnListClassAsync.OutParamMetaDataList.Count == 1);

            var inParam1 = methodWithInputReturnListClassAsync.InParamMetaDataList.First();
            Assert.True(inParam1.TypeWrapper.Type == typeof(SampleClass));
            Assert.True(inParam1.Name == "a");

            var outParam = methodWithInputReturnListClassAsync.OutParamMetaDataList.First();
            Assert.True(outParam.TypeWrapper.Type == typeof(SampleClass));
            Assert.True(outParam.TypeWrapper.IsArray);



            name = "MethodWithInputReturnNullableClassAsync";
            var methodWithInputReturnNullableClassAsync = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(methodWithInputReturnNullableClassAsync);
            Assert.True(methodWithInputReturnNullableClassAsync.IsTask);
            Assert.True(methodWithInputReturnNullableClassAsync.InParamMetaDataList.Count == 2);
            Assert.True(methodWithInputReturnNullableClassAsync.OutParamMetaDataList.Count == 1);

            inParam1 = methodWithInputReturnNullableClassAsync.InParamMetaDataList.First();
            Assert.True(inParam1.TypeWrapper.Type == typeof(int));
            Assert.True(inParam1.Name == "a");

            var inParam2 = methodWithInputReturnNullableClassAsync.InParamMetaDataList.Last();
            Assert.True(inParam2.TypeWrapper.Type == typeof(string));
            Assert.True(inParam2.Name == "b");

            outParam = methodWithInputReturnNullableClassAsync.OutParamMetaDataList.First();
            Assert.True(outParam.TypeWrapper.Type == typeof(NullableClass));
            Assert.False(outParam.TypeWrapper.IsArray);


            //NullableClassMetaDataTest
            NullableClassMetaDataTest(outParam.ClassMetaData);
        }

        private void NotSupportMethodTest(InterfaceMetaData metaData)
        {
            var name = "NotSupport";
            var notSupport = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase));
            Assert.Null(notSupport);
        }

        private void OtherTest(InterfaceMetaData metaData)
        {
           var  name = "TestCancellationToken";
            var testCancellationToken = metaData.MethodMetaDataList.FirstOrDefault(d => d.Name == name);
            Assert.NotNull(testCancellationToken);
            Assert.False(testCancellationToken.IsTask);
            Assert.True(testCancellationToken.InParamMetaDataList.Count == 1);
            Assert.False(testCancellationToken.OutParamMetaDataList.Any());

            //var inParam = testCancellationToken.InParamMetaDataList.First();
            //Assert.True(inParam.TypeWrapper.Type == typeof(int));
            //Assert.True(inParam.Name == "a");
        }

        private void ClassMetaDataTest(ClassMetaData metaData)
        {
            Assert.NotNull(metaData);

            var classType = typeof(SampleClass);
            Assert.True(metaData.TypeWrapper.Type == classType);
            Assert.True(metaData.Name == classType.Name);
            Assert.True(metaData.Namespace == classType.Namespace);
            Assert.True(metaData.FullName == classType.FullName);
            
            Assert.True(metaData.AttributeMetaDataList.Count == 3);
            Assert.Contains(typeof(DescriptionAttribute), metaData.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayNameAttribute), metaData.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayAttribute), metaData.AttributeMetaDataList.Select(d => d.Type));

            Assert.Equal("SampleClass", metaData.Description);
            Assert.Equal("SampleClass", metaData.DisplayName);
            Assert.Equal("SampleClass", metaData.Display);


            var prop = metaData.PropertyMetaDataList.FirstOrDefault(d => d.Name == "IntColumn");
            Assert.NotNull(prop);
            Assert.True(prop.TypeWrapper.Type == typeof(int));
            Assert.False(prop.TypeWrapper.IsNullable);
            Assert.False(prop.TypeWrapper.IsEnum);
            Assert.False(prop.TypeWrapper.IsClass);

            Assert.True(prop.AttributeMetaDataList.Count == 3);
            Assert.Contains(typeof(DescriptionAttribute), prop.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayNameAttribute), prop.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayAttribute), prop.AttributeMetaDataList.Select(d => d.Type));

            Assert.Equal("IntColumn", prop.Description);
            Assert.Equal("IntColumn", prop.DisplayName);
            Assert.Equal("IntColumn", prop.Display);





            //EnumColumn
            prop = metaData.PropertyMetaDataList.FirstOrDefault(d => d.Name == "EnumColumn");
            Assert.NotNull(prop);
            Assert.True(prop.TypeWrapper.Type == typeof(SampleEnum));
            Assert.False(prop.TypeWrapper.IsNullable);
            Assert.True(prop.TypeWrapper.IsEnum);
            Assert.False(prop.TypeWrapper.IsClass);

            Assert.True(prop.AttributeMetaDataList.Count == 3);
            Assert.Contains(typeof(DescriptionAttribute), prop.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayNameAttribute), prop.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayAttribute), prop.AttributeMetaDataList.Select(d => d.Type));

            Assert.Equal("EnumColumn", prop.Description);
            Assert.Equal("EnumColumn", prop.DisplayName);
            Assert.Equal("EnumColumn", prop.Display);


            //ClassColumn
            prop = metaData.PropertyMetaDataList.FirstOrDefault(d => d.Name == "ClassColumn");
            Assert.NotNull(prop);
            Assert.True(prop.TypeWrapper.Type == typeof(SampleClass));
            Assert.False(prop.TypeWrapper.IsNullable);
            Assert.False(prop.TypeWrapper.IsEnum);
            Assert.True(prop.TypeWrapper.IsClass);

            Assert.True(prop.AttributeMetaDataList.Count == 3);
            Assert.Contains(typeof(DescriptionAttribute), prop.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayNameAttribute), prop.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayAttribute), prop.AttributeMetaDataList.Select(d => d.Type));

            Assert.Equal("ClassColumn", prop.Description);
            Assert.Equal("ClassColumn", prop.DisplayName);
            Assert.Equal("ClassColumn", prop.Display);


            //StructColumn
            prop = metaData.PropertyMetaDataList.FirstOrDefault(d => d.Name == "StructColumn");
            Assert.NotNull(prop);
            Assert.True(prop.TypeWrapper.Type == typeof(SampleStruct));
            Assert.False(prop.TypeWrapper.IsNullable);
            Assert.False(prop.TypeWrapper.IsEnum);
            Assert.False(prop.TypeWrapper.IsClass);
            Assert.True(prop.TypeWrapper.IsStruct);

            Assert.True(prop.AttributeMetaDataList.Count == 3);
            Assert.Contains(typeof(DescriptionAttribute), prop.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayNameAttribute), prop.AttributeMetaDataList.Select(d => d.Type));
            Assert.Contains(typeof(DisplayAttribute), prop.AttributeMetaDataList.Select(d => d.Type));

            Assert.Equal("StructColumn", prop.Description);
            Assert.Equal("StructColumn", prop.DisplayName);
            Assert.Equal("StructColumn", prop.Display);
        }

        private void TreeClassMetaDataTest(ClassMetaData metaData)
        {
            Assert.NotNull(metaData);

            var classType = typeof(TreeClass);
            Assert.True(metaData.TypeWrapper.Type == classType);
            Assert.True(metaData.Name == classType.Name);
            Assert.True(metaData.Namespace == classType.Namespace);
            Assert.True(metaData.FullName == classType.FullName);




            var prop = metaData.PropertyMetaDataList.FirstOrDefault(d => d.Name == "Children");
            Assert.NotNull(prop);
            Assert.True(prop.TypeWrapper.Type == typeof(TreeClass));
            Assert.True(prop.TypeWrapper.IsArray);
            Assert.True(prop.TypeWrapper.IsClass);
        }

        private void GenericClassMetaDataTest(ClassMetaData metaData)
        {
            Assert.NotNull(metaData);

            var classType = typeof(TreeNode<SampleClass>);
            Assert.True(metaData.TypeWrapper.Type == classType);
            Assert.True(metaData.Name == classType.Name + "_" + "SampleClass");
            Assert.True(metaData.Namespace == classType.Namespace);
            Assert.True(metaData.FullName == classType.Namespace + "." + classType.Name + "_" + "SampleClass");




            var prop = metaData.PropertyMetaDataList.FirstOrDefault(d => d.Name == "Children");
            Assert.NotNull(prop);
            Assert.True(prop.TypeWrapper.Type == typeof(SampleClass));
            Assert.True(prop.TypeWrapper.IsArray);
            Assert.True(prop.TypeWrapper.IsClass);
        }

        private void NullableClassMetaDataTest(ClassMetaData metaData)
        {
            Assert.NotNull(metaData);

            var classType = typeof(NullableClass);
            Assert.True(metaData.TypeWrapper.Type == classType);
            Assert.True(metaData.Name == classType.Name);
            Assert.True(metaData.Namespace == classType.Namespace);
            Assert.True(metaData.FullName == classType.FullName);
            
            var prop = metaData.PropertyMetaDataList.FirstOrDefault(d => d.Name == "NullableIntColumn");
            Assert.NotNull(prop);
            Assert.True(prop.TypeWrapper.Type == typeof(int));
            Assert.True(prop.TypeWrapper.IsNullable);
            Assert.False(prop.TypeWrapper.IsEnum);
            Assert.False(prop.TypeWrapper.IsClass);
            

            //EnumColumn
            prop = metaData.PropertyMetaDataList.FirstOrDefault(d => d.Name == "NullableEnumColumn");
            Assert.NotNull(prop);
            Assert.True(prop.TypeWrapper.Type == typeof(SampleEnum));
            Assert.True(prop.TypeWrapper.IsNullable);
            Assert.True(prop.TypeWrapper.IsEnum);
            Assert.False(prop.TypeWrapper.IsClass);
           
            SampleEnumTest(prop.EnumMetaData);
        }

        /// <summary>
        /// 枚举
        /// </summary>
        /// <param name="metaData"></param>
        private void SampleEnumTest(EnumMetaData metaData)
        {
            Assert.NotNull(metaData);

            var type = typeof(SampleEnum);
            Assert.True(metaData.Name == type.Name);
            Assert.True(metaData.Namespace == type.Namespace);
            Assert.True(metaData.FullName == type.FullName);

            Assert.True(metaData.Members.Count == 2);

            Assert.Equal("SampleEnum desc from  Description", metaData.Description);
            Assert.Contains(typeof(DescriptionAttribute), metaData.AttributeMetaDataList.Select(d => d.Type));

            var firstMember = metaData.Members.First();
            Assert.Equal("One1", firstMember.Name);
            Assert.Equal(0, firstMember.Value);
            Assert.Contains(typeof(DescriptionAttribute), firstMember.AttributeMetaDataList.Select(d => d.Type));
            Assert.Equal("One1", firstMember.AttributeMetaDataList.First(d => d.Type == typeof(DescriptionAttribute)).ConstructorDictionary.First().Value.ToString());

            var secondMember = metaData.Members.Last();
            Assert.Equal("Two2", secondMember.Name);
            Assert.Equal(12, secondMember.Value);
            Assert.Contains(typeof(DescriptionAttribute), secondMember.AttributeMetaDataList.Select(d => d.Type));
            Assert.Equal("Two2", secondMember.AttributeMetaDataList.First(d => d.Type == typeof(DescriptionAttribute)).ConstructorDictionary.First().Value.ToString());


        }
    }
}