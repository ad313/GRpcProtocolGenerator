using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Types;
using Namotion.Reflection;

namespace GRpcProtocolGenerator.Resolve;

/// <summary>
/// 元数据解析
/// </summary>
public class MetaDataResolve
{
    private readonly Dictionary<string, ClassMetaData> _classMetaDataDictionary = [];
    private readonly Dictionary<string, EnumMetaData> _enumMetaDataDictionary = [];

    private readonly List<NotSupportedMethodInfo> _notSupportMethodList = [];

    public AssemblyMetaData Resolve(Assembly assembly, Config config)
    {
        var interfaces = assembly.GetTypes().Where(d => d.IsInterface).ToList();

        return new AssemblyMetaData(assembly.GetName().Name,
            assembly.FullName,
            ToInterfaceMetaDataList(interfaces).ToDictionary(d => d.FullName!, d => d),
            _classMetaDataDictionary,
            _enumMetaDataDictionary,
            _notSupportMethodList,
            config);
    }

    #region Interface

    public List<InterfaceMetaData> ToInterfaceMetaDataList(List<Type> interfaces)
    {
        if (interfaces == null || interfaces.Count == 0)
            return [];

        var nodes = interfaces
            .Where(d => d.IsGRpcGenerator() && !d.IsGRpcIgnore())
            .Select(d => ToInterfaceMetaData(d.ToTypeWrapper()))
            .OfType<InterfaceMetaData>()
            .ToList();
        nodes = Distinct(nodes);
        return Format(nodes, null);
    }

    private InterfaceMetaData? ToInterfaceMetaData(TypeWrapper typeWrapper)
    {
        var type = typeWrapper.Type;
        if (type == null)
            return null;

        var node = new InterfaceMetaData(type.Namespace, type.Name, type,
            ToAttributeMetaDataList(type.CustomAttributes.ToList()),
            ToParamMetaDataList(type.GenericTypeArguments.ToList()),
            ToMethodMetaDataList(type.GetMethods().ToList()));

        // Children
        var childrenTypes = ((TypeInfo)type).ImplementedInterfaces;
        foreach (var childrenType in childrenTypes)
        {
            var child = ToInterfaceMetaData(childrenType.ToTypeWrapper());
            child?.SetParent(node);
            node.Children.Add(child!);
        }

        //设置 Key
        node.SetKey();

        return node;
    }

    /// <summary>
    /// 过滤重复接口
    /// </summary>
    private List<InterfaceMetaData> Distinct(List<InterfaceMetaData> nodes)
    {
        var nodeDic = nodes.GroupBy(d => d.Key ?? "").ToDictionary(d => d.Key, d => d.First());
        nodes = nodeDic.Select(d => d.Value).ToList();

        foreach (var node in nodes)
        {
            foreach (var treeNode in nodes.Where(d => d.Key != node.Key && d.Enable))
            {
                if (treeNode.Exists(node.Key))
                {
                    node.SetEnable(false);
                    node.Children.Clear();
                }

                if (node.Enable)
                {
                    Distinct(node.Children);
                }
            }

            node.SetChildren(node.Children.Where(d => d.Enable).ToList());
        }

        return nodes.Where(d => d.Enable && !d.Type.IsGenericType).ToList();
    }

    /// <summary>
    /// 处理接口，把所有基类上接口放到子节点
    /// </summary>
    private List<InterfaceMetaData> Format(List<InterfaceMetaData> nodes, InterfaceMetaData? parent)
    {
        if (nodes.Count == 0)
            return nodes;

        foreach (var node in nodes)
        {
            if (node.Children.Any())
            {
                Format(node.Children, node);
            }

            MergeMethods(node, parent);
        }

        return nodes;
    }

