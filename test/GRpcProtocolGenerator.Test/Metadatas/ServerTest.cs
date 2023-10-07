using GRpcProtocolGenerator.Renders;
using Sample.Services.Models;

namespace GRpcProtocolGenerator.Test.Metadatas
{
    public class ServerTest
    {
        private readonly Builder _codeRender;

        public ServerTest()
        {
            _codeRender = new MetaDataTest().Handler.CreateBuilder();
        }

        [Fact]
        public void MapperTest()
        {
            Assert.Equal("a.Adapt<b>()", BuilderPart.MapTo("a", "b"));
            Assert.Equal("a?.Adapt<b>()", BuilderPart.MapTo(true, "a", "b"));
            Assert.Equal("a.Adapt<b>()", BuilderPart.MapTo(false, "a", "b"));

            Assert.Equal("a.Adapt<List<b>>()", BuilderPart.MapToList("a", "b"));
            Assert.Equal("a?.Adapt<List<b>>()", BuilderPart.MapToList(true, "a", "b"));
            Assert.Equal("a.Adapt<List<b>>()", BuilderPart.MapToList(false, "a", "b"));

            //sampleClass
            var type = typeof(SampleClass);
            Assert.Equal($"a?.Adapt<{type.FullName}>()", BuilderPart.MapTo("a", type));

            //泛型
            type = typeof(TreeNode<SampleClass>);
            var a = $"a?.Adapt<{type.GetGenericTypeDefinition().FullName?.Replace("`1", "")}<{typeof(SampleClass).FullName}>>()";
            Assert.Equal(a, BuilderPart.MapTo("a", type));

            //泛型2
            type = typeof(TreeNode<SampleClass, int>);
            a = $"a?.Adapt<{type.GetGenericTypeDefinition().FullName?.Replace("`2", "")}<{typeof(SampleClass).FullName}, {typeof(int).FullName}>>()";
            Assert.Equal(a, BuilderPart.MapTo("a", type));

            //泛型3
            type = typeof(TreeNode<SampleClass, int, TreeClass>);
            a = $"a?.Adapt<{type.GetGenericTypeDefinition().FullName?.Replace("`3", "")}<{typeof(SampleClass).FullName}, {typeof(int).FullName}, {typeof(TreeClass).FullName}>>()";
            Assert.Equal(a, BuilderPart.MapTo("a", type));
        }

        [Fact]
        public void MethodTest()
        {
            Assert.Equal("async Task<int>",
                BuilderPart.BuildMethodReturnType(isWrapper: true,
                    isOutParamEmpty: true,
                    type: "int",
                    emptyType: "aaa"));

            Assert.Equal("async Task<int>",
                BuilderPart.BuildMethodReturnType(isWrapper: true,
                    isOutParamEmpty: false,
                    type: "int",
                    emptyType: "aaa"));

            Assert.Equal("async Task<aaa>",
                BuilderPart.BuildMethodReturnType(isWrapper: false,
                    isOutParamEmpty: true,
                    type: "int",
                    emptyType: "aaa"));

            Assert.Equal("async Task<int>",
                BuilderPart.BuildMethodReturnType(isWrapper: false,
                    isOutParamEmpty: false,
                    type: "int",
                    emptyType: "aaa"));
        }

        [Fact]
        public void InputTest()
        {
            //CancellationToken
            Assert.Equal("context.CancellationToken", BuilderPart.BuildCancellationTokenInput());
            Assert.Equal(BuilderPart.BuildCancellationTokenInput(), BuilderPart.BuildInputItem("request", "a", typeof(CancellationToken), false, true));

            //int
            Assert.Equal("request.A?.ToList()", BuilderPart.BuildInputItem("request", "a", typeof(int), true, true));
            Assert.Equal("request.A.ToList()", BuilderPart.BuildInputItem("request", "a", typeof(int), true, false));
            Assert.Equal("request.A", BuilderPart.BuildInputItem("request", "a", typeof(int), false, true));
            Assert.Equal("request.A", BuilderPart.BuildInputItem("request", "a", typeof(int), false, false));

            //string
            Assert.Equal("request.A?.ToList()", BuilderPart.BuildInputItem("request", "a", typeof(string), true, true));
            Assert.Equal("request.A.ToList()", BuilderPart.BuildInputItem("request", "a", typeof(string), true, false));
            Assert.Equal("request.A", BuilderPart.BuildInputItem("request", "a", typeof(string), false, true));
            Assert.Equal("request.A", BuilderPart.BuildInputItem("request", "a", typeof(string), false, false));

            //class
            Assert.Equal($"request.A?.Adapt<List<{typeof(SampleClass).FullName}>>()", BuilderPart.BuildInputItem("request", "a", typeof(SampleClass), true, true));
            Assert.Equal($"request.A?.Adapt<List<{typeof(SampleClass).FullName}>>()", BuilderPart.BuildInputItem("request", "a", typeof(SampleClass), true, false));
            Assert.Equal($"request.A?.Adapt<{typeof(SampleClass).FullName}>()", BuilderPart.BuildInputItem("request", "a", typeof(SampleClass), false, true));
            Assert.Equal($"request.A?.Adapt<{typeof(SampleClass).FullName}>()", BuilderPart.BuildInputItem("request", "a", typeof(SampleClass), false, false));
        }

        [Fact]
        public void ReturnTest()
        {
            //返回空
            Assert.Equal("return new a { Code = 1 };", BuilderPart.BuildEmptyReturn("a", true, 1));
            Assert.Equal("return new Empty();", BuilderPart.BuildEmptyReturn("a", false, 1));

            //返回class
            Assert.Equal("return new a { Code = 1, Data = data.Adapt<List<a>>() };", BuilderPart.BuildClassReturn("a", true, true, 1));
            Assert.Equal("return new a { Code = 1, Data = data.Adapt<a>() };", BuilderPart.BuildClassReturn("a", false, true, 1));

            Assert.Equal("return data.Adapt<List<a>>();", BuilderPart.BuildClassReturn("a", true, false, 1));
            Assert.Equal("return data.Adapt<a>();", BuilderPart.BuildClassReturn("a", false, false, 1));


            //项
            //class项
            Assert.Equal("a = { data.Adapt<List<int>>() }", BuilderPart.BuildClassItemReturn("a", true, "int"));
            Assert.Equal("a = data.Adapt<int>()", BuilderPart.BuildClassItemReturn("a", false, "int"));

            //枚举项
            Assert.Equal("a = { data.Adapt<List<int?>>() }", BuilderPart.BuildEnumItemReturn("a", true, true));
            Assert.Equal("a = { data.Adapt<List<int>>() }", BuilderPart.BuildEnumItemReturn("a", true, false));
            Assert.Equal("a = (int?)data", BuilderPart.BuildEnumItemReturn("a", false, true));
            Assert.Equal("a = (int)data", BuilderPart.BuildEnumItemReturn("a", false, false));

            //普通项
            Assert.Equal("a = { data.ToList() }", BuilderPart.BuildSampleItemReturn("a", true));
            Assert.Equal("a = data", BuilderPart.BuildSampleItemReturn("a", false));
        }
    }
}
