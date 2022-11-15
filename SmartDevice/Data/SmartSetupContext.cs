using Microsoft.EntityFrameworkCore;
using SmartDevice.Models;

namespace SmartDevice.Data
{
    public class SmartSetupContext : DbContext
    {

        public SmartSetupContext(DbContextOptions<SmartSetupContext> options)
            : base(options)
        {
        }
        
        public DbSet<SmartDeviceModel> SmartDevices { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
    }
}
