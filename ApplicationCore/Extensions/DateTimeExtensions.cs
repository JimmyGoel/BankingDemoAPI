using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Extensions
{
   public static class DateTimeExtensions
    {
        public static int GetAgeCalc(this DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob.Date > today.AddYears(age)) age--;
            return age;
        }
    }
}
