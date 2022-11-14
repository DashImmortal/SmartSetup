using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Data;
using SmartDevice.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SmartDevice.Controllers;

namespace SmartDevice.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly RoomsContext _context;

        public RoomService(RoomsContext context)
        {
            _context = context;
        }
        
        public async Task<ActionResult<IEnumerable<RoomModel>>> GetAllRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            if (rooms == null || rooms.Count == 0)
                return new NotFoundResult();
            foreach (var room in rooms)
            {
                if (room.SmartDevicesSerialized == null)
                    continue;
                room.SmartDevices =
                    JsonConvert.DeserializeObject<ObservableCollection<SmartDeviceModel>>(room.SmartDevicesSerialized);
            }
            
            return rooms;
        }
        
        public async Task<ActionResult<RoomModel>> GetSingleRoom(int id)
        {
            var roomModel = await _context.Rooms.FindAsync(id);

            if (roomModel == null)
            {
                return new NotFoundResult();
                //return NotFound();
            }

            roomModel.SmartDevices =
                JsonConvert.DeserializeObject<ObservableCollection<SmartDeviceModel>>(roomModel.SmartDevicesSerialized);
            return roomModel;
        }
        
        public async Task<IActionResult> UpdateRoom(int id, RoomModel roomModel)
        {

            var room = await _context.Rooms.FindAsync(id);

            if (room is null)
                return new NotFoundResult();

            room.Name = roomModel.Name;
            room.SmartDevicesSerialized = JsonConvert.SerializeObject(roomModel.SmartDevices);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomModelExists(id))
                {
                    return new NotFoundResult();
                    //return NotFound();
                }
            }

            return new OkResult();
            //return new Ok();
        }
        
        public async Task<ActionResult<RoomModel>> AddRoom(RoomModel roomModel)
        {
            _context.Rooms.Add(roomModel);
            var devices = JsonConvert.SerializeObject(roomModel.SmartDevices);
            await _context.SaveChangesAsync();

            return new CreatedAtActionResult("GetAllRooms", nameof(RoomModelsController), new { id = roomModel.Id }, roomModel);
        }
        
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var roomModel = await _context.Rooms.FindAsync(id);
            if (roomModel == null)
            {
                return new NotFoundResult();
                //return NotFound();
            }

            _context.Rooms.Remove(roomModel);
            await _context.SaveChangesAsync();

            return new NoContentResult();
            //return NoContent();
        }

        private bool RoomModelExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
