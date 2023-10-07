using GRpcProtocolGenerator.Models.Configs;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GRpcProtocolGenerator.Renders.Protocol
{
    public class ProtocolContent
    {
        public ProtocolService ProtoService { get; }

        public List<string> DependencyList { get; set; }

        public List<ProtocolMessage> ProtoMessageList { get; set; }

        private readonly StringBuilder _sbHeader;

        private readonly StringBuilder _sbContent;

        public string CSharpNamespace { get; set; }

        ///// <summary>
        ///// 枚举文件名称
        ///// </summary>
        //private const string EnumFileName = "enums";

        public ProtocolContent(ProtocolService protoService)
        {
            ProtoService = protoService;
            _sbHeader = new StringBuilder();
            DependencyList = new List<string>();

            //处理方法的传入和传出参数
            ProtoMessageList = ProtoService.Items.Where(d => d.InParam != null && !d.InParam.IsEmpty && !d.InParam.IsCancellationToken).Select(d => d.InParam).ToList();
            var outParams = ProtoService.Items.Where(d => d.OutParam != null && !d.OutParam.IsEmpty).Select(d => d.OutParam).ToList();

            foreach (var outParam in outParams.Where(d => !d.IsOriginalClass))
            {
                outParam.Items.Where(d => string.IsNullOrWhiteSpace(d.Name)).ToList().ForEach(d => { d.SetName("Data"); });
            }

            ProtoMessageList.AddRange(outParams);
            ProtoMessageList = ProtoMessageList.Distinct().OrderBy(d => d.Name).ToList();

            _sbContent = new StringBuilder();
            foreach (var message in ProtoMessageList)
            {
                message.GetOrSetGRpcServiceProtoPath(ProtoService.InterfaceMetaData, _sbContent);

                DependencyList.AddRange(message.Imports);
                DependencyList.Add(message.MessagePath);
            }

            //谷歌数据包装器
            DependencyList.Insert(0, new EmptyProtocolMessage().MessagePath);
            DependencyList.Insert(0, "google/protobuf/wrappers");

            // http api
            if (Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding)
                DependencyList.Insert(2, "google/api/annotations");

            ////枚举依赖 枚举统一写入 enums.proto 文件
            //if (ProtoMessageList.Any(d => d.EnumDependency != null && d.EnumDependency.Any()))
            //{
            //    DependencyList.Add($"{Config.Proto.ProtoDirectory}/{EnumFileName}");
            //}

            DependencyList = DependencyList.Where(d => d != ProtoService.InterfaceMetaData.FormatServiceProtoFileNameFullPath()).Distinct().ToList();

            CSharpNamespace = Config.ConfigInstance.Proto.GetCSharpNamespace(ProtoService.InterfaceMetaData);
        }

        public string ToContent()
        {
            CreateProtoVersion();
            CreateDependencyImport();
            CreateCSharpNamespace();
            CreatePackage();
            CreateEmptyLine();
            CreateContent();
            return _sbHeader.ToString();
        }

        public string ToEnumContent()
        {
            var enums = ProtoMessageList.SelectMany(d => d.EnumDependency).ToList();
            if (!enums.Any())
                return null;

            return null;
        }

        private void CreateProtoVersion()
        {
            _sbHeader.AppendLine("syntax = \"proto3\";");
        }

        private void CreateDependencyImport()
        {
            foreach (var dep in DependencyList)
            {
                _sbHeader.AppendLine($"import \"{dep}.proto\";");
            }
        }

        private void CreateCSharpNamespace()
        {
            _sbHeader.AppendLine($"option csharp_namespace = \"{CSharpNamespace}\";");
        }

        private void CreatePackage()
        {
            _sbHeader.AppendLine($"package {Config.ConfigInstance.Proto.GetPackageName(ProtoService.InterfaceMetaData)};");
        }

        private void CreateEmptyLine()
        {
            _sbHeader.AppendLine();
        }

        //private void CreateLine()
        //{
        //    _sbHeader.AppendLine("//----------------------------------------------------------");
        //}

        //private void CreateNote(string note)
        //{
        //    _sbHeader.AppendLine($"//{note}------------------------------------------------------");
        //}

        private void CreateContent()
        {
            _sbHeader.AppendLine(ProtoService.ToString());
            _sbHeader.Append(_sbContent.ToString());
        }
    }
}
