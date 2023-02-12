namespace Forum.Domain.Dtoes.Communities
{
    public class CommunityDetails
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
