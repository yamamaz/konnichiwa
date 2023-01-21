using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.Profiles;
public class KonnichiwaProfile : Profile
{
    public KonnichiwaProfile()
    {
        CreateMap<Domain.Entities.User, Dtos.CreateUserDto>().ReverseMap();
        CreateMap<Domain.Entities.User, Dtos.UpdateUser.UpdateUserDto>().ReverseMap();
        CreateMap<Domain.Entities.User, Dtos.UpdateUser.UpdateUserPasswordDto>().ReverseMap();
        CreateMap<Domain.Entities.User, Dtos.UpdateUser.UpdateUserProfilePicDto>().ReverseMap();
        CreateMap<Domain.Entities.User, Dtos.GetUserDto>().ReverseMap();
        CreateMap<Domain.Entities.User, Dtos.UserLoginDto>().ReverseMap();
        CreateMap<Domain.Entities.User, Dtos.UserLoginByEmailDto>().ReverseMap();
        CreateMap<Domain.Entities.Post, Dtos.CreatePostDto>().ReverseMap();
        CreateMap<Domain.Entities.Post, Dtos.GetPostDto>().ReverseMap();
        CreateMap<Domain.Entities.Post, Dtos.PostDto>().ReverseMap();
        CreateMap<Domain.Entities.Post, Dtos.UpdatePostDto>().ReverseMap();
        CreateMap<Domain.Entities.Likes, Dtos.AddLikeDto>().ReverseMap();
        CreateMap<Domain.Entities.Comments, Dtos.AddCommentDto>().ReverseMap();
        CreateMap<Domain.Entities.Comments, Dtos.UpdateCommentDto>().ReverseMap();
    }
}
