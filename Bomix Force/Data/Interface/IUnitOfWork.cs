using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Data.Interface
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit(bool dispose = true);
        void SaveChanges();
        void Rollback(bool dispose = true);
    }
}
