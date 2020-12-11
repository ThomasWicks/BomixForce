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
    public class BomixNotaFiscalVendaRepository: GenericRepository<Bomix_NotaFiscalVenda>, IBomixNotaFiscalVendaRepository
    {
        private ModelContext _context;

        public BomixNotaFiscalVendaRepository(ModelContext context) : base(context)
        {
            _context = context;
        }
        public List<Bomix_NotaFiscalVenda> GetParameters(string InitialDate, string FinalDate, string UserId)
        {
            try
            {
                var UserParam = new SqlParameter("@UserId", UserId);
                var InitialDateParam = new SqlParameter("@InitialDate", InitialDate);
                var FinalDateParam = new SqlParameter("@FinalDate", FinalDate);

                var PedidoVenda = _context.Bomix_NotaFiscalVendas.FromSqlRaw("Bomix_GetNotaFiscalVenda @InitialDate, @FinalDate, @UserId,''",InitialDateParam, FinalDateParam, UserParam).ToList();
                return PedidoVenda;

            }
            catch (Exception e)
            {
                List<Bomix_NotaFiscalVenda> emptyList = new List<Bomix_NotaFiscalVenda>();
                return emptyList;
            }

        }
    }
}
