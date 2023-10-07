using System.Collections.Generic;
using System.Linq;
using System.Text;
using GRpcProtocolGenerator.Models.Configs;

namespace GRpcProtocolGenerator.Renders.Protocol
{
    public class ProtocolServiceImpl
    {
        private readonly ProtocolContent _protoContent;

        public List<string> Using { get; set; }

        private readonly StringBuilder _sb = new StringBuilder();

        private readonly string _gRpcServiceName;

        public string ServerName;

        public string FullName;

        private readonly string _interfaceName;

        private const string EmptyType = "Empty";

        protected bool IsWrapper = false;

        /// <summary>
        /// 成功状态码
        /// </summary>
        protected int SuccessCode;

        /// <summary>
        /// 失败状态码
        /// </summary>
        protected int FailureCode;

        public ProtocolServiceImpl(ProtocolContent protoContent)
        {
            _protoContent = protoContent;

            _gRpcServiceName = _protoContent.ProtoService.InterfaceMetaData.FormatServiceName();
            ServerName = $"{_gRpcServiceName}Impl";
            FullName = $"{Config.ConfigInstance.Server?.GetServerNamespace()}.{ServerName}";
            _interfaceName = _protoContent.ProtoService.InterfaceMetaData.Name;

            Using = new List<string>
            {
                "System.Linq",
                "System.Collections.Generic",
                "System.Threading.Tasks",
                "Grpc.Core",
                "Google.Protobuf.WellKnownTypes",
                "Mapster",
                protoContent.CSharpNamespace,
                protoContent.ProtoService.InterfaceMetaData.Namespace,
            };

            IsWrapper = Config.ConfigInstance.JsonTranscoding.UseResultWrapper;
            SuccessCode = Config.ConfigInstance.JsonTranscoding.SuccessCode;
            FailureCode = Config.ConfigInstance.JsonTranscoding.ErrorCode;
        }

        public string ToContent()
        {
            CreateNote();
            CreateUsing();
            CreateClassBegin();
            CreateConstructor();
            CreateMethods();
            CreateClassEnd();

            return _sb.ToString();
        }

        private void CreateNote()
        {
            _sb.AppendLine("/*");
            _sb.AppendLine($"\t 此代码是自动生成，请勿修改，如果要修改，请继承它再重写方法。");
            _sb.AppendLine("*/");
            _sb.AppendLine();
        }

        private void CreateUsing()
        {
            foreach (var item in Using)
            {
                _sb.AppendLine($"using {item};");
            }

            _sb.AppendLine();
        }

