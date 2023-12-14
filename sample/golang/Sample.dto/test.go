package assets_dto

//SampleStruct
type SampleStructDto struct {
	IntColumn  int `json:"IntColumn" form:"int_column"`  // IntColumn
	LongColumn  int64 `json:"LongColumn" form:"long_column"`  // LongColumn
	DecimalColumn  float64 `json:"DecimalColumn" form:"decimal_column"`  // DecimalColumn
	DoubleColumn  float64 `json:"DoubleColumn" form:"double_column"`  // DoubleColumn
	FloatColumn  float32 `json:"FloatColumn" form:"float_column"`  // FloatColumn
	UintColumn  uint `json:"UintColumn" form:"uint_column"`  // UintColumn
	UlongColumn  uint64 `json:"UlongColumn" form:"ulong_column"`  // UlongColumn
	StringColumn  string `json:"StringColumn" form:"string_column"`  // StringColumn
	BoolColumn  bool `json:"BoolColumn" form:"bool_column"`  // BoolColumn
	GuidColumn  string `json:"GuidColumn" form:"guid_column"`  // GuidColumn
	DateTimeColumn  int64 `json:"DateTimeColumn" form:"date_time_column"`  // DateTimeColumn
	ByteColumn  byte `json:"ByteColumn" form:"byte_column"`  // ByteColumn
}

//SampleClass
type SampleClassDto struct {
	IntColumn  int `json:"IntColumn" form:"int_column"`  // IntColumn
	LongColumn  int64 `json:"LongColumn" form:"long_column"`  // LongColumn
	DecimalColumn  float64 `json:"DecimalColumn" form:"decimal_column"`  // DecimalColumn
	DoubleColumn  float64 `json:"DoubleColumn" form:"double_column"`  // DoubleColumn
	FloatColumn  float32 `json:"FloatColumn" form:"float_column"`  // FloatColumn
	UintColumn  uint `json:"UintColumn" form:"uint_column"`  // UintColumn
	UlongColumn  uint64 `json:"UlongColumn" form:"ulong_column"`  // UlongColumn
	StringColumn  string `json:"StringColumn" form:"string_column"`  // StringColumn
	BoolColumn  bool `json:"BoolColumn" form:"bool_column"`  // BoolColumn
	DateTimeColumn  int64 `json:"DateTimeColumn" form:"date_time_column"`  // DateTimeColumn
	ByteColumn  byte `json:"ByteColumn" form:"byte_column"`  // ByteColumn
	ClassColumn  *SampleClassDto `json:"ClassColumn" form:"class_column"`  // ClassColumn
	EnumColumn  int `json:"EnumColumn" form:"enum_column"`  // EnumColumn
	StructColumn  *SampleStructDto `json:"StructColumn" form:"struct_column"`  // StructColumn
}

//
type TreeNode_ExtClass2Dto struct {
	Key  string `json:"Key" form:"key"`  // 
	Parent  string `json:"Parent" form:"parent"`  // 
	Children  []*ExtClass2Dto `json:"Children" form:"children"`  // 
}

//
type ExtClass2Dto struct {
	GenericClass  *TreeNode_ExtClass2Dto `json:"GenericClass" form:"generic_class"`  // GenericClass
	IntColumn  int `json:"IntColumn" form:"int_column"`  // IntColumn
	LongColumn  int64 `json:"LongColumn" form:"long_column"`  // LongColumn
	DecimalColumn  float64 `json:"DecimalColumn" form:"decimal_column"`  // DecimalColumn
	DoubleColumn  float64 `json:"DoubleColumn" form:"double_column"`  // DoubleColumn
	FloatColumn  float32 `json:"FloatColumn" form:"float_column"`  // FloatColumn
	UintColumn  uint `json:"UintColumn" form:"uint_column"`  // UintColumn
	UlongColumn  uint64 `json:"UlongColumn" form:"ulong_column"`  // UlongColumn
	StringColumn  string `json:"StringColumn" form:"string_column"`  // StringColumn
	BoolColumn  bool `json:"BoolColumn" form:"bool_column"`  // BoolColumn
	DateTimeColumn  int64 `json:"DateTimeColumn" form:"date_time_column"`  // DateTimeColumn
	ByteColumn  byte `json:"ByteColumn" form:"byte_column"`  // ByteColumn
	ClassColumn  *SampleClassDto `json:"ClassColumn" form:"class_column"`  // ClassColumn
	EnumColumn  int `json:"EnumColumn" form:"enum_column"`  // EnumColumn
	StructColumn  *SampleStructDto `json:"StructColumn" form:"struct_column"`  // StructColumn
}

