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

        public Employee GetEmployeesBySeller_id(string seller_id)
        {
            var employee = _context.Employee.FromSqlRaw("select ep.[Id], ep.[Name], ep.[Email], ep.[Login], ep.[Idtotvs], ep.[IdentityUserId]" +
                " from((Employee ep" +
                " inner join Employee_Seller On ep.Id = Employee_Seller.EmployeeId)" +
                " inner join Bomix_PedidoVenda On Bomix_PedidoVenda.Vendedor_FK = Employee_Seller.SellerId)" +
                " where Bomix_PedidoVenda.Vendedor_FK = @Seller", new SqlParameter("@Seller", seller_id)
);
            return employee.FirstOrDefault();
        } 

        public Employee GetEmployeesByCNPJ(string cnpj)
        {
            var employee = _context.Employee.FromSqlRaw("select ep.[Id], ep.[Name], ep.[Email], ep.[Login], ep.[Idtotvs], ep.[IdentityUserId]" +
                " from((Employee ep" +
                " inner join Employee_Seller On ep.Id = Employee_Seller.EmployeeId)" +
                " inner join Bomix_Cliente On Bomix_Cliente.Vendedor_FK = Employee_Seller.SellerId)" +
                " where Bomix_Cliente.CNPJ = @cnpj", new SqlParameter("@cnpj", cnpj));

            return employee.FirstOrDefault();
        }
    }
}