        private void CreateClassBegin()
        {
            _sb.AppendLine($"namespace {Config.ConfigInstance.Server?.GetNamespace(_protoContent.ProtoService.InterfaceMetaData)}");
            _sb.AppendLine("{");

            //接口注释
            _sb.AppendLine("\t/// <summary>");
            _sb.AppendLine($"\t/// {Config.ConfigInstance.Proto.PropertyDescriptionFunc(_protoContent.ProtoService.InterfaceMetaData)} - {_protoContent.ProtoService.InterfaceMetaData.FullName}");
            _sb.AppendLine("\t/// </summary>");

            //附加 Attribute 属性
            Config.ConfigInstance.Server?.AppendAttributeToServer?.ForEach(attr => { _sb.AppendLine($"\t{attr}"); });

            if (Config.ConfigInstance.JsonTranscoding.UseJwtAuthentication)
                _sb.AppendLine("\t[Microsoft.AspNetCore.Authorization.Authorize]");

            _sb.AppendLine($"\tpublic class {ServerName} : {_gRpcServiceName}.{_gRpcServiceName}Base");
            _sb.AppendLine("\t{");
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private void CreateConstructor()
        {
            _sb.AppendLine($"\t\tprivate readonly {_interfaceName} _service;");
            _sb.AppendLine();
            _sb.AppendLine("\t\t/// <summary>");
            _sb.AppendLine("\t\t/// 初始化");
            _sb.AppendLine("\t\t/// </summary>");
            _sb.AppendLine("\t\t/// <param name=\"service\"></param>");
            _sb.AppendLine($"\t\tpublic {ServerName}({_interfaceName} service)");
            _sb.AppendLine("\t\t{");
            _sb.AppendLine($"\t\t\t_service = service;");
            _sb.AppendLine("\t\t}");
            _sb.AppendLine();
        }

        private void CreateMethods()
        {
            foreach (var item in _protoContent.ProtoService.Items)
            {
                var isInParamEmpty = item.MethodMetaData.InParamMetaDataListFilter().Count == 0;
                var inParam = isInParamEmpty
                    ? EmptyType
                    : item.InParam.GetGRpcName();

                var isOutParamEmpty = item.MethodMetaData.OutParamMetaDataList.Count == 0;
                var outParam = BuilderPart.BuildMethodReturnType(IsWrapper, isOutParamEmpty, item.OutParam.GetGRpcName(), EmptyType);

                //注释
                _sb.AppendLine($"\t\t/// {Config.ConfigInstance.Proto.PropertyDescriptionFunc(item.MethodMetaData)}");

                //方法名
                _sb.AppendLine($"\t\tpublic override {outParam} {item.Name}({inParam} request, ServerCallContext context)");
                _sb.AppendLine("\t\t{");

                //传入参数
                var inParamString = GetInParamString(item);

                //返回参数
                var outParamString = isOutParamEmpty ? "" : "var data = ";
                outParamString += item.MethodMetaData.IsTask ? "await " : "";

                _sb.AppendLine($"\t\t\t{outParamString}_service.{item.MethodMetaData.Name}({inParamString});");

                //return
                _sb.AppendLine(GetOutParamString(item).ToString());

                _sb.AppendLine("\t\t}");
                _sb.AppendLine();
            }
        }


        private void CreateClassEnd()
        {
            _sb.AppendLine("\t}");
            _sb.AppendLine("}");
        }

        public string GetInParamString(ProtoServiceItem item, string inputParamName = "request")
        {
            if (item.MethodMetaData.InParamMetaDataList.Count == 0)
                return null;

            //原始类
            if (item.InParam.IsOriginalClass)
                return item.MethodMetaData.HasCancellationToken
                    ? BuilderPart.BuildCancellationTokenInput()
                    : BuilderPart.MapTo(true, inputParamName, item.InParam.ClassMetaData.FullName);

            var inParamString = "";
            foreach (var prop in item.MethodMetaData.InParamMetaDataList)
            {
                inParamString += BuilderPart.BuildInputItem(prop, inputParamName) + ", ";
            }

            return inParamString.Trim().TrimEnd(',');
        }

        public StringBuilder GetOutParamString(ProtoServiceItem item)
        {
            var builder = new StringBuilder();

            //没有返回值
            if (item.MethodMetaData.OutParamMetaDataList.Count == 0)
            {
                builder.Append($"\t\t\t{BuilderPart.BuildEmptyReturn(item.OutParam.GetGRpcName(), IsWrapper, SuccessCode)}");
                return builder;
            }

            var param = item.OutParam.Items.Last();

            //原始类
            if (item.OutParam.IsOriginalClass)
            {
                builder.Append($"\t\t\t{BuilderPart.BuildClassReturn(item.OutParam.GetGRpcName(), param.IsArray, IsWrapper, SuccessCode)}");
                return builder;
            }

            //组合的参数
            builder.AppendLine($"\t\t\treturn new {item.OutParam.GetGRpcName()}");
            builder.AppendLine("\t\t\t{");

            if (IsWrapper)
                builder.AppendLine($"\t\t\t\tCode = {SuccessCode},");

            if (param.ClassMetaData != null)
            {
                builder.AppendLine("\t\t\t\t" + BuilderPart.BuildClassItemReturn(param.Name, param.IsArray, param.GRpcType));
            }
            else if (param.EnumMetaData != null)
            {

                builder.AppendLine("\t\t\t\t" + BuilderPart.BuildEnumItemReturn(param.Name, param.IsArray, param.IsNullable));
            }
            else
            {
                builder.AppendLine("\t\t\t\t" + BuilderPart.BuildSampleItemReturn(param.Name, param.IsArray));
            }

            builder.Append("\t\t\t};");

            return builder;
        }
    }
}