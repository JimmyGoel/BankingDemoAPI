using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicApi.DTO
{
    public class UserUpdateDTO
    {
        public String LookingFor { get; set; }
        public String Introduction { get; set; }
        public String Intrested { get; set; }
        public String city { get; set; }
        public String country { get; set; }
    }
}
