using GRpcProtocolGenerator.Types;
using Sample.Services.Models;
using System.Collections.Concurrent;

namespace GRpcProtocolGenerator.Test.types
{
    public class TypeTest
    {
        public static readonly Dictionary<string, Type> SampleTypeDictionary = new Dictionary<string, Type>
        {
            { "char", typeof(char) },
            { "string", typeof(string) },

            { "int", typeof(int) },
            { "int?", typeof(int?) },
            { "long", typeof(long) },
            { "short", typeof(short) },
            { "ushort", typeof(ushort) },
            { "uint", typeof(uint) },
            { "ulong", typeof(ulong) },

            { "float", typeof(float) },
            { "double", typeof(double) },
            { "decimal", typeof(decimal) },

            { "bool", typeof(bool) },
            { "Guid", typeof(Guid) },

            { "DateTime", typeof(DateTime) },
            { "DateTime?", typeof(DateTime?) },
            { "DateOnly", typeof(DateOnly) },
            { "DateTimeOffset", typeof(DateTimeOffset) },

            { "byte", typeof(byte) },
            { "sbyte", typeof(sbyte) },

            { "object", typeof(object) },
            { "void", typeof(void) },

            { "SampleEnum", typeof(SampleEnum) },
            { "SampleEnum?", typeof(SampleEnum?) },
            { "SampleClass", typeof(SampleClass) },
            { "SampleStruct", typeof(SampleStruct) },
            { "SampleStruct?", typeof(SampleStruct?) },
        };

        public static readonly Dictionary<string, Type> DictionaryDictionary = new Dictionary<string, Type>
        {
            { "Dictionary<int,string>", typeof(Dictionary<int, string>) },
            { "IDictionary<int,string>", typeof(IDictionary<int, string>) },
            { "IReadOnlyDictionary<int,string>", typeof(IReadOnlyDictionary<int, string>) },
            { "ConcurrentDictionary<int,string>", typeof(ConcurrentDictionary<int, string>) },
            { "SortedDictionary<int,string>", typeof(SortedDictionary<int, string>) },
        };

        public static readonly Dictionary<string, Type> ArrayDictionary = new Dictionary<string, Type>
        {
            { "byte[]", typeof(byte[]) },
            { "sbyte[]", typeof(sbyte[]) },
            { "List<SampleEnum>", typeof(List<SampleEnum>) },
            { "IList<SampleEnum>", typeof(IList<SampleEnum>) },
            { "IReadOnlyList<SampleEnum>", typeof(IReadOnlyList<SampleEnum>) },
            { "ICollection<SampleEnum>", typeof(ICollection<SampleEnum>) },
            { "IReadOnlyCollection<SampleEnum>", typeof(IReadOnlyCollection<SampleEnum>) },
            { "IEnumerable<SampleEnum>", typeof(IEnumerable<SampleEnum>) },
            { "ReadOnlyMemory<SampleEnum>", typeof(ReadOnlyMemory<SampleEnum>) },
            { "ReadOnlySpan<SampleEnum>", typeof(ReadOnlySpan<SampleEnum>) },
        };

        public static readonly Dictionary<string, Type> TaskDictionary = new Dictionary<string, Type>
        {
            { "Task", typeof(Task) },
            { "Task<int>", typeof(Task<int>) },
            { "ValueTask", typeof(ValueTask) },
            { "ValueTask<int>", typeof(ValueTask<int>) },
        };

        public static readonly Dictionary<string, Type> TupleDictionary = new Dictionary<string, Type>
        {
            { "Tuple<int, int>", typeof(Tuple<int, int>) },
            { "ValueTuple<int, int>", typeof(ValueTuple<int, int>) },
            { "ValueTuple<int, SampleClass>", typeof(ValueTuple<int, SampleClass>) },
        };

