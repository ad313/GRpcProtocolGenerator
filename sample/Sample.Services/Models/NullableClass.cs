using System;

namespace Sample.Services.Models
{
    /// <summary>
    /// NullableClass
    /// </summary>
    public class NullableClass
    {
        public int? NullableIntColumn { get; set; }
        
        public long? NullableLongColumn { get; set; }
        
        public decimal? NullableDecimalColumn { get; set; }
        
        public double? NullableDoubleColumn { get; set; }
        
        public float? NullableFloatColumn { get; set; }
        
        public uint? NullableUintColumn { get; set; }
        
        public ulong? NullableUlongColumn { get; set; }
        
        public string? NullableStringColumn { get; set; }
        
        public bool? NullableBoolColumn { get; set; }
        
        public Guid? NullableGuidColumn { get; set; }
        
        public DateTime? NullableDateTimeColumn { get; set; }
        
        public byte? NullableByteColumn { get; set; }
        
        public NullableClass NullableClassColumn { get; set; }
        
        public SampleEnum? NullableEnumColumn { get; set; }
        
        public SampleStruct? NullableStructColumn { get; set; }
    }
}
