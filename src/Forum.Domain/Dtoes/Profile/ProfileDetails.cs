namespace Forum.Domain.Dtoes.Profile
{
    public class ProfileDetails
    {
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String Name { get; set; }
        public String Family { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public bool IsMale { get; set; }
        public DateTime Age { get; set; }


        public int CommentsCount { get; set; }
        public int TopicsCount { get; set; }

        public PhotoDetails Photo { get; set; }
    }

    public class PhotoDetails
    {
        public Guid Id { get; set; }
        public String Url { get; set; }
    }
}
