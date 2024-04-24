using AutoMapper;
using Niqash.Dtos;
using Niqash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Niqash.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain To Dto
            Mapper.CreateMap<Post, PostDto>();

            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<ApplicationUser, AccountDto>();

            Mapper.CreateMap<Comment, CommentDto>();

            Mapper.CreateMap<Like, LikeDto>();

            Mapper.CreateMap<Love, LoveDto>();

            // Dto To Domain
            Mapper.CreateMap<PostDto, Post>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            Mapper.CreateMap<CommentDto, Comment>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            Mapper.CreateMap<LikeDto, Like>()
                .ForMember(m => m.Id, opt => opt.Ignore());

            Mapper.CreateMap<LoveDto, Love>()
                .ForMember(m => m.Id, opt => opt.Ignore());


        }
    }
}