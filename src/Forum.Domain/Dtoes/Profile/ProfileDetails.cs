namespace Forum.Domain.Dtoes.Profile
{
    public class ProfileDetails
    {
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public bool IsMale { get; set; }
        public DateTime Age { get; set; }

        public List<PhotoDetails> Photos { get; set; } = new List<PhotoDetails>();
    }

    public class PhotoDetails
    {
        public Guid Id { get; set; }
        public String Url { get; set; }
        public bool IsMain { get; set; }
    }
}
