namespace Forum.Domain.Dtoes.Topics
{
    public class TopicDetails
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid AuthorId { get; set; }
        public Guid CommunityId { get; set; }


    }

}
