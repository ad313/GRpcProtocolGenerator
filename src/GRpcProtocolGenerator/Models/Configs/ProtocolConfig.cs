using GRpcProtocolGenerator.Models.MetaData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GRpcProtocolGenerator.Models.Configs
{
    /// <summary>
    /// 生成proto 配置
    /// </summary>
    public class ProtocolConfig
    {
        /// <summary>
        /// 宿主程序运行根目录，用于相对路径定位到具体的路径
        /// </summary>
        public string CurrentPath { get; private set; }

        /// <summary>
        /// proto 文件 输出路径，路径的最后一层就是项目名称
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// proto 文件 输出路径，绝对路径
        /// </summary>
        public string OutputFullPath { get; private set; }

        /// <summary>
        /// 最末级目录名称，项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 存放 Proto 的文件夹
        /// </summary>
        public string ProtoDirectory { get; set; }

        /// <summary>
        /// 引用包时是否加上 ProtoDirectory
        /// </summary>
        public bool UseProtoDirectoryWhenImportPackage { get; set; } = true;

        /// <summary>
        /// 初始化，传入宿主程序地址，不是bin地址
        /// </summary>
        /// <param name="currentPath">宿主程序地址，不是bin地址</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProtocolConfig(string currentPath)
        {
            if (string.IsNullOrWhiteSpace(currentPath))
                throw new ArgumentNullException(nameof(currentPath));

            CurrentPath = currentPath;
        }

        public void Check()
        {
            ArgumentNullException.ThrowIfNull(Output, nameof(Output));

            ProjectName = Output?.Split('/').LastOrDefault() ?? "";
            OutputFullPath = Path.GetFullPath(Path.Combine(CurrentPath, Output));
        }

        /// <summary>
        /// 获取 proto 文件完整路径
        /// </summary>
        /// <returns></returns>
        public string GetProtoFileOutputPath()
        {
            var path = Path.Combine(OutputFullPath, ProtoDirectory ?? "");
            return Path.GetFullPath(path);
        }

        /// <summary>
        /// 获取 csproj 文件完整路径
        /// </summary>
        /// <returns></returns>
        public string GetCsprojFilePath()
        {
            return Path.GetFullPath(Path.Combine(OutputFullPath, ProjectName + ".csproj"));
        }

        #region 包名

        public string PackageName { get; set; }

        public Func<InterfaceMetaData, string> PackageNameFunc { get; set; }

        public string GetPackageName(InterfaceMetaData meta)
        {
            return PackageNameFunc?.Invoke(meta) ?? PackageName;
        }

        #endregion

        #region 命名空间

        public string CSharpNamespace { get; set; }

        public Func<InterfaceMetaData, string> CSharpNamespaceFunc { get; set; }

        public string GetCSharpNamespace(InterfaceMetaData meta)
        {
            return CSharpNamespaceFunc?.Invoke(meta) ?? CSharpNamespace;
        }

        #endregion

        #region 注释

        /// <summary>
        /// 属性注释
        /// </summary>
        public Func<CommentMetaData, string> PropertyDescriptionFunc { get; set; }

        #endregion

        #region 服务名称

        /// <summary>
        /// 服务名称
        /// </summary>
        public Func<InterfaceMetaData, string> ServiceNameFunc { get; set; }

        #endregion

        #region 方法名称

        /// <summary>
        /// 方法名称
        /// </summary>
        public Func<MethodMetaData, string> MethodNameFunc { get; set; }

        #endregion

        #region 传入参数 新建类名称

        /// <summary>
        /// 传入传出参数新建类名称
        /// </summary>
        public Func<MethodMetaData, List<PropertyMetaData>, string> MethodInOutParamNameFunc { get; set; }

        #endregion

        #region 原始类名称

        /// <summary>
        /// 原始类名称
        /// </summary>
        public Func<string, string> OriginalClassNameFunc { get; set; }

        #endregion

        #region 类型转换

        /// <summary>
        /// C# 类型 转换成 Protobuf 对应的类型
        /// </summary>
        public Func<Type, bool, string> CSharpTypeToProtobufString { get; set; }

        #endregion
    }
}
