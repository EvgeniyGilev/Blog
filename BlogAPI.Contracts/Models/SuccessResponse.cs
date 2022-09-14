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
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the info message.
        /// </summary>
        public string infoMessage { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public int code { get; set; }
    }
}