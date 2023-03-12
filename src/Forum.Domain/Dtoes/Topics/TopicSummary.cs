namespace Forum.Domain.Dtoes.Topics
{
    public class TopicSummary
    {
        public Guid Id { get; set; }

        public String Title { get; set; }

        public String AuthorName { get; set; }
        public String AuthorPhoto { get; set; }

        public int View { get; set; }

        public int Like { get; set; }

        public int DisLike { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
