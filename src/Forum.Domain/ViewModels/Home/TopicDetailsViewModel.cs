using Forum.Domain.Dtoes.Communities;
using Forum.Domain.Dtoes.Topics;

namespace Forum.Domain.ViewModels.Home
{
    public class TopicDetailsViewModel
    {
        public class AuthorDetails
        {
            public String UserName { get; set; }
            public String Photo { get; set; }
            public String FullName { get; set; }
        }

        public TopicDetails Topic { get; set; }
        public AuthorDetails Author { get; set; }
        public CommunitySummary Community { get; set; }
        public String Feeling { get; set; }
    }
}