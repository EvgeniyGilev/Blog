// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using AutoMapper;
using BlogAPI.Contracts.Models.Comments;
using BlogAPI.Contracts.Models.Roles;
using BlogAPI.Contracts.Models.Tags;
using BlogAPI.DATA.Models;
using static BlogAPI.Contracts.Models.Posts.GetPostsModel;

namespace BlogAPI
{
    /// <summary>
    /// Настройки маппинга всех сущностей приложения.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// В конструкторе настроим соответствие сущностей при маппинге.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Post, PostView>()
            .ForMember(
                d => d.PostTitle,
                opt => opt.MapFrom(r => r.postName))
            .ForMember(
                d => d.AuthorEmail,
                opt => opt.MapFrom(r => r.User.Email))
            .ForMember(
                d => d.CreateDate,
                opt => opt.MapFrom(r => DateTime.Parse(r.postCreateDate)));

            CreateMap<Tag, TagView>().ForMember(
                d => d.tagText,
                opt => opt.MapFrom(r => r.tagText));

            CreateMap<Comment, CommentView>().ForMember(
                c => c.CommentText,
                opt => opt.MapFrom(c => c.commentTexte))
                .ForMember(
                    c => c.CommentAuthorEmail,
                    opt => opt.MapFrom(c => c.User.Email))
                .ForMember(
                    d => d.DateCreate,
                    opt => opt.MapFrom(r => DateTime.Parse(r.commentCreatedDate)));

            CreateMap<Role, ShowRoleView>();
        }
    }
}