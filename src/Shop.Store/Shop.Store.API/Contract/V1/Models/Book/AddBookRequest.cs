using Microsoft.AspNetCore.Http;

namespace Shop.Store.API.Contract.V1.Models.Book
{
    public record AddBookRequest(string Name, string SureName, string Title,
        int Year, int IsbnType, string IsbnCode, int CategoryBook, string CategoryName, IFormFile File);
}
