using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Contracts.Models.Comments
{
    /// <summary>
    /// The comment view.
    /// </summary>
    public class CommentView
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the comment author email.
        /// </summary>
        public string CommentAuthorEmail { get; set; }

        /// <summary>
        /// Gets or sets the date create.
        /// </summary>
        public DateTime DateCreate { get; set; }
    }
}
