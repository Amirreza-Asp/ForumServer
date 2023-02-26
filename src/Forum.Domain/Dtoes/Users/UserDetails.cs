namespace Forum.Domain.Dtoes.Users
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        public String UserName { get; set; }
        public String Name { get; set; }
        public String Family { get; set; }
        public DateTime Age { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public bool isMale { get; set; }
        public String Role { get; set; }
    }
}
