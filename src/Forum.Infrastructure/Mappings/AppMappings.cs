
using AutoMapper;
using Forum.Domain.Dtoes;
using Forum.Domain.Dtoes.Communities;
using Forum.Domain.Dtoes.Profile;
using Forum.Domain.Dtoes.Roles;
using Forum.Domain.Dtoes.Topics;
using Forum.Domain.Dtoes.Users;
using Forum.Domain.Entities.Account;
using Forum.Domain.Entities.Communications;
using Forum.Domain.Queries.Account;
using Forum.Infrastructure.Utility;

namespace Forum.Infrastructure.Mappings
{
    public class AppMappings : Profile
    {

        public AppMappings()
        {
            // Community
            CreateMap<Community, Community>();
            CreateMap<Community, CommunitySummary>();
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
            CreateMap<AppUser, ProfileDetails>();
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

            // Topics
            CreateMap<Topic, TopicSummary>()
                .ForMember(b => b.AuthorName, d => d.MapFrom(b => b.Author.FullName))
                .ForMember(b => b.AuthorPhoto, d => d.MapFrom(b => b.Author.Photos.FirstOrDefault(b => b.IsMain).Url))
                .ForMember(b => b.Community, d => d.MapFrom(b => b.Community.Title));
            CreateMap<TopicFile, TopicFile>();
            CreateMap<Topic, TopicDetails>();
        }

    }
}
