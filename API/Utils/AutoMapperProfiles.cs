using API.Dtos.Book.Author;
using API.Entities.BookAggregate;
using AutoMapper;

namespace API.Utils;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.AuthorPicture.Url));
    }
}