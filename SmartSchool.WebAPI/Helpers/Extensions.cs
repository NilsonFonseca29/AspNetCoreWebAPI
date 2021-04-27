using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SmartSchool.WebAPI.Helpers
{
    //são formas de extender as classes que ja passuo no c#
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages) 
        {
            var paginationHeader = new PaginationHeader( currentPage, itemsPerPage,  totalItems, totalPages);
    
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader));
            response.Headers.Add("Acess-Control-Expose-Header","Pagination");
        }
    }
}