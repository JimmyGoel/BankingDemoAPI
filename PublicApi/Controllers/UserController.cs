using ApplicationCore.Entity;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{
    [Authorize]
    public class UserController : BaseAPIController
    {
        private readonly IUser userServices;
        private readonly IMapper mapper;
        private readonly IPhotoServices _photoServices;
        public UserController(IUser _userServices, IMapper mapper, IPhotoServices photoServices)
        {
            this.userServices = _userServices;
            this.mapper = mapper;
            this._photoServices = photoServices;
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

        [HttpGet("{Id}", Name = "GetUserDetail")]
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
        [HttpPut("{update-user}")]
        public async Task<IActionResult> UpdateUserMember(UserUpdateDTO userUpdateDTO)
        {
            var user = await userServices.GetuserAsync(null, User.GetuserName());
            mapper.Map(userUpdateDTO, user.clsUsers);

            var result = await userServices.UserUpdateDetails(user.clsUsers);
            if (result.Issucess) { return NoContent(); }
            return BadRequest("Fail to update");


        }
        [HttpPost("{add-Photo}")]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            var user = await userServices.GetuserAsync(null, User.GetuserName());
            var result = await _photoServices.AddPhotoAsync(file, user.clsUsers);
            if (result.Errror == "") return BadRequest();

            //return Ok(result.ImgUpdRst);
            //mapper.Map<PhotoDTO>(user.clsUsers.photos); // (userUpdateDTO, user.clsUsers);
            return CreatedAtRoute("GetUserDetail", new { Id = user.clsUsers.Id }, mapper.Map<PhotoDTO>(user.clsUsers.photos));
        }
        [HttpPut("set-main-photo/{photoID}")]
        public async Task<IActionResult> setMainPhoto(int photoID)
        {
            var user = await userServices.GetuserAsync(null, User.GetuserName());
            var result = await _photoServices.SetMainPhotoAsync(user.clsUsers, photoID);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errror);
        }
        [HttpDelete("Deletephoto/{photoID}")]
        public async Task<IActionResult> DeletePhoto(int photoID)
        {
            var user = await userServices.GetuserAsync(null, User.GetuserName());
            var result = await _photoServices.DeletphotAsync(user.clsUsers, photoID);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errror);
        }
    }
}
