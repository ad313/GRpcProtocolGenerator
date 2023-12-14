using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GRpcProtocolGenerator.Common.Attributes;
using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;

namespace GRpcProtocolGenerator.Renders.Protocol
{
    public class ProtocolService
    {
        /// <summary>
        /// Service名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 方法集合
        /// </summary>
        public List<ProtoServiceItem> Items { get; set; }

        /// <summary>
        /// 接口
        /// </summary>
        public InterfaceMetaData InterfaceMetaData { get; set; }

        public ProtocolService(string name, List<ProtoServiceItem> items, InterfaceMetaData interfaceMetaData)
        {
            Name = name;
            Items = items.OrderBy(d => d.Index).ToList();
            InterfaceMetaData = interfaceMetaData;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"//{Config.ConfigInstance.Proto.PropertyDescriptionFunc(InterfaceMetaData)}");
            sb.AppendLine($"service {InterfaceMetaData.FormatServiceName()} " + "{");

            foreach (var item in Items)
            {
                sb.AppendLine("\t" + item.ToString());
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class ProtoServiceItem
    {
        public int Index { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 传入参数
        /// </summary>
        public ProtocolMessage InParam { get; set; }

        /// <summary>
        /// 传出参数
        /// </summary>
        public ProtocolMessage OutParam { get; set; }

        public MethodMetaData MethodMetaData { get; set; }

        public ProtocolHttpOption HttpOption { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($"\t// {Config.ConfigInstance.Proto.PropertyDescriptionFunc(MethodMetaData)}");

            HttpOption = GetHttpOption(HttpMethod.Get, typeof(HttpGetAttribute)) ??
                         GetHttpOption(HttpMethod.Post, typeof(HttpPostAttribute)) ??
                         GetHttpOption(HttpMethod.Put, typeof(HttpPutAttribute)) ??
                         GetHttpOption(HttpMethod.Delete, typeof(HttpDeleteAttribute)) ??
                         GetHttpOption(HttpMethod.Patch, typeof(HttpPatchAttribute));

            ////没标记 http，不生成
            //if (HttpOption == null)
            //{
            //    //return string.Empty;
            //    HttpOption = GetDefaultHttpOption();
            //}

            if (Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding)
            {
                sb.Append($"\trpc {Name}({InParam.GetGRpcName()}) returns({OutParam.GetGRpcName()})");

                //没标记 http，不生成
                if (HttpOption != null)
                    sb.Append(HttpOption.ToString());
            }
            else
            {
                sb.Append($"\trpc {Name}({InParam.GetGRpcName()}) returns({OutParam.GetGRpcName()});");
            }

            return sb.ToString();
        }

        private ProtocolHttpOption GetHttpOption(HttpMethod httpMethod, Type type)
        {
            var httpAttribute = MethodMetaData.AttributeMetaDataList.FirstOrDefault(d => d.Type == type);
            if (httpAttribute != null)
            {
                //获取路由
                var route = httpAttribute.ConstructorDictionary.FirstOrDefault().Value ??
                            httpAttribute.NamedDictionary.FirstOrDefault().Value ?? "";

                return new ProtocolHttpOption(httpMethod, route.ToString(), MethodMetaData.InterfaceMetaData);
            }

            return null;
        }

        private ProtocolHttpOption GetDefaultHttpOption()
        {
            return new ProtocolHttpOption(HttpMethod.Get, MethodMetaData.FormatServiceMethodName(), MethodMetaData.InterfaceMetaData);
        }
    }
}
