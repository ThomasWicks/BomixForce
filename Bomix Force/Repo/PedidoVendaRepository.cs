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

        public List<Bomix_PedidoVenda> GetParameters(string type, string InitialDate, string FinalDate, string UserId)
        {
            var UserParam = new SqlParameter("@UserId", UserId);
            var typeParam = new SqlParameter("@type", type);
            var InitialDateParam = new SqlParameter("@InitialDate", InitialDate);
            var FinalDateParam = new SqlParameter("@FinalDate", FinalDate);

            var PedidoVenda = _context.Bomix_PedidoVenda.FromSqlRaw("Bomix_GetPedidoVenda @type, @InitialDate, @FinalDate,@UserId,''", 
                typeParam,InitialDateParam, FinalDateParam, UserParam).ToList();
            return PedidoVenda;
        }
    }
}
