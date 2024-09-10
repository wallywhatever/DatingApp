using System;
using System.Text.Json;
using API.Helpers;

namespace API.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader<T>(this HttpResponse response, PagedList<T> data)
    {
        var paginationHeader = new PaginationHeader(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);

        var jsonOptions = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));

        // This is needed to tell CORS to make the pagination header available to the client
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination"); 
        //response.Headers.AccessControlExposeHeaders = "Pagination";

    }
}
