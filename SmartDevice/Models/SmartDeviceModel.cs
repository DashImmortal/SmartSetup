using SmartDevice.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartDevice.Models
{
    public class SmartDeviceModel : ISmartObject
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [SwaggerSchema(ReadOnly = true)]
        [EnumDataType(typeof(SmartObjectType))]
        public SmartObjectType Type { get; set; }
        [Required]
        public bool IsOn { get; set; }
        [JsonIgnore]
        public virtual RoomModel RoomModel { get; set; }
    }
}
