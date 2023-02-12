using AutoMapper;
using Forum.Domain.Dtoes.Communities;
using Forum.Domain.Entities;

namespace Forum.Infrastructure.Services
{
    public class AppMappings : Profile
    {

        public AppMappings()
        {
            // Community
            CreateMap<Community, Community>();
            CreateMap<Community, CommunitySummary>();
            CreateMap<Community, CommunityDetails>();
        }

    }
}
