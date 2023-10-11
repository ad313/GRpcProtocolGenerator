using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GRpcProtocolGenerator.Common.Attributes;
using Sample.Services.Models;

namespace Sample.Services
{
    /// <summary>
    /// IServiceTest
    /// </summary>
    [Description("desc from Description")]
    [GRpcGenerator]
    public interface IServiceTest
    {
        #region 同步

        [HttpGet("VoidMethodSync")]
        [Display(Name = "title from Display attr")]
        [DisplayName("title from DisplayName attr")]
        [Description("title from Description attr")]
        void VoidMethodSync();

        void MethodWithInputReturnVoidSync(int a);

        void MethodWithInputReturnVoid2Sync(int a, string b);

        [HttpPost("MethodWithInputReturnVoid3Sync")]
        void MethodWithInputReturnVoid3Sync(SampleClass a);

        void MethodWithInputReturnVoid4Sync(List<int> a);

        void MethodWithInputEnumReturnVoid4Sync(ApplicationEnumType a);

        [HttpGet("MethodWithInputTreeReturnVoid5Sync")]
        void MethodWithInputTreeReturnVoid5Sync(TreeNode<SampleClass> a, TreeClass b, ExtClass c, int d);

        #endregion
        
        #region 异步

        Task VoidMethodAsync();

        Task MethodWithInputReturnVoidAsync(int a);

        Task MethodWithInputReturnVoid2Async(int a, string b);

        [HttpPost("MethodWithInputReturnVoid3Async")]
        Task MethodWithInputReturnVoid3Async(SampleClass a);

        Task MethodWithInputReturnVoid4Async(List<int> a);

        Task MethodWithInputEnumReturnVoid4Async(ApplicationEnumType a);

        #endregion

        #region 同步

        int MethodWithInputReturnIntSync(int a);

        string MethodWithInputReturnStringSync(int a, string b);

        SampleClass MethodWithInputReturnClassSync(int a, string b);

        [HttpPost("MethodWithInputReturnListClassSync")]
        List<SampleClass> MethodWithInputReturnListClassSync(SampleClass a);

        [HttpPost("MethodWithInputReturnListIntSync")]
        List<int> MethodWithInputReturnListIntSync(SampleClass a);

        ApplicationEnumType MethodWithInputReturnEnumSync(ApplicationEnumType a);

        #endregion

        #region 异步

        Task<int> MethodWithInputReturnIntAsync(int a);

        Task<string> MethodWithInputReturnStringAsync(int a, string b);

        Task<SampleClass> MethodWithInputReturnClassAsync(int a, string b);

        Task<NullableClass> MethodWithInputReturnNullableClassAsync(int a, string b);

        [HttpPost("MethodWithInputReturnListClassAsync")]
        Task<List<SampleClass>> MethodWithInputReturnListClassAsync(SampleClass a);

        [HttpPost("MethodWithInputReturnListIntAsync")]
        Task<List<int>> MethodWithInputReturnListIntAsync(SampleClass a);

        Task<ApplicationEnumType> MethodWithInputReturnEnumAsync(ApplicationEnumType a);

        ValueTask<ApplicationEnumType> MethodWithInputReturnEnumValueTaskAsync(ApplicationEnumType a);

        #endregion

        #region todo 支持

        void ChangeToSupportMethod7(object a);

        #endregion

        #region 不支持的方法：字典、嵌套列表、嵌套 Task、GRpcIgnore、object、StringBuilder

        Dictionary<int, int> NotSupportMethod1(int a);

        Task<Dictionary<int, int>> NotSupportMethod2(int a);

        Task<Task<Dictionary<int, int>>> NotSupportMethod3(int a);

        void NotSupportMethod4(List<List<int>> a);

        void NotSupportMethod5(Dictionary<int, int> a);

        [GRpcIgnore]
        void NotSupportMethod6(Dictionary<int, int> a);
        
        StringBuilder NotSupportMethod8();

        #endregion

        #region 其他

        void TestCancellationToken(CancellationToken token);

        void TestCancellationToken2(int a, CancellationToken token);

        [HttpPost("TestExtClass2")]
        void TestExtClass2(ExtClass2 a);

        #endregion
    }

    [Description("enum desc from Description")]
    public enum ApplicationEnumType
    {
        [Description("One desc from Description")]
        One,
        [Description("Two desc from Description")]
        Two = 12,
    }

    [Description("Struct desc from Description")]
    public struct ApplicationStruct
    {
        [Description("Age desc from Description")]
        public int Age { get; set; }

        [Display(Name = "Name desc from Description")]
        public string Name { get; set; }
    }
}