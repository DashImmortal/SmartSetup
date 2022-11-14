using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Models;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SmartDevice.Data
{
    public class RoomsContext : DbContext
    {
        public RoomsContext (DbContextOptions<RoomsContext> options)
            : base(options)
        {
        }

        public DbSet<RoomModel> Rooms { get; set; }
    }
}
