using ApplicationCore.Entity;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUser
    {
        Task<(bool IsSuccess, PagedList<clsUserEntity> clsUsers, string Errror)> GetuserAsync(UserParam userParam);
        Task<(bool IsSuccess, clsUserEntity clsUsers, string Errror)> GetuserAsync(int? Id, string userName = null);
        Task<(bool Issucess, string error)> UserUpdateDetails(clsUserEntity clsUsers);
    }
    public interface IuserExtend : IUser
    {
        Task<bool> SaveAllChanges();
    }
}
