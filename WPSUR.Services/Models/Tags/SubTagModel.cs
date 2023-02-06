using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSUR.Repository.Entities;

namespace WPSUR.Services.Models.Tags
{
    public class SubTagModel
    {
        public string Title { get; set; }

        public ICollection<string> MainTags { get; set; }

        public ICollection<string> Posts { get; set; }
    }
}
