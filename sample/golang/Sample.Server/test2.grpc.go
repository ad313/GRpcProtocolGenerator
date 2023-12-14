//----自动生成，请勿修改----//

package Server

import (
	"context"
	log "github.com/sirupsen/logrus"
	"sailing.cn/protobuf/core"
	"sailing.cn/protobuf/assets"
	"service.cn/assets/store111"
	"sailing.cn/v2/mapper"
	"google.golang.org/protobuf/types/known/emptypb"
)

// IServiceTest2
type Test2Server struct {
	assets.UnimplementedTest2ServiceServer
}

//接口类型
//type Test2 struct {
	
//}

// Test1 Test1
func (g *Test2Server) Test1(ctx context.Context, request *emptypb.Empty) (*emptypb.Empty, error) {
	model := &store.Test2{}
	err := model.Test1(ctx)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// Test2 这是修改
func (g *Test2Server) Test2(ctx context.Context, request *assets.Test2Service_Test2Async_Request) (*emptypb.Empty, error) {
	model := &store.Test2{}
	err := model.Test2(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// Test2_2 这是修改2
func (g *Test2Server) Test2_2(ctx context.Context, request *assets.Test2Service_Test2_2Async_Request) (*emptypb.Empty, error) {
	model := &store.Test2{}
	err := model.Test2_2(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// GetById 获取单个
func (g *Test2Server) GetById(ctx context.Context, request *assets.Test2Service_GetByIdAsync_Request) (*assets.GRpcSampleClass, error) {
	model := &store.Test2{}
	data, err := model.GetById(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.GRpcSampleClass{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// Test4 查询列表
func (g *Test2Server) Test4(ctx context.Context, request *assets.GRpcSampleClass) (*assets.Test2ServiceListSampleClassResponse, error) {
	model := &store.Test2{}
	data, err := model.Test4(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.Test2ServiceListSampleClassResponse{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// Test5 这是删除
func (g *Test2Server) Test5(ctx context.Context, request *assets.Test2Service_Test5Async_Request) (*assets.Test2ServiceListSampleClassResponse, error) {
	model := &store.Test2{}
	data, err := model.Test5(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.Test2ServiceListSampleClassResponse{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// Test6 
func (g *Test2Server) Test6(ctx context.Context, request *assets.Test2Service_Test6Async_Request) (*assets.Test2Service_Test6Async_Response, error) {
	model := &store.Test2{}
	data, err := model.Test6(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.Test2Service_Test6Async_Response{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// Test7 
func (g *Test2Server) Test7(ctx context.Context, request *assets.Test2ServiceIdRequest) (*emptypb.Empty, error) {
	model := &store.Test2{}
	err := model.Test7(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}
