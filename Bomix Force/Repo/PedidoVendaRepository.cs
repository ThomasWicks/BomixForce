using Bomix_Force.Data.Context;
using Bomix_Force.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.Repo.Interface
{
    public class PedidoVendaRepository : GenericRepository<Bomix_PedidoVenda>, IPedidoVendaRepository
    {
        private ModelContext _context;

        public PedidoVendaRepository(ModelContext context) : base(context)
        {
            _context = context;
        }

        public List<Bomix_PedidoVenda> GetParameters(string ClientId, string OrderId)
        {
            var ClientParam = new SqlParameter("@ClientId", ClientId);
            var OrderParam = new SqlParameter("@OrderId", OrderId);

            var PedidoVenda = _context.Bomix_PedidoVenda.FromSqlRaw("Bomix_GetPedidoVenda @ClientId, @OrderId ", ClientParam, OrderParam).ToList();
            return PedidoVenda;
        }
    }
}