    private void MergeMethods(InterfaceMetaData source, InterfaceMetaData? target)
    {
        if (source == null || target == null)
            return;

        foreach (var method in source.MethodMetaDataList)
        {
            //已存在
            var exists = target.MethodMetaDataList.FirstOrDefault(d => d.Key == method.Key) ??
                         target.MethodMetaDataList.FirstOrDefault(d => d.GetKey() == method.GetKey());

            if (exists != null)
            {
                MergeAttributes(method, exists);
            }
            else
            {
                target.MethodMetaDataList.Add(method);
            }
        }
    }

    private void MergeAttributes(MethodMetaData source, MethodMetaData target)
    {
        if (source == null || target == null)
            return;

        foreach (var attr in source.AttributeMetaDataList)
        {
            if (!target.AttributeMetaDataList.Exists(d => d.Type == attr.Type))
            {
                target.AttributeMetaDataList.Add(attr);
            }
        }
    }

    #endregion

    #region ParamMetaData

    private List<PropertyMetaData> ToParamMetaDataList(List<Type> types)
    {
        if (types == null || types.Count == 0)
            return [];

        return types.Select(d => ToParamMetaData(d.ToTypeWrapper())).OfType<PropertyMetaData>().ToList();
    }

    private PropertyMetaData? ToParamMetaData(TypeWrapper typeWrapper, ParameterInfo? info = null)
    {
        var type = typeWrapper.Type;
        if (type == null)
        {
            return null;
        }

        (ClassMetaData? meta, bool target) classMetaData = (null, false);
        if (typeWrapper.IsClass || typeWrapper.IsStruct)
        {
            classMetaData = ToClassMetaData(typeWrapper, []);
        }

        var enumMeta = typeWrapper.IsEnum ? ToEnumMetaData(typeWrapper) : null;

        //处理必填项，当如果项目启用了 nullable
        if (typeWrapper.Type == typeof(string))
        {
            typeWrapper.SetNullable(info?.ToContextualParameter().Nullability != Nullability.NotNullable);
        }

        return new PropertyMetaData(typeWrapper, info?.Name, ToAttributeMetaDataList(info?.CustomAttributes.ToList()), classMetaData.meta, enumMeta);
    }

    #endregion

    #region MethodMeta

    private List<MethodMetaData> ToMethodMetaDataList(List<MethodInfo> methodInfos)
    {
        if (methodInfos == null || methodInfos.Count == 0)
            return [];

        return methodInfos.Select(ToMethodMetaData).OfType<MethodMetaData>().ToList();
    }

