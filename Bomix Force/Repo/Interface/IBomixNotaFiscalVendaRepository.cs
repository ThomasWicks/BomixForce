using Bomix_Force.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Repo.Interface
{
    public interface IBomixNotaFiscalVendaRepository: IGenericRepository<Bomix_NotaFiscalVenda>
    {
        List<Bomix_NotaFiscalVenda> GetParameters(string InitialDate, string FinalDate, string UserId);
    }
}
