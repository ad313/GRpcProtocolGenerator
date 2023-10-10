using System;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders.Protocol;
using GRpcProtocolGenerator.Types;

namespace GRpcProtocolGenerator.Renders
{
    public partial class BuilderPart
    {
        /// <summary>
        /// 构建控制器参数
        /// </summary>
        /// <param name="item"></param>
        /// <param name="inputParamName"></param>
        /// <returns></returns>
        public string GetControllerInParamString(ProtoServiceItem item, string inputParamName = "request")
        {
            if (item.MethodMetaData.InParamMetaDataListFilter().Count == 0)
                return null;
            
            var inParamString = "";
            foreach (var prop in item.MethodMetaData.InParamMetaDataListFilter())
            {
                inParamString += BuilderPart.BuildControllerInputItem(prop, inputParamName) + ", ";
            }

            return inParamString.Trim().TrimEnd(',');
        }

        /// <summary>
        /// 创建输入参数想
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        public static string BuildControllerInputItem(PropertyMetaData prop, string paramName)
        {
            return BuildControllerInputItem(paramName, prop.Name, prop.TypeWrapper.Type, prop.TypeWrapper.IsArray, prop.TypeWrapper.IsNullable);
        }

        public static string BuildControllerInputItem(string name, string paramName, Type type, bool isArray, bool isNullable)
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
    }
}