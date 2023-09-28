using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace GRpcProtocolGenerator.Models.MetaData
{
    /// <summary>
    /// 方法元数据
    /// </summary>
    public class MethodMetaData : CommentMetaData
    {
        public int Index { get; set; }

        public InterfaceMetaData InterfaceMetaData { get; private set; }

        public MethodInfo MethodInfo { get; }

        public string Key { get; private set; }

        /// <summary>
        /// 标记 传入参数是否有 CancellationToken 参数
        /// </summary>
        public bool HasCancellationToken { get; private set; }

        /// <summary>
        /// 传入参数集合
        /// </summary>
        public List<PropertyMetaData> InParamMetaDataList { get; private set; }

        /// <summary>
        /// 传出参数集合
        /// </summary>
        public List<PropertyMetaData> OutParamMetaDataList { get; private set; }

        public bool IsTask { get; private set; }

        public string GrpcMethodName => this.FormatServiceMethodName();

        public MethodMetaData(string @namespace, string name, MethodInfo methodInfo, List<AttributeMetaData> attributeMetaDataList, List<PropertyMetaData> inParamMetaDataList, List<PropertyMetaData> outParamMetaDataList)
            : base(@namespace, name, attributeMetaDataList)
        {
            MethodInfo = methodInfo;
            InParamMetaDataList = inParamMetaDataList ?? new List<PropertyMetaData>();
            OutParamMetaDataList = outParamMetaDataList ?? new List<PropertyMetaData>();
            IsTask = methodInfo.ReturnType.IsTask();

            //标记 传入参数是否有 CancellationToken 参数
            HasCancellationToken = InParamMetaDataList.Exists(d => d.TypeWrapper.Type == typeof(CancellationToken));

            ////过滤 CancellationToken
            //InParamMetaDataList = InParamMetaDataList.Where(d => d.TypeWrapper.Type != typeof(CancellationToken)).ToList();

            SetKey();
        }

        /// <summary>
        /// 过滤得到有效参数
        /// </summary>
        /// <returns></returns>
        public List<PropertyMetaData> InParamMetaDataListFilter() => InParamMetaDataList.Where(d => d.TypeWrapper.Type != typeof(CancellationToken)).ToList();

        private void SetKey()
        {
            Key = !InParamMetaDataList.Any() ? FullName : $"{FullName}_{string.Join("_", InParamMetaDataList.Select(d => d?.TypeWrapper.IsArray + "_" + d?.TypeWrapper.IsNullable + "_" + d?.FullName))}";
        }

        public void SetInterfaceMetaData(InterfaceMetaData interfaceMetaData)
        {
            InterfaceMetaData = interfaceMetaData;
        }

        public string GetKey()
        {
            return $"{string.Join(".", InParamMetaDataList.Select(d => d.FullName))}_" +
                   Name +
                   $"{string.Join(".", OutParamMetaDataList.Select(d => d.FullName))}";
        }
    }
}