using System;
using GRpcProtocolGenerator.Models.MetaData;

namespace GRpcProtocolGenerator.Renders
{
    public class RenderHelper
    {
        #region MapTo

        public static string MapTo(string name, string type)
        {
            return $"{name}.Adapt<{type}>()";
        }

        public static string MapToList(string name, string type)
        {
            return $"{name}.Adapt<List<{type}>>()";
        }

        public static string MapTo(bool isNullable, string name, string type)
        {
            return MapTo(isNullable ? $"{name}?" : name, type);
        }

        public static string MapToList(bool isNullable, string name, string type)
        {
            return MapToList(isNullable ? $"{name}?" : name, type);
        }

        /// <summary>
        /// 类、泛型类
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeWrapper"></param>
        /// <returns></returns>
        public static string MapTo(string name, Type typeWrapper)
        {
            if (typeWrapper.IsGenericType == false)
            {
                return MapTo(true, name, typeWrapper.FullName);
            }

            //泛型处理
            var type = typeWrapper.GetGenericTypeDefinition().FullName?.Replace($"`{typeWrapper.GetGenericArguments().Length}", "") + "<";
            foreach (var argument in typeWrapper.GetGenericArguments())
            {
                type += argument.FullName + ", ";
            }

            type = type.Trim().TrimEnd(',') + ">";

            return MapTo(true, name, type);
        }

        #endregion
        
        #region Method

        /// <summary>
        /// 方法定义的返回值
        /// </summary>
        /// <param name="isWrapper"></param>
        /// <param name="isOutParamEmpty"></param>
        /// <param name="type"></param>
        /// <param name="emptyType"></param>
        /// <returns></returns>
        public static string BuildMethodReturnType(bool isWrapper, bool isOutParamEmpty, string type, string emptyType)
        {
            if (isWrapper)
                return $"async Task<{type}>";

            return isOutParamEmpty
                ? $"async Task<{emptyType}>"
                : $"async Task<{type}>";
        } 

        #endregion

        #region 输入

        /// <summary>
        /// 创建输入参数想
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static string BuildInputItem(PropertyMetaData prop, string paramName)
        {
            return BuildInputItem(paramName, prop.Name, prop.TypeWrapper.Type, prop.TypeWrapper.IsArray, prop.TypeWrapper.IsNullable);
        }

        public static string BuildInputItem(string name, string paramName, Type type, bool isArray, bool isNullable)
        {
            var par = $"{name}.{paramName.ToFirstUpString()}";
            var typeWrapper = type.ToTypeWrapper();
            if (isArray)
            {
                if (typeWrapper.IsClass)
                    return MapToList(true, par, typeWrapper.Type.FullName);

                if (typeWrapper.IsEnum)
                    return MapToList(isNullable, par, typeWrapper.Type.FullName);

                return isNullable ? $"{par}?.ToList()" : $"{par}.ToList()";
            }

            if (typeWrapper.Type.IsCancellationToken())
                return BuildCancellationTokenInput();

            if (typeWrapper.IsClass)
                return MapTo(par, typeWrapper.Type);

            if (typeWrapper.IsEnum)
                return MapTo(isNullable, par, typeWrapper.Type.FullName);

            return par;
        }

        /// <summary>
        /// 创建 CancellationToken 输入参数
        /// </summary>
        /// <returns></returns>
        public static string BuildCancellationTokenInput()
        {
            return "context.CancellationToken";
        }

        #endregion
        
        #region 输出

        /// <summary>
        /// 方法返回值 空
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isWrapper"></param>
        /// <param name="successCode"></param>
        /// <returns></returns>
        public static string BuildEmptyReturn(string name, bool isWrapper, int successCode)
        {
            return isWrapper ? $"return new {name}" + " { Code = " + successCode + " };" : "return new Empty();";
        }

        /// <summary>
        /// 方法返回值 类
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isArray"></param>
        /// <param name="isWrapper"></param>
        /// <param name="successCode"></param>
        /// <returns></returns>
        public static string BuildClassReturn(string name, bool isArray, bool isWrapper, int successCode)
        {
            var body = isArray ? MapToList("data", name) : MapTo("data", name);
            return isWrapper ? $"return new {name}" + " { Code = " + successCode + ", Data = " + body + " };" : $"return {body};";
        }

        /// <summary>
        ///  返回值，构建类参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isArray"></param>
        /// <param name="gRpcType"></param>
        /// <returns></returns>
        public static string BuildClassItemReturn(string name, bool isArray, string gRpcType)
        {
            return isArray
                ? $"{name} = " + "{" + $" {MapToList("data", gRpcType)} " + "}"
                : $"{name} = " + $"{MapTo("data", gRpcType)}";
        }

        /// <summary>
        /// 返回值，构建普通参数（除枚举和类以外）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isArray"></param>
        /// <returns></returns>
        public static string BuildSampleItemReturn(string name, bool isArray)
        {
            return isArray ? $"{name} = " + "{" + " data.ToList() " + "}" : $"{name} = data";
        }

        /// <summary>
        ///  返回值，构建枚举参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isArray"></param>
        /// <param name="isNullable"></param>
        /// <returns></returns>
        public static string BuildEnumItemReturn(string name, bool isArray, bool isNullable)
        {
            return isArray
                ? $"{name} = " + "{" + $" {MapToList("data", isNullable ? "int?" : "int")} " + "}"
                : $"{name} = " + (isNullable ? "(int?)data" : "(int)data");
        }

        #endregion

        #region proto message

        public static string BuildMessageItem(string name, Type type, bool isArray, bool isNullable, int index, string desc)
        {
            if (type.IsCancellationToken())
                return null;

            var wrapper = type.ToTypeWrapper();

            var typeString = wrapper.IsClass || wrapper.IsStruct ? wrapper.Type.GetGenericClassName().FormatMessageName() : type.ToProtobufString(isNullable);

            return $"{(isArray ? "repeated " : "")}{typeString} {name} = {index};" + (string.IsNullOrWhiteSpace(desc) ? null : $" // {desc}");
        }

        #endregion
    }
}