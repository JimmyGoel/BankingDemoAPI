using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUser
    {
        Task<(bool IsSuccess, IEnumerable<clsUserEntity> clsUsers, string Errror)> GetuserAsync();
        Task<(bool IsSuccess, clsUserEntity clsUsers, string Errror)> GetuserAsync(int Id);

    }
}
