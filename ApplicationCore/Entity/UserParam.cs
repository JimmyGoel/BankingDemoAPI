namespace ApplicationCore.Entity
{
    public class UserParam
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string CurrentUser { get; set; }
        public string Gender { get; set; }

        public int minAge { get; set; }
        public int maxAge { get; set; }
        public string Orderby { get; set; } = "lastActive";

        //public static void GetMinandMaxAge(out DateTime MinAge, out DateTime MaxAge)
        //{
        //    _minAge = MinAge;
        //    MinAge = DateTime.Now.AddYears(-maxAge - 1);
        //    MaxAge = DateTime.Now.AddYears(-minAge);
        //}

        //public DateTime GetMaxAge()
        //{
        //    return minAge.GetmaxAge();

        //}
        //public DateTime GetMinAge()
        //{
        //    return minAge.GetminAge();
        //}


    }

}
