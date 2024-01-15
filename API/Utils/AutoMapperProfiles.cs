using API.Dtos.BookDtoAggregate.AuthorDtos;
using API.Dtos.BookDtoAggregate.BookDtos;
using API.Dtos.BookDtoAggregate.LanguageDtos;
using API.Dtos.BookDtoAggregate.PublisherDtos;
using API.Dtos.ProductDtoAggregate.CategoryDtos;
using API.Dtos.ProductDtoAggregate.DepartmentDtos;
using API.Dtos.ProductDtoAggregate.ProductDtos;
using API.Entities.BookAggregate;
using API.Entities.ProductAggregate;
using AutoMapper;

namespace API.Utils;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Category, CategoryDetailsDto>();
        CreateMap<Picture, ProductPictureDto>();
        CreateMap<Price, PriceDto>()
            .ForMember(dest => dest.MeasureType, opt => opt.MapFrom(src => src.MeasureType.Type))
            .ForMember(dest => dest.MeasureOption, opt => opt.MapFrom(src => src.MeasureOption.Option));
        CreateMap<Department, DepartmentDto>()
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Name));
        CreateMap<CreateDepartmentDto, Department>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Department));

        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Product.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.MainPictureUrl, opt => opt.MapFrom(src => src.Product.Pictures.FirstOrDefault(pic => pic.IsMain).Url))
            .ForMember(dest => dest.PriceList, opt => opt.MapFrom(src => src.Product.PriceList));
        
        CreateMap<Book, BookDetailsDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Product.Department))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Product.Categories))
            .ForMember(dest => dest.PriceList, opt => opt.MapFrom(src => src.Product.PriceList))
            .ForMember(dest => dest.Pictures, opt => opt.MapFrom(src => src.Product.Pictures));
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();

        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorDetailsDto>()
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.AuthorPicture.Url));
        
        CreateMap<Publisher, PublisherDto>();
        CreateMap<PublisherDto, Publisher>();
        CreateMap<CreatePublisherDto, Publisher>();
        CreateMap<UpdatePublisherDto, Publisher>();

        CreateMap<Language, LanguageDto>()
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Name));
        CreateMap<LanguageDto, Language>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Language));
        CreateMap<CreateLanguageDto, Language>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Language));
        CreateMap<UpdateLanguageDto, Language>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Language));

    }
}