    private MethodMetaData? ToMethodMetaData(MethodInfo methodInfo)
    {
        if (methodInfo == null)
            return null;

        //检测 gRPC 流式（IAsyncEnumerable<T>）
        var streaming = DetectStreaming(methodInfo);
        if (streaming.ErrorMessage != null)
        {
            _notSupportMethodList.Add(new NotSupportedMethodInfo(methodInfo, streaming.ErrorMessage));
            return null;
        }

        //过滤不支持的方法，并记录具体原因
        var parameters = methodInfo.GetParameters().Select(p => (Parameter: p, Wrapper: p.ParameterType.ToTypeWrapper())).ToList();
        foreach (var (parameter, wrapper) in parameters)
        {
            //流式参数：校验元素类型而非 IAsyncEnumerable<T> 本身
            var checkWrapper = parameter.ParameterType.IsAsyncEnumerable() && streaming.RequestElementType != null
                ? streaming.RequestElementType.ToTypeWrapper()
                : wrapper;

            if (!checkWrapper.IsSupport())
            {
                _notSupportMethodList.Add(new NotSupportedMethodInfo(methodInfo,
                    $"参数 '{parameter.Name}' 的类型 '{parameter.ParameterType.FullName}' 不受支持：{GetUnsupportedReason(checkWrapper)}"));
                return null;
            }
        }

        var returnWrapper = methodInfo.ReturnType.ToTypeWrapper();
        //流式响应：校验元素类型；非流式：校验返回值本身
        var checkReturnWrapper = streaming.ResponseElementType != null
            ? streaming.ResponseElementType.ToTypeWrapper()
            : returnWrapper;
        if (!checkReturnWrapper.IsSupport())
        {
            _notSupportMethodList.Add(new NotSupportedMethodInfo(methodInfo,
                $"返回值类型 '{methodInfo.ReturnType.FullName}' 不受支持：{GetUnsupportedReason(checkReturnWrapper)}"));
            return null;
        }

        //传入参数（保留原始签名，含流式参数与 CancellationToken）
        var inParam = parameters.Select(x => ToParamMetaData(x.Wrapper, x.Parameter)).ToList();

        //传出参数
        var outParam = new List<PropertyMetaData>();
        PropertyMetaData? streamResponseElement = null;
        if (streaming.Kind is StreamKind.ServerStreaming or StreamKind.DuplexStreaming)
        {
            //服务端流：响应消息按元素类型生成
            streamResponseElement = ToParamMetaData(streaming.ResponseElementType!.ToTypeWrapper());
            if (streamResponseElement == null)
            {
                _notSupportMethodList.Add(new NotSupportedMethodInfo(methodInfo,
                    $"方法 '{methodInfo.Name}' 的响应流元素类型 '{streaming.ResponseElementType!.FullName}' 无法解析"));
                return null;
            }

            outParam.Add(streamResponseElement);
        }
        else
        {
            var outTypeWrapper = methodInfo.ReturnType.ToTypeWrapper();
            if (outTypeWrapper.Type != null)
            {
                outParam.Add(ToParamMetaData(outTypeWrapper)!);
            }
        }

        //流式请求元素元数据（客户端流/双向流：用于生成请求消息）
        PropertyMetaData? streamRequestElement = null;
        if (streaming.Kind is StreamKind.ClientStreaming or StreamKind.DuplexStreaming)
        {
            var streamParameter = methodInfo.GetParameters().First(p => p.ParameterType.IsAsyncEnumerable());
            streamRequestElement = ToParamMetaData(streaming.RequestElementType!.ToTypeWrapper(), streamParameter);
            if (streamRequestElement == null)
            {
                _notSupportMethodList.Add(new NotSupportedMethodInfo(methodInfo,
                    $"方法 '{methodInfo.Name}' 的请求流元素类型 '{streaming.RequestElementType!.FullName}' 无法解析"));
                return null;
            }
        }

        return new MethodMetaData(methodInfo.DeclaringType?.Namespace, methodInfo.Name, methodInfo,
            ToAttributeMetaDataList(methodInfo.CustomAttributes.ToList()), inParam!, outParam,
            streaming.Kind, streamRequestElement, streamResponseElement);
    }

