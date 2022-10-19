// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace BlogAPI.Contracts.Models.Tags
{
    /// <summary>
    /// The tag view.
    /// </summary>
    public class TagView
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the tag text.
        /// </summary>
        public string tagText { get; set; }
    }
}
