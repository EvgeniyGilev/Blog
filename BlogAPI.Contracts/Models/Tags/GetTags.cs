
namespace BlogAPI.Contracts.Models.Tags
{
    public class GetTags
    {
        public int Count { get; set; }
        public List<TagView> Tags { get; set; }

    }

    public class TagView
    {
        public int id { get; set; }
        public string tagText { get; set; }
    }
}
