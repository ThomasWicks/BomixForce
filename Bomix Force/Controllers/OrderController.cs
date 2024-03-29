﻿using System;
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
using System.IO;

namespace Bomix_Force.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IGenericRepository<Employee> _genericEmployeeService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IPedidoVendaRepository _pedidoVendaRepository;
        private readonly IPedidoItemRepository _pedidoItemRepository;
        private readonly IMapper _mapper;
        public OrderController(IGenericRepository<Person> genericPersonService, IGenericRepository<Company> genericCompanyService, IMapper mapper
            , RoleManager<IdentityRole> roleManager,
            IGenericRepository<Employee> genericEmployeeService, IEmailSender emailSender, IPedidoVendaRepository pedidoVendaRepository, IPedidoItemRepository pedidoItemRepository, UserManager<IdentityUser> userManager)
        {
            _genericPersonService = genericPersonService;
            _mapper = mapper;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _genericCompanyService = genericCompanyService;
            _genericEmployeeService = genericEmployeeService;
            _pedidoVendaRepository = pedidoVendaRepository;
            _pedidoItemRepository = pedidoItemRepository;
            _userManager = userManager;
        }
        public static volatile List<Bomix_PedidoVenda> orders = new List<Bomix_PedidoVenda>();
        // GET: OrderController
        public ActionResult Index(string searchString, DateTime dateInit, DateTime dateEnd, string filter, string filterStatus)
        {
            try
            {
                var identityUser = _userManager.GetUserAsync(User);
                if (identityUser.Result.EmailConfirmed == true)
                {
                    ViewBag.filterStatus = filterStatus;
                    ViewBag.filter = filter;
                    ViewBag.dateInit = dateInit == DateTime.MinValue ? null : dateInit.Date.ToString("yyyy-MM-dd");
                    ViewBag.dateEnd = dateEnd == DateTime.MinValue ? null : dateEnd.Date.ToString("yyyy-MM-dd");
                    ViewBag.searchString = searchString;

                    string dateInitString = dateInit == DateTime.MinValue ? DateTime.Now.AddYears(-2).Date.ToString() : dateInit.Date.ToString();
                    string dateEndString = dateEnd == DateTime.MinValue ? DateTime.Now.Date.ToString() : dateEnd.Date.ToString();
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    orders = _pedidoVendaRepository.GetParameters("PCP", dateInitString, dateEndString, user).ToList();
                    var orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orders).ToList();

                    if (orders.Count == 0)
                    {
                        return View();
                    }
                    else
                    {
                        return View(orderView);
                    }
                }
                else
                {
                    return Redirect("~/Identity/Account/Manage");
                }
            }
            catch
            {
                List<OrderViewModel> orderView = new List<OrderViewModel>();
                return View(orderView);
            }
        }



        //Get: OrderController/DuplicateData/5
        [HttpGet]
        [Route("Order/DuplicateData/{Pedido}")]
        public ActionResult DuplicateData(string Pedido)
        {
            List<Bomix_PedidoVenda> order = orders.Where(o => o.Pedido == Pedido).ToList();
            List<OrderViewModel> orderView = _mapper.Map<List<OrderViewModel>>(order);
            return PartialView("_orderDetailsPartial", orderView);
        }
        // Post: OrderController/Duplicate/5
        [HttpPost]
        public async Task<ActionResult> Duplicate(string Pedido, string Ordem)
        {
            try
            {
                Company company = new Company();
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                List<Bomix_PedidoVenda> order = orders.Where(o => o.Pedido == Pedido).ToList();
                string email = String.Empty;
                if (User.IsInRole("User"))
                {
                    Person person = _genericPersonService.Get(p => p.IdentityUserId == user).First();
                    var personCompanyId = person.CompanyId;
                    email = person.Email;
                    company = _genericCompanyService.Get(u => u.Id == personCompanyId).First();
                }
                else
                {
                    company = _genericCompanyService.Get(u => user == u.IdentityUserId).First();
                    email = company.Email;
                }
                Employee employee = _pedidoVendaRepository.GetEmployeesBySeller_id(order[0].Vendedor_FK);
                string FilePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/TemplateEmail", "Order.html");
                StreamReader str = new StreamReader(FilePath);
                string mensage = str.ReadToEnd();
                mensage = mensage.Replace("NomeCliente", company.Name);
                mensage = mensage.Replace("CnpjCliente", company.Cnpj);
                mensage = mensage.Replace("Pedido", order[0].Pedido);
                mensage = mensage.Replace("Ordem", Ordem);
                foreach (var item in order)
                {
                    string Orderpath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot/TemplateEmail", "OrderTable.html.html");
                    StreamReader oederstr = new StreamReader(Orderpath);
                    string msg = oederstr.ReadToEnd();
                    msg = msg.Replace("Produto", item.Produto);
                    msg = msg.Replace("Qtd", item.Quantidade.ToString());
                    mensage = mensage.Replace("<!--replace-->", msg);
                };
                await _emailSender.SendEmailAsync(employee.Email, "Replicação Pedido", mensage, null);
                await _emailSender.SendEmailAsync(email, "Replicação Pedido", $"A requisição do pedido de número: {order[0].Pedido} foi realiada com sucesso.", null);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }


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
        [HttpPost]
        [Route("Order/InfiniteScroll")]
        public ActionResult InfiniteScroll(int? pageNumber, int pageSize, string filter, string searchString, string filterStatus)
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
                    searchString = searchString.Trim();
                    var orderViewPedido = orderView.Where(o => o.Pedido != null && o.Pedido.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewPCP = orderView.Where(o => o.PCP != null && o.PCP.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewStatus = orderView.Where(o => o.Status != null && o.Status.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewProduto = orderView.Where(o => o.Produto != null && o.Produto.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewPersonalizacao = orderView.Where(o => o.Personalizacao != null && o.Personalizacao.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewCidade = orderView.Where(o => o.Cidade.ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewUF = orderView.Where(o => o.UF.ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewOrdem = orderView.Where(o => o.OrdemCompra.ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewCliente = orderView.Where(o => o.Cliente != null && o.Cliente.ToLower().Contains(searchString.ToLower())).ToList();
                    var orderViewValor = orderView.Where(o => o.Valor.ToString().Contains(searchString)).ToList();
                    try{
                        var seacrhValue = float.Parse(searchString); 
                        orderViewValor = orderView.Where(o => o.Valor <= seacrhValue).OrderByDescending(o => o.Valor).ToList();
                    }
                    catch{
                        
                    }

                    if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                    {
                        orderView = orderViewPedido.Union(orderViewOrdem).Union(orderViewPCP).Union(orderViewStatus).
                            Union(orderViewPersonalizacao).Union(orderViewProduto).Union(orderViewCidade)
                            .Union(orderViewUF).Union(orderViewCliente).Union(orderViewValor).ToList();
                    }
                    else
                    {
                        orderView = orderViewPedido.Union(orderViewOrdem).Union(orderViewPCP).Union(orderViewPersonalizacao)
                            .Union(orderViewCidade).Union(orderViewUF)
                            .Union(orderViewProduto).Union(orderViewStatus).ToList();
                    }

                }
                switch (filter)
                {
                    case ("PCPDesc"):
                        orderView = orderView.OrderByDescending(s => s.PCP);
                        break;
                    case ("PCPAsc"):
                        orderView = orderView.OrderBy(s => s.PCP);
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
                    case ("ProdutoDesc"):
                        orderView = orderView.OrderByDescending(o => o.Produto);
                        break;
                    case ("ProdutoAsc"):
                        orderView = orderView.OrderBy(o => o.Produto);
                        break;
                    case ("ArteDesc"):
                        orderView = orderView.OrderByDescending(o => o.Arte);
                        break;
                    case ("ArteAsc"):
                        orderView = orderView.OrderBy(o => o.Arte);
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
                    case ("ValorAsc"):
                        orderView = orderView.OrderBy(o => o.Valor);
                        break;
                    case ("ValorDesc"):
                        orderView = orderView.OrderByDescending(o => o.Valor);
                        break;
                    case ("QtdAsc"):
                        orderView = orderView.OrderBy(o => o.Quantidade);
                        break;
                    case ("QtdDesc"):
                        orderView = orderView.OrderByDescending(o => o.Quantidade);
                        break;
                    default:
                        break;

                }
                if (filterStatus != "" && filterStatus != null)
                    orderView = _mapper.Map<IEnumerable<OrderViewModel>>(orderView).Where(o => filterStatus.Contains(o.Status)).ToList();
                orderView = orderView.Skip(page * pageSize).Take(pageSize).ToList();
                foreach (var order in orderView)
                {

                    order.Pedido = !String.IsNullOrEmpty(order.Pedido) ? order.Pedido : "-";
                    order.Status = !String.IsNullOrEmpty(order.Status) ? order.Status : "-";
                    order.Cliente = !String.IsNullOrEmpty(order.Cliente) ? order.Cliente : "-";
                    order.Arte = !String.IsNullOrEmpty(order.Arte) ? order.Arte : "-";
                    order.Produto = !String.IsNullOrEmpty(order.Produto) ? order.Produto : "-";
                    order.Personalizacao = !String.IsNullOrEmpty(order.Personalizacao) ? order.Personalizacao : "-";
                    order.Quantidade = !String.IsNullOrEmpty(order.Quantidade.ToString()) ? order.Quantidade : 0;
                    order.OrdemCompra = !String.IsNullOrEmpty(order.OrdemCompra) ? order.OrdemCompra : "-";
                    order.Valor = !String.IsNullOrEmpty(order.Valor.ToString()) ? order.Valor : 0;


                }
                return PartialView("_orderScrollPartial", orderView);
            }
            catch
            {
                //TODO TRATAR ERRO E VER QUANDO NÃO HÁ PEDIDOS
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
