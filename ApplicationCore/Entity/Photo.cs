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
    }
}