using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Renders;
using GRpcProtocolGenerator.Types;
using Sample.Services.Models;

namespace GRpcProtocolGenerator.Test.Metadatas
{
    public class MessageTest
    {
        private readonly Builder _codeRender;

        public MessageTest()
        {
            _codeRender = new MetaDataTest().Handler.CreateBuilder();
        }


        [Fact]
        public void MessageItemTest()
        {
            Assert.True(_codeRender.ClassMessages.Any());

            var sampleClassMessage = _codeRender.ClassMessages.FirstOrDefault(x => x.Key == typeof(SampleClass).FullName).Value;
            Assert.NotNull(sampleClassMessage);
            Assert.NotNull(sampleClassMessage.ClassMetaData);
            Assert.True(sampleClassMessage.IsOriginalClass);
            Assert.Equal(nameof(SampleClass), sampleClassMessage.Name);
            Assert.Equal("GRpcSampleClass", sampleClassMessage.GetGRpcName());

            //属性总数
            Assert.True(sampleClassMessage.Items.Count == 14);

            //IntColumn
            var item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "IntColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.Int.GetDescription());

            var itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // IntColumn";
            Assert.Equal(item.ToString(), itemString);


            //LongColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "LongColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.Long.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // LongColumn";
            Assert.Equal(item.ToString(), itemString);


            //DecimalColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "DecimalColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.Double.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // DecimalColumn";
            Assert.Equal(item.ToString(), itemString);


            //DoubleColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "DoubleColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.Double.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // DoubleColumn";
            Assert.Equal(item.ToString(), itemString);

            
            //FloatColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "FloatColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.Float.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // FloatColumn";
            Assert.Equal(item.ToString(), itemString);

            //UintColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "UintColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.UInt.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // UintColumn";
            Assert.Equal(item.ToString(), itemString);

            //UlongColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "UlongColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.ULong.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // UlongColumn";
            Assert.Equal(item.ToString(), itemString);

            //StringColumn  默认可为空
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "StringColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.True(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpNullableTypeEnum.String.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // StringColumn";
            Assert.Equal(item.ToString(), itemString);

            //BoolColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "BoolColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.Bool.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // BoolColumn";
            Assert.Equal(item.ToString(), itemString);


            //DateTimeColumn 默认转string
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "DateTimeColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.String.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // DateTimeColumn";
            Assert.Equal(item.ToString(), itemString);

            //ByteColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "ByteColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.Int.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // ByteColumn";
            Assert.Equal(item.ToString(), itemString);

            //ClassColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "ClassColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.True(item.ClassMetaData != null);
            Assert.Equal("GRpcSampleClass", item.GRpcType);

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // ClassColumn";
            Assert.Equal(item.ToString(), itemString);

            //EnumColumn 默认转数字
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "EnumColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.False(item.IsNullable);
            Assert.Equal(item.GRpcType, CSharpTypeEnum.Int.GetDescription());

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // EnumColumn";
            Assert.Equal(item.ToString(), itemString);

            //StructColumn
            item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "StructColumn");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.Equal("GRpcSampleStruct", item.GRpcType);

            itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // StructColumn";
            Assert.Equal(item.ToString(), itemString);
        }

        [Fact]
        public void MessageItemTest2()
        {
            Assert.True(_codeRender.ClassMessages.Any());

            var sampleClassMessage = _codeRender.ClassMessages.FirstOrDefault(x => x.Key == typeof(ExtClass).FullName).Value;
            Assert.NotNull(sampleClassMessage);
            Assert.NotNull(sampleClassMessage.ClassMetaData);
            Assert.True(sampleClassMessage.IsOriginalClass);
            Assert.Equal(nameof(ExtClass), sampleClassMessage.Name);
            Assert.Equal("GRpcExtClass", sampleClassMessage.GetGRpcName());

            //属性总数
            Assert.True(sampleClassMessage.Items.Count == 15);

            //GenericClass
            var item = sampleClassMessage.Items.FirstOrDefault(x => x.Name == "GenericClass");
            Assert.NotNull(item);
            Assert.False(item.IsArray);
            Assert.Equal("GRpcTreeNode_SampleClass", item.GRpcType);

            var itemString = $"{item.GRpcType} {(Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? item.Name.ToFirstLowString() : item.Name)} = 0; // GenericClass";
            Assert.Equal(item.ToString(), itemString);
        }

