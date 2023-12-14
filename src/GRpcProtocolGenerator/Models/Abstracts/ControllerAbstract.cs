using System.Collections.Generic;
using GRpcProtocolGenerator.Renders.Protocol;

namespace GRpcProtocolGenerator.Models.Abstracts
{
    /// <summary>
    /// Controller 抽象模型
    /// </summary>
    public class ControllerAbstract
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 依赖外部命名空间、引入包路径等
        /// </summary>
        public List<string> Dependency { get; set; }

        /// <summary>
        /// 命名空间、包名称
        /// </summary>
        public string NamespaceOrPackage { get; set; }

        /// <summary>
        /// 控制器前置路由
        /// </summary>
        public string BaseRoute { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// api 版本
        /// </summary>
        public string Version { get; set; }

        public List<ControllerItemAbstract> Items { get; set; }
    }

    public class ControllerItemAbstract
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 方法路由前缀
        /// </summary>
        public string BaseRoute { get; set; }

        /// <summary>
        /// 方法路由
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// 原始的方法路由
        /// </summary>
        public string OriginalRoute { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        public string RouteParam { get; set; }

        /// <summary>
        /// 路由参数
        /// </summary>
        public string RouteParamUpper { get; set; }

        /// <summary>
        /// HttpMethod
        /// </summary>
        public HttpMethod? HttpMethod { get; set; }

        /// <summary>
        /// HttpMethod
        /// </summary>
        public string Http { get; set; }

        /// <summary>
        /// gRpc 输入参数  empty时忽略
        /// </summary>
        public string GRpcInputType { get; set; }

        /// <summary>
        /// gRpc 输出参数
        /// </summary>
        public string GRpcOutType { get; set; }
        
        /// <summary>
        /// dto 输入参数
        /// </summary>
        public string DtoInputType { get; set; }

        /// <summary>
        /// dto 输入参数 备注
        /// </summary>
        public string DtoInputTypeDescription { get; set; }

        /// <summary>
        /// dto 输出参数
        /// </summary>
        public string DtoOutType { get; set; }

        /// <summary>
        /// dto 输出参数 备注
        /// </summary>
        public string DtoOutTypeDescription { get; set; }

        /// <summary>
        /// 原始接口输入参数
        /// </summary>
        public List<string> OriginalOutParam { get; set; }

        /// <summary>
        /// 返回值类型 0：没有返回值；1：包装器，返回单个值；2：返回对象
        /// </summary>
        public int Return { get; set; }
        
        /// <summary>
        /// Return 为 1时，单个参数是否是class
        /// </summary>
        public bool ReturnClass { get; set; }

        /// <summary>
        /// Return 为 1时，单个参数数据类型
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// Return 为 1时，单个参数字段名称
        /// </summary>
        public string ReturnName { get; set; }

        /// <summary>
        /// Return 为 1时，单个参数 是否是集合
        /// </summary>
        public bool IsArray { get; set; }

        /// <summary>
        /// 是否有输入参数
        /// </summary>
        public bool HasInputParam { get; set; }
    }
}