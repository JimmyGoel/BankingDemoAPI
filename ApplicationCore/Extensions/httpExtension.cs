using ApplicationCore.Entity;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Extensions
{
    public static class httpExtension
    {
        public static void AddPagingHeader(this HttpResponse response, int currentpage,
            int itemperpage, int totalitems, int totalpage)
        {
            var pagingHeader = new PaginationHelper(currentpage, itemperpage, totalitems, totalpage);
            response.Headers.Add("pagining", pagingHeader.ToJson());
            response.Headers.Add("Access-Control-Expose-Headers", "pagining");
        }
    }
}
