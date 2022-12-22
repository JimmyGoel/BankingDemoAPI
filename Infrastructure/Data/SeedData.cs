using ApplicationCore.Entity;
using ApplicationCore.Extensions;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SeedData
    {
        public static async Task SeedUser(DataContext dataContext)
        {
            if (await dataContext.Users.AnyAsync()) return;
            var solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).GetDirectories()[4].FullName;
            var userData = await System.IO.File.ReadAllTextAsync(solutiondir + $"/Data/jsonSeeds.json");
            var users = userData.FromListJson<clsUserEntity>();
            foreach (var item in users)
            {
                using var hmac = new HMACSHA512();
                item.userName = item.userName.ToLower();
                item.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                item.PasswordSalt = hmac.Key;
                dataContext.Users.Add(item);
            }
            await dataContext.SaveChangesAsync();
        }
    }
}
