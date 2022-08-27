namespace BlogWebApp.BLL.Models.Views
{
    public class TagView
    {
        public int Id { get; set; }
        public string TagText { get; set; }

        public TagView(int id, string tagText)
        {
            Id = id;
            TagText = tagText;
        }

    }
}
