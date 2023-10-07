using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sample.Services.Models
{
    /// <summary>
    /// SampleListClass
    /// </summary>
    [Display(Name = "SampleListClass")]
    [DisplayName("SampleListClass")]
    [Description("SampleListClass")]
    public class SampleListClass
    {
        public List<int> ListIntColumn { get; set; }
        
        public List<long> ListLongColumn { get; set; }
        
        public List<decimal> ListDecimalColumn { get; set; }
        
        public List<double> ListDoubleColumn { get; set; }
        
        public List<float> ListFloatColumn { get; set; }
        
        public List<uint> ListUintColumn { get; set; }
        
        public List<ulong> ListUlongColumn { get; set; }
        
        public List<string> ListStringColumn { get; set; }
        
        public List<bool> ListBoolColumn { get; set; }
        
        public List<Guid> ListGuidColumn { get; set; }
        
        public List<DateTime> ListDateTimeColumn { get; set; }
        
        public List<byte> ListByteColumn { get; set; }
        
        public List<SampleClass> ListClassColumn { get; set; }
        
        public List<SampleEnum> ListEnumColumn { get; set; }
        
        public List<SampleStruct> ListStructColumn { get; set; }
    }
}
