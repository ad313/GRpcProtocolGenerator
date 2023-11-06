using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GRpcProtocolGenerator.Models.Configs;
using GRpcProtocolGenerator.Models.MetaData;
using GRpcProtocolGenerator.Types;
using Namotion.Reflection;

namespace GRpcProtocolGenerator.Resolve
{
    /// <summary>
    /// 元数据解析
    /// </summary>
    public class MetaDataResolve
    {
        private readonly Dictionary<string, ClassMetaData> _classMetaDataDictionary = new Dictionary<string, ClassMetaData>();
        private readonly Dictionary<string, EnumMetaData> _enumMetaDataDictionary = new Dictionary<string, EnumMetaData>();

        private readonly List<MethodInfo> _notSupportMethodList = new List<MethodInfo>();

        public AssemblyMetaData Resolve(Assembly assembly, Config config)
        {
            var interfaces = assembly.GetTypes().Where(d => d.IsInterface).ToList();

            return new AssemblyMetaData(assembly.GetName().Name,
                assembly.FullName,
                ToInterfaceMetaDataList(interfaces).ToDictionary(d => d.FullName, d => d),
                _classMetaDataDictionary,
                _enumMetaDataDictionary,
                _notSupportMethodList,
                config);
        }

        #region Interface

        public List<InterfaceMetaData> ToInterfaceMetaDataList(List<Type> interfaces)
        {
            if (interfaces == null || interfaces.Count == 0)
                return new List<InterfaceMetaData>();

            var nodes = interfaces.Where(d => d.IsGRpcGenerator() && !d.IsGRpcIgnore()).Select(d => ToInterfaceMetaData(d.ToTypeWrapper())).Where(d => d != null).ToList();
            nodes = Distinct(nodes);
            return Format(nodes, null);
        }

        private InterfaceMetaData ToInterfaceMetaData(TypeWrapper typeWrapper)
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
                child.SetParent(node);
                node.Children.Add(child);
            }

            //设置 Key
            node.SetKey();

            return node;
        }

        /// <summary>
        /// 过滤重复接口
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
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
        /// <param name="nodes"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private List<InterfaceMetaData> Format(List<InterfaceMetaData> nodes, InterfaceMetaData parent)
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

        private void MergeMethods(InterfaceMetaData source, InterfaceMetaData target)
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
                return new List<PropertyMetaData>();

            return types.Select(d => ToParamMetaData(d.ToTypeWrapper())).ToList();
        }

        private PropertyMetaData ToParamMetaData(TypeWrapper typeWrapper, ParameterInfo info = null)
        {
            var type = typeWrapper.Type;
            if (type == null)
            {
                //return new PropertyMetaData(typeWrapper, info?.Name, null, null, null);
                return null;
            }

            (ClassMetaData meta, bool target) classMetaData = (null, false);
            if (typeWrapper.IsClass || typeWrapper.IsStruct)
            {
                classMetaData = ToClassMetaData(typeWrapper, new List<Type>());
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
                return new List<MethodMetaData>();

            return methodInfos.Select(ToMethodMetaData).Where(d => d != null).ToList();
        }

        private MethodMetaData ToMethodMetaData(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                return null;

            //过滤不支持的方法
            if (methodInfo.GetParameters().Any(d => d.ParameterType.ToTypeWrapper().IsSupport() == false) ||
                methodInfo.ReturnType.ToTypeWrapper().IsSupport() == false)
            {
                _notSupportMethodList.Add(methodInfo);
                return null;
            }

            //传入参数
            var inParam = methodInfo.GetParameters().Select(d => ToParamMetaData(d.ParameterType.ToTypeWrapper(), d)).ToList();

            //传出参数
            var outParam = new List<PropertyMetaData>();
            var outTypeWrapper = methodInfo.ReturnType.ToTypeWrapper();
            if (outTypeWrapper.Type != null)
            {
                outParam.Add(ToParamMetaData(outTypeWrapper));
            }

            return new MethodMetaData(methodInfo.DeclaringType?.Namespace, methodInfo.Name, methodInfo, ToAttributeMetaDataList(methodInfo.CustomAttributes.ToList()), inParam, outParam);
        }

        #endregion

        #region ClassMeta

        private (ClassMetaData, bool) ToClassMetaData(TypeWrapper typeWrapper, List<Type> parentType)
        {
            if (!typeWrapper.IsClass && !typeWrapper.IsStruct)
                return (null, false);

            // todo 无限循环，当子类和父类一样，需要特殊处理
            if (parentType != null && parentType.Contains(typeWrapper.Type))
            {
                return (null, true);
            }

            parentType?.Add(typeWrapper.Type);

            if (_classMetaDataDictionary.TryGetValue(typeWrapper.Type.FullName ?? "", out ClassMetaData value))
                return (value, false);

            //不处理父级
            //(ClassMetaData meta, bool target) parentClass = ToClassMetaData(typeWrapper.Type.BaseType.FormatType(), typeWrapper.Type);
            (ClassMetaData meta, bool target) parentClass = (null, false);

            var actionList = new List<Action<ClassMetaData>>();
            var props = new List<PropertyMetaData>();
            foreach (var info in typeWrapper.Type.GetProperties().Where(d => d.CanRead))
            {
                var infoType = info.PropertyType.ToTypeWrapper();

                //过滤不支持的类型
                if (infoType.IsSupport() == false)
                    continue;

                var attributes = ToAttributeMetaDataList(info.CustomAttributes.ToList());
                (ClassMetaData meta, bool target) currentClass = ToClassMetaData(infoType, parentType);
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

            var classMetaData = new ClassMetaData(typeWrapper, typeWrapper.Type.Namespace, typeWrapper.Type.Name, ToAttributeMetaDataList(typeWrapper.Type.CustomAttributes.ToList()), props);
            if (string.IsNullOrWhiteSpace(typeWrapper.Type.FullName))
            {
                classMetaData = null;
            }
            else
            {
                _classMetaDataDictionary.TryAdd(typeWrapper.Type.FullName, classMetaData);

                ////无限循环
                //if (parentClass.target)
                //    classMetaData.SetParent(classMetaData);

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

        private EnumMetaData ToEnumMetaData(TypeWrapper typeWrapper)
        {
            if (!typeWrapper.IsEnum)
                return null;

            if (_enumMetaDataDictionary.TryGetValue(typeWrapper.Type.FullName ?? "", out EnumMetaData value))
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

        private List<AttributeMetaData> ToAttributeMetaDataList(List<CustomAttributeData> attrs)
        {
            if (attrs == null || !attrs.Any())
                return new List<AttributeMetaData>();

            var list = new List<AttributeMetaData>();
            foreach (var attr in attrs)
            {
                var constructorArgumentsDictionary = new Dictionary<int, object>();
                for (var i = 0; i < attr.ConstructorArguments.Count; i++)
                {
                    constructorArgumentsDictionary.Add(i, attr.ConstructorArguments[0].Value);
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
}