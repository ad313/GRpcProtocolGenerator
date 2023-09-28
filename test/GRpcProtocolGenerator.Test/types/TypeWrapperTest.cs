using Sample.Services.Models;
using System.Text;

namespace GRpcProtocolGenerator.Test.types
{
    public class TypeWrapperTest
    {
        [Fact]
        public void TypeResultTest()
        {
            foreach (var pair in TypeTest.TaskDictionary)
            {
                var resultWrapper = pair.Value.ToTypeWrapper();
                Assert.True(resultWrapper.IsTask);
            }

            var result = typeof(byte[]).ToTypeWrapper();
            Assert.True(result.IsByteArray);
            Assert.False(result.IsArray);
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(sbyte[]).ToTypeWrapper();
            Assert.True(result.IsByteArray);
            Assert.False(result.IsArray);
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(void).ToTypeWrapper();
            Assert.True(result.IsVoid);
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(Task).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsVoid);
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(ValueTask).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsVoid);
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(Task<int>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(int));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(Task<int?>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.True(result.IsNullable);
            Assert.True(result.Type == typeof(int));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(Task<Dictionary<int, int>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(Dictionary<int, int>));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);


            result = typeof(Tuple<int, string>).ToTypeWrapper();
            Assert.True(result.IsTuple);
            Assert.False(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(Tuple<int, string>));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(Dictionary<int, string>).ToTypeWrapper();
            Assert.True(result.IsDictionary);
            Assert.False(result.IsTuple);
            Assert.False(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(Dictionary<int, string>));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(SampleEnum).ToTypeWrapper();
            Assert.True(result.IsEnum);
            Assert.False(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(SampleEnum));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(SampleEnum?).ToTypeWrapper();
            Assert.True(result.IsEnum);
            Assert.False(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.True(result.IsNullable);
            Assert.True(result.Type == typeof(SampleEnum));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(SampleStruct).ToTypeWrapper();
            Assert.True(result.IsStruct);
            Assert.False(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(SampleStruct));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(SampleStruct?).ToTypeWrapper();
            Assert.True(result.IsStruct);
            Assert.False(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.True(result.IsNullable);
            Assert.True(result.Type == typeof(SampleStruct));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(SampleClass).ToTypeWrapper();
            Assert.True(result.IsClass);
            Assert.False(result.IsStruct);
            Assert.False(result.IsTask);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(SampleClass));
            Assert.True(result.IsValid);

            //array
            result = typeof(Array).ToTypeWrapper();
            Assert.True(result.IsArray);

            //list
            result = typeof(Task<List<int>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsArray);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(int));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(Task<List<int?>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsArray);
            Assert.False(result.IsVoid);
            Assert.True(result.IsNullable);
            Assert.True(result.Type == typeof(int));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(Task<List<SampleClass>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsArray);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(SampleClass));
            Assert.True(result.IsClass);
            Assert.True(result.IsValid);

            result = typeof(Task<List<Dictionary<int, int>>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsArray);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(Dictionary<int, int>));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);
            Assert.True(result.IsDictionary);

            result = typeof(Task<List<Tuple<int, int>>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsArray);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(Tuple<int, int>));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);
            Assert.True(result.IsTuple);

            result = typeof(Task<List<SampleEnum>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsArray);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(SampleEnum));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);
            Assert.True(result.IsEnum);

            result = typeof(Task<List<SampleStruct>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsArray);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(SampleStruct));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);
            Assert.True(result.IsStruct);

            result = typeof(Task<List<byte>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsArray);
            Assert.False(result.IsVoid);
            Assert.False(result.IsNullable);
            Assert.True(result.Type == typeof(byte));
            Assert.False(result.IsClass);
            Assert.True(result.IsValid);
            Assert.False(result.IsByteArray);

            //not valid
            result = typeof(Task<List<List<int>>>).ToTypeWrapper();
            Assert.False(result.IsValid);

            result = typeof(Task<Task>).ToTypeWrapper();
            Assert.False(result.IsValid);

            result = typeof(object).ToTypeWrapper();
            Assert.False(result.IsValid);

            result = typeof(StringBuilder).ToTypeWrapper();
            Assert.False(result.IsValid);
        }


    }
}