using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Data;
using SmartDevice.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly SmartSetupContext _context;

        public RoomService(SmartSetupContext context)
        {
            _context = context;
        }
        
        public async Task<ActionResult<IEnumerable<RoomModel>>> GetAllRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            if (rooms == null || rooms.Count == 0)
                return new NotFoundResult();
            
            return rooms;
        }
        
        public async Task<ActionResult<RoomModel>> GetSingleRoom(int id)
        {
            var roomModel = await _context.Rooms.FindAsync(id);

            if (roomModel == null)
                return new NotFoundResult();
            
            return roomModel;
        }
        
        public async Task<IActionResult> UpdateRoom(int id, RoomModel roomModel)
        {

            var room = await _context.Rooms.FindAsync(id);

            if (room is null)
                return new NotFoundResult();

            room.Name = roomModel.Name;
            room.SmartDevices = roomModel.SmartDevices;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomModelExists(id))
                    return new NotFoundResult();
            }

            return new OkResult();
        }
        
        public async Task<ActionResult<RoomModel>> AddRoom(RoomModel roomModel)
        {
            _context.Rooms.Add(roomModel);

            await _context.SaveChangesAsync();
            return new OkResult();
        }
        
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var roomModel = await _context.Rooms.FindAsync(id);
            if (roomModel == null)
                return new NotFoundResult();

            _context.Rooms.Remove(roomModel);
            await _context.SaveChangesAsync();

            return new OkResult();
        }

        private bool RoomModelExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
