// Package assets 此代码自动生成，请勿修改
package assets

import (
	"sailing.cn/v2/mapper"
	"sailing.cn/v2/restful"
	"github.com/gin-gonic/gin"
	"google.golang.org/protobuf/types/known/emptypb"
	"media.gateway.cn/assets/controller/v1/common"
	grpc "media.gateway.cn/common/client/assets"
	"media.gateway.cn/common/http/dto/assets"
	"sailing.cn/protobuf/assets"
	//"sailing.cn/v2/restful/jwt"
	"google.golang.org/protobuf/types/known/emptypb"
)

type TestController struct{}

// 路由配置
func addTestRouter(router *gin.RouterGroup) {
	api := new(TestController)
	router = router.Group("/Test")
	router.("/", common.CustomAuth, api.ChangeToSupportMethod7)
	router.("/", common.CustomAuth, api.MethodWithInputEnumReturnVoid4)
	router.("/", common.CustomAuth, api.MethodWithInputEnumReturnVoid4Sync)
	router.("/", common.CustomAuth, api.MethodWithInputReturnClass)
	router.("/", common.CustomAuth, api.MethodWithInputReturnClassSync)
	router.("/", common.CustomAuth, api.MethodWithInputReturnEnum)
	router.("/", common.CustomAuth, api.MethodWithInputReturnEnumSync)
	router.("/", common.CustomAuth, api.MethodWithInputReturnEnumValueTask)
	router.("/", common.CustomAuth, api.MethodWithInputReturnInt)
	router.("/", common.CustomAuth, api.MethodWithInputReturnIntSync)
	router.POST("/MethodWithInputReturnListClassAsync", common.CustomAuth, api.MethodWithInputReturnListClass)
	router.POST("/MethodWithInputReturnListClassSync", common.CustomAuth, api.MethodWithInputReturnListClassSync)
	router.POST("/MethodWithInputReturnListIntAsync", common.CustomAuth, api.MethodWithInputReturnListInt)
	router.POST("/MethodWithInputReturnListIntSync", common.CustomAuth, api.MethodWithInputReturnListIntSync)
	router.("/", common.CustomAuth, api.MethodWithInputReturnNullableClass)
	router.("/", common.CustomAuth, api.MethodWithInputReturnString)
	router.("/", common.CustomAuth, api.MethodWithInputReturnStringSync)
	router.("/", common.CustomAuth, api.MethodWithInputReturnVoid)
	router.("/", common.CustomAuth, api.MethodWithInputReturnVoid2)
	router.("/", common.CustomAuth, api.MethodWithInputReturnVoid2Sync)
	router.POST("/MethodWithInputReturnVoid3Async", common.CustomAuth, api.MethodWithInputReturnVoid3)
	router.POST("/MethodWithInputReturnVoid3Sync", common.CustomAuth, api.MethodWithInputReturnVoid3Sync)
	router.("/", common.CustomAuth, api.MethodWithInputReturnVoid4)
	router.("/", common.CustomAuth, api.MethodWithInputReturnVoid4Sync)
	router.("/", common.CustomAuth, api.MethodWithInputReturnVoidSync)
	router.GET("/MethodWithInputTreeReturnVoid5Sync", common.CustomAuth, api.MethodWithInputTreeReturnVoid5Sync)
	router.("/", common.CustomAuth, api.TestCancellationToken)
	router.("/", common.CustomAuth, api.TestCancellationToken2)
	router.POST("/TestExtClass2", common.CustomAuth, api.TestExtClass2)
	router.("/", common.CustomAuth, api.VoidMethod)
	router.GET("/VoidMethodSync", common.CustomAuth, api.VoidMethodSync)
}

// ChangeToSupportMethod7 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) ChangeToSupportMethod7(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().ChangeToSupportMethod7(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputEnumReturnVoid4 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputEnumReturnVoid4(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputEnumReturnVoid4(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputEnumReturnVoid4Sync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputEnumReturnVoid4Sync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputEnumReturnVoid4Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnClass 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.SampleClassDto}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnClass(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnClass(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.SampleClassDto{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnClassSync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.SampleClassDto}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnClassSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnClassSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.SampleClassDto{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnEnum 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnEnum(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnEnum(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnEnumSync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnEnumSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnEnumSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnEnumValueTask 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnEnumValueTask(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnEnumValueTask(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnInt 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnInt(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnInt(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnIntSync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnIntSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnIntSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnListClass 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnListClassAsync [post]
func (x *TestController) MethodWithInputReturnListClass(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnListClass(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnListClassSync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnListClassSync [post]
func (x *TestController) MethodWithInputReturnListClassSync(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnListClassSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnListInt 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnListIntAsync [post]
func (x *TestController) MethodWithInputReturnListInt(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnListInt(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnListIntSync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnListIntSync [post]
func (x *TestController) MethodWithInputReturnListIntSync(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnListIntSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnNullableClass 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.NullableClassDto}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnNullableClass(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnNullableClass(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.NullableClassDto{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnString 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnString(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnString(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnStringSync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=assets_dto.}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnStringSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnStringSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	_result := &assets_dto.{}
	err = mapper.MapProtoToStruct(result, &_result)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, _result)
}

// MethodWithInputReturnVoid 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid2 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid2(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid2Sync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid2Sync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid2Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid3 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnVoid3Async [post]
func (x *TestController) MethodWithInputReturnVoid3(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid3(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid3Sync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputReturnVoid3Sync [post]
func (x *TestController) MethodWithInputReturnVoid3Sync(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid3Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid4 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid4(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid4(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoid4Sync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoid4Sync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoid4Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputReturnVoidSync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) MethodWithInputReturnVoidSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputReturnVoidSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// MethodWithInputTreeReturnVoid5Sync 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/MethodWithInputTreeReturnVoid5Sync [get]
func (x *TestController) MethodWithInputTreeReturnVoid5Sync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().MethodWithInputTreeReturnVoid5Sync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// TestCancellationToken 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) TestCancellationToken(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().TestCancellationToken(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// TestCancellationToken2 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) TestCancellationToken2(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().TestCancellationToken2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// TestExtClass2 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@Param		model	body		assets_dto.ExtClass2Dto			false	"输入参数"
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/TestExtClass2 [post]
func (x *TestController) TestExtClass2(c *gin.Context) {

    var err error
	_request := &assets_dto.ExtClass2Dto{}
	err = c.ShouldBindJSON(_request)	
	if err != nil {
		restful.Fail(c, err)
		return
	}

	request := &assets.GRpcExtClass2{}
	err = mapper.MapStructToProto(_request, request)
	if err != nil {
		restful.Fail(c, err)
		return
	}

    result, err := grpc.GetTestClient().TestExtClass2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// VoidMethod 
// @Summary  [desc from Description-控制器] - 
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/ []
func (x *TestController) VoidMethod(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().VoidMethod(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// VoidMethodSync title from Display attr
// @Summary  [desc from Description-控制器] - title from Display attr
// @Tags     media-assets/Test
//
//	@success	200	{object}	restful.JSONResult{data=emptypb.Empty}	"返回结果"
//
// @Router   /v1/media-assets/Test/VoidMethodSync [get]
func (x *TestController) VoidMethodSync(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTestClient().VoidMethodSync(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}
