namespace BlogAPI.Contracts.Models.Tags
{
    /// <summary>
    /// The get tags.
    /// </summary>
    public class GetTags
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        public List<TagView> Tags { get; set; }
    }
}
