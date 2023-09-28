using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders;

namespace GRpcProtocolGenerator
{
    public class GeneratorHandler
    {
        private readonly GeneratorConfig _assemblyLoaderConfig;
        private readonly bool _isContinue = false;

        public AssemblyMetaData AssemblyMetaData { get; private set; }


        public GeneratorHandler(GeneratorConfig assemblyLoaderConfig, Func<bool> enable)
        {
            _assemblyLoaderConfig = assemblyLoaderConfig;
            _isContinue = enable?.Invoke() == true;

            Init();
        }

        private void Init()
        {
            if (!_isContinue)
                return;

            ArgumentNullException.ThrowIfNull(_assemblyLoaderConfig, nameof(_assemblyLoaderConfig));
            ArgumentNullException.ThrowIfNull(_assemblyLoaderConfig.Proto, nameof(_assemblyLoaderConfig.Proto));
            _assemblyLoaderConfig.Check();
            _assemblyLoaderConfig.Proto.Check();
            _assemblyLoaderConfig.Server?.Check();

            //获取当前作用域内所有程序集
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            //过滤
            var assembly = allAssemblies.FirstOrDefault(d => _assemblyLoaderConfig.Assemblies.Contains(d.ManifestModule.ToString().Replace(".dll", "")));
            if (assembly != null)
            {
                Console.WriteLine($"匹配到程序集：{assembly.FullName}");
            }
            else
            {
                Console.WriteLine($"未匹配到程序集：{_assemblyLoaderConfig.Assemblies}");
                return;
            }

            NameExtensions.config = _assemblyLoaderConfig;

            AssemblyMetaData = new MetaDataResolve()
                .Resolve(assembly)
                .Filter(_assemblyLoaderConfig);

            ShowLog(AssemblyMetaData);
        }

        public async Task GeneratorAsync()
        {
            if (!_isContinue)
                return;

            await CreateCodeRender().RenderAsync();
        }

        public CodeRender CreateCodeRender()
        {
            return new CodeRender(AssemblyMetaData, _assemblyLoaderConfig);
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