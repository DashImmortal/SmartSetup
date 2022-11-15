using Microsoft.AspNetCore.Mvc;
using SmartDevice.Models;
using SmartDevice.Services.RoomService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDevice.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class RoomModelsController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomModelsController(IRoomService service)
        {
            _service = service;
        }

        // GET: API/RoomModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomModel>>> GetAllRooms()
        {
            var response = await _service.GetAllRooms();
            if (response.Result is NotFoundResult)
                return NotFound("There are no rooms to show!");
            return response;
        }

        // GET: API/RoomModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomModel>> GetSingleRoom(int id)
        {
            var response = await _service.GetSingleRoom(id);

            if (response.Result is NotFoundResult)
            {
                return NotFound("The room doesn't exists!");
            }

            return response;
        }

        // PUT: API/RoomModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, RoomModel roomModel)
        {

            var response = await _service.UpdateRoom(id, roomModel);
            if (response is NotFoundResult)
            {
                return NotFound("The room doesn't exists");
            }
            return response;
        }

        // POST: API/RoomModels
        [HttpPost]
        public async Task<ActionResult<RoomModel>> AddRoom(RoomModel roomModel) =>
            await _service.AddRoom(roomModel);

        // DELETE: API/RoomModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var response = await _service.DeleteRoom(id);
            if (response is NotFoundResult)
            {
                return NotFound("The room doesn't exists!");
            }

            return response;
        }
        
    }
}
