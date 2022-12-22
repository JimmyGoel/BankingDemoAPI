using ApplicationCore.Entity;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAccountRegistor
    {
        Task<(bool IsSuccess, clsUserEntity clsUsers, string Errror)> RegistorUserAsync(clsUserEntity registor);

    }
}
