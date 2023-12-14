using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Renders.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRpcProtocolGenerator.Renders.GoLang
{
    //public class ProtocolDtoMessage : ProtocolMessage
    //{
    //    public ProtocolDtoMessage(string name, List<ProtocolDtoItemMessage> items, List<ProtocolMessage> classDependency, List<EnumProtocolMessage> enumDependency, bool isOriginalClass, bool isEmpty, ClassMetaData classMetaData) : base(name, items, classDependency, enumDependency, isOriginalClass, isEmpty, classMetaData)
    //    {
    //    }

    //    public ProtocolDtoMessage(string name, List<ProtocolDtoItemMessage> items, EnumMetaData enumMetaData) : base(name, items, enumMetaData)
    //    {
    //    }

    //    public ProtocolDtoMessage(string name, string messagePath, bool isEmpty) : base(name, messagePath, isEmpty)
    //    {
    //    }

    //    /// <summary>Returns a string that represents the current object.</summary>
    //    /// <returns>A string that represents the current object.</returns>
    //    public override string ToString()
    //    {
    //        var sb = new StringBuilder();
    //        sb.AppendLine($"//{Config.ConfigInstance.Proto.PropertyDescriptionFunc(ClassMetaData)} {ClassMetaData?.FullName ?? Name}");
    //        sb.AppendLine($"type {GetGRpcName()} " + "struct {");

    //        var index = 1;
    //        foreach (var item in Items)
    //        {
    //            item.SetIndex(index);

    //            var text = item.ToString();
    //            if (string.IsNullOrWhiteSpace(text))
    //                continue;

    //            sb.AppendLine("\t" + text);
    //            index++;
    //        }

    //        sb.AppendLine("}");
    //        return sb.ToString();
    //    }
    //}

    //public class ProtocolDtoItemMessage: ProtocolItemMessage
    //{
    //    public ProtocolDtoItemMessage(Type type1, bool isNullable, bool isArray, string name, string gRpcType, string type, int index, CommentMetaData commentMetaData, ClassMetaData classMetaData, EnumMetaData enumMetaData) : base(type1, isNullable, isArray, name, gRpcType, type, index, commentMetaData, classMetaData, enumMetaData)
    //    {
    //    }

    //    /// <summary>Returns a string that represents the current object.</summary>
    //    /// <returns>A string that represents the current object.</returns>
    //    public override string ToString()
    //    {
    //        //GRpc json，属性小写，否则大写
    //        var name = Config.ConfigInstance.JsonTranscoding.UseJsonTranscoding ? Name.ToFirstLowString() : Name;
    //        return BuilderPart.BuildMessageItem(name,
    //            Type,
    //            IsArray,
    //            IsNullable,
    //            Index,
    //            Config.ConfigInstance.Proto.PropertyDescriptionFunc(CommentMetaData));
    //    }
    //}

}