        [Fact]
        public void MessageItemTest3()
        {
            //CancellationToken
            Assert.Null(BuilderPart.BuildMessageItem("a", typeof(CancellationToken), false, false, 0, null));

            //int
            Assert.Equal("int32 a = 1; // desc", BuilderPart.BuildMessageItem("a", typeof(int), false, false, 1, "desc"));
            Assert.Equal("int32 a = 2;", BuilderPart.BuildMessageItem("a", typeof(int), false, false, 2, ""));
            Assert.Equal("google.protobuf.Int32Value a = 3;", BuilderPart.BuildMessageItem("a", typeof(int), false, true, 3, ""));
            Assert.Equal("repeated google.protobuf.Int32Value a = 4;", BuilderPart.BuildMessageItem("a", typeof(int), true, true, 4, ""));
            Assert.Equal("repeated int32 a = 5;", BuilderPart.BuildMessageItem("a", typeof(int), true, false, 5, ""));

            //string
            Assert.Equal("string a = 0;", BuilderPart.BuildMessageItem("a", typeof(string), false, false, 0, ""));
            Assert.Equal("google.protobuf.StringValue a = 0;", BuilderPart.BuildMessageItem("a", typeof(string), false, true, 0, ""));
            Assert.Equal("repeated google.protobuf.StringValue a = 0;", BuilderPart.BuildMessageItem("a", typeof(string), true, true, 0, ""));
            Assert.Equal("repeated string a = 0;", BuilderPart.BuildMessageItem("a", typeof(string), true, false, 0, ""));

            //class
            Assert.Equal("GRpcSampleClass a = 0;", BuilderPart.BuildMessageItem("a", typeof(SampleClass), false, false, 0, ""));
            Assert.Equal("GRpcSampleClass a = 0;", BuilderPart.BuildMessageItem("a", typeof(SampleClass), false, true, 0, ""));
            Assert.Equal("repeated GRpcSampleClass a = 0;", BuilderPart.BuildMessageItem("a", typeof(SampleClass), true, true, 0, ""));
            Assert.Equal("repeated GRpcSampleClass a = 0;", BuilderPart.BuildMessageItem("a", typeof(SampleClass), true, false, 0, ""));

            //泛型class
            Assert.Equal($"{typeof(TreeNode<SampleClass>).GetGenericClassName().FormatMessageName()} a = 0;",
                BuilderPart.BuildMessageItem("a", typeof(TreeNode<SampleClass>), false, false, 0, ""));

            //struct
            Assert.Equal("GRpcSampleStruct a = 0;", BuilderPart.BuildMessageItem("a", typeof(SampleStruct), false, false, 0, ""));
            Assert.Equal("GRpcSampleStruct a = 0;", BuilderPart.BuildMessageItem("a", typeof(SampleStruct), false, true, 0, ""));
            Assert.Equal("repeated GRpcSampleStruct a = 0;", BuilderPart.BuildMessageItem("a", typeof(SampleStruct), true, true, 0, ""));
            Assert.Equal("repeated GRpcSampleStruct a = 0;", BuilderPart.BuildMessageItem("a", typeof(SampleStruct), true, false, 0, ""));

            //bytes
            Assert.Equal("bytes a = 0;", BuilderPart.BuildMessageItem("a", typeof(byte[]), false, false, 0, ""));
        }

        [Fact]
        public void GenericClassNameTest()
        {
            Assert.Equal("SampleClass", typeof(SampleClass).GetGenericClassName());
            Assert.Equal("TreeNode`1_SampleClass", typeof(TreeNode<SampleClass>).GetGenericClassName());
            Assert.Equal("TreeNode`2_SampleClass_Int32", typeof(TreeNode<SampleClass, int>).GetGenericClassName());
        }
    }
}
