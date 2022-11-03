// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
namespace BlogAPI.Contracts.Models
{
    /// <summary>
    /// The success response.
    /// </summary>
    public class SuccessResponse
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the info message.
        /// </summary>
        public string? InfoMessage { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public int Code { get; set; }
    }
}