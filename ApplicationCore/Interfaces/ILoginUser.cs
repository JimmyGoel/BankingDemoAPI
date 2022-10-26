using ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ILoginUser
    {
        Task<(bool IsSuccess, clsUserEntity clsUsers, string Errror)> LoginUserAsync(clsUserEntity registor, string password = null);

    }
}
