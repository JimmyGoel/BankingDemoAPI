using System;

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
        public static DateTime GetmaxAge(this int maxAge)
        {
            return DateTime.Now.AddYears(-maxAge - 1);

        }
        public static DateTime GetminAge(this int minAge)
        {
            return DateTime.Now.AddYears(-minAge - 1);
        }
    }
}
