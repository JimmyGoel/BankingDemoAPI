using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PublicApi.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi.Controllers
{

    public class AccountController : BaseAPIController
    {
        private readonly IAccountRegistor _accountService;
        private readonly ILoginUser _LoginService;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper mapper;
       // private readonly IMemoryCache _memoryCache;
        public AccountController(IAccountRegistor accountService, ILoginUser LoginService, IMapper mapper, ITokenServices
            tokenServices)
        {
            //_memoryCache = memoryCache;
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
                var resp = new UserDTO
                {
                    userName = result.clsUsers.userName,
                    Token = token,
                    Id = result.clsUsers.Id,
                    Gender = result.clsUsers.Gender,
                    // Photourl = result.clsUsers.photos.FirstOrDefault(x => x.IsMain)?.Url
                };
                return Ok(resp);
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
                var token = _tokenServices.TokenServices(result.clsUsers);
                //var resp = new { userName = result.clsUsers.userName, Token = token, Id = result.clsUsers.Id, Photourl = result.clsUsers.photos.FirstOrDefault(x => x.IsMain)?.Url };
                //return Ok(new { rst = result.clsUsers.ToJson(), tkn = token });
                var resp = new UserDTO
                {
                    userName = result.clsUsers.userName,
                    Token = token,
                    Id = result.clsUsers.Id,
                    Photourl = result.clsUsers.photos.FirstOrDefault(x => x.IsMain)?.Url,
                    Gender = result.clsUsers.Gender,
                };
                return Ok(resp);
            }
            return Unauthorized(result.Errror);
        }
    }

}
