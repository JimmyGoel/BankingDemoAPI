using ApplicationCore.Entity;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ILoginUser
    {
        Task<(bool IsSuccess, clsUserEntity clsUsers, string Errror)> LoginUserAsync(clsUserEntity registor, string password = null);

    }
}
