using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class clsUserServices : IUser
    {
        private readonly DataContext dbContext;
        private readonly IAppLogger<clsUserServices> logger;
        private readonly IMapper mapper;
        public clsUserServices(IAppLogger<clsUserServices> _logger, DataContext _dbContext, IMapper mapper)
        {
            this.dbContext = _dbContext;
            this.logger = _logger;
            this.mapper = mapper;
        }
        public async Task<(bool IsSuccess, IEnumerable<clsUserEntity> clsUsers, string Errror)> GetuserAsync()
        {
            try
            {
                logger.LogInformation("Query user");
                var users = await dbContext.Users
                                .Include(p => p.photos).ToListAsync();
                if (users != null && users.Any())
                {
                    logger?.LogInformation($"{users.Count} customer(s) found");
                    //var result = mapper.Map<IEnumerable<clsUserEntity>(users);
                    return (true, users, null);
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

        public async Task<(bool IsSuccess, clsUserEntity clsUsers, string Errror)> GetuserAsync(int? Id, string userName = null)
        {
            try
            {
                logger.LogInformation("Query user");
                var user = await dbContext.Users
                     .Include(p => p.photos).FirstOrDefaultAsync(option =>
                     (Id == null ? option.userName == userName : option.Id == Id));
                if (user != null)
                {
                    logger?.LogInformation($"{user} customer(s) found");
                    //var result = mapper.Map<IEnumerable<clsUserEntity>(users);
                    return (true, user, null);
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



        public async Task<(bool Issucess, string error)> UserUpdateDetails(clsUserEntity clsUsers)
        {
            try
            {
                dbContext.Entry(clsUsers).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return (true, "No Error");
            }
            catch (Exception ex)
            {

                return (false, ex.StackTrace);
            }

        }
    }
}
