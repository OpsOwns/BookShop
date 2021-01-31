using Microsoft.AspNetCore.Mvc;
using Shop.Shared.API;
using Shop.Store.API.Contract.V1;
using Shop.Store.API.Contract.V1.Models.Book;
using Shop.Store.Application.Command.Book;
using System.Threading.Tasks;

namespace Shop.Store.API.Controllers
{
    [Route(Routes.Book)]
    public class BookController : BaseController
    {
        [HttpPost, Route(Routes.AddBook)]
        public async Task<IActionResult> Create([FromBody] AddBookRequest addBookRequest) => Ok(await Mediator.Send(new CreateBookCommand(addBookRequest.Name, addBookRequest.SureName, addBookRequest.Title, addBookRequest.Year, addBookRequest.IsbnType, addBookRequest.IsbnCode, addBookRequest.CategoryBook, addBookRequest.CategoryName)));
    }
}