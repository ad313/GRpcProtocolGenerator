using GRpcProtocolGenerator.Common.Attributes;
using GRpcProtocolGenerator.Models.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace GRpcProtocolGenerator.Types
{
    /// <summary>
    /// 类型处理扩展
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// 判断 CancellationToken
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCancellationToken(this Type type)
        {
            return type == typeof(CancellationToken);
        }

        /// <summary>
        /// 获取泛型类名称，a<b> => a_b
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetGenericClassName(this Type type)
        {
            if (type.IsGenericType == false)
                return type.Name;

            return $"{type.Name}_{string.Join("_", type.GetGenericArguments().Select(d => d.Name))}";
        }

        /// <summary>
        /// 是否有 GrpcGenerator 标记，生成gRpc
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGRpcGenerator(this Type type)
        {
            return type.CustomAttributes.Any(d => d.AttributeType == typeof(GRpcGeneratorAttribute)) || type.GetInterfaces()?.Any(t => t.IsGRpcGenerator()) == true;
        }

        /// <summary>
        /// 是否忽略gRpc生成
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGRpcIgnore(this Type type)
        {
            return type.CustomAttributes.Any(d => d.AttributeType == typeof(GRpcIgnoreAttribute)) || type.GetInterfaces()?.Any(t => t.IsGRpcIgnore()) == true;
        }

        /// <summary>
        /// 是否忽略gRpc生成
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns></returns>
        public static bool IsGRpcIgnore(this IEnumerable<CustomAttributeData> attrs)
        {
            return attrs.Any(d => d.AttributeType == typeof(GRpcIgnoreAttribute));
        }

        /// <summary>
        /// 是否是值类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValueType(this Type type)
        {
            return type.IsValueType;
        }

        /// <summary>
        /// 是否是数组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsArray(this Type type)
        {
            return type.IsDictionary() == false && type.IsArrayOriginal();
        }

        /// <summary>
        /// 是否是数组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsArrayOriginal(this Type type)
        {
            return type != null && type != typeof(string) && type.GetInterfaces().Any(d => d == typeof(IEnumerable));
        }

        /// <summary>
        /// 是否是字典
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDictionary(this Type type)
        {
            if (type == null)
                return false;

            if (type.GetInterfaces().Any(d => d == typeof(IDictionary)))
                return true;

            return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IDictionary<,>)
                                    || type.GetGenericTypeDefinition() == typeof(IReadOnlyDictionary<,>));
        }

        /// <summary>
        /// 是否是枚举
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnum(this Type type)
        {
            return type != null && (type.IsEnum || type.BaseType == typeof(Enum));
        }

        /// <summary>
        /// 是否是结构体
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsStruct(this Type type)
        {
            return !type.IsPrimitive && !type.IsEnum() && type.IsValueType && !type.IsGenericType
                   && type != typeof(DateTime)
                   && type != typeof(DateTimeOffset)
                   && type != typeof(DateOnly)
                   && type != typeof(Guid)
                   && type != typeof(decimal)
                   && type != typeof(float)
                   && type != typeof(double)
                   && type != typeof(int)
                   && type != typeof(long)
                   && type != typeof(uint)
                   && type != typeof(ulong)
                   && type != typeof(byte)
                ;
        }

        /// <summary>
        /// 是否是 Task 或 ValueTask
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTask(this Type type)
        {
            if (type == null)
                return false;

            return ((TypeInfo)type).ImplementedInterfaces.Any(d => d == typeof(IAsyncResult)) ||
                   ((TypeInfo)type).ImplementedInterfaces.Any(d =>
                       d == typeof(IEquatable<ValueTask>)) || type.FullName?.StartsWith("System.Threading.Tasks.ValueTask") == true;
        }

        /// <summary>
        /// 是否可为空
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static NullableTypeResult IsNullable(this Type type)
        {
            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    return new NullableTypeResult(type.GetGenericArguments()[0], true);
                }
                return new NullableTypeResult(type.UnderlyingSystemType, false);
            }
            return new NullableTypeResult(type, false);
        }

        /// <summary>
        /// 是否是 Tuple
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsTuple(this Type type)
        {
            return type != null && type.GetInterfaces().Any(d => d == typeof(ITuple));
        }

        /// <summary>
        /// 获取泛型第一个类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetGenericType(this Type type)
        {
            return type.GetGenericArguments().FirstOrDefault();
        }

        /// <summary>
        /// 类型转化包装器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeWrapper ToTypeWrapper(this Type type)
        {
            return new TypeWrapper(type);
        }

        public static string ToProtobufString(this Type type, bool isNullable)
        {
            var result = Config.ConfigInstance.Proto.CSharpTypeToProtobufString?.Invoke(type, isNullable);
            if (!string.IsNullOrWhiteSpace(result))
                return result;

            return TypeConvert.Convert(type, isNullable);
        }
    }

    /// <summary>
    /// 可为空数据模型
    /// </summary>
    public class NullableTypeResult
    {
        /// <summary>
        /// 真实类型
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool IsNullable { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isNullable"></param>
        public NullableTypeResult(Type type, bool isNullable)
        {
            Type = type;
            IsNullable = isNullable;
        }
    }
}