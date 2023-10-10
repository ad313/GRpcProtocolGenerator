using GRpcProtocolGenerator.Models.Configs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GRpcProtocolGenerator.Renders.Protocol
{
    public class ControllerImpl
    {
        public ProtocolService ProtocolService { get; }

        public ControllerImpl(ProtocolService protocolService)
        {
            ProtocolService = protocolService;

            Items = protocolService.Items.Select(d => new ControllerImplItem(d)).ToList();

            Name = protocolService.InterfaceMetaData.Name.TrimStart('I');
            if (Name.Length > "Service".Length)
            {
                if (Name.Substring(Name.Length - "Service".Length) == "Service")
                {
                    Name = Name.Substring(0, Name.Length - "Service".Length) ;
                }
            }

            Name += "Controller";

            ClientInterface = $"I{ProtocolService.Name.FormatGRpcClientName()}";
        }

        public List<ControllerImplItem> Items { get;  }

        public string Name { get;  }

        public string ClientInterface { get; set; }

        
    }

    public class ControllerImplItem: ProtoServiceItem
    {
        public ProtoServiceItem Item { get; }

        public ControllerImplItem(ProtoServiceItem protoServiceItem)
        {
            Item = protoServiceItem;

            HttpType = Item.HttpOption.HttpMethod;
            IsPost = Item.HttpOption.HttpMethod == HttpMethod.Post || Item.HttpOption.HttpMethod == HttpMethod.Put ||
                     Item.HttpOption.HttpMethod == HttpMethod.Patch;

            switch (HttpType)
            {
                case HttpMethod.Get:
                    Http = string.IsNullOrWhiteSpace(Item.HttpOption.Route) ? "[HttpGet]" : $"[HttpGet(\"{Item.HttpOption.Route}\")]";
                    break;
                case HttpMethod.Post:
                    Http = string.IsNullOrWhiteSpace(Item.HttpOption.Route) ? "[HttpPost]" : $"[HttpPost(\"{Item.HttpOption.Route}\")]";
                    break;
                case HttpMethod.Put:
                    Http = string.IsNullOrWhiteSpace(Item.HttpOption.Route) ? "[HttpPut]" : $"[HttpPut(\"{Item.HttpOption.Route}\")]";
                    break;
                case HttpMethod.Delete:
                    Http = string.IsNullOrWhiteSpace(Item.HttpOption.Route) ? "[HttpDelete]" : $"[HttpDelete(\"{Item.HttpOption.Route}\")]";
                    break;
                case HttpMethod.Patch:
                    Http = string.IsNullOrWhiteSpace(Item.HttpOption.Route) ? "[HttpPatch]" : $"[HttpPatch(\"{Item.HttpOption.Route}\")]";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ParamFlag = IsPost ? "[FromBody]" : "[FromQuery]";

            Description = FilterFunctions.GetMethodDescription(Item);

            //输入参数
            if (Item.MethodMetaData.InParamMetaDataListFilter().Count > 0)
            {
                InputType =$"{ParamFlag} {FilterFunctions.GetMethodInString(Item)} request, ";
                ClientInputType = "request";
            }
            else
            {
                ClientInputType = "new Google.Protobuf.WellKnownTypes.Empty()";
            }

            ReturnType = FilterFunctions.GetMethodReturnType(Config.ConfigInstance, Item);

            Name = Item.MethodMetaData.Name;
            ClientMethodName = Item.Name;
        }

        public string Http { get; set; }

        public HttpMethod HttpType { get; set; }

        public bool IsPost { get; set; }

        public string ParamFlag { get; set; }

        public string ReturnType { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 输入参数
        /// </summary>
        public string InputType { get; set; }

        /// <summary>
        /// 客户端输入参数
        /// </summary>
        public string ClientInputType { get; set; }

        public string Name { get; set; }


        public string ClientMethodName { get; set; }

        
    }
}
