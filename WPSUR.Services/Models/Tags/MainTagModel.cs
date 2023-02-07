using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSUR.Repository.Entities;

namespace WPSUR.Services.Models.Tags
{
    public class MainTagModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public ICollection<string> SubTags { get; set; }

        public ICollection<string> Posts { get; set; }
    }
}
