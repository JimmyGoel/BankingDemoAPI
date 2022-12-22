namespace ApplicationCore.Entity
{
    public class PaginationHelper
    {
        public PaginationHelper(int currentpage, int itemperpage, int totalitems, int totalpage)
        {
            CurrentPage = currentpage;
            ItemPerPage = itemperpage;
            TotalItems = totalitems;
            TotalPage = totalpage;
        }
        public int CurrentPage { get; set; }
        public int ItemPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPage { get; set; }

    }
}
