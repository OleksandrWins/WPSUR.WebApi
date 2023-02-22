using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSUR.Services.Models.Tags;

namespace WPSUR.Services.Models.Post
{
    public  class PostState
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public MainTagState MainTag { get; set; }
        public ICollection<SubTagState> SubTags { get; set; }
    }
}
