using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using X.PagedList;
using Bomix_Force.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bomix_Force.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Bomix_Force.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IGenericRepository<Employee> _genericEmployeeService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPedidoVendaRepository _pedidoVendaRepository;
        private readonly IMapper _mapper;
        public OrderController(IGenericRepository<Person> genericPersonService, IGenericRepository<Company> genericCompanyService, IMapper mapper
            , RoleManager<IdentityRole> roleManager,
            IGenericRepository<Employee> genericEmployeeService, IPedidoVendaRepository pedidoVendaRepository)
        {
            _genericPersonService = genericPersonService;
            _mapper = mapper;
            _roleManager = roleManager;
            _genericCompanyService = genericCompanyService;
            _genericEmployeeService = genericEmployeeService;
            _pedidoVendaRepository = pedidoVendaRepository;
        }
        // GET: OrderController
        public ActionResult Index(string filter, string searchString, int? pageNumber,string selectType)
        {
            int page = (pageNumber ?? 1);
            try
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                List<Bomix_PedidoVenda> orders = _pedidoVendaRepository.GetParameters(user, "").ToList();
   
                ViewBag.filter = filter;
                ViewBag.selectType = selectType;
                ViewBag.searchString = searchString;
                
                IEnumerable<OrderViewModel> orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
                if (!String.IsNullOrEmpty(searchString))
                {
                    if (selectType == "nOrder")
                        orderView = orderView.Where(o => o.Pedido.ToString().ToLower().Contains(searchString.ToLower()));
                    else if (selectType == "status")
                        orderView = orderView.Where(o => o.Status.ToString().ToLower().Contains(searchString.ToLower()));

                }
                switch (filter)
                {
                    case ("EmissaoDesc"):
                        orderView = orderView.OrderByDescending(s => s.Emissao);
                        break;
                    case ("EmissaoAsc"):
                        orderView = orderView.OrderBy(s => s.Emissao);
                        break;
                    default:
                        break;

                }
                orderView = orderView.ToPagedList(page, 10);
                return View(orderView);
            }
            catch (Exception ex)
            {
                //TODO TRATAR ERRO E VER QUANDO NÃO HÁ PEDIDOS
                return View();
            }
        }

        // GET: OrderController/Details/5
        [Route("Order/Details/{id}")]
        public ActionResult Details(int id)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Bomix_PedidoVenda> orders = _pedidoVendaRepository.GetParameters(user, id.ToString()).ToList();
            OrderViewModel orderView = _mapper.Map<OrderViewModel>(orders);
            return PartialView("_orderDetailsPartial", orderView);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
