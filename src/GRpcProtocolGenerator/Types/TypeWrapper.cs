using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GRpcProtocolGenerator.Types
{
    /// <summary>
    /// 类型包装器
    /// </summary>
    public class TypeWrapper
    {
        /// <summary>
        /// 处理前的原始类型
        /// </summary>
        public Type Source { get; private set; }
        /// <summary>
        /// 去除各种包装后的真实类型
        /// </summary>
        public Type Type { get; private set; }
        /// <summary>
        /// 是否是空
        /// </summary>
        public bool IsVoid { get; private set; }
        /// <summary>
        /// 是否是字节数组，直接数组特殊处理
        /// </summary>
        public bool IsByteArray { get; private set; }
        /// <summary>
        /// 是否是 Tuple
        /// </summary>
        public bool IsTuple { get; private set; }
        /// <summary>
        /// 是否是字典
        /// </summary>
        public bool IsDictionary { get; private set; }
        /// <summary>
        /// 是否是枚举
        /// </summary>
        public bool IsEnum { get; private set; }
        /// <summary>
        /// 是否是类
        /// </summary>
        public bool IsClass { get; private set; }
        ///// <summary>
        ///// 是否是泛型类
        ///// </summary>
        //public bool IsGenericClass { get; set; }
        /// <summary>
        /// 是否是结构体
        /// </summary>
        public bool IsStruct { get; private set; }
        /// <summary>
        /// 是否是数组
        /// </summary>
        public bool IsArray { get; private set; }
        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool IsNullable { get; private set; }
        /// <summary>
        /// 是否是 Task
        /// </summary>
        public bool IsTask { get; private set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; private set; }
        /// <summary>
        /// 当无效时，具体的错误消息
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// 子类型
        /// </summary>
        public TypeWrapper Child { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="source"></param>
        public TypeWrapper(Type source)
        {
            IsValid = true;
            Source = source;

            IsVoid = source == typeof(void);
            if (IsVoid)
                return;

            //if (source == typeof(object))
            //{
            //    SetValid("不支持 object 类型");
            //    return;
            //}

            if (source == typeof(StringBuilder))
            {
                SetValid("不支持 StringBuilder 类型");
                return;
            }

            //处理 Task
            IsTask = source.IsTask();
            if (IsTask)
            {
                // Task 获取实际类型
                source = source.IsGenericType ? source.GetGenericArguments()[0] : null;
            }

            if (source == null)
            {
                IsVoid = true;
                return;
            }

            if (source.IsTask())
            {
                SetValid("不支持 Task 嵌套 Task");
                return;
            }

            Type = source;

            //可空
            var nullable = Type.IsNullable();
            if (nullable.IsNullable)
            {
                IsNullable = true;
                Type = nullable.Type;
            }

            IsByteArray = Type == typeof(byte[]) || Type == typeof(sbyte[]);
            if (IsByteArray)
                return;

            IsTuple = Type.IsTuple();
            if (IsTuple)
                return;

            //字典
            IsDictionary = Type.IsDictionary();
            if (IsDictionary)
                return;

            //枚举
            IsEnum = Type.IsEnum();
            if (IsEnum)
                return;

            //结构体
            IsStruct = Type.IsStruct();
            if (IsStruct)
                return;

            // 数组
            IsArray = Type.IsArray();
            if (IsArray)
            {
                // 特殊处理 xxx[] 类型数组
                if (Type.FullName?.EndsWith("[]") == true)
                {
                    Type = Type.GetType(Type.FullName.Replace("[]", ""));
                }
                else
                {
                    Type = Type.GetGenericType();
                }

                Child = new TypeWrapper(Type);
                SetChildData(Child);
            }

            if (Type == null
                || Type == typeof(object)
                || Type == typeof(ValueType)
                || Type == typeof(string)
                || Type.IsValueType()
                || !Type.GetTypeInfo().IsClass
                || Type.IsValueType
                || IsByteArray
                || IsDictionary
                || IsTuple
                || IsEnum
                || IsStruct
               )
                IsClass = false;
            else
                IsClass = true;
        }

        private void SetChildData(TypeWrapper child)
        {
            if (!child.IsValid)
            {
                SetValid(child.ErrorMessage);
                return;
            }

            if (child.IsByteArray)
            {
                IsByteArray = true;
                return;
            }

            if (child.IsTuple)
            {
                IsTuple = true;
                return;
            }

            if (child.IsDictionary)
            {
                IsDictionary = true;
                return;
            }

            if (child.IsEnum)
            {
                IsEnum = true;
                Type = child.Type;
                IsNullable = child.IsNullable;
                return;
            }

            if (child.IsStruct)
            {
                IsStruct = true;
                IsNullable = child.IsNullable;
                return;
            }

            if (child.IsClass)
            {
                IsClass = true;
                return;
            }

            if (child.IsArray)
            {
                if (IsArray)
                {
                    SetValid("不支持集合嵌套集合");
                    return;
                }

                IsArray = true;
                return;
            }

            if (child.IsNullable)
            {
                IsNullable = true;
                Type = child.Type;
                return;
            }

            if (child.IsTask)
            {
                if (IsTask)
                {
                    SetValid("不支持 Task 嵌套 Task");
                    return;
                }

                IsTask = true;
                return;
            }
        }

        private void SetValid(string errorMessage)
        {
            IsValid = false;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// 设置是否可为空
        /// </summary>
        /// <param name="nullable"></param>
        public void SetNullable(bool nullable)
        {
            IsNullable = nullable;
        }

        /// <summary>
        /// 传入类型，判断并设置是否可为空，比如判断是否有 RequiredAttribute 特性，有就不能为空
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public bool SetNullable(List<Type> types)
        {
            if (Type == typeof(string))
                return IsNullable;

            //byte[] 默认可为空
            if (IsByteArray)
                IsNullable = true;

            //Required 不为空
            if (types?.Any(d => d == typeof(RequiredAttribute)) == true)
                IsNullable = false;

            //有些读取不到 NullableAttribute
            //if (types?.Any(d => d.FullName.Equals("System.Runtime.CompilerServices.NullableAttribute", StringComparison.OrdinalIgnoreCase)) == true)
            //    IsNullable = true;

            return IsNullable;
        }

        /// <summary>
        /// 判断类型是否支持
        /// </summary>
        /// <returns></returns>
        public bool IsSupport()
        {
            if (!IsValid)
                return false;

            //不支持字典
            if (IsDictionary || Child?.IsDictionary == true)
                return false;

            //不支持 Tuple
            if (IsTuple || Child?.IsTuple == true)
                return false;

            return true;
        }


    }
}
