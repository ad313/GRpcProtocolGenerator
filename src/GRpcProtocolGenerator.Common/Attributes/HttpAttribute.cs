using System;

namespace GRpcProtocolGenerator.Common.Attributes;

/// <summary>
/// Http 路由标记基类
/// </summary>
public abstract class HttpMethodAttribute : Attribute
{
    /// <summary>
    /// 路由内容
    /// </summary>
    public string? Template { get; set; }

    protected HttpMethodAttribute() { }

    protected HttpMethodAttribute(string template)
    {
        Template = template;
    }
}

/// <summary>
/// HttpGet
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class HttpGetAttribute : HttpMethodAttribute
{
    public HttpGetAttribute() { }

    public HttpGetAttribute(string template) : base(template) { }
}

/// <summary>
/// HttpPost
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class HttpPostAttribute : HttpMethodAttribute
{
    public HttpPostAttribute() { }

    public HttpPostAttribute(string template) : base(template) { }
}

/// <summary>
/// HttpPut
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class HttpPutAttribute : HttpMethodAttribute
{
    public HttpPutAttribute() { }

    public HttpPutAttribute(string template) : base(template) { }
}

/// <summary>
/// HttpDelete
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class HttpDeleteAttribute : HttpMethodAttribute
{
    public HttpDeleteAttribute() { }

    public HttpDeleteAttribute(string template) : base(template) { }
}

/// <summary>
/// HttpPatch
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class HttpPatchAttribute : HttpMethodAttribute
{
    public HttpPatchAttribute() { }

    public HttpPatchAttribute(string template) : base(template) { }
}
