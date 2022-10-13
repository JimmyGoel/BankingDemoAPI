using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser userServices;
        public UserController(IUser _userServices)
        {
            this.userServices = _userServices;
        }
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await userServices.GetuserAsync();
            if (result.IsSuccess)
            {
                var retunstr = result.clsUsers.ToJson();
                return Ok(retunstr);
            }
            return NotFound();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsersAsync(int Id)
        {
            var result = await userServices.GetuserAsync(Id);

            if (result.IsSuccess)
                return Ok(result.clsUsers);
            return NotFound();
        }
    }
}
