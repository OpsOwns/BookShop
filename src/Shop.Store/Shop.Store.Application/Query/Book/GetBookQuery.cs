using AutoMapper;
using Shop.Shared.ResultResponse;
using Shop.Store.Application.Dto.Book;
using Shop.Store.Core.Book;
using Shop.Store.Core.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Application.Query.Book
{
    public record GetBookQuery(Guid BookId) : IQuery<BooksDto>;
    public class GetBookQueryHandler : IQueryHandler<GetBookQuery, BooksDto>
    {
        private readonly IBookRepository _bookRepository;

        private readonly IMapper _mapper;

        public GetBookQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BooksDto> Handle(GetBookQuery request, CancellationToken cancellationToken)
            => _mapper.Map<BooksDto>(await _bookRepository.FindBook(new BookId(request.BookId)));
    }
}
