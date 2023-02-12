namespace Forum.Application.Models
{
    public class GridQuery
    {
        public int Size { get; set; } = 10;
        public int Page { get; set; } = 1;
        public SortModel[] Sorted { get; set; } = new SortModel[]
        {
            new SortModel
            {
                column = "CreateAt",
                desc = true
            }
        };
        public FilterModel[] Filters { get; set; } = new FilterModel[] { };
    }

    public class FilterModel
    {
        public string column { get; set; }
        public string value { get; set; }

        public override string ToString()
        {
            return $"{column} = {value}";
        }
    }

    public class SortModel
    {
        public string column { get; set; }
        public bool desc { get; set; } = false;

        public override string ToString()
        {
            return $"{column} {(desc ? "DESC" : "ASC")}";
        }
    }
}
