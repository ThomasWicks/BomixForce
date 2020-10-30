using Bomix_Force.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Repo.Interface
{
    public interface IPedidoVendaRepository: IGenericRepository<Bomix_PedidoVenda>
    {
        List<Bomix_PedidoVenda> GetParameters(string type, string InitialDate, string FinalDate, string UserId);
    }
}