        [Fact]
        public void IsValueTypeTest()
        {
            foreach (var keyValuePair in SampleTypeDictionary)
            {
                var valueType1 = keyValuePair.Value.IsValueType;
                var valueType2 = keyValuePair.Value.IsValueType();

                if (keyValuePair.Key == "string" || keyValuePair.Key == "SampleClass" || keyValuePair.Key == "object")
                {
                    Assert.False(valueType1);
                    Assert.False(valueType2);
                }
                else
                {
                    Assert.True(valueType1);
                    Assert.True(valueType2);
                }
            }

            foreach (var keyValuePair in DictionaryDictionary)
            {
                var valueType1 = keyValuePair.Value.IsValueType;
                var valueType2 = keyValuePair.Value.IsValueType();

                Assert.False(valueType1);
                Assert.False(valueType2);
            }

            foreach (var keyValuePair in ArrayDictionary)
            {
                var valueType1 = keyValuePair.Value.IsValueType;
                var valueType2 = keyValuePair.Value.IsValueType();

                if (keyValuePair.Key == "ReadOnlyMemory<SampleEnum>" || keyValuePair.Key == "ReadOnlySpan<SampleEnum>")
                {
                    Assert.True(valueType1);
                    Assert.True(valueType2);
                }
                else
                {
                    Assert.False(valueType1);
                    Assert.False(valueType2);
                }
            }

            foreach (var keyValuePair in TupleDictionary)
            {
                var valueType1 = keyValuePair.Value.IsValueType;
                var valueType2 = keyValuePair.Value.IsValueType();

                if (keyValuePair.Key == "ValueTuple<int, int>" || keyValuePair.Key == "ValueTuple<int, SampleClass>")
                {
                    Assert.True(valueType1);
                    Assert.True(valueType2);
                }
                else
                {
                    Assert.False(valueType1);
                    Assert.False(valueType2);
                }
            }
        }

        [Fact]
        public void IsArrayTest()
        {
            foreach (var keyValuePair in ArrayDictionary)
            {
                var isArray = keyValuePair.Value.IsArray();

                if (keyValuePair.Key == "ReadOnlyMemory<SampleEnum>" || keyValuePair.Key == "ReadOnlySpan<SampleEnum>")
                {
                    Assert.False(isArray);
                }
                else
                {
                    Assert.True(isArray);
                }
            }

            foreach (var keyValuePair in SampleTypeDictionary)
            {
                var isArray = keyValuePair.Value.IsArray();
                Assert.False(isArray);
            }

            foreach (var keyValuePair in DictionaryDictionary)
            {
                var isArray = keyValuePair.Value.IsArray();
                Assert.False(isArray);
            }

            foreach (var keyValuePair in TupleDictionary)
            {
                var isArray = keyValuePair.Value.IsArray();
                Assert.False(isArray);
            }
        }

        [Fact]
        public void IsDictionaryTest()
        {
            foreach (var keyValuePair in DictionaryDictionary)
            {
                var isDictionary = keyValuePair.Value.IsDictionary();
                Assert.True(isDictionary);
            }

            foreach (var keyValuePair in SampleTypeDictionary)
            {
                var isDictionary = keyValuePair.Value.IsDictionary();
                Assert.False(isDictionary);
            }

            foreach (var keyValuePair in ArrayDictionary)
            {
                var isDictionary = keyValuePair.Value.IsDictionary();
                Assert.False(isDictionary);
            }

            foreach (var keyValuePair in TupleDictionary)
            {
                var isDictionary = keyValuePair.Value.IsDictionary();
                Assert.False(isDictionary);
            }
        }

        [Fact]
        public void IsEnumTest()
        {
            var result = typeof(Task<List<SampleEnum?>>).ToTypeWrapper();
            Assert.True(result.IsTask);
            Assert.True(result.IsArray);
            Assert.True(result.IsNullable);
            Assert.True(result.IsEnum);
            Assert.True(result.Type == typeof(SampleEnum));

            foreach (var keyValuePair in SampleTypeDictionary)
            {
                var isEnum = keyValuePair.Value.IsEnum();

                if (keyValuePair.Key == "SampleEnum")
                {
                    Assert.True(isEnum);
                }
                else
                {
                    Assert.False(isEnum);
                }
            }

            foreach (var keyValuePair in DictionaryDictionary)
            {
                var isEnum = keyValuePair.Value.IsEnum();
                Assert.False(isEnum);
            }

            foreach (var keyValuePair in ArrayDictionary)
            {
                var isEnum = keyValuePair.Value.IsEnum();
                Assert.False(isEnum);
            }

            foreach (var keyValuePair in TupleDictionary)
            {
                var isEnum = keyValuePair.Value.IsEnum();
                Assert.False(isEnum);
            }
        }

