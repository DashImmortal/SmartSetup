using Microsoft.AspNetCore.Mvc;
using SmartDevice.Data;
using SmartDevice.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class RoomDevicesController : ControllerBase
    {
        private readonly SmartSetupContext _context;

        public RoomDevicesController(SmartSetupContext context)
        {
            _context = context;
        }

        // GET API/Room5
        [HttpGet("/API/Room{roomId}")]
        public async Task<ActionResult<SmartDeviceModel>> GetRoomSmartDevices(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room is null)
                return NotFound("The room doesn't exists!");
            var devices = new List<SmartDeviceModel>(_context.SmartDevices.Where(device => device.RoomModel.Id == roomId));
            if (!devices.Any())
                return BadRequest("There are no smart devices in this room!");

            return Ok(devices);
        }

        // POST API/Room5
        [HttpPost("/API/Room{roomId}")]
        public async Task<ActionResult<SmartDeviceModel>> AddDevice(int roomId, [FromBody] int deviceId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room is null)
                return NotFound("The room doesn't exists!");
            var device = await _context.SmartDevices.FindAsync(deviceId);
            if (device is null)
                return BadRequest("There is no such device!");
            if (device.RoomModel?.Id == roomId)
                return BadRequest("The device is already in this room!");

            room.SmartDevices.Add(device);
            await _context.SaveChangesAsync();
            return Ok(room.SmartDevices);
        }

        // PUT API/Room5/ToggleAllOn
        [HttpPut("/API/Room{roomId}/ToggleAllOn")]
        public async Task<IActionResult> ToggleAllOn(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);

            if (room is null)
                return NotFound("The room doesn't exists!");

            var devices = new List<SmartDeviceModel>(_context.SmartDevices.Where(device => device.RoomModel.Id == roomId));

            if (!devices.Any())
                return BadRequest("There are no smart devices in this room!");

            foreach (var device in devices)
                device.IsOn = true;

            await _context.SaveChangesAsync();
            return Ok("All the devices has been turned on successfully!");
        }

        // PUT API/RoomDevices/ToggleAllOff/5
        [HttpPut("/API/Room{roomId}/ToggleAllOff")]
        public async Task<IActionResult> ToggleAllOff(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);

            if (room is null)
                return NotFound("The room doesn't exists!");

            var devices = new List<SmartDeviceModel>(_context.SmartDevices.Where(device => device.RoomModel.Id == roomId));

            if (!devices.Any())
                return BadRequest("There are no smart devices in this room!");

            foreach (var device in devices)
                device.IsOn = false;

            await _context.SaveChangesAsync();
            return Ok("All the devices has been turned off successfully!");
        }

        // PUT API/Room5/ToggleAllOn/1
        [HttpPut("/API/Room{roomId}/ToggleAllOn/Type{type}")]
        public async Task<IActionResult> ToggleAllOn(int roomId, SmartObjectType type)
        {
            var room = await _context.Rooms.FindAsync(roomId);

            if (room is null)
                return NotFound("The room doesn't exists!");

            var devices =
                new List<SmartDeviceModel>(_context.SmartDevices.Where(device =>
                    device.RoomModel.Id == roomId && device.Type == type));

            if (!devices.Any())
                return BadRequest($"There are no smart devices of type {type} in this room!");

            foreach (var device in devices)
                device.IsOn = true;

            await _context.SaveChangesAsync();
            return Ok($"All the {type} devices has been turned on successfully!");
        }

        // PUT API/RoomDevices/ToggleAllOff/5
        [HttpPut("/API/Room{roomId}/ToggleAllOff/Type{type}")]
        public async Task<IActionResult> ToggleAllOff(int roomId, SmartObjectType type)
        {
            var room = await _context.Rooms.FindAsync(roomId);

            if (room is null)
                return NotFound("The room doesn't exists!");

            var devices =
                new List<SmartDeviceModel>(_context.SmartDevices.Where(device =>
                    device.RoomModel.Id == roomId && device.Type == type));

            if (!devices.Any())
                return BadRequest("There are no smart devices in this room!");

            foreach (var device in devices)
                device.IsOn = false;

            await _context.SaveChangesAsync();
            return Ok($"All the {type} devices has been turned off successfully!");
        }
        
        // DELETE API/Room5
        [HttpDelete("/API/Room{roomId}")]
        public async Task<IActionResult> Delete(int roomId,[FromBody] int deviceId)
        {
            var room = await _context.Rooms.FindAsync(roomId);

            if (room is null)
                return NotFound("The room doesn't exists!");

            var device = await _context.SmartDevices.FindAsync(deviceId);

            if (device is null || device.RoomModel.Id != roomId)
                return BadRequest("Device doesn't exists in this room!");

            device.RoomModel = null;
            await _context.SaveChangesAsync();
            return Ok("The device has been removed from this room!");
        }
    }
}
