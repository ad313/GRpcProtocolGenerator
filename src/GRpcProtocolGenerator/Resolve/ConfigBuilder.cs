using System;
using GRpcProtocolGenerator.Models.Configs;

namespace GRpcProtocolGenerator.Resolve;

public class ConfigBuilder(string currentPath)
{
    public Config Config { get; private set; } = new(currentPath ?? throw new ArgumentNullException(nameof(currentPath)));

    /// <summary>
    /// 配置目标程序集名称
    /// </summary>
    public ConfigBuilder SetAssembly(string assemblyName)
    {
        ArgumentNullException.ThrowIfNull(assemblyName);

        Config.Assemblies = assemblyName;

        return this;
    }

    /// <summary>
    /// 配置 protocol
    /// </summary>
    public ConfigBuilder SetProtocolConfig(Action<ProtocolConfig> protocolConfigAction)
    {
        ArgumentNullException.ThrowIfNull(protocolConfigAction);

        Config.Proto = new ProtocolConfig(Config.CurrentPath);

        protocolConfigAction.Invoke(Config.Proto);

        return this;
    }

    public ConfigBuilder SetServerConfig(Action<ServerConfig> serverConfigAction)
    {
        ArgumentNullException.ThrowIfNull(serverConfigAction);

        Config.Server = new ServerConfig(Config.CurrentPath);

        serverConfigAction.Invoke(Config.Server);

        return this;
    }

    /// <summary>
    /// 配置json转码
    /// </summary>
    public ConfigBuilder SetJsonTranscoding(Action<JsonTranscodingConfig> jsonTranscodingConfigAction)
    {
        ArgumentNullException.ThrowIfNull(jsonTranscodingConfigAction);

        Config.JsonTranscoding ??= new JsonTranscodingConfig();
        Config.JsonTranscoding.UseJsonTranscoding = true;

        jsonTranscodingConfigAction.Invoke(Config.JsonTranscoding);

        return this;
    }

    /// <summary>
    /// 生成控制器配置
    /// </summary>
    public ConfigBuilder SetController(Action<ControllerConfig> controllerConfig)
    {
        ArgumentNullException.ThrowIfNull(controllerConfig);

        Config.Controller = new ControllerConfig(Config.CurrentPath);

        controllerConfig.Invoke(Config.Controller);

        return this;
    }

    /// <summary>
    /// 生成UI配置
    /// </summary>
    public ConfigBuilder SetUi(Action<UiConfig> uiConfig)
    {
        ArgumentNullException.ThrowIfNull(uiConfig);

        Config.UiConfig = new UiConfig(Config.CurrentPath);

        uiConfig.Invoke(Config.UiConfig);

        return this;
    }

    /// <summary>
    /// 元数据过滤
    /// </summary>
    public ConfigBuilder SetFilter(Action<Filter> filterAction)
    {
        ArgumentNullException.ThrowIfNull(filterAction);

        Config.Filter = new Filter();

        filterAction.Invoke(Config.Filter);

        return this;
    }
}
