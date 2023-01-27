using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    internal interface IManageableRepositoryBase<T> : IRepositotyBase<T> where T : ManageableEntityBase
    {
        public Task DeleteAsyncBase(T manageableEntity);
    }
}
