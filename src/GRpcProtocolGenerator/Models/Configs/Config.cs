using GRpcProtocolGenerator.Common;
using GRpcProtocolGenerator.Models.MetaData;
using System;

namespace GRpcProtocolGenerator.Models.Configs
{
    /// <summary>
    /// 生成相关配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 配置实例
        /// </summary>
        public static Config ConfigInstance { get; private set; }

        /// <summary>
        /// 宿主程序运行根目录，用于相对路径定位到具体的路径
        /// </summary>
        public string CurrentPath { get; }

        /// <summary>
        /// 提取接口的程序集集合
        /// </summary>
        public string Assemblies { get; set; }

        /// <summary>
        /// Proto 配置
        /// </summary>
        public ProtocolConfig Proto { get; set; }

        /// <summary>
        /// 服务端配置
        /// </summary>
        public ServerConfig Server { get; set; }

        /// <summary>
        /// 是否生成Server
        /// </summary>
        public bool HasServer => Server != null;

        /// <summary>
        /// 数据过滤
        /// </summary>
        public Filter Filter { get; set; }

        /// <summary>
        /// Json 转码 为 gRPC 服务创建 RESTful JSON API 
        /// </summary>
        public JsonTranscodingConfig JsonTranscoding { get; set; }
        
        /// <summary>
        /// 生成控制器配置
        /// </summary>
        public ControllerConfig Controller { get; set; }

        /// <summary>
        /// Ui 配置
        /// </summary>
        public UiConfig UiConfig { get; set; }

        /// <summary>
        /// 初始化，传入宿主程序地址，不是bin地址
        /// </summary>
        /// <param name="currentPath">宿主程序地址，不是bin地址</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Config(string currentPath)
        {
            if (string.IsNullOrWhiteSpace(currentPath))
                throw new ArgumentNullException(nameof(currentPath));

            CurrentPath = currentPath;

            JsonTranscoding = new JsonTranscodingConfig() { Swagger = new SwaggerConfig() };

            ConfigInstance = this;
        }

        public void Check()
        {
            if (string.IsNullOrWhiteSpace(Assemblies))
                throw new Exception("请指定程序集");

            ArgumentNullException.ThrowIfNull(Proto, nameof(Proto));
        }
    }

    /// <summary>
    /// 数据过滤
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// 接口过滤
        /// </summary>
        public Func<InterfaceMetaData, bool> InterfaceFilterFunc { get; set; }

        /// <summary>
        /// 方法过滤
        /// </summary>
        public Func<InterfaceMetaData, MethodMetaData, bool> MethodFilterFunc { get; set; }
    }
}
