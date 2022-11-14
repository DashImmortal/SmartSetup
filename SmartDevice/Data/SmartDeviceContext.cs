using Microsoft.EntityFrameworkCore;

namespace SmartDevice.Data
{
    public class SmartDeviceContext : DbContext
    {
        public SmartDeviceContext (DbContextOptions<SmartDeviceContext> options)
            : base(options)
        {
        }

        public DbSet<Models.SmartDeviceModel> SmartDeviceModels { get; set; }
    }
}