    /// <summary>
    /// 检测 gRPC 流式方法
    /// 约定：
    /// <list type="bullet">
    /// <item>返回 <c>IAsyncEnumerable&lt;T&gt;</c>（或 <c>Task&lt;IAsyncEnumerable&lt;T&gt;&gt;</c>）→ 服务端流</item>
    /// <item>参数为 <c>IAsyncEnumerable&lt;T&gt;</c>（唯一业务参数，<c>CancellationToken</c> 除外）→ 客户端流</item>
    /// <item>两者兼有 → 双向流</item>
    /// </list>
    /// </summary>
    private (StreamKind Kind, Type? RequestElementType, Type? ResponseElementType, string? ErrorMessage) DetectStreaming(MethodInfo methodInfo)
    {
        Type? requestElementType = null;
        Type? responseElementType = null;

        //返回值：解开 Task/ValueTask 后判断是否为 IAsyncEnumerable<T>
        var returnType = methodInfo.ReturnType.ToTypeWrapper().Type;
        if (returnType != null && returnType.IsAsyncEnumerable())
        {
            responseElementType = returnType.GetAsyncEnumerableElementType();
        }

        //参数：查找 IAsyncEnumerable<T> 参数
        var allParameters = methodInfo.GetParameters();
        var streamParameters = allParameters.Where(p => p.ParameterType.IsAsyncEnumerable()).ToList();
        if (streamParameters.Count > 0)
        {
            var businessParameters = allParameters.Where(p => p.ParameterType != typeof(CancellationToken)).ToList();
            if (streamParameters.Count > 1)
            {
                return (StreamKind.None, null, null,
                    $"方法 '{methodInfo.Name}' 有 {streamParameters.Count} 个 IAsyncEnumerable<T> 参数，gRPC 只支持一个流式参数");
            }

            if (businessParameters.Count != 1)
            {
                return (StreamKind.None, null, null,
                    $"方法 '{methodInfo.Name}' 的流式参数不能与其他业务参数混用，流式参数必须是唯一的业务参数（CancellationToken 除外）");
            }

            requestElementType = streamParameters[0].ParameterType.GetAsyncEnumerableElementType();
        }

        //校验元素类型
        foreach (var (label, elementType) in new[] { ("请求", requestElementType), ("响应", responseElementType) })
        {
            if (elementType == null)
                continue;

            if (elementType.IsAsyncEnumerable())
            {
                return (StreamKind.None, null, null,
                    $"方法 '{methodInfo.Name}' 的{label}流元素类型 '{elementType.FullName}' 不受支持：不支持嵌套的 IAsyncEnumerable");
            }

            var elementWrapper = elementType.ToTypeWrapper();
            if (elementWrapper.IsTask)
            {
                return (StreamKind.None, null, null,
                    $"方法 '{methodInfo.Name}' 的{label}流元素类型 '{elementType.FullName}' 不受支持：元素类型不能是 Task");
            }

            if (!elementWrapper.IsSupport())
            {
                return (StreamKind.None, null, null,
                    $"方法 '{methodInfo.Name}' 的{label}流元素类型 '{elementType.FullName}' 不受支持：{GetUnsupportedReason(elementWrapper)}");
            }
        }

        var kind = (requestElementType, responseElementType) switch
        {
            (not null, not null) => StreamKind.DuplexStreaming,
            (not null, null) => StreamKind.ClientStreaming,
            (null, not null) => StreamKind.ServerStreaming,
            _ => StreamKind.None,
        };

        return (kind, requestElementType, responseElementType, null);
    }

    private static string GetUnsupportedReason(TypeWrapper wrapper)
    {
        if (!string.IsNullOrWhiteSpace(wrapper.ErrorMessage))
            return wrapper.ErrorMessage;

        if (wrapper.IsDictionary || wrapper.Child?.IsDictionary == true)
            return "不支持字典类型（Dictionary<TKey, TValue>），proto3 的 map 字段需要显式建模";

        if (wrapper.IsTuple || wrapper.Child?.IsTuple == true)
            return "不支持 Tuple 类型，请使用具名类代替";

        return "不支持的类型";
    }

    #endregion

    #region ClassMeta

