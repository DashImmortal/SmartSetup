using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDevice.Services.RoomService
{
    public interface IRoomService
    {
        Task<ActionResult<IEnumerable<RoomModel>>> GetAllRooms();
        Task<ActionResult<RoomModel>> GetSingleRoom(int id);
        Task<IActionResult> UpdateRoom(int id, RoomModel roomModel);
        Task<ActionResult<RoomModel>> AddRoom(RoomModel roomModel);
        Task<IActionResult> DeleteRoom(int id);

    }
}
