using System;
using System.Threading.Tasks;
using AccessControl.API.ZebraModels;
using AccessControl.API.ZebraRepo;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.API.ZebraControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZebraController : ControllerBase
    {
        private readonly IZebraRepository _repo;
        public ZebraController(IZebraRepository repo)
        {
            _repo = repo;

        }
        [HttpPost("Receiving")]
        public async Task<IActionResult> RMA_Receiving(RMA_Receiving receiving)
        {

            var isSuccess = await _repo.AddRmaReceiving(receiving);

            if (isSuccess)
            {
                return NoContent();
            }

            else
            {
                return BadRequest("Add RMA Receiving failed!");
            }
        }
        [HttpPost("StationUser")]
        public IActionResult ZebraUser(Zebra_User user){

            user.User_Login_Time = DateTime.Now;
            user.LastModifyDate = DateTime.Now;
            _repo.UpdateUser(user);

            return Ok();


        }
    }
}