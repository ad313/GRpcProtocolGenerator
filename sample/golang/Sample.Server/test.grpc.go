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

// desc from Description
type TestServer struct {
	assets.UnimplementedTestServiceServer
}

//接口类型
//type Test struct {
	
//}

// VoidMethodSync title from Display attr
func (g *TestServer) VoidMethodSync(ctx context.Context, request *emptypb.Empty) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.VoidMethodSync(ctx)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputReturnVoidSync 
func (g *TestServer) MethodWithInputReturnVoidSync(ctx context.Context, request *assets.TestService_MethodWithInputReturnVoidSync_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputReturnVoidSync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputReturnVoid2Sync 
func (g *TestServer) MethodWithInputReturnVoid2Sync(ctx context.Context, request *assets.TestService_MethodWithInputReturnVoid2Sync_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputReturnVoid2Sync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputReturnVoid3Sync 
func (g *TestServer) MethodWithInputReturnVoid3Sync(ctx context.Context, request *assets.GRpcSampleClass) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputReturnVoid3Sync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputReturnVoid4Sync 
func (g *TestServer) MethodWithInputReturnVoid4Sync(ctx context.Context, request *assets.TestService_MethodWithInputReturnVoid4Sync_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputReturnVoid4Sync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputEnumReturnVoid4Sync 
func (g *TestServer) MethodWithInputEnumReturnVoid4Sync(ctx context.Context, request *assets.TestService_MethodWithInputEnumReturnVoid4Sync_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputEnumReturnVoid4Sync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputTreeReturnVoid5Sync 
func (g *TestServer) MethodWithInputTreeReturnVoid5Sync(ctx context.Context, request *assets.TestService_MethodWithInputTreeReturnVoid5Sync_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputTreeReturnVoid5Sync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// VoidMethod 
func (g *TestServer) VoidMethod(ctx context.Context, request *emptypb.Empty) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.VoidMethod(ctx)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputReturnVoid 
func (g *TestServer) MethodWithInputReturnVoid(ctx context.Context, request *assets.TestService_MethodWithInputReturnVoidAsync_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputReturnVoid(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputReturnVoid2 
func (g *TestServer) MethodWithInputReturnVoid2(ctx context.Context, request *assets.TestService_MethodWithInputReturnVoid2Async_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputReturnVoid2(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputReturnVoid3 
func (g *TestServer) MethodWithInputReturnVoid3(ctx context.Context, request *assets.GRpcSampleClass) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputReturnVoid3(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputReturnVoid4 
func (g *TestServer) MethodWithInputReturnVoid4(ctx context.Context, request *assets.TestService_MethodWithInputReturnVoid4Async_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputReturnVoid4(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputEnumReturnVoid4 
func (g *TestServer) MethodWithInputEnumReturnVoid4(ctx context.Context, request *assets.TestService_MethodWithInputEnumReturnVoid4Async_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.MethodWithInputEnumReturnVoid4(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// MethodWithInputReturnIntSync 
func (g *TestServer) MethodWithInputReturnIntSync(ctx context.Context, request *assets.TestService_MethodWithInputReturnIntSync_Request) (*assets.TestService_MethodWithInputReturnIntSync_Response, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnIntSync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestService_MethodWithInputReturnIntSync_Response{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnStringSync 
func (g *TestServer) MethodWithInputReturnStringSync(ctx context.Context, request *assets.TestService_MethodWithInputReturnStringSync_Request) (*assets.TestServiceStringResponse, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnStringSync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestServiceStringResponse{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnClassSync 
func (g *TestServer) MethodWithInputReturnClassSync(ctx context.Context, request *assets.TestService_MethodWithInputReturnClassSync_Request) (*assets.GRpcSampleClass, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnClassSync(ctx, request)
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

// MethodWithInputReturnListClassSync 
func (g *TestServer) MethodWithInputReturnListClassSync(ctx context.Context, request *assets.GRpcSampleClass) (*assets.TestServiceListSampleClassResponse, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnListClassSync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestServiceListSampleClassResponse{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnListIntSync 
func (g *TestServer) MethodWithInputReturnListIntSync(ctx context.Context, request *assets.GRpcSampleClass) (*assets.TestServiceListInt32Response, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnListIntSync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestServiceListInt32Response{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnEnumSync 
func (g *TestServer) MethodWithInputReturnEnumSync(ctx context.Context, request *assets.TestService_MethodWithInputReturnEnumSync_Request) (*assets.TestService_MethodWithInputReturnEnumSync_Response, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnEnumSync(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestService_MethodWithInputReturnEnumSync_Response{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnInt 
func (g *TestServer) MethodWithInputReturnInt(ctx context.Context, request *assets.TestService_MethodWithInputReturnIntAsync_Request) (*assets.TestService_MethodWithInputReturnIntAsync_Response, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnInt(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestService_MethodWithInputReturnIntAsync_Response{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnString 
func (g *TestServer) MethodWithInputReturnString(ctx context.Context, request *assets.TestService_MethodWithInputReturnStringAsync_Request) (*assets.TestServiceStringResponse, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnString(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestServiceStringResponse{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnClass 
func (g *TestServer) MethodWithInputReturnClass(ctx context.Context, request *assets.TestService_MethodWithInputReturnClassAsync_Request) (*assets.GRpcSampleClass, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnClass(ctx, request)
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

// MethodWithInputReturnNullableClass 
func (g *TestServer) MethodWithInputReturnNullableClass(ctx context.Context, request *assets.TestService_MethodWithInputReturnNullableClassAsync_Request) (*assets.GRpcNullableClass, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnNullableClass(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.GRpcNullableClass{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnListClass 
func (g *TestServer) MethodWithInputReturnListClass(ctx context.Context, request *assets.GRpcSampleClass) (*assets.TestServiceListSampleClassResponse, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnListClass(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestServiceListSampleClassResponse{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnListInt 
func (g *TestServer) MethodWithInputReturnListInt(ctx context.Context, request *assets.GRpcSampleClass) (*assets.TestServiceListInt32Response, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnListInt(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestServiceListInt32Response{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnEnum 
func (g *TestServer) MethodWithInputReturnEnum(ctx context.Context, request *assets.TestService_MethodWithInputReturnEnumAsync_Request) (*assets.TestService_MethodWithInputReturnEnumAsync_Response, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnEnum(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestService_MethodWithInputReturnEnumAsync_Response{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// MethodWithInputReturnEnumValueTask 
func (g *TestServer) MethodWithInputReturnEnumValueTask(ctx context.Context, request *assets.TestService_MethodWithInputReturnEnumValueTaskAsync_Request) (*assets.TestService_MethodWithInputReturnEnumValueTaskAsync_Response, error) {
	model := &store.Test{}
	data, err := model.MethodWithInputReturnEnumValueTask(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	result := &assets.TestService_MethodWithInputReturnEnumValueTaskAsync_Response{}
	err = mapper.MapStructToProto(data, result)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return result, err	
}

// ChangeToSupportMethod7 
func (g *TestServer) ChangeToSupportMethod7(ctx context.Context, request *assets.TestService_ChangeToSupportMethod7_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.ChangeToSupportMethod7(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// TestCancellationToken 
func (g *TestServer) TestCancellationToken(ctx context.Context, request *assets.CancellationToken) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.TestCancellationToken(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// TestCancellationToken2 
func (g *TestServer) TestCancellationToken2(ctx context.Context, request *assets.TestService_TestCancellationToken2_Request) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.TestCancellationToken2(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}

// TestExtClass2 
func (g *TestServer) TestExtClass2(ctx context.Context, request *assets.GRpcExtClass2) (*emptypb.Empty, error) {
	model := &store.Test{}
	err := model.TestExtClass2(ctx, request)
	if err != nil {
		log.Error(err)
		return nil, err
	}
	return &emptypb.Empty{}, err	
}
