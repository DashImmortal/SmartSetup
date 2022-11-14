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
        private readonly SmartDeviceContext _context;
        public SmartDeviceService(SmartDeviceContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<SmartDeviceModel>>> GetAllSmartDeviceModels()
        {
            var devices = _context.SmartDeviceModels.ToListAsync();
            if (devices.Result.Count == 0)
                return NotFound("There is no device to be shown!");
            return await _context.SmartDeviceModels.ToListAsync();
        }

        public async Task<ActionResult<SmartDeviceModel>> GetSingleSmartDevice(int id)
        {
            var smartDeviceModel = await _context.SmartDeviceModels.FindAsync(id);
            
            if (smartDeviceModel == null)
            {
                return NotFound("Device Not Found!");
            }
            
            return smartDeviceModel;
        }

        public async Task<ActionResult<SmartDeviceModel>> AddSmartDeviceModel([FromBody] SmartDeviceModel smartDeviceModel)
        {
            _context.SmartDeviceModels.Add(smartDeviceModel);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetAllSmartDeviceModels", new { id = smartDeviceModel.Id }, smartDeviceModel);
        }

        public async Task<IActionResult> UpdateSmartDeviceModel(int id, [FromBody] SmartDeviceModel smartDeviceModel)
        {
            var device = await _context.SmartDeviceModels.FindAsync(id);

            if(device is null)
                return NotFound("There is no such device!");

            device.Name = smartDeviceModel.Name;
            device.IsOn = smartDeviceModel.IsOn;
            
            //_context.Entry(smartDeviceModel).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SmartDeviceModelExists(id))
                    return NotFound("There is no such device!");
                else
                    throw;
                
            }
            
            return Ok("Device has been updated successfully");
        }

        public async Task<IActionResult> DeleteSmartDeviceModel(int id)
        {
            var smartDeviceModel = await _context.SmartDeviceModels.FindAsync(id);
            if (smartDeviceModel == null)
            {
                return NotFound("Device does not Exists");
            }
            
            _context.SmartDeviceModels.Remove(smartDeviceModel);
            await _context.SaveChangesAsync();

            return Ok("Device has been updated successfully");
        }
        private bool SmartDeviceModelExists(int id)
        {
            return _context.SmartDeviceModels.Any(e => e.Id == id);
        }
    }
}
