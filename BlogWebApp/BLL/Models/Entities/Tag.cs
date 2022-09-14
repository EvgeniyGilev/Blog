namespace BlogWebApp.BLL.Models.Entities
{
    /// <summary>
    /// The tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the tag text.
        /// </summary>
        public string tagText { get; set; }

        /// <summary>
        /// Gets or sets the posts.
        /// ссылка на статьи.
        /// </summary>
        public virtual List<Post> Posts { get; set; } = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <param name="tagText">The tag text.</param>
        public Tag(string tagText)
        {
            this.tagText = tagText;
        }
    }
}
