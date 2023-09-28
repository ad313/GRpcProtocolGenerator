using System.ComponentModel;

namespace Sample.Services.Models
{
    [Description("SampleEnum desc from  Description")]
    public enum SampleEnum
    {
        [Description("One1")]
        One1,
        [Description("Two2")]
        Two2 = 12,
    }
}
