using System;
using System.Collections.Generic;

namespace PublicApi.DTO
{
    public class UserEntityDTO
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public int Age { get; set; }
        public string KnownUs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public String Gender { get; set; }
        public String LookingFor { get; set; }
        public String Introduction { get; set; }
        public String Intrested { get; set; }
        public String city { get; set; }
        public String country { get; set; }
        public ICollection<PhotoDTO> photos { get; set; }
        public string PhotoUrl { get; set; }
    }
}
