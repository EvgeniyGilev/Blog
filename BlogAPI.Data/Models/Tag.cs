// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace BlogAPI.DATA.Models
{
    /// <summary>
    /// The tag.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the tag text.
        /// </summary>
        public string TagText { get; set; }

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
            this.TagText = tagText;
        }
    }
}
