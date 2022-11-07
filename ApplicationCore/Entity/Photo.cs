using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entity
{
    [Table("Photos")]
    public sealed class Photo : BaseEntity<int>
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PhotoId { get; set; }

        public clsUserEntity userEntity { get; set; }
        public int userEntity_Fk { get; set; }
        //public int GetPhotoId()
        //{
        //    return PhotoId.GetPhotoId();
        //}
    }
    //public static class stringExtension
    //{
    //    public static int GetPhotoId(this string photo)
    //    {
    //        return Convert.ToInt32(photo);
    //    }
    //}
}