        [Fact]
        public void IsStructTest()
        {
            foreach (var keyValuePair in SampleTypeDictionary)
            {
                var isStruct = keyValuePair.Value.IsStruct();

                if (keyValuePair.Key == "SampleStruct" || keyValuePair.Key == "void")
                {
                    Assert.True(isStruct);
                }
                else
                {
                    Assert.False(isStruct);
                }
            }

            foreach (var keyValuePair in DictionaryDictionary)
            {
                var isStruct = keyValuePair.Value.IsStruct();
                Assert.False(isStruct);
            }

            foreach (var keyValuePair in ArrayDictionary)
            {
                var isStruct = keyValuePair.Value.IsStruct();
                Assert.False(isStruct);
            }

            foreach (var keyValuePair in TupleDictionary)
            {
                var isStruct = keyValuePair.Value.IsStruct();
                Assert.False(isStruct);
            }
        }

        [Fact]
        public void IsTaskTest()
        {
            foreach (var keyValuePair in TaskDictionary)
            {
                var isTask = keyValuePair.Value.IsTask();
                Assert.True(isTask);
            }

            foreach (var keyValuePair in SampleTypeDictionary)
            {
                var isTask = keyValuePair.Value.IsTask();
                Assert.False(isTask);
            }

            foreach (var keyValuePair in DictionaryDictionary)
            {
                var isTask = keyValuePair.Value.IsTask();
                Assert.False(isTask);
            }

            foreach (var keyValuePair in ArrayDictionary)
            {
                var isTask = keyValuePair.Value.IsTask();
                Assert.False(isTask);
            }

            foreach (var keyValuePair in TupleDictionary)
            {
                var isTask = keyValuePair.Value.IsTask();
                Assert.False(isTask);
            }
        }

        [Fact]
        public void IsNullableTest()
        {
            foreach (var keyValuePair in SampleTypeDictionary)
            {
                var isNullable = keyValuePair.Value.IsNullable();
                if (keyValuePair.Key == "int?"
                    || keyValuePair.Key == "DateTime?"
                    || keyValuePair.Key == "SampleEnum?"
                    || keyValuePair.Key == "SampleStruct?")
                {
                    Assert.True(isNullable.IsNullable);
                }
                else
                {
                    Assert.False(isNullable.IsNullable);
                }
            }

            foreach (var keyValuePair in DictionaryDictionary)
            {
                var isNullable = keyValuePair.Value.IsNullable();
                Assert.False(isNullable.IsNullable);
            }

            foreach (var keyValuePair in ArrayDictionary)
            {
                var isNullable = keyValuePair.Value.IsNullable();
                Assert.False(isNullable.IsNullable);
            }

            foreach (var keyValuePair in TaskDictionary)
            {
                var isNullable = keyValuePair.Value.IsNullable();
                Assert.False(isNullable.IsNullable);
            }

            foreach (var keyValuePair in TupleDictionary)
            {
                var isNullable = keyValuePair.Value.IsNullable();
                Assert.False(isNullable.IsNullable);
            }
        }

        [Fact]
        public void IsTupleTest()
        {
            foreach (var keyValuePair in TupleDictionary)
            {
                var isTuple = keyValuePair.Value.IsTuple();
                Assert.True(isTuple);
            }

            foreach (var keyValuePair in SampleTypeDictionary)
            {
                var isTuple = keyValuePair.Value.IsTuple();
                Assert.False(isTuple);
            }

            foreach (var keyValuePair in DictionaryDictionary)
            {
                var isTuple = keyValuePair.Value.IsTuple();
                Assert.False(isTuple);
            }

            foreach (var keyValuePair in ArrayDictionary)
            {
                var isTuple = keyValuePair.Value.IsTuple();
                Assert.False(isTuple);
            }

            foreach (var keyValuePair in TaskDictionary)
            {
                var isTuple = keyValuePair.Value.IsTuple();
                Assert.False(isTuple);
            }
        }
    }
}