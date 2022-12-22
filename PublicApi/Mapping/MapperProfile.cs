using ApplicationCore.Entity;
using AutoMapper;
using PublicApi.DTO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PublicApi.Mapping
{
    public class MapperProfile
    {
        public static MapperConfiguration RegisterMaps()
        {
            var HMA = new HMACSHA512();
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<RegistorDTO, clsUserEntity>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => HMA.ComputeHash(Encoding.UTF8.GetBytes(src.Password))))
                .ForMember(dest => dest.PasswordSalt, opt => opt.MapFrom(src => HMA.Key))
                .ReverseMap();

                config.CreateMap<LoginDTO, clsUserEntity>().ReverseMap();
                config.CreateMap<Photo, PhotoDTO>().ReverseMap();
                config.CreateMap<clsUserEntity, UserEntityDTO>()
               .ForMember(dest => dest.PhotoUrl,
                opt => opt.MapFrom(src => src.photos.FirstOrDefault(x => x.IsMain).Url))
                .ReverseMap();
                config.CreateMap<UserUpdateDTO, clsUserEntity>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
