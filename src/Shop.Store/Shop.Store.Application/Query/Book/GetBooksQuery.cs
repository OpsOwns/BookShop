using AutoMapper;
using Shop.Shared.ResultResponse;
using Shop.Store.Application.Dto.Book;
using Shop.Store.Core.Repository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Application.Query.Book
{
    public record GetBooksQuery : IQuery<IEnumerable<BooksDto>>;
    public class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, IEnumerable<BooksDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BooksDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken) => _mapper.Map<IEnumerable<BooksDto>>(await _bookRepository.GetBooks(cancellationToken));
    }
}