//
type NullableClassDto struct {
	NullableIntColumn  int `json:"NullableIntColumn" form:"nullable_int_column"`  // 
	NullableLongColumn  int64 `json:"NullableLongColumn" form:"nullable_long_column"`  // 
	NullableDecimalColumn  float64 `json:"NullableDecimalColumn" form:"nullable_decimal_column"`  // 
	NullableDoubleColumn  float64 `json:"NullableDoubleColumn" form:"nullable_double_column"`  // 
	NullableFloatColumn  float32 `json:"NullableFloatColumn" form:"nullable_float_column"`  // 
	NullableUintColumn  uint `json:"NullableUintColumn" form:"nullable_uint_column"`  // 
	NullableUlongColumn  uint64 `json:"NullableUlongColumn" form:"nullable_ulong_column"`  // 
	NullableStringColumn  string `json:"NullableStringColumn" form:"nullable_string_column"`  // 
	NullableBoolColumn  bool `json:"NullableBoolColumn" form:"nullable_bool_column"`  // 
	NullableGuidColumn  string `json:"NullableGuidColumn" form:"nullable_guid_column"`  // 
	NullableDateTimeColumn  int64 `json:"NullableDateTimeColumn" form:"nullable_date_time_column"`  // 
	NullableByteColumn  byte `json:"NullableByteColumn" form:"nullable_byte_column"`  // 
	NullableClassColumn  *NullableClassDto `json:"NullableClassColumn" form:"nullable_class_column"`  // 
	NullableEnumColumn  int `json:"NullableEnumColumn" form:"nullable_enum_column"`  // 
	NullableStructColumn  *SampleStructDto `json:"NullableStructColumn" form:"nullable_struct_column"`  // 
}

//
type TreeNode_SampleClassDto struct {
	Key  string `json:"Key" form:"key"`  // 
	Parent  string `json:"Parent" form:"parent"`  // 
	Children  []*SampleClassDto `json:"Children" form:"children"`  // 
}

//
type ExtClassDto struct {
	GenericClass  *TreeNode_SampleClassDto `json:"GenericClass" form:"generic_class"`  // GenericClass
	IntColumn  int `json:"IntColumn" form:"int_column"`  // IntColumn
	LongColumn  int64 `json:"LongColumn" form:"long_column"`  // LongColumn
	DecimalColumn  float64 `json:"DecimalColumn" form:"decimal_column"`  // DecimalColumn
	DoubleColumn  float64 `json:"DoubleColumn" form:"double_column"`  // DoubleColumn
	FloatColumn  float32 `json:"FloatColumn" form:"float_column"`  // FloatColumn
	UintColumn  uint `json:"UintColumn" form:"uint_column"`  // UintColumn
	UlongColumn  uint64 `json:"UlongColumn" form:"ulong_column"`  // UlongColumn
	StringColumn  string `json:"StringColumn" form:"string_column"`  // StringColumn
	BoolColumn  bool `json:"BoolColumn" form:"bool_column"`  // BoolColumn
	DateTimeColumn  int64 `json:"DateTimeColumn" form:"date_time_column"`  // DateTimeColumn
	ByteColumn  byte `json:"ByteColumn" form:"byte_column"`  // ByteColumn
	ClassColumn  *SampleClassDto `json:"ClassColumn" form:"class_column"`  // ClassColumn
	EnumColumn  int `json:"EnumColumn" form:"enum_column"`  // EnumColumn
	StructColumn  *SampleStructDto `json:"StructColumn" form:"struct_column"`  // StructColumn
}

//
type TreeClassDto struct {
	Key  string `json:"Key" form:"key"`  // 
	Parent  string `json:"Parent" form:"parent"`  // 
	Children  []*TreeClassDto `json:"Children" form:"children"`  // 
}

