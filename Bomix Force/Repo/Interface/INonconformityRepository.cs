using Bomix_Force.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Repo.Interface
{
    public interface INonconformityRepository: IGenericRepository<Nonconformity>
    {
        string GetSellerEmail(string CNPJ);
    }
}
