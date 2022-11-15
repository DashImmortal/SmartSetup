#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Data;
using SmartDevice.Models;

namespace SmartDevice.Services.SmartDeviceService
{
    public class SmartDeviceService : Controller, ISmartDeviceService
    {
        private readonly SmartSetupContext _context;
        public SmartDeviceService(SmartSetupContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<SmartDeviceModel>>> GetAllSmartDeviceModels()
        {
            var devices = _context.SmartDevices.ToListAsync();
            if (devices.Result.Count == 0)
                return NotFound("There is no device to be shown!");
            return await _context.SmartDevices.ToListAsync();
        }

        public async Task<ActionResult<SmartDeviceModel>> GetSingleSmartDevice(int id)
        {
            var smartDeviceModel = await _context.SmartDevices.FindAsync(id);
            
            if (smartDeviceModel == null)
            {
                return NotFound("Device Not Found!");
            }
            
            return smartDeviceModel;
        }

        public async Task<ActionResult<SmartDeviceModel>> AddSmartDeviceModel([FromBody] SmartDeviceModel smartDeviceModel)
        {
            _context.SmartDevices.Add(smartDeviceModel);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetAllSmartDeviceModels", new { id = smartDeviceModel.Id }, smartDeviceModel);
        }

        public async Task<IActionResult> UpdateSmartDeviceModel(int id, [FromBody] SmartDeviceModel smartDeviceModel)
        {
            var device = await _context.SmartDevices.FindAsync(id);

            if(device is null)
                return NotFound("There is no such device!");

            device.Name = smartDeviceModel.Name;
            device.Type = smartDeviceModel.Type;
            device.IsOn = smartDeviceModel.IsOn;

            await _context.SaveChangesAsync();
            
            return Ok("Device has been updated successfully");
        }

        public async Task<IActionResult> ToggleSmartDeviceModel(int id)
        {
            if(_context.SmartDevices.All(device => device.Id != id))
                return NotFound("There is no such device!");
            var device = await _context.SmartDevices.FindAsync(id);
            device.IsOn = !device.IsOn;
            var stateDependentString = device.IsOn ? "on" : "off";
            await _context.SaveChangesAsync();
            return Ok($"Device has been turned {stateDependentString} successfully");
        }

        public async Task<IActionResult> DeleteSmartDeviceModel(int id)
        {
            var smartDeviceModel = await _context.SmartDevices.FindAsync(id);
            if (smartDeviceModel == null)
            {
                return NotFound("Device does not Exists");
            }
            
            _context.SmartDevices.Remove(smartDeviceModel);
            await _context.SaveChangesAsync();

            return Ok("Device has been updated successfully");
        }
    }
}
