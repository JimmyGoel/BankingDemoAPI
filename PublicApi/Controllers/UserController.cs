using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{
    public class UserController : BaseAPIController
    {
        private readonly IUser userServices;
        public UserController(IUser _userServices)
        {
            this.userServices = _userServices;
        }
        [AllowAnonymous]
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
        [Authorize]
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
