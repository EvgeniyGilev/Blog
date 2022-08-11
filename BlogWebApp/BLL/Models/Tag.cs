namespace BlogWebApp.BLL.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagText { get; set; }

        public Tag (int id, string tagText)
        {
            Id = id;
            TagText = tagText;
        }

    }
}
