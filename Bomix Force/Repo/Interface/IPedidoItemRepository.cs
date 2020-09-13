using Bomix_Force.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Repo.Interface
{
    public interface IPedidoItemRepository : IGenericRepository<Bomix_PedidoVendaItem>
    {
        List<Bomix_PedidoVendaItem> GetParameters(string ClientId, string OrderId);
    }
}
