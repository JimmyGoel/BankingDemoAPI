using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{
    [Authorize]
    public class UserController : BaseAPIController
    {
        private readonly IUser userServices;
        private readonly IMapper mapper;
        public UserController(IUser _userServices, IMapper mapper)
        {
            this.userServices = _userServices;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await userServices.GetuserAsync();
            if (result.IsSuccess)
            {
                var objMap = mapper.Map<IEnumerable<UserEntityDTO>>(result.clsUsers);
                //var retunstr = result.clsUsers.ToJson();
                return Ok(objMap.ToJson());
            }
            return NotFound();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUsersAsync(int Id)
        {
            var result = await userServices.GetuserAsync(Id);

            if (result.IsSuccess)
            {
                var objMap = mapper.Map<UserEntityDTO>(result.clsUsers);
                return Ok(objMap.ToJson());
            }
            return NotFound();
        }
    }
}
