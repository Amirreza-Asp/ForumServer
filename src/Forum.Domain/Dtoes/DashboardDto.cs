namespace Forum.Domain.Dtoes
{
    public class DashboardDto
    {
        public List<NumberOfTopicsPerMonth> NumberOfTopicPerMonth { get; set; } = new List<NumberOfTopicsPerMonth>();
        public long UsersCount { get; set; }
        public long CommunitiesCount { get; set; }
        public long TopicsCount { get; set; }
        public long CommentsCount { get; set; }
        public List<NewMembersJoined> NewMembersJoined { get; set; } = new List<NewMembersJoined>();
        public List<CommunitiesWithMostTopics> CommunitiesWithMostTopics { get; set; }
        public List<TopicsWithMostComments> TopicsWithMostComments { get; set; }
        public List<UsersGender> UsersGender { get; set; }
        public List<UsersCountByAge> UsersCountByAge { get; set; }
    }

    public class NumberOfTopicsPerMonth
    {
        public String Month { get; set; }
        public long Count { get; set; }
    }

    public class NewMembersJoined
    {
        public String Month { get; set; }
        public long Count { get; set; }
    }

    public class CommunitiesWithMostTopics
    {
        public string Community { get; set; }
        public long TopicsCount { get; set; }
    }

    public class TopicsWithMostComments
    {
        public String Topic { get; set; }
        public long CommentsCount { get; set; }
    }

    public class UsersGender
    {
        public bool IsMale { get; set; }
        public long Count { get; set; }
    }

    public class UsersCountByAge
    {
        public int Age { get; set; }
        public long Count { get; set; }
    }
}
