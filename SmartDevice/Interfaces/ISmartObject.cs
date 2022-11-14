using Swashbuckle.AspNetCore.Annotations;

namespace SmartDevice.Interfaces
{
    public interface ISmartObject
    {
        [SwaggerSchema(ReadOnly = true)]
        int Id { get; set; }
        string Name { get; set; }
        bool IsOn { get; set; }
    }
}
