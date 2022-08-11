﻿namespace BlogWebApp.BLL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CommentTextle { get; set; }
        public DateTime CommentCreatedDate { get; set; }

        public Comment (int id, int userId, string commentTextle, DateTime commentCreatedDate)
        {
            Id = id;
            UserId = userId;
            CommentTextle = commentTextle;
            CommentCreatedDate = commentCreatedDate;
        }
    }
}
