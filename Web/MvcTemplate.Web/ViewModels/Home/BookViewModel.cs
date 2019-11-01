﻿namespace MvcTemplate.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using MvcTemplate.Common.Mapping;
    using MvcTemplate.Data.Models;
    using MvcTemplate.Services.Web;

    public class BookViewModel : IMapFrom<Book>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Content { get; set; }

        public double Raiting { get; set; }

        public string Language { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<AuthorViewModel> Authors { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"/Book/{identifier.EncodeId(this.Id)}";
            }
        }

        public string UrlReview
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"/Books/Review/{identifier.EncodeId(this.Id)}";
            }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<BookAuthorBooks, AuthorViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Author.Name));

            configuration.CreateMap<Book, BookViewModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category.Name))
                .ForMember(x => x.Authors, opt => opt.MapFrom(x => x.BookAuthorBooks));
                //.ForMember(x => x.Author, opt => opt.MapFrom(x => x.Author.Name));
        }
    }
}
