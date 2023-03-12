namespace Forum.Domain.Dtoes.Communities
{
    public class CommunitySummary
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Icon { get; set; }
        public String Image { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
