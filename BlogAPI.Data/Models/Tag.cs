﻿namespace BlogAPI.DATA.Models
{
    public class Tag
    {
        public int id { get; set; }
        public string tagText { get; set; }

        //ссылка на статьи
        public virtual List<Post> Posts { get; set; } = new();

        public Tag(string tagText)
        {
            this.tagText = tagText;
        }
    }
}
