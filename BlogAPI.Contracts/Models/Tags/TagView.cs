using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Contracts.Models.Tags
{
    public class TagView
    {
        public int id { get; set; }
        public string tagText { get; set; }
    }
}
