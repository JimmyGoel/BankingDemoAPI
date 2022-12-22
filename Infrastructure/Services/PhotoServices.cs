using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PhotoServices : IPhotoServices
    {
        private readonly DataContext dbContext;
        private readonly IAppLogger<clsUserServices> logger;

        private readonly Cloudinary _cloudinery;
        public PhotoServices(IOptions<CloudinarySettings> config, IAppLogger<clsUserServices>
            _logger, DataContext _dbContext)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.APIKey,
                config.Value.APISecret
                );
            _cloudinery = new Cloudinary(acc);

            this.dbContext = _dbContext;
            this.logger = _logger;
        }
        public async Task<(bool IsSuccess, ImageUploadResult ImgUpdRst, string Errror)>
            AddPhotoAsync(IFormFile file, clsUserEntity user)
        {
            try
            {

                var uploadResult = new ImageUploadResult();
                if (file.Length > 0)
                {
                    using var stream = file.OpenReadStream();
                    var uploadParam = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Height("500").Width("500").Crop("fill").Gravity("face")

                    };
                    uploadResult = await _cloudinery.UploadAsync(uploadParam);
                }

                var photo = new Photo()
                {
                    Url = uploadResult.SecureUrl.AbsoluteUri,
                    PhotoId = uploadResult.PublicId
                };

                if (user.photos.Count == 0)
                {
                    photo.IsMain = true;
                }
                user.photos.Add(photo);
                await dbContext.SaveChangesAsync();
                return (true, uploadResult, "No Error");

            }
            catch (Exception ex)
            {
                return (true, null, ex.StackTrace);
            }

        }

        public async Task<(bool IsSuccess, DeletionResult ImgUpdRst, string Errror)> DeletphotAsync(clsUserEntity user, int PhotoID)
        {
            var photo = user.photos.FirstOrDefault(x => x.Id == PhotoID);
            if (photo == null) return (false, null, "Not Found");
            if (photo.IsMain) return (false, null, "You cannot Delete main photo");
            if (photo.PhotoId != null)
            {
                var deleteParam = new DeletionParams(photo.PhotoId);
                var result = await _cloudinery.DestroyAsync(deleteParam);
                if (result.Error != null) return (false, null, result.Error.Message);
                user.photos.Remove(photo);
                await dbContext.SaveChangesAsync();
                return (true, result, "No Error");
            }

            return (false, null, "Error");
        }

        public async Task<(bool IsSuccess, string Errror)> SetMainPhotoAsync(clsUserEntity user, int PhotID)
        {
            var photo = user.photos.FirstOrDefault(x => x.Id == PhotID);
            if (photo.IsMain) return (false, "This Photo Already your Main Photo");
            var currentmain = user.photos.FirstOrDefault(x => x.IsMain);
            if (currentmain.IsMain) currentmain.IsMain = false;
            photo.IsMain = true;
            await dbContext.SaveChangesAsync();
            return (true, "Photo Main Set Succesfully");
        }
    }
}
