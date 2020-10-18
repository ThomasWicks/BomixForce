using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Bomix_Force.AppServices.Interface;
using Bomix_Force.Areas.Identity.Pages.Account;
using Bomix_Force.Data.Entities;
using Bomix_Force.Data.Enum;
using Bomix_Force.Repo.Interface;
using Bomix_Force.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bomix_Force.Controllers
{
    public class NonconformityController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly INonconformityRepository _nonconformityRepository;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;

        public NonconformityController(INonconformityRepository nonconformityRepository, IGenericRepository<Company> genericCompanyService, IGenericRepository<Person> genericPersonService,
        IMapper mapper, SignInManager<IdentityUser> signInManager, IEmailSender emailSender, UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _nonconformityRepository = nonconformityRepository;
            _genericCompanyService = genericCompanyService;
            _genericPersonService = genericPersonService;

        }
        // GET: Nonconformity
        public ActionResult Index()
        {
            List<NonconformityViewModel> nonconformityView = new List<NonconformityViewModel>();
            List<Nonconformity> nonconformities = new List<Nonconformity>();
            if (User.IsInRole("Admin")|| User.IsInRole("Employee"))
            {
                nonconformities = _nonconformityRepository.GetAll().ToList();
                nonconformityView = _mapper.Map<IEnumerable<NonconformityViewModel>>(nonconformities).ToList();
            }
            else if (User.IsInRole("Company"))
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Company company = _genericCompanyService.Get(u => u.IdentityUserId == user).First();

                nonconformities = _nonconformityRepository.Get(n => n.CompanyId == company.Id).ToList();
                nonconformityView = _mapper.Map<IEnumerable<NonconformityViewModel>>(nonconformities).ToList();

            }else if (User.IsInRole("User"))
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Person person = _genericPersonService.Get(p => p.IdentityUserId == user).First();
                Company company = _genericCompanyService.Get(u => u.Id == person.CompanyId).First();

                nonconformities = _nonconformityRepository.Get(n => n.CompanyId == company.Id).ToList();
                nonconformityView = _mapper.Map<IEnumerable<NonconformityViewModel>>(nonconformities).ToList();
            }

            return View(nonconformityView);
        }

        // GET: Nonconformity/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Nonconformity/Create
        public ActionResult Create()
        {
            try
            {
                
                NonconformityViewModel nonconformityViewModel = new NonconformityViewModel();
                nonconformityViewModel.Itens = new List<string>();
                var values = Enum.GetValues(typeof(ItemEnum));
                foreach (var item in values)
                {
                    nonconformityViewModel.Itens.Add(item.ToString());
                }
                return View(nonconformityViewModel);
            }
            catch (Exception)
            {
                return View();
            }
 
        }

        // POST: Nonconformity/Create
        [HttpPost]
        public async Task<ActionResult> Create(NonconformityViewModel nonconformityViewModel)
        {
            try
            {
                Notify("Registro enviado com sucesso", "Não Conformidade");
                Company company = new Company();
                if (User.IsInRole("Company"))
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    company = _genericCompanyService.Get(u => u.IdentityUserId == user).First();
                    //string Email = _nonconformityRepository.GetSellerEmail(company.Cnpj);

                }
                else if (User.IsInRole("User"))
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    Person person = _genericPersonService.Get(p => p.IdentityUserId == user).First();
                    company = _genericCompanyService.Get(u => u.Id == person.CompanyId).First();
                    //string Email = _nonconformityRepository.GetSellerEmail(company.Cnpj);

                }
                string Message = string.Format(
                    "Prezado(a), <br> " +
                    "o cliente <b>{0}</b> de cnpj: {1}, abriu um registro de não conformidade em seu pedido:<br>" +
                    "Nota Fiscal: {2}<br> " +
                    "Lote: {3}<br>" +
                    "Quantidade:{4}<br> " +
                    "Item: {5}<br> " +
                    "Descrição do problema: {6}\n ", company.Name, company.Cnpj, nonconformityViewModel.Nf, nonconformityViewModel.Lote, nonconformityViewModel.Quantity,
                    nonconformityViewModel.SelectedItem, nonconformityViewModel.Description);
                await _emailSender.SendEmailAsync("thomas.wicks@hotmail.com", "Registro de não conformidade", Message);
                Nonconformity nonconformity = _mapper.Map<Nonconformity>(nonconformityViewModel);
                nonconformity.Company = company;
                var values = Enum.GetValues(typeof(ItemEnum));
                int index = 0;
                foreach (var item in values)
                {
                    if(item.ToString() == nonconformityViewModel.SelectedItem)
                    {
                        nonconformity.ItemEnum = index;
                    }
                    index++;
                }
                 _nonconformityRepository.Insert(nonconformity);
                _nonconformityRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Nonconformity/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Nonconformity/Edit/5
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

        // GET: Nonconformity/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Nonconformity/Delete/5
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
