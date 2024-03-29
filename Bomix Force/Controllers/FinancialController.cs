﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Bomix_Force.AppServices.Interface;
using Bomix_Force.Areas.Identity.Pages.Account;
using Bomix_Force.Data.Entities;
using Bomix_Force.Models;
using Bomix_Force.Repo.Interface;
using Bomix_Force.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bomix_Force.Controllers
{
    public class FinancialController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly INonconformityRepository _nonconformityRepository;
        private readonly IBomixNotaFiscalVendaRepository _bomixNotaFiscalVendaRepository;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private IWebHostEnvironment _environment;

        public FinancialController(INonconformityRepository nonconformityRepository, IGenericRepository<Company> genericCompanyService, IGenericRepository<Person> genericPersonService,
        IMapper mapper, SignInManager<IdentityUser> signInManager, IEmailSender emailSender, UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger
            , IBomixNotaFiscalVendaRepository bomixNotaFiscalVendaRepository, IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _nonconformityRepository = nonconformityRepository;
            _genericCompanyService = genericCompanyService;
            _genericPersonService = genericPersonService;
            _bomixNotaFiscalVendaRepository = bomixNotaFiscalVendaRepository;
            _environment = environment;

        }
        public static volatile List<FinancialViewModel> financialViewModel = new List<FinancialViewModel>();
        public ActionResult Index(string filter, string searchString, DateTime dateInit, DateTime dateEnd)
        {
            ViewBag.filter = filter;
            string dateInitString;
            string dateEndString;

            if (!String.IsNullOrEmpty(TempData["searchString"]?.ToString()) && String.IsNullOrEmpty(searchString))
            {
                ViewBag.searchString = TempData["searchString"].ToString();

            }
            else
            {
                ViewBag.searchString = searchString;
                TempData["searchString"] = searchString;
            }
            if (!String.IsNullOrEmpty(TempData["dateInit"]?.ToString()) && dateInit == DateTime.MinValue)
            {
                DateTime tempdate = DateTime.ParseExact(TempData["dateinit"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ViewBag.dateInit = tempdate.Date.ToString("yyyy-MM-dd");
                dateInitString = tempdate.Date.ToString();
            }
            else
            {
                ViewBag.dateInit = dateInit == DateTime.MinValue ? null : dateInit.Date.ToString("yyyy-MM-dd");
                TempData["dateInit"] = dateInit == DateTime.MinValue ? "" : dateInit.Date.ToString("yyyy-MM-dd");
                dateInitString = dateInit == DateTime.MinValue ? DateTime.Now.AddYears(-2).Date.ToString() : dateInit.Date.ToString();

            }
            if (!String.IsNullOrEmpty(TempData["dateEnd"]?.ToString()) && dateEnd == DateTime.MinValue)
            {
                DateTime tempdate = DateTime.ParseExact(TempData["dateEnd"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ViewBag.dateEnd = tempdate.Date.ToString("yyyy-MM-dd");
                dateEndString = tempdate.Date.ToString();
            }
            else
            {
                ViewBag.dateEnd = dateEnd == DateTime.MinValue ? null : dateEnd.Date.ToString("yyyy-MM-dd");
                TempData["dateEnd"] = dateEnd == DateTime.MinValue ? "" : dateEnd.Date.ToString("yyyy-MM-dd");
                dateEndString = dateEnd == DateTime.MinValue ? DateTime.Now.Date.ToString() : dateEnd.Date.ToString();
            }
            var identityUser = _userManager.GetUserAsync(User);
            if (identityUser.Result.EmailConfirmed == true)
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;


                financialViewModel = _mapper.Map<IEnumerable<FinancialViewModel>>(_bomixNotaFiscalVendaRepository.GetParameters(dateInitString, dateEndString, user)).ToList();
                if (financialViewModel.Count == 0)
                {
                    return View();
                }
                else
                {
                    return View(financialViewModel);
                }

            }
            else
            {
                return Redirect("~/Identity/Account/Manage");
            }
        }

        public ActionResult InfiniteScroll(int? pageNumber, int pageSize, string filter, string searchString)
        {
            int page = (pageNumber ?? 1);
            try
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                ViewBag.filter = filter;
                ViewBag.searchString = searchString;


                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.Trim();
                    var financialNota = financialViewModel.Where(f => f.Nota != null && f.Nota.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var financialClient = financialViewModel.Where(f => f.Cliente != null && f.Cliente.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var financialEmissao = financialViewModel.Where(f => f.Emissao != null && f.Emissao.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    financialViewModel = financialNota.Union(financialEmissao).Union(financialClient).ToList();
                }
                switch (filter)
                {
                    case ("EmissaoDesc"):
                        financialViewModel = financialViewModel.OrderByDescending(s => s.Emissao).ToList();
                        break;
                    case ("EmissaoAsc"):
                        financialViewModel = financialViewModel.OrderBy(s => s.Emissao).ToList();
                        break;
                    case ("NotaDesc"):
                        financialViewModel = financialViewModel.OrderByDescending(o => o.Nota).ToList();
                        break;
                    case ("NotaAsc"):
                        financialViewModel = financialViewModel.OrderBy(o => o.Nota).ToList();
                        break;
                }
                var financialViewModelPart = financialViewModel.Skip(page * pageSize).Take(pageSize).ToList();
                return PartialView("_FinancialScrollPartial", financialViewModelPart);
            }

            catch (Exception ex)
            {
                //TODO TRATAR ERRO E VER QUANDO NÃO HÁ PEDIDOS
                return null;
            }

        }
        [HttpPost]
        public async Task<ActionResult> Download(string Nota, string typeFinancial, string parcelaSelect)
        {
            try
            {
                string sufixPath = "";
                var typeFile = "";
                switch (typeFinancial)
                {
                    case "NF":
                        sufixPath = $"Danfe\\{Nota}.pdf";
                        typeFile = "pdf";
                        break;
                    case "XML":
                        sufixPath = $"XML\\{Nota}.xml";
                        typeFile = "xml";
                        break;
                    case "Boletos":
                        sufixPath = $"Boletos\\Itau\\{Nota}-{parcelaSelect}.pdf";
                        typeFile = "pdf";
                        break;
                }
                string wwwPath = _environment.WebRootPath;
                var path = wwwPath + $"\\documentos\\{sufixPath}";

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;
                return File(memory, $"application/{typeFile}", Path.GetFileName(path));
            }
            catch (Exception x)
            {
                Notify("O arquivo não está disponivél", "Erro", NotificationType.error);
                return RedirectToAction(nameof(Index));
            }
        }


        // GET: FinancialController/Details/5
        public ActionResult Details(string nota, string searchString)
        {
            FinancialViewModel financial = financialViewModel.Where(e => e.Nota == nota).First();
            ViewBag.searchString = searchString;
            return View(financial);
        }

        // GET: FinancialController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialController/Create
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

        // GET: FinancialController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FinancialController/Edit/5
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

        // GET: FinancialController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialController/Delete/5
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
