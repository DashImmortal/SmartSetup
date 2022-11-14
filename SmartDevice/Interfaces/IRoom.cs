using Swashbuckle.AspNetCore.Annotations;

namespace SmartDevice.Interfaces
{
    public interface IRoom
    {
        [SwaggerSchema(ReadOnly = true)]
        int Id { get; set; }
        string Name { get; set; }
    }
}
