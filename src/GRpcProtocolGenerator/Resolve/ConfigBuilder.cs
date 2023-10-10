using GRpcProtocolGenerator.Models.Configs;
using System;

namespace GRpcProtocolGenerator.Resolve
{
    public class ConfigBuilder
    {
        public Config Config { get; private set; }

        public ConfigBuilder(string currentPath)
        {
            Config = new Config(currentPath ?? throw new ArgumentNullException(nameof(currentPath)));
        }

        /// <summary>
        /// 配置目标程序集名称
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public ConfigBuilder SetAssembly(string assemblyName)
        {
            ArgumentNullException.ThrowIfNull(assemblyName, nameof(assemblyName));

            Config.Assemblies = assemblyName;

            return this;
        }

        /// <summary>
        /// 配置 protocol
        /// </summary>
        /// <param name="protocolConfigAction"></param>
        /// <returns></returns>
        public ConfigBuilder SetProtocolConfig(Action<ProtocolConfig> protocolConfigAction)
        {
            ArgumentNullException.ThrowIfNull(protocolConfigAction, nameof(protocolConfigAction));

            Config.Proto = new ProtocolConfig(Config.CurrentPath);

            protocolConfigAction.Invoke(Config.Proto);

            return this;
        }

        public ConfigBuilder SetServerConfig(Action<ServerConfig> serverConfigAction)
        {
            ArgumentNullException.ThrowIfNull(serverConfigAction, nameof(serverConfigAction));

            Config.Server = new ServerConfig(Config.CurrentPath);

            serverConfigAction.Invoke(Config.Server);

            return this;
        }

        /// <summary>
        /// 配置json转码
        /// </summary>
        /// <param name="jsonTranscodingConfigAction"></param>
        /// <returns></returns>
        public ConfigBuilder SetJsonTranscoding(Action<JsonTranscodingConfig> jsonTranscodingConfigAction)
        {
            ArgumentNullException.ThrowIfNull(jsonTranscodingConfigAction, nameof(jsonTranscodingConfigAction));

            Config.JsonTranscoding ??= new JsonTranscodingConfig();
            Config.JsonTranscoding.UseJsonTranscoding = true;

            jsonTranscodingConfigAction.Invoke(Config.JsonTranscoding);

            return this;
        }

        /// <summary>
        /// 生成客户端包装
        /// </summary>
        /// <param name="clientWrapperAction"></param>
        /// <returns></returns>
        public ConfigBuilder SetClientWrapper(Action<ClientWrapperConfig> clientWrapperAction)
        {
            ArgumentNullException.ThrowIfNull(clientWrapperAction, nameof(clientWrapperAction));

            Config.ClientWrapper = new ClientWrapperConfig(Config.CurrentPath);

            clientWrapperAction.Invoke(Config.ClientWrapper);

            return this;
        }

        /// <summary>
        /// 生成控制器配置
        /// </summary>
        /// <param name="controllerConfig"></param>
        /// <returns></returns>
        public ConfigBuilder SetController(Action<ControllerConfig> controllerConfig)
        {
            ArgumentNullException.ThrowIfNull(controllerConfig, nameof(controllerConfig));

            Config.Controller = new ControllerConfig(Config.CurrentPath);

            controllerConfig.Invoke(Config.Controller);

            return this;
        }

        /// <summary>
        /// 元数据过滤
        /// </summary>
        /// <param name="filterAction"></param>
        /// <returns></returns>
        public ConfigBuilder SetFilter(Action<Filter> filterAction)
        {
            ArgumentNullException.ThrowIfNull(filterAction, nameof(filterAction));

            Config.Filter = new Filter();

            filterAction.Invoke(Config.Filter);

            return this;
        }
    }
}