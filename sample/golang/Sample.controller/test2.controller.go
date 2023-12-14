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
)

type Test2Controller struct{}

// 路由配置
func addTest2Router(router *gin.RouterGroup) {
	api := new(Test2Controller)
	router = router.Group("/Test2")
	router.("/", api.Test1)
	router.PUT("/aaa/:a", api.Test2)
	router.PUT("/aaa2/:a", api.Test2_2)
	router.GET("/:id", api.GetById)
	router.GET("/aaa", api.Test4)
	router.DELETE("/aaa/:id", api.Test5)
	router.GET("/Test6", api.Test6)
	router.GET("/Test7", api.Test7)
}

// Test1 Test1
// @Summary  Test1
// @Tags     media-assets/Test2
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test2/ []
func (x *Test2Controller) Test1(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTest2Client().Test1(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// Test2 这是修改
// @Summary  这是修改
// @Tags     media-assets/Test2
//
//	@Param		a		path		string							true	"xxxxx"
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test2/aaa/{a} [put]
func (x *Test2Controller) Test2(c *gin.Context) {

    var err error
	var a = c.Param("a")

	request := &emptypb.Empty{}

    result, err := grpc.GetTest2Client().Test2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// Test2_2 这是修改2
// @Summary  这是修改2
// @Tags     media-assets/Test2
//
//	@Param		a		path		string							true	"xxxxx"
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test2/aaa2/{a} [put]
func (x *Test2Controller) Test2_2(c *gin.Context) {

    var err error
	var a = c.Param("a")

	request := &emptypb.Empty{}

    result, err := grpc.GetTest2Client().Test2_2(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}

// GetById 获取单个
// @Summary  获取单个
// @Tags     media-assets/Test2
//
//	@Param		id		path		string							true	"xxxxx"
//	@success	200	{object}	restful.JSONResult{ data = assets_dto.SampleClassDto }	"返回结果"
//
// @Router   /v1/media-assets/Test2/{id} [get]
func (x *Test2Controller) GetById(c *gin.Context) {

    var err error
	var id = c.Param("id")

	request := &emptypb.Empty{}

    result, err := grpc.GetTest2Client().GetById(common.BuildContext(c), request)
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

// Test4 查询列表
// @Summary  查询列表
// @Tags     media-assets/Test2
//
//	@Param		model	query		assets_dto.SampleClassDto			false	"SampleClass"
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test2/aaa [get]
func (x *Test2Controller) Test4(c *gin.Context) {

    var err error
	_request := &assets_dto.SampleClassDto{}
	err = c.ShouldBind(_request)
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

    result, err := grpc.GetTest2Client().Test4(common.BuildContext(c), request)
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

// Test5 这是删除
// @Summary  这是删除
// @Tags     media-assets/Test2
//
//	@Param		id		path		string							true	"xxxxx"
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test2/aaa/{id} [delete]
func (x *Test2Controller) Test5(c *gin.Context) {

    var err error
	var id = c.Param("id")

	request := &emptypb.Empty{}

    result, err := grpc.GetTest2Client().Test5(common.BuildContext(c), request)
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

// Test6 
// @Summary  
// @Tags     media-assets/Test2
//
//	@success	200	{object}	restful.JSONResult{ data = assets_dto. }	"返回结果"
//
// @Router   /v1/media-assets/Test2/Test6 [get]
func (x *Test2Controller) Test6(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTest2Client().Test6(common.BuildContext(c), request)
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

// Test7 
// @Summary  
// @Tags     media-assets/Test2
//
//	@success	200	{object}	restful.JSONResult{ data = emptypb.Empty }	"返回结果"
//
// @Router   /v1/media-assets/Test2/Test7 [get]
func (x *Test2Controller) Test7(c *gin.Context) {

    var err error

	request := &emptypb.Empty{}

    result, err := grpc.GetTest2Client().Test7(common.BuildContext(c), request)
	if err != nil {
		restful.Fail(c, err)
		return
	}
	restful.Success(c, result)
}
