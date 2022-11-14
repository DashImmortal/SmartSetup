using Newtonsoft.Json;
using SmartDevice.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartDevice.Models
{
    public class SmartDeviceModel : ISmartObject
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsOn { get; set; } = false;
    }
}
