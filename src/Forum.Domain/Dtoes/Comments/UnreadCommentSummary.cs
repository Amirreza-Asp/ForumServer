namespace Forum.Domain.Dtoes.Comments
{
    public class UnreadCommentSummary
    {
        public Guid Id { get; set; }
        public String Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public String TopicTitle { get; set; }
        public Guid TopicId { get; set; }

        public int Like { get; set; }
        public int DisLike { get; set; }

        public CommentAuthorSummary Author { get; set; }
    }
}
