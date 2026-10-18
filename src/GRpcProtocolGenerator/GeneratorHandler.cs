using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders;
using GRpcProtocolGenerator.Resolve;

namespace GRpcProtocolGenerator;

public class GeneratorHandler
{
    private readonly Config? _config;
    private readonly bool _isContinue = false;

    public AssemblyMetaData? AssemblyMetaData { get; private set; }

    public GeneratorHandler(Config config, Func<bool>? enable)
    {
        _config = config;
        _isContinue = enable?.Invoke() == true;

        Init();
    }

    public GeneratorHandler(string currentPath, Action<ConfigBuilder> configBuilderAction)
    {
        ArgumentNullException.ThrowIfNull(configBuilderAction);

        var configBuilder = new ConfigBuilder(currentPath);
        configBuilderAction.Invoke(configBuilder);

        _config = configBuilder.Config;
        _isContinue = true;

        Init();
    }

    public async Task GeneratorAsync()
    {
        if (!_isContinue)
            return;

        await CreateBuilder().RenderAsync();
    }

    public Builder CreateBuilder()
    {
        return new Builder(AssemblyMetaData!);
    }

    private void Init()
    {
        if (!_isContinue)
            return;

        ArgumentNullException.ThrowIfNull(_config);
        ArgumentNullException.ThrowIfNull(_config.Proto);
        _config.Check();
        _config.Proto.Check();
        _config.Server?.Check();
        _config.Controller?.Check();
        _config.UiConfig?.Check();

        //获取当前作用域内所有程序集
        var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        //过滤
        var assembly = allAssemblies.FirstOrDefault(d => _config.Assemblies!.Contains(d.ManifestModule.ToString().Replace(".dll", "", StringComparison.Ordinal)));
        if (assembly != null)
        {
            Console.WriteLine($"匹配到程序集：{assembly.FullName}");
        }
        else
        {
            Console.WriteLine($"未匹配到程序集：{_config.Assemblies}");
            return;
        }

        AssemblyMetaData = new MetaDataResolve().Resolve(assembly, _config);

        ShowLog(AssemblyMetaData);
    }

    private static void ShowLog(AssemblyMetaData assemblyMetaData)
    {
        var sb = new StringBuilder();
        foreach (var interfaceMetaData in assemblyMetaData.InterfaceMetaDataDictionary.Select(d => d.Value))
        {
            sb.AppendLine($"接口名称：{interfaceMetaData.Key}");
            sb.AppendLine($"方法个数：{interfaceMetaData.MethodMetaDataList.Count}");

            foreach (var method in interfaceMetaData.MethodMetaDataList)
            {
                sb.AppendLine($"    方法名称：{method.Name}");
                sb.AppendLine($"    传入参数：{method.InParamMetaDataList.Count} 个");
                foreach (var inParam in method.InParamMetaDataList)
                {
                    if (inParam.ClassMetaData == null)
                    {
                        sb.AppendLine($"      =>：{inParam.Name}。");
                    }
                    else
                    {
                        sb.AppendLine($"      =>：{inParam.Name}。是否集合：{inParam.TypeWrapper.IsArray}。属性个数：{inParam.ClassMetaData.PropertyMetaDataList.Count}");
                    }
                }

                sb.AppendLine();

                sb.AppendLine($"    传出参数：{method.OutParamMetaDataList.Count} 个");
                foreach (var outParam in method.OutParamMetaDataList)
                {
                    if (outParam.ClassMetaData == null)
                    {
                        sb.AppendLine($"      =>：{outParam.Name}。");
                    }
                    else
                    {
                        sb.AppendLine($"      =>：{outParam.Name}。是否集合：{outParam.TypeWrapper.IsArray}。属性个数：{outParam.ClassMetaData.PropertyMetaDataList.Count}");
                    }
                }

                sb.AppendLine("-------------------------------------------------------------------------------------");
            }

            sb.AppendLine();
        }

        Console.WriteLine(sb.ToString());
    }
}
