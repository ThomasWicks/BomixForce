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

namespace Bomix_Force.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IGenericRepository<Order> _genericOrderService;
        private readonly IGenericRepository<Item> _genericItemService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public OrderController(IGenericRepository<Person> genericPersonService, IGenericRepository<Company> genericCompanyService, IMapper mapper
            , IGenericRepository<Order> genericOrderService, IGenericRepository<Item> genericItemService, RoleManager<IdentityRole> roleManager)
        {
            _genericPersonService = genericPersonService;
            _mapper = mapper;
            _roleManager = roleManager;
            _genericCompanyService = genericCompanyService;
            _genericOrderService = genericOrderService;
            _genericItemService = genericItemService;

        }
        // GET: OrderController
        public ActionResult Index(string filter, string searchString, int? pageNumber,string selectType)
        {
            int page = (pageNumber ?? 1);
            try
            {
                //string selectType = typeSearch["selectType"].ToString();
                List <Order> orders = new List<Order>();
                IEnumerable<OrderViewModel> orderView;
                ViewBag.filter = filter;
                ViewBag.selectType = selectType;
                ViewBag.searchString = searchString;
                if (User.IsInRole("Admin"))
                {
                    orders = _genericOrderService.Get().ToList();
                    orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
                }
                else if (User.IsInRole("User"))
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    Person person = _genericPersonService.Get(u => u.UserId == user).First();
                    orders = _genericOrderService.Get(o => o.PersonId == person.Id && o.Status != "FINALIZADO").ToList();
                    orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
                }
                else
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    Person person = _genericPersonService.Get(u => u.UserId == user).First();
                    _genericOrderService.Get(o => o.CompanyId == person.CompanyId && o.Status != "FINALIZADO").ToList();
                    orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

                }
                if (!String.IsNullOrEmpty(searchString))
                {
                    if(selectType=="nOrder")
                    orderView = orderView.Where(o => o.NumeroPedido.ToString().ToLower().Contains(searchString.ToLower()));
                    else if(selectType == "status")
                        orderView = orderView.Where(o => o.Status.ToString().ToLower().Contains(searchString.ToLower()));
                    
                }
                switch (filter)
                {
                    case ("EntregaDes"):
                        orderView = orderView.OrderByDescending(s => s.Entrega);
                        break;
                    case ("EntregaAsc"):
                        orderView = orderView.OrderBy(s => s.Entrega);
                        break;
                    case ("EmissaoDesc"):
                        orderView = orderView.OrderByDescending(s => s.Emissao);
                        break;
                    case ("EmissaoAsc"):
                        orderView = orderView.OrderBy(s => s.Emissao);
                        break;
                    default:
                        break;

                }
                orderView=orderView.ToPagedList(page, 2);
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
            Order order = _genericOrderService.Get(g => g.Id == id).First();
            List<Item> itens = _genericItemService.Get(i => i.OrderId == order.Id).ToList();
            OrderViewModel orderView = _mapper.Map<OrderViewModel>(order);
            orderView.Item = itens;
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
