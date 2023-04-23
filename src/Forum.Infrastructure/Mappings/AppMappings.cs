
using AutoMapper;
using Forum.Application.Services;
using Forum.Domain.Dtoes;
using Forum.Domain.Dtoes.Comments;
using Forum.Domain.Dtoes.Communities;
using Forum.Domain.Dtoes.Home;
using Forum.Domain.Dtoes.Logs;
using Forum.Domain.Dtoes.Profile;
using Forum.Domain.Dtoes.Roles;
using Forum.Domain.Dtoes.Topics;
using Forum.Domain.Dtoes.Users;
using Forum.Domain.Entities.Account;
using Forum.Domain.Entities.Communications;
using Forum.Domain.Entities.Logs;
using Forum.Domain.Queries.Account;
using Forum.Domain.ViewModels.Home;
using Forum.Infrastructure.Utility;

namespace Forum.Infrastructure.Mappings
{
    public class AppMappings : Profile
    {

        public AppMappings(IUserAccessor userAccessor)
        {
            // Community
            CreateMap<Community, Community>();
            CreateMap<Community, CommunitySummary>()
                .ForMember(b => b.Manager, s => s.MapFrom(b => b.Manager != null ? b.Manager.Manager.UserName : ""));

            CreateMap<Community, CommunityDetails>();
            CreateMap<Community, SelectOption>()
                .ForMember(b => b.Text, s => s.MapFrom(b => b.Title))
                .ForMember(b => b.Value, s => s.MapFrom(b => b.Id));

            // Account
            CreateMap<AppUser, AppUser>();
            CreateMap<UserPhoto, UserPhoto>();
            CreateMap<RegisterModel, AppUser>()
                .ForMember(entity => entity.IsDeleted, dto => dto.MapFrom(b => false))
                .ForMember(entity => entity.PasswordHash, dto => dto.MapFrom(b => ""));

            // Profile
            CreateMap<AppUser, ProfileDetails>()
                  .ForMember(b => b.CommentsCount, c => c.MapFrom(b => b.Comments.Count()))
                  .ForMember(b => b.TopicsCount, c => c.MapFrom(b => b.Topics.Count()));

            CreateMap<UserPhoto, PhotoDetails>();


            // Roles
            CreateMap<AppRole, RoleDto>()
                .ForMember(dto => dto.Title, entity => entity.MapFrom(u => u.Name))
                .ReverseMap();

            // Users
            CreateMap<AppUser, UserSummary>()
                .ForMember(dto => dto.Age, entity => entity.MapFrom(b => b.Age.GetAge()))
                .ForMember(dto => dto.Gender, entity => entity.MapFrom(b => b.IsMale ? "Man" : "Woman"))
                .ForMember(dto => dto.Role, entity => entity.MapFrom(b => b.UserRole.Any() ? b.UserRole.First().Role.Name : ""));
            CreateMap<AppUser, UserDetails>()
               .ForMember(dto => dto.Role, entity => entity.MapFrom(b => b.UserRole.Any() ? b.UserRole.First().Role.Name : ""));
            CreateMap<AppUser, TopContributorsDto>()
                .ForMember(b => b.FullName, entity => entity.MapFrom(b => b.Name + " " + b.Family))
                .ForMember(b => b.TopicsCount, entity => entity.MapFrom(b => b.Topics.Count()))
                .ForMember(b => b.Image, entity => entity.MapFrom(b => b.Photo.Url));

            // Topics
            CreateMap<Topic, TopicSummary>()
                .ForMember(b => b.AuthorName, d => d.MapFrom(b => b.Author.FullName))
                .ForMember(b => b.AuthorPhoto, d => d.MapFrom(b => b.Author.Photo != null ? b.Author.Photo.Url : ""))
                .ForMember(b => b.Community, d => d.MapFrom(b => b.Community.Title));
            CreateMap<Topic, TopicDetails>();

            // Logs
            CreateMap<LogModel, LogSummary>();
            CreateMap<LogModel, LogDetails>();


            // Home
            CreateMap<AppUser, TopicDetailsViewModel.AuthorDetails>()
                .ForMember(b => b.FullName, e => e.MapFrom(user => user.Name + " " + user.Family))
                .ForMember(b => b.UserName, e => e.MapFrom(user => user.UserName))
                .ForMember(b => b.Photo, e => e.MapFrom(user => user.Photo != null ? user.Photo.Url : ""));
            CreateMap<Community, CommunitiesListDto>()
                .ForMember(b => b.TopicsCount, e => e.MapFrom(c => c.Topics.Count));
            CreateMap<Community, CommunityPresentationDto>();

            // Comment
            CreateMap<AppUser, CommentAuthorSummary>()
                .ForMember(b => b.Image, b => b.MapFrom(d => d.Photo != null ? d.Photo.Url : ""));
            CreateMap<Comment, CommentSummary>()
                .ForMember(b => b.Reaction, b =>
                    b.MapFrom(d => d.Reactions.Any(b => b.By == userAccessor.GetUserName()) ?
                        d.Reactions.First(b => b.By == userAccessor.GetUserName()).Feeling == Feeling.Like ? "like" : "dislike" : ""));

            CreateMap<Comment, UnreadCommentSummary>()
                .ForMember(b => b.TopicId, d => d.MapFrom(b => b.Topic.Id))
                .ForMember(b => b.TopicTitle, d => d.MapFrom(b => b.Topic.Title));
        }

    }
}
