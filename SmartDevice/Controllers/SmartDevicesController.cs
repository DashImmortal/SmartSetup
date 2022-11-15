using Microsoft.AspNetCore.Mvc;
using SmartDevice.Models;
using SmartDevice.Services.SmartDeviceService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDevice.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class SmartDevicesController : ControllerBase
    {
        private readonly ISmartDeviceService _service;

        public SmartDevicesController(ISmartDeviceService service)
        {
            _service = service;
        }

        // GET: API/SmartDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SmartDeviceModel>>> GetAllSmartDeviceModels() =>
            await _service.GetAllSmartDeviceModels();

        // GET: API/SmartDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SmartDeviceModel>> GetSingleSmartDevice(int id) =>
            await _service.GetSingleSmartDevice(id);

        // POST: API/SmartDevices
        [HttpPost]
        public async Task<ActionResult<SmartDeviceModel>> AddSmartDeviceModel(SmartDeviceModel smartDeviceModel) =>
            await _service.AddSmartDeviceModel(smartDeviceModel);

        // PUT: API/SmartDevices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSmartDeviceModel(int id, SmartDeviceModel smartDeviceModel) =>
            await _service.UpdateSmartDeviceModel(id, smartDeviceModel);

        // PUT: API/SmartDevices/Toggle/5
        [HttpPut("{id}/Toggle")]
        public async Task<IActionResult> ToggleSmartDeviceModel(int id) => await _service.ToggleSmartDeviceModel(id);

        // DELETE: API/SmartDevices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSmartDeviceModel(int id) => await _service.DeleteSmartDeviceModel(id);
        
    }
}
