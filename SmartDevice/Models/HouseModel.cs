using System.Collections.ObjectModel;
using SmartDevice.Interfaces;

namespace SmartDevice.Models
{
    public class HouseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ObservableCollection<IRoom> Rooms { get; set; } = new ObservableCollection<IRoom>();
    }
}
