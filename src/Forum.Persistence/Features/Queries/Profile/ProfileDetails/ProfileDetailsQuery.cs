using MediatR;

namespace Forum.Persistence.Features.Queries.Profile.ProfileDetails
{
    public class ProfileDetailsQuery : IRequest<Domain.Dtoes.Profile.ProfileDetails>
    {
        public String UserName { get; set; }
    }
}
