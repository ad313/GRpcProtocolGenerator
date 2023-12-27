package assets_dto

// SampleStructDto SampleStruct
type SampleStructDto struct {
	IntColumn  int32 `json:"int_column" form:"int_column"`  // IntColumn
	LongColumn  int64 `json:"long_column" form:"long_column"`  // LongColumn
	DecimalColumn  float64 `json:"decimal_column" form:"decimal_column"`  // DecimalColumn
	DoubleColumn  float64 `json:"double_column" form:"double_column"`  // DoubleColumn
	FloatColumn  float32 `json:"float_column" form:"float_column"`  // FloatColumn
	UintColumn  uint `json:"uint_column" form:"uint_column"`  // UintColumn
	UlongColumn  uint64 `json:"ulong_column" form:"ulong_column"`  // UlongColumn
	StringColumn  string `json:"string_column" form:"string_column"`  // StringColumn
	BoolColumn  bool `json:"bool_column" form:"bool_column"`  // BoolColumn
	GuidColumn  string `json:"guid_column" form:"guid_column"`  // GuidColumn
	DateTimeColumn  int64 `json:"date_time_column" form:"date_time_column"`  // DateTimeColumn
	ByteColumn  byte `json:"byte_column" form:"byte_column"`  // ByteColumn
}

// SampleClassDto SampleClass
type SampleClassDto struct {
	IntColumn  int32 `json:"int_column" form:"int_column"`  // IntColumn
	LongColumn  int64 `json:"long_column" form:"long_column"`  // LongColumn
	DecimalColumn  float64 `json:"decimal_column" form:"decimal_column"`  // DecimalColumn
	DoubleColumn  float64 `json:"double_column" form:"double_column"`  // DoubleColumn
	FloatColumn  float32 `json:"float_column" form:"float_column"`  // FloatColumn
	UintColumn  uint `json:"uint_column" form:"uint_column"`  // UintColumn
	UlongColumn  uint64 `json:"ulong_column" form:"ulong_column"`  // UlongColumn
	StringColumn  string `json:"string_column" form:"string_column"`  // StringColumn
	BoolColumn  bool `json:"bool_column" form:"bool_column"`  // BoolColumn
	DateTimeColumn  int64 `json:"date_time_column" form:"date_time_column"`  // DateTimeColumn
	ByteColumn  byte `json:"byte_column" form:"byte_column"`  // ByteColumn
	ClassColumn  *SampleClassDto `json:"class_column" form:"class_column"`  // ClassColumn
	EnumColumn  int32 `json:"enum_column" form:"enum_column"`  // EnumColumn
	StructColumn  *SampleStructDto `json:"struct_column" form:"struct_column"`  // StructColumn
}

// TreeNode_ExtClass2Dto 
type TreeNode_ExtClass2Dto struct {
	Key  string `json:"key" form:"key"`  // 
	Parent  string `json:"parent" form:"parent"`  // 
	Children  []*ExtClass2Dto `json:"children" form:"children"`  // 
}

// ExtClass2Dto 
type ExtClass2Dto struct {
	GenericClass  *TreeNode_ExtClass2Dto `json:"generic_class" form:"generic_class"`  // GenericClass
	IntColumn  int32 `json:"int_column" form:"int_column"`  // IntColumn
	LongColumn  int64 `json:"long_column" form:"long_column"`  // LongColumn
	DecimalColumn  float64 `json:"decimal_column" form:"decimal_column"`  // DecimalColumn
	DoubleColumn  float64 `json:"double_column" form:"double_column"`  // DoubleColumn
	FloatColumn  float32 `json:"float_column" form:"float_column"`  // FloatColumn
	UintColumn  uint `json:"uint_column" form:"uint_column"`  // UintColumn
	UlongColumn  uint64 `json:"ulong_column" form:"ulong_column"`  // UlongColumn
	StringColumn  string `json:"string_column" form:"string_column"`  // StringColumn
	BoolColumn  bool `json:"bool_column" form:"bool_column"`  // BoolColumn
	DateTimeColumn  int64 `json:"date_time_column" form:"date_time_column"`  // DateTimeColumn
	ByteColumn  byte `json:"byte_column" form:"byte_column"`  // ByteColumn
	ClassColumn  *SampleClassDto `json:"class_column" form:"class_column"`  // ClassColumn
	EnumColumn  int32 `json:"enum_column" form:"enum_column"`  // EnumColumn
	StructColumn  *SampleStructDto `json:"struct_column" form:"struct_column"`  // StructColumn
}

