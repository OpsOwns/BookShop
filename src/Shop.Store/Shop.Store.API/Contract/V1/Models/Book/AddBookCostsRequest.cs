namespace Shop.Store.API.Contract.V1.Models.Book
{
    public record AddBookCostsRequest(decimal Amount, string Currency, int Quantity);
}
