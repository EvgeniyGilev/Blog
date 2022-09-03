using AutoMapper;
using BlogAPI.DATA.Models;
using static BlogAPI.Contracts.Models.Posts.GetPostsResponse;

namespace BlogAPI
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
            CreateMap<Post, PostView>().ForMember(d => d.PostTitle,
                    opt => opt.MapFrom(r => r.postName))
                    .ForMember(d => d.AuthorEmail,
                    opt => opt.MapFrom(r => r.User.Email));
        }
    }
}