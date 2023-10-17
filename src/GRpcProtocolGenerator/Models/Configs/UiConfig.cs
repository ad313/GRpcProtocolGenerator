using System;
using System.IO;

namespace GRpcProtocolGenerator.Models.Configs
{
    /// <summary>
    /// UI 生成配置
    /// </summary>
    public class UiConfig
    {
        /// <summary>
        /// 宿主程序运行根目录，用于相对路径定位到具体的路径
        /// </summary>
        public string CurrentPath { get; private set; }

        /// <summary>
        /// 输出路径
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// 输出路径，绝对路径
        /// </summary>
        public string OutputFullPath { get; private set; }

        /// <summary>
        /// 存放 ts 模型
        /// </summary>
        public string TsModelFileDirectory { get; set; }

        /// <summary>
        /// 初始化，传入宿主程序地址，不是bin地址
        /// </summary>
        /// <param name="currentPath">宿主程序地址，不是bin地址</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UiConfig(string currentPath)
        {
            if (string.IsNullOrWhiteSpace(currentPath))
                throw new ArgumentNullException(nameof(currentPath));

            CurrentPath = currentPath;
        }

        public void Check()
        {
            ArgumentNullException.ThrowIfNull(Output, nameof(Output));

            OutputFullPath = Path.GetFullPath(Path.Combine(CurrentPath, Output));
        }

        public string GetTsFileOutputPath()
        {
            var path = Path.Combine(OutputFullPath, TsModelFileDirectory ?? "");
            return Path.GetFullPath(path);
        }
    }
}
