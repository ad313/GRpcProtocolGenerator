/*
     此代码是自动生成，请勿修改，如果要修改，请继承它再重写方法。
*/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;
using Sample.GRpc.Protocol;
using Sample.ClientWrapper;

namespace Sample.Gateway.Controllers.v1
{
    /// desc from Description
    [ApiController]
    [Route("Sample/api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "desc from Description")]
    public partial class ServiceTestController : ControllerBase
    {
        private readonly IGRpcServiceTestClient _client;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="client"></param>
        public ServiceTestController(IGRpcServiceTestClient client)
        {
            _client = client;
        }
                
        /// <summary>
        /// title from Display attr
        /// </summary>
        /// <returns></returns>
        [HttpGet("VoidMethodSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("title from Display attr")]        
        public virtual async Task<IActionResult> VoidMethodSync(CancellationToken cancellationToken = default)
        {
            var result = await _client.VoidMethodSync(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoidSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoidSync([FromQuery] GRpcServiceTest_MethodWithInputReturnVoidSync_Request18 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoidSync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid2Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid2Sync([FromQuery] GRpcServiceTest_MethodWithInputReturnVoid2Sync_Request15 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid2Sync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnVoid3Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid3Sync([FromBody] GRpcSampleClass request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid3Sync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid4Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid4Sync([FromQuery] GRpcServiceTest_MethodWithInputReturnVoid4Sync_Request17 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid4Sync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputEnumReturnVoid4Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputEnumReturnVoid4Sync([FromQuery] GRpcServiceTest_MethodWithInputEnumReturnVoid4Sync_Request2 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputEnumReturnVoid4Sync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputTreeReturnVoid5Sync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputTreeReturnVoid5Sync([FromBody] GRpcServiceTest_MethodWithInputTreeReturnVoid5Sync_Request19 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputTreeReturnVoid5Sync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("VoidMethod")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> VoidMethodAsync(CancellationToken cancellationToken = default)
        {
            var result = await _client.VoidMethod(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoidAsync([FromQuery] GRpcServiceTest_MethodWithInputReturnVoidAsync_Request13 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid2")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid2Async([FromQuery] GRpcServiceTest_MethodWithInputReturnVoid2Async_Request14 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid2(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnVoid3Async")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid3Async([FromBody] GRpcSampleClass request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid3(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnVoid4")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputReturnVoid4Async([FromQuery] GRpcServiceTest_MethodWithInputReturnVoid4Async_Request16 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnVoid4(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputEnumReturnVoid4")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> MethodWithInputEnumReturnVoid4Async([FromQuery] GRpcServiceTest_MethodWithInputEnumReturnVoid4Async_Request1 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputEnumReturnVoid4(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnIntSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTest_MethodWithInputReturnIntSync_Response5))]        
        public virtual async Task<IActionResult> MethodWithInputReturnIntSync([FromQuery] GRpcServiceTest_MethodWithInputReturnIntSync_Request9 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnIntSync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnStringSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTestStringResponse))]        
        public virtual async Task<IActionResult> MethodWithInputReturnStringSync([FromQuery] GRpcServiceTest_MethodWithInputReturnStringSync_Request12 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnStringSync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnClassSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcSampleClass))]        
        public virtual async Task<IActionResult> MethodWithInputReturnClassSync([FromQuery] GRpcServiceTest_MethodWithInputReturnClassSync_Request4 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnClassSync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnListClassSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTestListSampleClassResponse))]        
        public virtual async Task<IActionResult> MethodWithInputReturnListClassSync([FromBody] GRpcSampleClass request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnListClassSync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnListIntSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTestListInt32Response))]        
        public virtual async Task<IActionResult> MethodWithInputReturnListIntSync([FromBody] GRpcSampleClass request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnListIntSync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnEnumSync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTest_MethodWithInputReturnEnumSync_Response2))]        
        public virtual async Task<IActionResult> MethodWithInputReturnEnumSync([FromQuery] GRpcServiceTest_MethodWithInputReturnEnumSync_Request6 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnEnumSync(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnInt")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTest_MethodWithInputReturnIntAsync_Response4))]        
        public virtual async Task<IActionResult> MethodWithInputReturnIntAsync([FromQuery] GRpcServiceTest_MethodWithInputReturnIntAsync_Request8 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnInt(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnString")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTestStringResponse))]        
        public virtual async Task<IActionResult> MethodWithInputReturnStringAsync([FromQuery] GRpcServiceTest_MethodWithInputReturnStringAsync_Request11 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnString(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnClass")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcSampleClass))]        
        public virtual async Task<IActionResult> MethodWithInputReturnClassAsync([FromQuery] GRpcServiceTest_MethodWithInputReturnClassAsync_Request3 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnClass(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnNullableClass")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcNullableClass))]        
        public virtual async Task<IActionResult> MethodWithInputReturnNullableClassAsync([FromQuery] GRpcServiceTest_MethodWithInputReturnNullableClassAsync_Request10 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnNullableClass(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnListClassAsync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTestListSampleClassResponse))]        
        public virtual async Task<IActionResult> MethodWithInputReturnListClassAsync([FromBody] GRpcSampleClass request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnListClass(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("MethodWithInputReturnListIntAsync")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTestListInt32Response))]        
        public virtual async Task<IActionResult> MethodWithInputReturnListIntAsync([FromBody] GRpcSampleClass request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnListInt(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnEnum")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTest_MethodWithInputReturnEnumAsync_Response1))]        
        public virtual async Task<IActionResult> MethodWithInputReturnEnumAsync([FromQuery] GRpcServiceTest_MethodWithInputReturnEnumAsync_Request5 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnEnum(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("MethodWithInputReturnEnumValueTask")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]
        [SwaggerResponse(200, "响应结果", typeof(GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Response3))]        
        public virtual async Task<IActionResult> MethodWithInputReturnEnumValueTaskAsync([FromQuery] GRpcServiceTest_MethodWithInputReturnEnumValueTaskAsync_Request7 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.MethodWithInputReturnEnumValueTask(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestCancellationToken")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> TestCancellationToken(CancellationToken cancellationToken = default)
        {
            var result = await _client.TestCancellationToken(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("TestCancellationToken2")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> TestCancellationToken2([FromQuery] GRpcServiceTest_TestCancellationToken2_Request20 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.TestCancellationToken2(request, cancellationToken: cancellationToken);
            return Ok(result);
        }
                
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("TestExtClass2")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation("")]        
        public virtual async Task<IActionResult> TestExtClass2([FromBody] GRpcExtClass2 request, CancellationToken cancellationToken = default)
        {
            var result = await _client.TestExtClass2(request, cancellationToken: cancellationToken);
            return Ok(result);
        }        
    }
}