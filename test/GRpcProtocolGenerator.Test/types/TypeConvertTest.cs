using GRpcProtocolGenerator.Common.Attributes;
using GRpcProtocolGenerator.Resolve;
using GRpcProtocolGenerator.Types;

namespace GRpcProtocolGenerator.Test.types;

/// <summary>
/// TypeConvert 类型映射测试（覆盖 0.4.0 新增的类型映射与错误提示）
/// </summary>
public class TypeConvertTest
{
    [Theory]
    // 0.4.0 新增映射
    [InlineData(typeof(short), false, "int32")]
    [InlineData(typeof(short), true, "google.protobuf.Int32Value")]
    [InlineData(typeof(ushort), false, "uint32")]
    [InlineData(typeof(sbyte), false, "int32")]
    [InlineData(typeof(char), false, "string")]
    [InlineData(typeof(Half), false, "float")]
    [InlineData(typeof(nint), false, "int64")]
    [InlineData(typeof(nuint), false, "uint64")]
    [InlineData(typeof(DateOnly), false, "string")]
    [InlineData(typeof(TimeOnly), false, "string")]
    [InlineData(typeof(TimeSpan), false, "string")]
    [InlineData(typeof(DateTimeOffset), false, "string")]
    // 既有映射回归
    [InlineData(typeof(int), false, "int32")]
    [InlineData(typeof(int), true, "google.protobuf.Int32Value")]
    [InlineData(typeof(long), false, "int64")]
    [InlineData(typeof(uint), false, "uint32")]
    [InlineData(typeof(ulong), false, "uint64")]
    [InlineData(typeof(float), false, "float")]
    [InlineData(typeof(double), false, "double")]
    [InlineData(typeof(decimal), false, "double")]
    [InlineData(typeof(bool), false, "bool")]
    [InlineData(typeof(string), false, "string")]
    [InlineData(typeof(string), true, "google.protobuf.StringValue")]
    [InlineData(typeof(byte), false, "int32")]
    [InlineData(typeof(byte[]), false, "bytes")]
    [InlineData(typeof(Guid), false, "string")]
    [InlineData(typeof(DateTime), false, "string")]
    public void Convert_MapsToExpectedProtobufType(Type type, bool isNullable, string expected)
    {
        Assert.Equal(expected, TypeConvert.Convert(type, isNullable));
    }

    [Fact]
    public void Convert_UnsupportedType_ThrowsNotSupportedExceptionWithGuidance()
    {
        var ex = Assert.Throws<NotSupportedException>(() => TypeConvert.Convert(typeof(System.Text.StringBuilder), false));

        // 错误信息应指引用户如何处理
        Assert.Contains("CSharpTypeToProtobufString", ex.Message);
        Assert.Contains("GRpcIgnore", ex.Message);
    }

    /// <summary>
    /// 特性构造参数必须按参数索引映射（回归：之前误用固定索引 0）
    /// </summary>
    [Fact]
    public void AttributeConstructorArguments_MapsByParameterIndex()
    {
        var resolver = new MetaDataResolve();
        var interfaces = resolver.ToInterfaceMetaDataList([typeof(IAttributeArgTestService)]);

        var meta = Assert.Single(interfaces);
        var method = Assert.Single(meta.MethodMetaDataList,
            m => m.Name == nameof(IAttributeArgTestService.MethodWithTwoArgAttribute));
        var attr = Assert.Single(method.AttributeMetaDataList,
            a => a.Type == typeof(TwoConstructorArgAttribute));

        Assert.Equal(2, attr.ConstructorDictionary!.Count);
        Assert.Equal("hello", attr.ConstructorDictionary[0]);
        Assert.Equal(42, attr.ConstructorDictionary[1]);
    }

    [AttributeUsage(AttributeTargets.Method)]
    private sealed class TwoConstructorArgAttribute : Attribute
    {
        public TwoConstructorArgAttribute(string name, int order)
        {
            Name = name;
            Order = order;
        }

        public string Name { get; }
        public int Order { get; }
    }

    [GRpcGenerator]
    private interface IAttributeArgTestService
    {
        [TwoConstructorArg("hello", 42)]
        void MethodWithTwoArgAttribute();
    }
}