    private (ClassMetaData?, bool) ToClassMetaData(TypeWrapper typeWrapper, List<Type> parentType)
    {
        if (!typeWrapper.IsClass && !typeWrapper.IsStruct)
            return (null, false);

        // 无限循环，当子类和父类一样，需要特殊处理
        if (parentType != null && parentType.Contains(typeWrapper.Type!))
        {
            return (null, true);
        }

        parentType?.Add(typeWrapper.Type!);

        if (_classMetaDataDictionary.TryGetValue(typeWrapper.Type!.FullName ?? "", out ClassMetaData? value))
            return (value, false);

        var actionList = new List<Action<ClassMetaData>>();
        var props = new List<PropertyMetaData>();
        foreach (var info in typeWrapper.Type.GetProperties().Where(d => d.CanRead))
        {
            var infoType = info.PropertyType.ToTypeWrapper();

            //过滤不支持的类型
            if (!infoType.IsSupport())
                continue;

            var attributes = ToAttributeMetaDataList(info.CustomAttributes.ToList());
            (ClassMetaData? meta, bool target) currentClass = ToClassMetaData(infoType, parentType!);
            var enumMeta = infoType.IsEnum ? ToEnumMetaData(infoType) : null;

            //处理必填项，当如果项目启用了 nullable
            if (infoType.Type == typeof(string))
            {
                infoType.SetNullable(info.ToContextualProperty().Nullability != Nullability.NotNullable);
            }

            var prop = new PropertyMetaData(infoType, info.Name, attributes, !currentClass.target ? currentClass.meta : null, enumMeta);
            props.Add(prop);

            //无限循环
            if (currentClass.target)
                actionList.Add(data => { prop.SetClassMetaData(data); });
        }

        ClassMetaData? classMetaData = new(typeWrapper, typeWrapper.Type.Namespace, typeWrapper.Type.Name, ToAttributeMetaDataList(typeWrapper.Type.CustomAttributes.ToList()), props);
        if (string.IsNullOrWhiteSpace(typeWrapper.Type.FullName))
        {
            classMetaData = null;
        }
        else
        {
            _classMetaDataDictionary.TryAdd(typeWrapper.Type.FullName, classMetaData);

            //无限循环
            foreach (var action in actionList)
            {
                action.Invoke(classMetaData);
            }
        }

        return (classMetaData, false);
    }

    #endregion

    #region EnumMetaData

    private EnumMetaData? ToEnumMetaData(TypeWrapper typeWrapper)
    {
        if (!typeWrapper.IsEnum)
            return null;

        if (_enumMetaDataDictionary.TryGetValue(typeWrapper.Type!.FullName ?? "", out EnumMetaData? value))
        {
            return value;
        }

        var fields = ((TypeInfo)typeWrapper.Type).DeclaredFields.ToList();
        var members = fields.Count <= 1
            ? new List<EnumMemberMetaData>()
            : new List<EnumMemberMetaData>(fields.Count - 1);

        var index = 1;
        foreach (Enum itemValue in Enum.GetValues(typeWrapper.Type))
        {
            members.Add(new EnumMemberMetaData(itemValue.ToString(), Convert.ToInt32(itemValue), ToAttributeMetaDataList(fields[index].CustomAttributes.ToList())));
            index++;
        }

        var metaData = new EnumMetaData(typeWrapper.Type.Namespace, typeWrapper.Type.Name, ToAttributeMetaDataList(typeWrapper.Type.CustomAttributes.ToList()), members);
        _enumMetaDataDictionary.TryAdd(typeWrapper.Type.FullName ?? "", metaData);
        return metaData;
    }

    #endregion

    #region AttributeMetaData

    private List<AttributeMetaData> ToAttributeMetaDataList(List<CustomAttributeData>? attrs)
    {
        if (attrs == null || attrs.Count == 0)
            return [];

        var list = new List<AttributeMetaData>(attrs.Count);
        foreach (var attr in attrs)
        {
            //过滤编译器生成的可空注解特性（如启用 <Nullable> 后自动附加的 Nullable/NullableContext），避免污染元数据
            if (attr.AttributeType.Namespace == "System.Runtime.CompilerServices" &&
                (attr.AttributeType.Name is "NullableAttribute" or "NullableContextAttribute"))
            {
                continue;
            }

            var constructorArgumentsDictionary = new Dictionary<int, object?>();
            for (var i = 0; i < attr.ConstructorArguments.Count; i++)
            {
                //注意：必须取第 i 个构造参数，之前误写为固定取第 0 个
                constructorArgumentsDictionary.Add(i, attr.ConstructorArguments[i].Value);
            }

            var data = new AttributeMetaData(attr.AttributeType,
                attr.NamedArguments?.ToDictionary(d => d.MemberName, d => d.TypedValue.Value),
                constructorArgumentsDictionary);

            list.Add(data);
        }

        return list;
    }

    #endregion
}
