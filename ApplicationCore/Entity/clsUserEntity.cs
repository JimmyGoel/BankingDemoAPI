using ApplicationCore.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entity
{
    public sealed class clsUserEntity : BaseEntity<int>
    {
        public string userName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownUs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public String Gender { get; set; }
        public String LookingFor { get; set; }
        public String Introduction { get; set; }
        public String Intrested{ get; set; }
        public String city { get; set; }
        public String country { get; set; }
        public ICollection<Photo> photos { get; set; }

       
        public int GetAge()
        {
            return DateOfBirth.GetAgeCalc();
        }

    }
}
