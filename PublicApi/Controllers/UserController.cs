using ApplicationCore.Entity;
using ApplicationCore.Enums;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{
    [Authorize]
    public class UserController : BaseAPIController
    {
        //private readonly IUser userServices;
        //private readonly IMapper mapper;
        //private readonly IPhotoServices _photoServices;
        private readonly (IUser userServices, IMapper mapper, IPhotoServices _photoServices) GlobalVar;
        public UserController(IUser _userServices, IMapper mapper, IPhotoServices photoServices)
        {
            //this.userServices = _userServices;
            //this.mapper = mapper;
            //this._photoServices = photoServices;
            this.GlobalVar.userServices = _userServices;
            this.GlobalVar.mapper = mapper;
            this.GlobalVar._photoServices = photoServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync([FromQuery] UserParam userParam)
        {
            var Currentuser = await GlobalVar.userServices.GetuserAsync(null, User.GetuserName());
            userParam.CurrentUser = Currentuser.clsUsers.userName;

            if (string.IsNullOrEmpty(userParam.Gender))
            {
                userParam.Gender = Currentuser.clsUsers.Gender == Gender.Male.ToString() ? Gender.Female.ToString() : Gender.Male.ToString();
            }
            var result = await GlobalVar.userServices.GetuserAsync(userParam);

            if (result.IsSuccess)
            {
                Response.AddPagingHeader(result.clsUsers.CurrentPage, result.clsUsers.PageSize,
               result.clsUsers.TotalCount, result.clsUsers.TotalPage);
                var objMap = GlobalVar.mapper.Map<IEnumerable<UserEntityDTO>>(result.clsUsers);
                //var retunstr = result.clsUsers.ToJson();
                return Ok(objMap.ToJson());
            }
            return NotFound();
        }

        [HttpGet("{Id}", Name = "GetUserDetail")]
        public async Task<IActionResult> GetUsersAsync(int Id)
        {
            var result = await GlobalVar.userServices.GetuserAsync(Id);

            if (result.IsSuccess)
            {
                var objMap = GlobalVar.mapper.Map<UserEntityDTO>(result.clsUsers);
                return Ok(objMap.ToJson());
            }
            return NotFound();
        }
        [HttpPut("{update-user}")]
        public async Task<IActionResult> UpdateUserMember(UserUpdateDTO userUpdateDTO)
        {
            var user = await GlobalVar.userServices.GetuserAsync(null, User.GetuserName());
            GlobalVar.mapper.Map(userUpdateDTO, user.clsUsers);

            var result = await GlobalVar.userServices.UserUpdateDetails(user.clsUsers);
            if (result.Issucess) { return NoContent(); }
            return BadRequest("Fail to update");


        }
        [HttpPost("{add-Photo}")]
        public async Task<IActionResult> AddPhoto(IFormFile file)
        {
            var user = await GlobalVar.userServices.GetuserAsync(null, User.GetuserName());
            var result = await GlobalVar._photoServices.AddPhotoAsync(file, user.clsUsers);
            if (result.Errror == "") return BadRequest();

            //return Ok(result.ImgUpdRst);
            //mapper.Map<PhotoDTO>(user.clsUsers.photos); // (userUpdateDTO, user.clsUsers);
            return CreatedAtRoute("GetUserDetail", new { Id = user.clsUsers.Id }, GlobalVar.mapper.Map<PhotoDTO>(user.clsUsers.photos));
        }
        [HttpPut("set-main-photo/{photoID?}")]
        public async Task<IActionResult> setMainPhoto(int photoID)
        {
            var user = await GlobalVar.userServices.GetuserAsync(null, User.GetuserName());
            var result = await GlobalVar._photoServices.SetMainPhotoAsync(user.clsUsers, photoID);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errror);
        }
        [HttpDelete("Deletephoto/{photoID}")]
        public async Task<IActionResult> DeletePhoto(int photoID)
        {
            var user = await GlobalVar.userServices.GetuserAsync(null, User.GetuserName());
            var result = await GlobalVar._photoServices.DeletphotAsync(user.clsUsers, photoID);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Errror);
        }
    }
}
