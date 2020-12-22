using AutoMapper;
using Bomix_Force.AppServices.Interface;
using Bomix_Force.Areas.Identity.Pages.Account;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Bomix_Force.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bomix_Force.Controllers
{
    public class DocumentController : BaseController
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly INonconformityRepository _nonconformityRepository;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IPedidoVendaRepository _pedidoVendaRepository;

        public DocumentController(INonconformityRepository nonconformityRepository, IGenericRepository<Company> genericCompanyService, IGenericRepository<Person> genericPersonService,
        IMapper mapper, SignInManager<IdentityUser> signInManager, IEmailSender emailSender, UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger, IPedidoVendaRepository pedidoVendaRepository)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _nonconformityRepository = nonconformityRepository;
            _genericCompanyService = genericCompanyService;
            _genericPersonService = genericPersonService;
            _pedidoVendaRepository = pedidoVendaRepository;

        }
        // GET: DocumentController
        public ActionResult Index()
        {
            var identityUser = _userManager.GetUserAsync(User);
            if (identityUser.Result.EmailConfirmed == true)
            {
                DocumentViewModel documentViewModel = new DocumentViewModel();
                return View(documentViewModel);
            }
            else
                return Redirect("~/Identity/Account/Manage");
        }

        // GET: DocumentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DocumentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DocumentViewModel document)
        {
            try
            {
                Company company = new Company();
                if (User.IsInRole("Company"))
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    company = _genericCompanyService.Get(u => u.IdentityUserId == user).First();

                }
                else if (User.IsInRole("User"))
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    Person person = _genericPersonService.Get(p => p.IdentityUserId == user).First();
                    company = _genericCompanyService.Get(u => u.Id == person.CompanyId).First();

                }

                string Message = string.Format(
                    "Prezado(a), <br> " +
                    "o cliente <b>{0}</b> de cnpj: {1}, abriu um requerimento solicitando o seguinte documento: {2} <br> \n ", company.Name, company.Cnpj, document.Type);
                if (document.Type == "Outros")
                {
                    Message += "<br>" + "Outros: " + document.Other;
                }
                else if (document.Type == "Especificações técnicas")
                {
                    Message += "<br>" + "Tipo de balde: " + document.BucketType + "<br>";
                    Message += "<br>" + "Tipo de tampa: " + document.BucketLidType + "<br>";

                }
                else if (document.Type == "Certidões negativas de débitos")
                {
                    Message += "<br>" + "Tipo: " + document.Debit + "<br>";
                }
                Employee employee = _pedidoVendaRepository.GetEmployeesByCNPJ(company.Cnpj);
                await _emailSender.SendEmailAsync("bomixforcedev@gmail.com", "Documento", Message, document.FilePath);
                Notify("Documento enviado com sucesso", "Documento");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DocumentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DocumentController/Edit/5
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

        // GET: DocumentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DocumentController/Delete/5
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
