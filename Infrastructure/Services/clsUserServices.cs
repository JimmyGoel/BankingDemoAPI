using ApplicationCore.Entity;
using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class clsUserServices : IUser, IuserExtend
    {
        //private readonly DataContext dbContext;
        //private readonly IAppLogger<clsUserServices> logger;
        //private readonly IMapper mapper;
        private readonly (DataContext dbContext, IAppLogger<clsUserServices> logger, IMapper mapper) GlobalVar;
        public clsUserServices(IAppLogger<clsUserServices> _logger, DataContext _dbContext, IMapper mapper)
        {
            //this.dbContext = _dbContext;
            //this.logger = _logger;
            //this.mapper = mapper;
            this.GlobalVar.dbContext = _dbContext;
            this.GlobalVar.logger = _logger;
            this.GlobalVar.mapper = mapper;
        }
        public async Task<(bool IsSuccess, PagedList<clsUserEntity> clsUsers, string Errror)> GetuserAsync(UserParam userParam)
        {
            try
            {
                GlobalVar.logger.LogInformation("Query user");
                var users = GlobalVar.dbContext.Users.Include(p => p.photos)
                    .Where(u => u.userName != userParam.CurrentUser)
                    .Where(u => u.Gender == userParam.Gender)
                    .AsNoTracking();
               

                users = users.Where(u => u.DateOfBirth >= userParam.maxAge.GetmaxAge() && u.DateOfBirth <= userParam.minAge.GetminAge());

                users = userParam.Orderby switch
                {
                    "created" => users.OrderByDescending(u => u.Created),
                    _ => users.OrderByDescending(u => u.LastActive),
                };

                if (users != null && users.Any())
                {
                    var pagelist = await PagedList<clsUserEntity>.CreateAsync(users, userParam.PageNumber, userParam.PageSize);
                    GlobalVar.logger?.LogInformation($"{pagelist.Count} customer(s) found");
                    //var result = mapper.Map<IEnumerable<clsUserEntity>(users);
                    return (true, pagelist, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception Ex)
            {
                GlobalVar.logger?.LogError(Ex.ToString());
                return (false, null, Ex.ToString());
                throw new NotFoundExceptions();
            }
        }

        public async Task<(bool IsSuccess, clsUserEntity clsUsers, string Errror)> GetuserAsync(int? Id, string userName = null)
        {
            try
            {
                GlobalVar.logger.LogInformation("Query user");
                var user = await GlobalVar.dbContext.Users
                     .Include(p => p.photos).FirstOrDefaultAsync(option =>
                     (Id == null ? option.userName == userName : option.Id == Id));
                if (user != null)
                {
                    GlobalVar.logger?.LogInformation($"{user} customer(s) found");
                    //var result = mapper.Map<IEnumerable<clsUserEntity>(users);
                    return (true, user, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception Ex)
            {
                GlobalVar.logger?.LogError(Ex.ToString());
                return (false, null, Ex.ToString());
                throw new NotFoundExceptions();
            }
        }

        public async Task<bool> SaveAllChanges()
        {
            return await GlobalVar.dbContext.SaveChangesAsync() > 0;
        }

        public async Task<(bool Issucess, string error)> UserUpdateDetails(clsUserEntity clsUsers)
        {
            try
            {
                GlobalVar.dbContext.Entry(clsUsers).State = EntityState.Modified;
                await GlobalVar.dbContext.SaveChangesAsync();
                return (true, "No Error");
            }
            catch (Exception ex)
            {

                return (false, ex.StackTrace);
            }

        }
    }
}