// NullableClassDto 
type NullableClassDto struct {
	NullableIntColumn  int32 `json:"nullable_int_column" form:"nullable_int_column"`  // 
	NullableLongColumn  int64 `json:"nullable_long_column" form:"nullable_long_column"`  // 
	NullableDecimalColumn  float64 `json:"nullable_decimal_column" form:"nullable_decimal_column"`  // 
	NullableDoubleColumn  float64 `json:"nullable_double_column" form:"nullable_double_column"`  // 
	NullableFloatColumn  float32 `json:"nullable_float_column" form:"nullable_float_column"`  // 
	NullableUintColumn  uint `json:"nullable_uint_column" form:"nullable_uint_column"`  // 
	NullableUlongColumn  uint64 `json:"nullable_ulong_column" form:"nullable_ulong_column"`  // 
	NullableStringColumn  string `json:"nullable_string_column" form:"nullable_string_column"`  // 
	NullableBoolColumn  bool `json:"nullable_bool_column" form:"nullable_bool_column"`  // 
	NullableGuidColumn  string `json:"nullable_guid_column" form:"nullable_guid_column"`  // 
	NullableDateTimeColumn  int64 `json:"nullable_date_time_column" form:"nullable_date_time_column"`  // 
	NullableByteColumn  byte `json:"nullable_byte_column" form:"nullable_byte_column"`  // 
	NullableClassColumn  *NullableClassDto `json:"nullable_class_column" form:"nullable_class_column"`  // 
	NullableEnumColumn  int32 `json:"nullable_enum_column" form:"nullable_enum_column"`  // 
	NullableStructColumn  *SampleStructDto `json:"nullable_struct_column" form:"nullable_struct_column"`  // 
}

// TreeNode_SampleClassDto 
type TreeNode_SampleClassDto struct {
	Key  string `json:"key" form:"key"`  // 
	Parent  string `json:"parent" form:"parent"`  // 
	Children  []*SampleClassDto `json:"children" form:"children"`  // 
}

// ExtClassDto 
type ExtClassDto struct {
	GenericClass  *TreeNode_SampleClassDto `json:"generic_class" form:"generic_class"`  // GenericClass
	IntColumn  int32 `json:"int_column" form:"int_column"`  // IntColumn
	LongColumn  int64 `json:"long_column" form:"long_column"`  // LongColumn
	DecimalColumn  float64 `json:"decimal_column" form:"decimal_column"`  // DecimalColumn
	DoubleColumn  float64 `json:"double_column" form:"double_column"`  // DoubleColumn
	FloatColumn  float32 `json:"float_column" form:"float_column"`  // FloatColumn
	UintColumn  uint `json:"uint_column" form:"uint_column"`  // UintColumn
	UlongColumn  uint64 `json:"ulong_column" form:"ulong_column"`  // UlongColumn
	StringColumn  string `json:"string_column" form:"string_column"`  // StringColumn
	BoolColumn  bool `json:"bool_column" form:"bool_column"`  // BoolColumn
	DateTimeColumn  int64 `json:"date_time_column" form:"date_time_column"`  // DateTimeColumn
	ByteColumn  byte `json:"byte_column" form:"byte_column"`  // ByteColumn
	ClassColumn  *SampleClassDto `json:"class_column" form:"class_column"`  // ClassColumn
	EnumColumn  int32 `json:"enum_column" form:"enum_column"`  // EnumColumn
	StructColumn  *SampleStructDto `json:"struct_column" form:"struct_column"`  // StructColumn
}

// TreeClassDto 
type TreeClassDto struct {
	Key  string `json:"key" form:"key"`  // 
	Parent  string `json:"parent" form:"parent"`  // 
	Children  []*TreeClassDto `json:"children" form:"children"`  // 
}

