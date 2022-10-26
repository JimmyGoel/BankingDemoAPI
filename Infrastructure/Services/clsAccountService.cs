using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class clsAccountService : IAccountRegistor, ILoginUser
    {
        private readonly DataContext dbContext;
        private readonly IAppLogger<clsUserServices> logger;
        private readonly IMapper mapper;
        public clsAccountService(IAppLogger<clsUserServices> _logger, DataContext _dbContext, IMapper mapper)
        {
            this.dbContext = _dbContext;
            this.logger = _logger;
            this.mapper = mapper;
        }

        public async Task<(bool IsSuccess, clsUserEntity clsUsers, string Errror)> LoginUserAsync(clsUserEntity registor, string password = null)
        {
            try
            {
                logger.LogInformation("Query user");

                var UserExists = await dbContext.Users.SingleOrDefaultAsync(x => x.userName == registor.userName);

                if (UserExists != null)
                {
                    logger?.LogInformation($"{UserExists} found");

                    using var hmac = new HMACSHA512(UserExists.PasswordSalt);
                    var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                    for (int i = 0; i < ComputeHash.Length; i++)
                    {
                        if (ComputeHash[i] != UserExists.PasswordHash[i]) return (false, null, "Password Not Matched");
                    }

                   // var resultobj = mapper.Map<clsUserEntity>(registor);
                    return (true, UserExists, null);
                }
                return (false, null, "User Not Exists");
            }
            catch (Exception Ex)
            {
                logger?.LogError(Ex.ToString());
                return (false, null, Ex.ToString());
                throw new NotFoundExceptions();
            }
        }

        public async Task<(bool IsSuccess, clsUserEntity clsUsers, string Errror)> RegistorUserAsync(clsUserEntity registor)
        {
            try
            {
                logger.LogInformation("Query user");

                var UserExists = dbContext.Users.AnyAsync(x => x.userName == registor.userName);
                if (await UserExists)
                {
                    return (false, null, "Already Exists");
                }
                await dbContext.AddAsync(registor);
                var result = await dbContext.SaveChangesAsync();

                if (!result.Equals(null))
                {
                    logger?.LogInformation($"{result} found");
                    var resultobj = mapper.Map<clsUserEntity>(registor);
                    return (true, resultobj, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception Ex)
            {
                logger?.LogError(Ex.ToString());
                return (false, null, Ex.ToString());
                throw new NotFoundExceptions();
            }
        }
    }
}
