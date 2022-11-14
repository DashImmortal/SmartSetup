using Swashbuckle.AspNetCore.Annotations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartDevice.Models
{
    public class RoomModel
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public ObservableCollection<SmartDeviceModel> SmartDevices { get; set; } = new ObservableCollection<SmartDeviceModel>();
        [System.Text.Json.Serialization.JsonIgnore]
        public string SmartDevicesSerialized { get; set; } = string.Empty;
    }
}
