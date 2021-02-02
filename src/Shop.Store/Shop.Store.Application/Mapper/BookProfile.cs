﻿using AutoMapper;
using Shop.Store.Application.Dto.Book;
using Shop.Store.Core.Book;

namespace Shop.Store.Application.Mapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookInfo, BooksDto>()
                .ForMember(x => x.Title, dest
                    => dest.MapFrom(map => map.BookDescription.Title)).ForMember(x => x.Name, dest
                    => dest.MapFrom(map => map.Author.Name))
                .ForMember(x => x.SureName, dest
                    => dest.MapFrom(map => map.Author.SureName)).ForMember(x => x.Year, dest
                    => dest.MapFrom(map => map.BookDescription.Year)).ForMember(x => x.IsbnCode, dest
                    => dest.MapFrom(map => map.Isbn.IsbnCode)).ForMember(x => x.NameCategory, dest =>
                    dest.MapFrom(map => map.BookCategory.CategoryName)).ForMember(x => x.BooksCategory, dest
                    => dest.MapFrom(map => map.BookCategory.CategoryBook))
                .ForMember(x => x.BookId, dest
                    => dest.MapFrom(map => map.BookId.Value));
        }
    }
}