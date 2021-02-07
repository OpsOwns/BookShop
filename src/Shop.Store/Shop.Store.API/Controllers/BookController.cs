using Microsoft.AspNetCore.Mvc;
using Shop.Shared.API;
using Shop.Shared.Shared;
using Shop.Store.API.Contract.V1;
using Shop.Store.API.Contract.V1.Models.Book;
using Shop.Store.Application.Command.Book;
using Shop.Store.Application.Query.Book;
using System;
using System.Threading.Tasks;

namespace Shop.Store.API.Controllers
{
    [Route(Routes.Book)]
    public class BookController : BaseController
    {
        [HttpPost, Route(Routes.AddBook)]
        public async Task<IActionResult> Create([FromForm] AddBookRequest addBookRequest)
        {
            var result = await Mediator.Send(new CreateBookCommand(addBookRequest.Name, addBookRequest.SureName,
                addBookRequest.Title,
                addBookRequest.Year, addBookRequest.IsbnType, addBookRequest.IsbnCode,
                addBookRequest.CategoryBook, addBookRequest.CategoryName, (addBookRequest.File is not null ? new BookContent(addBookRequest.File.FileName, await addBookRequest.File?.GetBytes()) : null)!));
            return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
        }
        [HttpGet, Route(Routes.GetBook)]
        public async Task<IActionResult> GetBook(Guid bookId) => Ok(await Mediator.Send(new GetBookQuery(bookId)));
        [HttpGet, Route(Routes.GetBooks)]
        public async Task<IActionResult> GetBooks() => Ok(await Mediator.Send(new GetBooksQuery()));

        [HttpPost, Route(Routes.AddCosts)]
        public async Task<IActionResult> AddCostsBook(Guid bookId, [FromBody] AddBookCostsRequest addBookCosts)
        {
            var result = await Mediator.Send(new CreateBookCostsCommand(bookId, addBookCosts.Amount,
                addBookCosts.Currency,
                addBookCosts.Quantity));
            return result.IsFailure ? BadRequest(result.Error) : Ok(result.Value);
        }
    }
}