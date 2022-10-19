// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
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
