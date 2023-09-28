using GRpcProtocolGenerator.Renders;
using Sample.Services.Models;

namespace GRpcProtocolGenerator.Test.Metadatas
{
    public class ServerTest
    {
        private readonly CodeRender _codeRender;

        public ServerTest()
        {
            _codeRender = new MetaDataTest().Handler.CreateCodeRender();
        }

        [Fact]
        public void MapperTest()
        {
            Assert.Equal("a.Adapt<b>()", RenderHelper.MapTo("a", "b"));
            Assert.Equal("a?.Adapt<b>()", RenderHelper.MapTo(true, "a", "b"));
            Assert.Equal("a.Adapt<b>()", RenderHelper.MapTo(false, "a", "b"));

            Assert.Equal("a.Adapt<List<b>>()", RenderHelper.MapToList("a", "b"));
            Assert.Equal("a?.Adapt<List<b>>()", RenderHelper.MapToList(true, "a", "b"));
            Assert.Equal("a.Adapt<List<b>>()", RenderHelper.MapToList(false, "a", "b"));

            //sampleClass
            var type = typeof(SampleClass);
            Assert.Equal($"a?.Adapt<{type.FullName}>()", RenderHelper.MapTo("a", type));

            //泛型
            type = typeof(TreeNode<SampleClass>);
            var a = $"a?.Adapt<{type.GetGenericTypeDefinition().FullName?.Replace("`1", "")}<{typeof(SampleClass).FullName}>>()";
            Assert.Equal(a, RenderHelper.MapTo("a", type));

            //泛型2
            type = typeof(TreeNode<SampleClass, int>);
            a = $"a?.Adapt<{type.GetGenericTypeDefinition().FullName?.Replace("`2", "")}<{typeof(SampleClass).FullName}, {typeof(int).FullName}>>()";
            Assert.Equal(a, RenderHelper.MapTo("a", type));

            //泛型3
            type = typeof(TreeNode<SampleClass, int, TreeClass>);
            a = $"a?.Adapt<{type.GetGenericTypeDefinition().FullName?.Replace("`3", "")}<{typeof(SampleClass).FullName}, {typeof(int).FullName}, {typeof(TreeClass).FullName}>>()";
            Assert.Equal(a, RenderHelper.MapTo("a", type));
        }

        [Fact]
        public void MethodTest()
        {
            Assert.Equal("async Task<int>",
                RenderHelper.BuildMethodReturnType(isWrapper: true,
                    isOutParamEmpty: true,
                    type: "int",
                    emptyType: "aaa"));

            Assert.Equal("async Task<int>",
                RenderHelper.BuildMethodReturnType(isWrapper: true,
                    isOutParamEmpty: false,
                    type: "int",
                    emptyType: "aaa"));

            Assert.Equal("async Task<aaa>",
                RenderHelper.BuildMethodReturnType(isWrapper: false,
                    isOutParamEmpty: true,
                    type: "int",
                    emptyType: "aaa"));

            Assert.Equal("async Task<int>",
                RenderHelper.BuildMethodReturnType(isWrapper: false,
                    isOutParamEmpty: false,
                    type: "int",
                    emptyType: "aaa"));
        }

        [Fact]
        public void InputTest()
        {
            //CancellationToken
            Assert.Equal("context.CancellationToken", RenderHelper.BuildCancellationTokenInput());
            Assert.Equal(RenderHelper.BuildCancellationTokenInput(), RenderHelper.BuildInputItem("request", "a", typeof(CancellationToken), false, true));

            //int
            Assert.Equal("request.A?.ToList()", RenderHelper.BuildInputItem("request", "a", typeof(int), true, true));
            Assert.Equal("request.A.ToList()", RenderHelper.BuildInputItem("request", "a", typeof(int), true, false));
            Assert.Equal("request.A", RenderHelper.BuildInputItem("request", "a", typeof(int), false, true));
            Assert.Equal("request.A", RenderHelper.BuildInputItem("request", "a", typeof(int), false, false));

            //string
            Assert.Equal("request.A?.ToList()", RenderHelper.BuildInputItem("request", "a", typeof(string), true, true));
            Assert.Equal("request.A.ToList()", RenderHelper.BuildInputItem("request", "a", typeof(string), true, false));
            Assert.Equal("request.A", RenderHelper.BuildInputItem("request", "a", typeof(string), false, true));
            Assert.Equal("request.A", RenderHelper.BuildInputItem("request", "a", typeof(string), false, false));

            //class
            Assert.Equal($"request.A?.Adapt<List<{typeof(SampleClass).FullName}>>()", RenderHelper.BuildInputItem("request", "a", typeof(SampleClass), true, true));
            Assert.Equal($"request.A?.Adapt<List<{typeof(SampleClass).FullName}>>()", RenderHelper.BuildInputItem("request", "a", typeof(SampleClass), true, false));
            Assert.Equal($"request.A?.Adapt<{typeof(SampleClass).FullName}>()", RenderHelper.BuildInputItem("request", "a", typeof(SampleClass), false, true));
            Assert.Equal($"request.A?.Adapt<{typeof(SampleClass).FullName}>()", RenderHelper.BuildInputItem("request", "a", typeof(SampleClass), false, false));
        }

        [Fact]
        public void ReturnTest()
        {
            //返回空
            Assert.Equal("return new a { Code = 1 };", RenderHelper.BuildEmptyReturn("a", true, 1));
            Assert.Equal("return new Empty();", RenderHelper.BuildEmptyReturn("a", false, 1));

            //返回class
            Assert.Equal("return new a { Code = 1, Data = data.Adapt<List<a>>() };", RenderHelper.BuildClassReturn("a", true, true, 1));
            Assert.Equal("return new a { Code = 1, Data = data.Adapt<a>() };", RenderHelper.BuildClassReturn("a", false, true, 1));

            Assert.Equal("return data.Adapt<List<a>>();", RenderHelper.BuildClassReturn("a", true, false, 1));
            Assert.Equal("return data.Adapt<a>();", RenderHelper.BuildClassReturn("a", false, false, 1));


            //项
            //class项
            Assert.Equal("a = { data.Adapt<List<int>>() }", RenderHelper.BuildClassItemReturn("a", true, "int"));
            Assert.Equal("a = data.Adapt<int>()", RenderHelper.BuildClassItemReturn("a", false, "int"));

            //枚举项
            Assert.Equal("a = { data.Adapt<List<int?>>() }", RenderHelper.BuildEnumItemReturn("a", true, true));
            Assert.Equal("a = { data.Adapt<List<int>>() }", RenderHelper.BuildEnumItemReturn("a", true, false));
            Assert.Equal("a = (int?)data", RenderHelper.BuildEnumItemReturn("a", false, true));
            Assert.Equal("a = (int)data", RenderHelper.BuildEnumItemReturn("a", false, false));

            //普通项
            Assert.Equal("a = { data.ToList() }", RenderHelper.BuildSampleItemReturn("a", true));
            Assert.Equal("a = data", RenderHelper.BuildSampleItemReturn("a", false));
        }
    }
}
