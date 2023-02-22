using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSUR.Services.Models.Tags;

namespace WPSUR.Services.Interfaces
{
    public interface IMainTagService
    {
        Task<ICollection<MainTagModel>> FindMainTagModelAsync(string title);
        public Task<ICollection<MainTagModel>> ReceiveMainTags();
        public Task<MainTagState> ReceiveMainTagState(Guid id);
    }
}
