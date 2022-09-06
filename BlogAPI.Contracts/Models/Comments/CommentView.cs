using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Contracts.Models.Comments
{
    public class CommentView
    {
        public int id { get; set; }
        public string CommentText { get; set; }
        public string CommentAuthorEmail { get; set; }
        public DateTime DateCreate { get; set; }

    }
}
