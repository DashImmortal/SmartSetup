using SmartDevice.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartDevice.Interfaces
{
    public interface ISmartObject
    {

        [SwaggerSchema(ReadOnly = true)]
        int Id { get; set; }
        public string Name { get; set; }
        SmartObjectType Type { get; set; }
        bool IsOn { get; set; }
    }
}
