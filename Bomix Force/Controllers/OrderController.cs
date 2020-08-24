using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
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
        public ActionResult Index()
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    List<Order> orders = _genericOrderService.Get().ToList();
                    IEnumerable<OrderViewModel> orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
                    return View(orderView);
                }
                else if (User.IsInRole("User"))
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var test = _roleManager.FindByIdAsync(user);
                    Person person = _genericPersonService.Get(u => u.UserId == user).First();

                    //pega todos as orders cujo status não está finalizado e tem algum person com o id igual ao person que está logado
                    List<Order> orders = _genericOrderService.Get(o => person.OrderId == o.Id && o.Status_Order != "FINALIZADO").ToList();
                    IEnumerable<OrderViewModel> orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
                    return View(orderView);

                }
                else
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var test = _roleManager.FindByIdAsync(user);
                    Person person = _genericPersonService.Get(u => u.UserId == user).First();
                    List<Order> orders = _genericOrderService.Get(o => o.CompanyId == person.CompanyId && o.Status_Order != "FINALIZADO").ToList();
                    IEnumerable<OrderViewModel> orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
                    return View(orderView);
                };
            }
            catch (Exception ex)
            {
                //TODO TRATAR ERRO E VER QUANDO NÃO HÁ PEDIDOS
                return View();
            }
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            Order order = _genericOrderService.Get(g => g.Id == id).First();
            List<Item> itens = _genericItemService.Get(i => i.OrderId == order.Id).ToList();
            OrderViewModel orderView = _mapper.Map<OrderViewModel>(order);
            orderView.Item = itens;
            return View(orderView);
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
