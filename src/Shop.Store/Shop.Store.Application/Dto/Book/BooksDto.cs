using System;

namespace Shop.Store.Application.Dto.Book
{
    public class BooksDto
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string BooksCategory { get; set; }
        public string NameCategory { get; set; }
        public string IsbnCode { get; set; }
        public BookContentDto BookContentDto { get; set; }
    }

    public class BookContentDto
    {
        public string Title { get; set; }
        public byte[] File { get; set; }
    }
}
