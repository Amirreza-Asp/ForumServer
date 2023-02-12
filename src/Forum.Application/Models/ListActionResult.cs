namespace Forum.Application.Models
{
    public class ListActionResult<T> where T : class
    {
        public List<T> Data { get; set; }
        public int Total { get; set; } = 0;
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
