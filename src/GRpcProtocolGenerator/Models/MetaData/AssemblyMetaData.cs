using System.Collections.Generic;
using System.Linq;
using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Types;

namespace GRpcProtocolGenerator.Models.MetaData;

public class AssemblyMetaData
{
    public string? Name { get; private set; }

    public string? FullName { get; private set; }

    /// <summary>
    /// Interface 元数据
    /// </summary>
    public Dictionary<string, InterfaceMetaData> InterfaceMetaDataDictionary { get; private set; }

    /// <summary>
    /// 类元数据
    /// </summary>
    public Dictionary<string, ClassMetaData> ClassMetaDataDictionary { get; private set; }

    /// <summary>
    /// enum 元数据
    /// </summary>
    public Dictionary<string, EnumMetaData> EnumMetaDataDictionary { get; private set; }

    /// <summary>
    /// 不支持的方法（含原因）
    /// </summary>
    public List<NotSupportedMethodInfo> NotSupportMethodList { get; private set; }

    public AssemblyMetaData(string? name, string? fullName,
        Dictionary<string, InterfaceMetaData> interfaceMetaDataDictionary,
        Dictionary<string, ClassMetaData> classMetaDataDictionary,
        Dictionary<string, EnumMetaData> enumMetaDataDictionary,
        List<NotSupportedMethodInfo> notSupportMethodList,
        Config config)
    {
        Name = name;
        FullName = fullName;
        InterfaceMetaDataDictionary = interfaceMetaDataDictionary;
        ClassMetaDataDictionary = classMetaDataDictionary;
        EnumMetaDataDictionary = enumMetaDataDictionary;
        NotSupportMethodList = notSupportMethodList;

        //过滤不支持的方法和属性
        ClearNotSupport();

        //数据过滤
        Filter(config);

        //处理 method 对应的接口元数据
        SetMethodInterface();
    }

    /// <summary>
    /// 过滤不支持的方法和属性
    /// </summary>
    private void ClearNotSupport()
    {
        foreach (var pair in InterfaceMetaDataDictionary)
        {
            var newList = pair.Value.MethodMetaDataList
                .Where(d => d.MethodInfo.CustomAttributes.IsGRpcIgnore() == false)
                .Where(d => d.InParamMetaDataList.All(p => p.TypeWrapper.IsSupport()) && d.OutParamMetaDataList.All(p => p.TypeWrapper.IsSupport()))
                .ToList();

            if (newList.Count != pair.Value.MethodMetaDataList.Count)
            {
                pair.Value.MethodMetaDataList.Clear();
                pair.Value.MethodMetaDataList.AddRange(newList);
            }
        }
    }

    /// <summary>
    /// 数据过滤
    /// </summary>
    private void Filter(Config config)
    {
        var filter = config?.Filter;
        if (filter == null)
            return;

        if (filter.InterfaceFilterFunc != null)
        {
            InterfaceMetaDataDictionary = InterfaceMetaDataDictionary.Select(d => d.Value)
                .Where(d => filter.InterfaceFilterFunc(d)).ToDictionary(d => d.FullName!, d => d);
        }

        if (filter.MethodFilterFunc != null)
        {
            foreach (var pair in InterfaceMetaDataDictionary)
            {
                var newList = pair.Value.MethodMetaDataList.Where(d => filter.MethodFilterFunc(pair.Value, d)).ToList();
                if (newList.Count != pair.Value.MethodMetaDataList.Count)
                {
                    pair.Value.MethodMetaDataList.Clear();
                    pair.Value.MethodMetaDataList.AddRange(newList);
                }
            }
        }
    }

    /// <summary>
    /// 处理 method 对应的接口元数据
    /// </summary>
    private void SetMethodInterface()
    {
        foreach (var pair in InterfaceMetaDataDictionary)
        {
            pair.Value.SetMethodInterface();
        }
    }
}
