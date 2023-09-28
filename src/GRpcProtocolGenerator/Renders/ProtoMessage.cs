using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GRpcProtocolGenerator.Models.MetaData;

namespace GRpcProtocolGenerator.Renders
{
    public class ProtoMessage: IEquatable<ProtoMessage>
    {
        /// <summary>
        /// 包名
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 包引用地址
        /// </summary>
        public string MessagePath { get; private set; }
        /// <summary>
        /// 包本身引用的其他依赖包
        /// </summary>
        public List<string> Imports { get; private set; }

        /// <summary>
        /// message 项集合
        /// </summary>
        public List<ProtoItemMessage> Items { get; private set; } = new List<ProtoItemMessage>();
        /// <summary>
        /// 依赖的message 集合
        /// </summary>
        public List<ProtoMessage> ClassDependency { get; private set; }
        /// <summary>
        /// 依赖的枚举 集合
        /// </summary>
        public List<EnumProtoMessage> EnumDependency { get; private set; }
        /// <summary>
        /// 是原始的类，而不是新建的类
        /// </summary>
        public bool IsOriginalClass { get; private set; }
        /// <summary>
        /// 是否是空包
        /// </summary>
        public bool IsEmpty { get; private set; }
        /// <summary>
        /// 原始类
        /// </summary>
        public ClassMetaData ClassMetaData { get; private set; }
        /// <summary>
        /// 原始枚举
        /// </summary>
        public EnumMetaData EnumMetaData { get; private set; }

        /// <summary>
        /// IsCancellationToken
        /// </summary>
        public bool IsCancellationToken { get; private set; }

        public ProtoMessage(string name, List<ProtoItemMessage> items, List<ProtoMessage> classDependency, List<EnumProtoMessage> enumDependency, bool isOriginalClass, bool isEmpty, ClassMetaData classMetaData)
        {
            Name = name;
            Items = items;
            ClassDependency = classDependency ?? new List<ProtoMessage>();
            EnumDependency = enumDependency ?? new List<EnumProtoMessage>();
            IsOriginalClass = isOriginalClass;
            ClassMetaData = classMetaData;
            IsEmpty = isEmpty;

            IsCancellationToken = isOriginalClass && ClassMetaData.TypeWrapper.Type.IsCancellationToken();
        }

        public ProtoMessage(string name, List<ProtoItemMessage> items, EnumMetaData enumMetaData)
        {
            Name = name;
            Items = items;
            EnumMetaData = enumMetaData;
            ClassDependency = new List<ProtoMessage>();
            EnumDependency = new List<EnumProtoMessage>();
        }

        public ProtoMessage(string name, string messagePath, bool isEmpty)
        {
            Name = name;
            MessagePath = messagePath;
            IsEmpty = isEmpty;
            ClassDependency = new List<ProtoMessage>();
            EnumDependency = new List<EnumProtoMessage>();
        }

        public void SetClassDependency(List<ProtoMessage> protoMessageList)
        {
            if (IsCancellationToken)
            {
                ClassDependency = new List<ProtoMessage>();
                return;
            }

            ClassDependency = protoMessageList;
        }

        public void AddClassDependency(List<ProtoMessage> protoMessageList)
        {
            if (protoMessageList == null)
                return;

            ClassDependency ??= new List<ProtoMessage>();

            foreach (var message in protoMessageList)
            {
                if (ClassDependency.Exists(d => d.Name == message.Name))
                    continue;

                ClassDependency.Add(message);
            }
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"//{CodeRender.Config.Proto.PropertyDescriptionFunc(ClassMetaData)} {ClassMetaData?.FullName ?? Name}");
            sb.AppendLine($"message {GetGRpcName()} " + "{");

            var index = 1;
            foreach (var item in Items)
            {
                item.SetIndex(index);

                var text = item.ToString();
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                sb.AppendLine("\t" + text);
                index++;
            }

            sb.AppendLine("}");
            return sb.ToString();
        }

        public void GetOrSetGRpcServiceProtoPath(InterfaceMetaData interfaceMetaData, StringBuilder sb)
        {
            Imports = new List<string>();
            if (string.IsNullOrWhiteSpace(MessagePath))
            {
                MessagePath = interfaceMetaData.FormatServiceProtoFileNameFullPath();

                if (ClassDependency != null && ClassDependency.Any())
                {
                    foreach (var message in ClassDependency.OrderBy(d => d.Name))
                    {
                        message.GetOrSetGRpcServiceProtoPath(interfaceMetaData, sb);
                    }

                    Imports.AddRange(ClassDependency.Select(d => d.MessagePath));
                }
                
                Imports = Imports.Distinct().ToList();

                if (IsCancellationToken == false)
                    sb.AppendLine(ToString());
            }
        }

        public string GetGRpcName()
        {
            if (IsEmpty)
                return Name;

            if (IsCancellationToken)
            {
                IsEmpty = true;
                return "google.protobuf.Empty";
            }

            return this.FormatMessageName();
        }

        public bool Equals(ProtoMessage other)
        {
            return Name == other?.Name;
        }

        /// <summary>
        /// 获取哈希
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }

    public class ProtoItemMessage
    {
        public bool IsNullable { get; private set; }

        public bool IsArray { get; private set; }

        public string Name { get; private set; }

        public string GRpcType { get; private set; }

        public Type Type { get; private set; }

        public int Index { get; private set; }

        public CommentMetaData CommentMetaData { get; private set; }

        public ClassMetaData ClassMetaData { get; private set; }

        public EnumMetaData EnumMetaData { get; private set; }

        public ProtoItemMessage(Type type1, bool isNullable, bool isArray, string name, string gRpcType, string type, int index, CommentMetaData commentMetaData, ClassMetaData classMetaData, EnumMetaData enumMetaData)
        {
            IsNullable = isNullable;
            IsArray = isArray;
            Name = name;
            GRpcType = !string.IsNullOrWhiteSpace(gRpcType) ? gRpcType : type;
            Type = type1;
            Index = index;
            CommentMetaData = commentMetaData;
            ClassMetaData = classMetaData;
            EnumMetaData = enumMetaData;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            //GRpc json，属性小写，否则大写
            var name = CodeRender.Config.JsonTranscoding.UseJsonTranscoding ? Name.ToFirstLowString() : Name;
            return RenderHelper.BuildMessageItem(name,
                Type,
                IsArray,
                IsNullable,
                Index,
                CodeRender.Config.Proto.PropertyDescriptionFunc(CommentMetaData));
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetIndex(int index)
        {
            Index = index;
        }
    }

    public class EmptyProtoMessage : ProtoMessage
    {
        public EmptyProtoMessage() : base("google.protobuf.Empty", "google/protobuf/empty", true)
        {

        }
    }

    public class EnumProtoMessage : ProtoMessage
    {
        private readonly EnumMetaData _enumMetaData;

        public EnumProtoMessage(string name, List<ProtoItemMessage> items, EnumMetaData enumMetaData) : base(name, items, enumMetaData)
        {
            _enumMetaData = enumMetaData;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"\t//{CodeRender.Config.Proto.PropertyDescriptionFunc(EnumMetaData)} {_enumMetaData?.FullName ?? Name}");
            sb.AppendLine($"\tenum {this.FormatMessageName()} " + "{");

            foreach (var item in Items)
            {
                sb.AppendLine("\t\t" + item.ToString());
            }
            sb.AppendLine("\t}");
            return sb.ToString();
        }
    }
}