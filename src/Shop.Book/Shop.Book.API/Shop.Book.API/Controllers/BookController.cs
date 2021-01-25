using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Book.API.Contract.V1;
using Shop.Shared.API;

namespace Shop.Book.API.Controllers
{
    [Route(Routes.Book)]
    public class BookController : BaseController
    {

        
        [HttpGet, Route("test"), Authorize]
        public IActionResult Get()
        {
            return Ok("działa");
        }
    }
}
