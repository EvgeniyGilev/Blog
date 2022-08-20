using AutoMapper;
using BlogWebApp.BLL.Models.Entities;
using BlogWebApp.BLL.Models.Views;
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
            CreateMap<User, UserView>();
            CreateMap<Comment, Comment>();
            CreateMap<Post, PostView>();
            CreateMap<Tag, TagView>();
            CreateMap<Role, RoleView>();

        }
    }
}
