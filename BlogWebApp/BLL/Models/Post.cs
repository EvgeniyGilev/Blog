namespace BlogWebApp.BLL.Models
{
    public class Post
    {   public int Id { get; set; }
        public int UserId { get; set; }
        public string PostText { get; set; }
        public DateTime PostCreateDate { get; set; }

        public Post (int id, int userId, string postText, DateTime postCreateDate)
        {
            Id = id;
            UserId = userId;
            PostText = postText;
            PostCreateDate = postCreateDate;
        }
    }
}
