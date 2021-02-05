using System.IO;
using Microsoft.AspNetCore.Http;
using Shop.Store.API.Contract.V1.Models.Book;
using Swashbuckle.AspNetCore.Filters;

namespace Shop.Store.API.Contract.V1.Sample
{
    public class AddBookSample : IExamplesProvider<AddBookRequest>
    {
        public AddBookRequest GetExamples()
        {
            return new("Jan", "Brzechwa", "Deszcz", 1992, 1, "ISBN 1-58182-008-9", 2, "Test", null);
        }
    }
}
