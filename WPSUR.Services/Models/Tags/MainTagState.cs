using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSUR.Repository.Entities;
using WPSUR.Services.Models.Post;

namespace WPSUR.Services.Models.Tags
{
    public class MainTagState
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public ICollection<SubTagState> SubTags { get; set; }

        public ICollection<PostState> Posts { get; set; }
    }
}
