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

            //如果以 Service 结尾，则去掉 Service
            var flag = "Service";
            Name = protocolService.InterfaceMetaData.Name.TrimStart('I');
            if (Name.Length > flag.Length)
            {
                if (Name.Substring(Name.Length - flag.Length) == flag)
                {
                    Name = Name.Substring(0, Name.Length - flag.Length);
                }
            }

            Name += "Controller";

            ClientInterface = $"I{ProtocolService.Name.FormatGRpcClientName()}";
        }

        public List<ControllerImplItem> Items { get; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// GRpc 接口名称
        /// </summary>
        public string ClientInterface { get; set; }
    }

    public class ControllerImplItem
    {
        public ProtoServiceItem Item { get; }
        
        public ControllerImplItem(ProtoServiceItem protoServiceItem)
        {
            Item = protoServiceItem;

            switch (Item.HttpOption.HttpMethod)
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

            //参数修饰符
            ParamFlag = Item.HttpOption.HttpMethod == HttpMethod.Post || Item.HttpOption.HttpMethod == HttpMethod.Put || Item.HttpOption.HttpMethod == HttpMethod.Patch
                ? "[FromBody]"
                : "[FromQuery]";

            //方法注释
            Description = FilterFunctions.GetMethodDescription(Item);

            //全部有效的输入参数集合
            //处理控制器方法输入参数
            var inParamList = Item.MethodMetaData.InParamMetaDataListFilter();
            if (inParamList.Count > 0)
            {
                InputType = "";
                foreach (var prop in inParamList)
                {
                    if (prop.ClassMetaData != null)
                    {
                        var gRpcTypeName = prop.ClassMetaData.Name.FormatMessageName();

                        InputType += prop.TypeWrapper.IsArray
                            ? $"{ParamFlag} List<{gRpcTypeName}> {prop.Name}, "
                            : $"{ParamFlag} {gRpcTypeName} {prop.Name}, ";
                    }
                    else
                    {
                        //判断枚举
                        var type = prop.EnumMetaData != null ? "System.Int32" : prop.TypeWrapper.Type.FullName;

                        //判断可为空
                        if (prop.TypeWrapper.IsNullable) type += "?";

                        //判断集合
                        if (prop.TypeWrapper.IsArray) type = $"List<{type}>";

                        //组装
                        InputType += $"{type} {prop.Name}, ";
                    }
                }
            }

            //gRpc 方法原始输入参数名称
            var modelName = FilterFunctions.GetMethodInString(Item);

            //特殊处理单个参数，且是 class
            if (inParamList.Count == 1 && inParamList.First().ClassMetaData != null)
            {
                InputType = $"{ParamFlag} {modelName} clientInput, ";
            }

            //处理 gRpc client 方法输入参数
            if (inParamList.Count > 0)
            {
                //特殊处理单个参数，且是 class
                if (inParamList.Count == 1 && inParamList.First().ClassMetaData != null)
                {
                    ClientInputType = "clientInput";
                }
                else
                {
                    ClientInputType = $"new {modelName}() " + "{ ";

                    foreach (var prop in inParamList)
                    {
                        ClientInputType += prop.Name.ToFirstUpString() +
                                           (prop.TypeWrapper.IsArray
                                               ? " = { " + prop.Name + " }, "
                                               : $" = {prop.Name}, ");
                    }

                    ClientInputType = ClientInputType.Trim().TrimEnd(',') + " }";
                }
            }
            else
            {
                ClientInputType = "new Google.Protobuf.WellKnownTypes.Empty()";
            }

            ReturnType = FilterFunctions.GetMethodReturnType(Config.ConfigInstance, Item);
        }
        
        /// <summary>
        /// 方法 http 类型，包含路由
        /// </summary>
        public string Http { get; set; }

        /// <summary>
        /// 控制器 Action 方法名称
        /// </summary>
        public string Name => Item.MethodMetaData.Name;

        /// <summary>
        /// 方法输入参数修饰符
        /// </summary>
        public string ParamFlag { get; set; }

        /// <summary>
        /// 方法返回参数强类型，用于swagger
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// 方法注释
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 方法输入参数
        /// </summary>
        public string InputType { get; set; }
        

        /// <summary>
        /// 对应的 GRpc 接口方法名称
        /// </summary>
        public string ClientMethodName => Item.Name;

        /// <summary>
        /// 客户端输入参数
        /// </summary>
        public string ClientInputType { get; set; }
    }
}