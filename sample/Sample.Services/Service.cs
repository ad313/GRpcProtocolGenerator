using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sample.Services.Models;

namespace Sample.Services
{
    public class Service: IServiceTest
    {
        public void VoidMethodSync()
        {
            throw new NotImplementedException();
        }

        public void MethodWithInputReturnVoidSync(int a)
        {
            throw new NotImplementedException();
        }

        public void MethodWithInputReturnVoid2Sync(int a, string b)
        {
            throw new NotImplementedException();
        }

        public void MethodWithInputReturnVoid3Sync(SampleClass a)
        {
            throw new NotImplementedException();
        }

        public void MethodWithInputReturnVoid4Sync(List<int> a)
        {
            throw new NotImplementedException();
        }

        public void MethodWithInputEnumReturnVoid4Sync(ApplicationEnumType a)
        {
            throw new NotImplementedException();
        }

        public void MethodWithInputTreeReturnVoid5Sync(TreeNode<SampleClass> a, TreeClass b, ExtClass c, int d)
        {
            throw new NotImplementedException();
        }
        
        public async Task VoidMethodAsync()
        {
            throw new NotImplementedException();
        }

        public async Task MethodWithInputReturnVoidAsync(int a)
        {
            throw new NotImplementedException();
        }

        public async Task MethodWithInputReturnVoid2Async(int a, string b)
        {
            throw new NotImplementedException();
        }

        public async Task MethodWithInputReturnVoid3Async(SampleClass a)
        {
            throw new NotImplementedException();
        }

        public async Task MethodWithInputReturnVoid4Async(List<int> a)
        {
            throw new NotImplementedException();
        }

        public async Task MethodWithInputEnumReturnVoid4Async(ApplicationEnumType a)
        {
            throw new NotImplementedException();
        }

        public int MethodWithInputReturnIntSync(int a)
        {
            throw new NotImplementedException();
        }

        public string MethodWithInputReturnStringSync(int a, string b)
        {
            throw new NotImplementedException();
        }

        public SampleClass MethodWithInputReturnClassSync(int a, string b)
        {
            return new SampleClass()
            {
                DateTimeColumn = DateTime.Now,
                StructColumn = new SampleStruct()
                {
                    DateTimeColumn = DateTime.Now
                }
            };
        }

        public List<SampleClass> MethodWithInputReturnListClassSync(SampleClass a)
        {
            throw new NotImplementedException();
        }

        public List<int> MethodWithInputReturnListIntSync(SampleClass a)
        {
            throw new NotImplementedException();
        }

        public ApplicationEnumType MethodWithInputReturnEnumSync(ApplicationEnumType a)
        {
            throw new NotImplementedException();
        }

        public async Task<int> MethodWithInputReturnIntAsync(int a)
        {
            return await Task.FromResult(1);
        }

        public async Task<string> MethodWithInputReturnStringAsync(int a, string b)
        {
            throw new NotImplementedException();
        }

        public async Task<SampleClass> MethodWithInputReturnClassAsync(int a, string b)
        {
            throw new NotImplementedException();
        }

        public async Task<NullableClass> MethodWithInputReturnNullableClassAsync(int a, string b)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SampleClass>> MethodWithInputReturnListClassAsync(SampleClass a)
        {
            throw new NotImplementedException();
        }

        public async Task<List<int>> MethodWithInputReturnListIntAsync(SampleClass a)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationEnumType> MethodWithInputReturnEnumAsync(ApplicationEnumType a)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<ApplicationEnumType> MethodWithInputReturnEnumValueTaskAsync(ApplicationEnumType a)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, int> NotSupportMethod1(int a)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<int, int>> NotSupportMethod2(int a)
        {
            throw new NotImplementedException();
        }

        public async Task<Task<Dictionary<int, int>>> NotSupportMethod3(int a)
        {
            throw new NotImplementedException();
        }

        public void NotSupportMethod4(List<List<int>> a)
        {
            throw new NotImplementedException();
        }

        public void NotSupportMethod5(Dictionary<int, int> a)
        {
            throw new NotImplementedException();
        }

        public void NotSupportMethod6(Dictionary<int, int> a)
        {
            throw new NotImplementedException();
        }

        public void NotSupportMethod7(object a)
        {
            throw new NotImplementedException();
        }

        public StringBuilder NotSupportMethod8()
        {
            throw new NotImplementedException();
        }

        public void TestCancellationToken(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void TestCancellationToken2(int a, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void TestExtClass2(ExtClass2 a)
        {
            throw new NotImplementedException();
        }
    }
}
