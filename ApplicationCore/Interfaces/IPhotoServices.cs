using ApplicationCore.Entity;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
   public interface IPhotoServices
    {
        Task<(bool IsSuccess, ImageUploadResult ImgUpdRst, string Errror)> AddPhotoAsync(IFormFile file, clsUserEntity user);
        Task<(bool IsSuccess, DeletionResult ImgUpdRst, string Errror)> DeletphotAsync(clsUserEntity photo,int PhotoID);
        Task<(bool IsSuccess, string Errror)> SetMainPhotoAsync(clsUserEntity user, int PhotID);

    }
}
