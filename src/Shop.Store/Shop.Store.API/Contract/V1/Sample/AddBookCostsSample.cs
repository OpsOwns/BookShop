using Shop.Store.API.Contract.V1.Models.Book;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Shop.Store.API.Contract.V1.Sample
{
    public class AddBookCostsSample : IExamplesProvider<AddBookCostsRequest>
    {
        public AddBookCostsRequest GetExamples()
        {
            return new(20.5M, "PL", 15);
        }
    }
}
