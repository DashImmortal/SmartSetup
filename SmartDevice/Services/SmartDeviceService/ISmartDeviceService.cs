#nullable enable
using Microsoft.AspNetCore.Mvc;
using SmartDevice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDevice.Services.SmartDeviceService
{
    public interface ISmartDeviceService
    {
        Task<ActionResult<IEnumerable<SmartDeviceModel>>> GetAllSmartDeviceModels();
        Task<ActionResult<SmartDeviceModel>> GetSingleSmartDevice(int id);
        Task<ActionResult<SmartDeviceModel>> AddSmartDeviceModel(SmartDeviceModel smartDeviceModel);
        Task<IActionResult> UpdateSmartDeviceModel(int id, SmartDeviceModel smartDeviceModel);
        Task<IActionResult> DeleteSmartDeviceModel(int id);
    }
}
