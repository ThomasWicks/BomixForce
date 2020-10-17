using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Bomix_Force.ViewModels;
using IEmailSender = Bomix_Force.AppServices.Interface.IEmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bomix_Force.Data.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Bomix_Force.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IGenericRepository<Employee> _genericEmployeeService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IPedidoVendaRepository _pedidoVendaRepository;
        private readonly IPedidoItemRepository _pedidoItemRepository;
        private readonly IMapper _mapper;
        public OrderController(IGenericRepository<Person> genericPersonService, IGenericRepository<Company> genericCompanyService, IMapper mapper
            , RoleManager<IdentityRole> roleManager,
            IGenericRepository<Employee> genericEmployeeService, IEmailSender emailSender, IPedidoVendaRepository pedidoVendaRepository, IPedidoItemRepository pedidoItemRepository)
        {
            _genericPersonService = genericPersonService;
            _mapper = mapper;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _genericCompanyService = genericCompanyService;
            _genericEmployeeService = genericEmployeeService;
            _pedidoVendaRepository = pedidoVendaRepository;
            _pedidoItemRepository = pedidoItemRepository;
        }
        // GET: OrderController
        public static volatile List<Bomix_PedidoVenda> orders = new List<Bomix_PedidoVenda>();
        public ActionResult Index(string searchString, string filter)
        {

            try
            {
                ViewBag.filter = filter;
                ViewBag.searchString = searchString;
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                orders = _pedidoVendaRepository.GetParameters(user, "").ToList();
                List<OrderViewModel> orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders).ToList();
                if (orders.Count == 0)
                {
                    return View();
                }
                else
                {
                    return View(orderView);
                }
            }
            catch (Exception ex)
            {
                //TODO TRATAR ERRO E VER QUANDO NÃO HÁ PEDIDOS
                List<OrderViewModel> orderView = new List<OrderViewModel>();
                //ModelState.AddModelError("orderIndexError", "Não foi possivel adquirir a lista de pedidos devido ao seguinte erro: " + ex.Message);
                return View(orderView);
            }
        }

        // GET: OrderController/Details/5
        [Route("Order/Details/{id}")]
        public ActionResult Details(int id)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Bomix_PedidoVenda orders = _pedidoVendaRepository.GetParameters(user, id.ToString()).First();
            List<Bomix_PedidoVendaItem> item = _pedidoItemRepository.GetParameters(user, id.ToString()).ToList();
            OrderViewModel orderView = _mapper.Map<OrderViewModel>(orders);
            orderView.Item = item;
            return PartialView("_orderDetailsPartial", orderView);
        }


        [HttpGet]
        [Route("Order/DuplicateData/{id}")]
        //Get: OrderController/DuplicateData/5
        public ActionResult DuplicateData(string id)
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Bomix_PedidoVenda order = orders.Where(o => o.id == Int32.Parse(id)).First();
            List<Bomix_PedidoVendaItem> item = _pedidoItemRepository.GetParameters(user, id.ToString()).ToList();
            OrderViewModel orderView = _mapper.Map<OrderViewModel>(order);
            orderView.Item = item;
            return PartialView("_orderDetailsPartial", orderView);
        }

        [HttpPost]
        // Post: OrderController/Duplicate/5
        public async Task<ActionResult> Duplicate(string Pedido)
        {

            string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //Person person = _genericPersonService.Get(u => u.Id.ToString() == user).First();
            //Company company = _genericCompanyService.Get(c => c.Id == person.CompanyId).First();
            await _emailSender.SendEmailAsync("bomixforcedev@gmail.com", "Replicação Pedido", $"Foi requisitada a duplicação do pedido de número {Pedido}");
            return RedirectToAction(nameof(Index));


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
        //}
        [HttpPost]
        [Route("Order/InfiniteScroll")]
        public ActionResult InfiniteScroll(int? pageNumber, int pageSize, string filter, string searchString)
        {
            int page = (pageNumber ?? 1);
            try
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                ViewBag.filter = filter;
                ViewBag.searchString = searchString;

                IEnumerable<OrderViewModel> orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
                if (!String.IsNullOrEmpty(searchString))
                {

                    var orderViewPedido = orderView.Where(o => o.Pedido.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewEmissao = orderView.Where(o => o.Emissao.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewStatus = orderView.Where(o => o.Status.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewCidade = orderView.Where(o => o.Cidade.ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewCliente = orderView.Where(o => o.Cliente.ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewUF = orderView.Where(o => o.UF.ToLower().Contains(searchString.ToLower())).ToList();
                    orderView = orderViewPedido.Union(orderViewEmissao).Union(orderViewCidade).Union(orderViewStatus).Union(orderViewUF).Union(orderViewCliente).ToList();

                }
                switch (filter)
                {
                    case ("EmissaoDesc"):
                        orderView = orderView.OrderByDescending(s => s.Emissao);
                        break;
                    case ("EmissaoAsc"):
                        orderView = orderView.OrderBy(s => s.Emissao);
                        break;
                    case ("nPedidoDesc"):
                        orderView = orderView.OrderByDescending(o => o.Pedido);
                        break;
                    case ("nPedidoAsc"):
                        orderView = orderView.OrderBy(o => o.Pedido);
                        break;
                    case ("ClienteDesc"):
                        orderView = orderView.OrderByDescending(o => o.Cliente);
                        break;
                    case ("ClienteAsc"):
                        orderView = orderView.OrderBy(o => o.Cliente);
                        break;
                    case ("StatusDesc"):
                        orderView = orderView.Where(o => o.Status == "ENCERRADO").Concat(orderView.Where(o => o.Status == "ORCAMENTO"))
                            .Concat(orderView.Where(o => o.Status == "LIBERADO")).Concat(orderView.Where(o => o.Status == "PARCIAL"))
                            .Concat(orderView.Where(o => o.Status == "ABERTO"));
                        break;
                    case ("StatusAsc"):
                        orderView = orderView.Where(o => o.Status == "ABERTO").Concat(orderView.Where(o => o.Status == "LIBERADO"))
                          .Concat(orderView.Where(o => o.Status == "ORCAMENTO")).Concat(orderView.Where(o => o.Status == "PARCIAL"))
                          .Concat(orderView.Where(o => o.Status == "ENCERRADO"));
                        break;
                    default:
                        break;

                }
                orderView = orderView.Skip(page * pageSize).Take(pageSize).ToList();
                foreach (var order in orderView)
                {
                    order.Item = _pedidoItemRepository.GetParameters(user, order.id.ToString()).ToList();
                    order.Pedido = !String.IsNullOrEmpty(order.Pedido) ? order.Pedido : "-";
                    order.Status = !String.IsNullOrEmpty(order.Status) ? order.Status : "-";
                    order.Cliente = !String.IsNullOrEmpty(order.Cliente) ? order.Cliente : "-";
                    foreach (var item in order.Item)
                    {
                        item.Arte = !String.IsNullOrEmpty(item.Arte) ? item.Arte : "-";
                        item.Produto = !String.IsNullOrEmpty(item.Produto) ? item.Produto : "-";
                        item.Personalizacao = !String.IsNullOrEmpty(item.Personalizacao) ? item.Personalizacao : "-";
                        item.Quantidade = !String.IsNullOrEmpty(item.Quantidade.ToString()) ? item.Quantidade : 0;
                    }

                }
                return PartialView("_orderScrollPartial", orderView);
            }
            catch (Exception ex)
            {
                //TODO TRATAR ERRO E VER QUANDO NÃO HÁ PEDIDOS
                return null;
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
