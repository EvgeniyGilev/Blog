using AutoMapper;
using BlogWebApp.BLL.Models;
using BlogWebApp.DAL.Entities;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace BlogWebApp
{
    /// <summary>
    /// Настройки маппинга всех сущностей приложения
    /// </summary>
    public class MappingProfile : Profile
    {
/// <summary>
/// В конструкторе настроим соответствие сущностей при маппинге
/// </summary>
        public MappingProfile()
{
            CreateMap<UserEntity, User>();
            CreateMap<CommentEntity, Comment>();
            CreateMap<PostEntity, Post>();
            CreateMap<TagEntity, Tag>();
            CreateMap<RoleEntity, Role>();

        }
    }
}
