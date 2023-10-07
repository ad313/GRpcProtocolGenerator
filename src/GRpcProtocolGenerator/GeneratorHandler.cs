using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders;
using GRpcProtocolGenerator.Resolve;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRpcProtocolGenerator
{
    public class GeneratorHandler
    {
        private readonly Config _config;
        private readonly bool _isContinue = false;

        public AssemblyMetaData AssemblyMetaData { get; private set; }
        
        public GeneratorHandler(Config config, Func<bool> enable)
        {
            _config = config;
            _isContinue = enable?.Invoke() == true;

            Init();
        }

        public GeneratorHandler(string currentPath, Action<ConfigBuilder> configBuilderAction)
        {
            ArgumentNullException.ThrowIfNull(configBuilderAction, nameof(configBuilderAction));

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
            return new Builder(AssemblyMetaData, _config);
        }

        private void Init()
        {
            if (!_isContinue)
                return;

            ArgumentNullException.ThrowIfNull(_config, nameof(_config));
            ArgumentNullException.ThrowIfNull(_config.Proto, nameof(_config.Proto));
            _config.Check();
            _config.Proto.Check();
            _config.Server?.Check();

            //获取当前作用域内所有程序集
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            //过滤
            var assembly = allAssemblies.FirstOrDefault(d => _config.Assemblies.Contains(d.ManifestModule.ToString().Replace(".dll", "")));
            if (assembly != null)
            {
                Console.WriteLine($"匹配到程序集：{assembly.FullName}");
            }
            else
            {
                Console.WriteLine($"未匹配到程序集：{_config.Assemblies}");
                return;
            }

            BuilderName.Config = _config;

            AssemblyMetaData = new MetaDataResolve().Resolve(assembly, _config);

            ShowLog(AssemblyMetaData);
        }

        private void ShowLog(AssemblyMetaData assemblyMetaData)
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
                    foreach (var inParam in method.OutParamMetaDataList)
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

                    sb.AppendLine("-------------------------------------------------------------------------------------");
                }

                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString());
        }
    }
}