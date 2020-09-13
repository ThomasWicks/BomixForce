using Bomix_Force.Data.Context;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Repo
{
    public class PedidoItemRepository : GenericRepository<Bomix_PedidoVendaItem>, IPedidoItemRepository
    {
        private ModelContext _context;

        public PedidoItemRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public List<Bomix_PedidoVendaItem> GetParameters(string ClientId, string OrderId)
        {
            var ClientParam = new SqlParameter("@ClientId", ClientId);
            var OrderParam = new SqlParameter("@OrderId", OrderId);

            var PedidoVenda = _context.Bomix_PedidoVendaItem.FromSqlRaw("Bomix_GetPedidoVendaItem @ClientId, @OrderId ", ClientParam, OrderParam).ToList();
            return PedidoVenda;
        }
    }

}
