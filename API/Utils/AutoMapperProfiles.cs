using API.Dtos.Book.Author;
using API.Dtos.Book.Language;
using API.Dtos.Book.Publisher;
using API.Entities.BookAggregate;
using AutoMapper;

namespace API.Utils;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Author, AuthorDto>()
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