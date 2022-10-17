using ApplicationCore.Entity;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{

    public class AccountController : BaseAPIController
    {
        private readonly IAccountRegistor _accountService;
        private readonly ILoginUser _LoginService;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper mapper;
        public AccountController(IAccountRegistor accountService, ILoginUser LoginService, IMapper mapper, ITokenServices
            tokenServices)
        {
            this._accountService = accountService;
            this._LoginService = LoginService;
            this.mapper = mapper;
            this._tokenServices = tokenServices;
        }

        [HttpPost("registor-user")]
        public async Task<IActionResult> RegistorUserAsync(RegistorDTO registor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }
            var srRegistorObj = mapper.Map<clsUserEntity>(registor);
            var result = await _accountService.RegistorUserAsync(srRegistorObj);
            if (result.IsSuccess)
            {
                var token = _tokenServices.TokenServices(srRegistorObj);
                return Ok(new { rst = result.clsUsers.ToJson(), tkn = token });
            }
            return BadRequest(result.Errror);
        }

        [HttpPost("Login-user")]
        public async Task<IActionResult> LoginUserAsync(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }
            var srLoginObj = mapper.Map<clsUserEntity>(loginDTO);
            var result = await _LoginService.LoginUserAsync(srLoginObj, loginDTO.Password);
            if (result.IsSuccess)
            {
                var token = _tokenServices.TokenServices(srLoginObj);
                return Ok(new { rst = result.clsUsers.ToJson(), tkn = token });
            }
            return Unauthorized(result.Errror);
        }
    }

}
