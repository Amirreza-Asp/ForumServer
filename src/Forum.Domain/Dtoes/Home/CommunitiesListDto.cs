namespace Forum.Domain.Dtoes.Home
{
    public class CommunitiesListDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public DateTime CreateAt { get; set; }

        public int TopicsCount { get; set; }
    }
}
