using Forum.Domain.ViewModels.Home;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Forum.Persistence.Features.Queries.Home.FindTopic
{
    public class FindTopicQuery : IRequest<TopicDetailsViewModel>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
