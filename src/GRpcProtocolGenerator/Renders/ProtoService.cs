using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GRpcProtocolGenerator.Common.Attributes;
using GRpcProtocolGenerator.Models.MetaData;

namespace GRpcProtocolGenerator.Renders
{
    public class ProtoService
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

        public ProtoService(string name, List<ProtoServiceItem> items, InterfaceMetaData interfaceMetaData)
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
            sb.AppendLine($"//{CodeRender.Config.Proto.PropertyDescriptionFunc(InterfaceMetaData)} {InterfaceMetaData.FullName}");
            sb.AppendLine($"service {InterfaceMetaData.FormatServiceName()} " + "{");
            
            foreach (var item in Items)
            {
                sb.AppendLine("\t"+item.ToString());
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
        public ProtoMessage InParam { get; set; }

        /// <summary>
        /// 传出参数
        /// </summary>
        public ProtoMessage OutParam { get; set; }

        public MethodMetaData MethodMetaData { get; set; }
        
        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($"\t// {CodeRender.Config.Proto.PropertyDescriptionFunc(MethodMetaData)}");

            if (CodeRender.Config.JsonTranscoding.UseJsonTranscoding)
            {
                sb.Append($"\trpc {Name}({InParam.GetGRpcName()}) returns({OutParam.GetGRpcName()})");

                var httpOption = GetHttpOption(HttpMethod.Get, typeof(HttpGetAttribute)) ??
                                 GetHttpOption(HttpMethod.Post, typeof(HttpPostAttribute)) ??
                                 GetHttpOption(HttpMethod.Put, typeof(HttpPutAttribute)) ??
                                 GetHttpOption(HttpMethod.Delete, typeof(HttpDeleteAttribute)) ??
                                 GetHttpOption(HttpMethod.Patch, typeof(HttpPatchAttribute));

                if (httpOption == null)
                {
                    httpOption = GetDefaultHttpOption();
                }

                sb.Append(httpOption.ToString());
            }
            else
            {
                sb.Append($"\trpc {Name}({InParam.GetGRpcName()}) returns({OutParam.GetGRpcName()});");
            }
           
            return sb.ToString();
        }

        private ProtoHttpOption GetHttpOption(HttpMethod httpMethod, Type type)
        {
            var httpAttribute = MethodMetaData.AttributeMetaDataList.FirstOrDefault(d => d.Type == type);
            if (httpAttribute != null)
            {
                //获取路由
                var route = httpAttribute.ConstructorDictionary.FirstOrDefault().Value ??
                            httpAttribute.NamedDictionary.FirstOrDefault().Value ?? "";
                
                return new ProtoHttpOption(httpMethod, route.ToString(), MethodMetaData.InterfaceMetaData);
            }

            return null;
        }

        private ProtoHttpOption GetDefaultHttpOption()
        {
            return new ProtoHttpOption(HttpMethod.Get, MethodMetaData.FormatServiceMethodName(), MethodMetaData.InterfaceMetaData);
        }
    }
}
