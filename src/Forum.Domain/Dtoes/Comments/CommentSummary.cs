namespace Forum.Domain.Dtoes.Comments
{
    public class CommentSummary
    {
        public Guid Id { get; set; }
        public String Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool ReadByAuthor { get; set; }

        public int Like { get; set; }
        public int DisLike { get; set; }

        public String Reaction { get; set; }

        public CommentAuthorSummary Author { get; set; }
    }

    public class CommentAuthorSummary
    {
        public String UserName { get; set; }
        public String FullName { get; set; }
        public String Image { get; set; }
    }
}
