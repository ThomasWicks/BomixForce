﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Bomix_Force.Areas.Identity.Pages.Account;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Bomix_Force.ViewModels;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using IEmailSender = Bomix_Force.AppServices.Interface.IEmailSender;
using Microsoft.AspNetCore.Identity;
using Bomix_Force.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Bomix_Force.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Employee> _genericEmployeeService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IMapper _mapper;
        private readonly IPedidoVendaRepository _pedidoVendaRepository;

        public UserController(IGenericRepository<Person> genericPersonService,
            IGenericRepository<Company> genericCompanyService, IGenericRepository<Employee> genericEmployeeService,
        IMapper mapper, SignInManager<IdentityUser> signInManager, IEmailSender emailSender, UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger,
        IPedidoVendaRepository pedidoVendaRepository)
        {
            _genericPersonService = genericPersonService;
            _mapper = mapper;
            _genericEmployeeService = genericEmployeeService;
            _genericCompanyService = genericCompanyService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _pedidoVendaRepository = pedidoVendaRepository;


        }
        [Authorize]
        // GET: UserController
        public ActionResult Index(string searchString, int? pageNumber)
        {
            ViewBag.searchString = searchString;
            ViewBag.indexEmployees = false;
            int page = (pageNumber ?? 1);
            List<UserViewModel> userView = new List<UserViewModel>();
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {

                IEnumerable<Company> Company = _genericCompanyService.GetAll();
                userView = _mapper.Map<IEnumerable<UserViewModel>>(Company).ToList();
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.Trim();
                    var userViewCompany = userView.Where(s => s.Name != null && s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                    var userViewEmail = userView.Where(s => s.Email != null && s.Email.ToLower().Contains(searchString.ToLower())).ToList();
                    userView = userViewCompany.Union(userViewEmail).ToList();

                }

            }
            else if (User.IsInRole("Company"))
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Company company = _genericCompanyService.Get(u => u.IdentityUserId == user).First();

                IEnumerable<Person> people = _genericPersonService.Get(g => g.CompanyId == company.Id);
                userView = _mapper.Map<IEnumerable<UserViewModel>>(people).ToList();

                foreach (var item in userView)
                {
                    item.Company = company;
                }
                if (!String.IsNullOrEmpty(searchString))
                {
                    searchString = searchString.Trim();
                    var userViewName = userView.Where(s =>s.Name !=null && s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                    var userViewCargo = userView.Where(s =>s.Cargo !=null && s.Cargo.ToLower().Contains(searchString.ToLower())).ToList();
                    var userViewSetor = userView.Where(s =>s.Setor !=null && s.Setor.ToLower().Contains(searchString.ToLower())).ToList();
                    var userViewEmail = userView.Where(s =>s.Email !=null && s.Email.ToLower().Contains(searchString.ToLower())).ToList();
                    userView = userViewName.Union(userViewCargo).Union(userViewSetor).Union(userViewEmail).Union(userViewName).ToList();

                }
            }
            else if (User.IsInRole("User"))
            {
                //todo user can't see user page
                return View();
            }

            var userList = userView.ToPagedList(page, 9);
            return View(userList);
        }
        public ActionResult indexEmployees(int id, string searchString, int? pageNumber)
        {
            ViewBag.searchString = searchString;
            int page = (pageNumber ?? 1);
            ViewBag.searchString = searchString;
            ViewBag.indexEmployeeID = id;
            string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Company company = _genericCompanyService.Get(c => c.Id == id).First();
            IEnumerable<Person> people = _genericPersonService.Get(g => g.CompanyId == id);
            List<UserViewModel> userView = _mapper.Map<IEnumerable<UserViewModel>>(people).ToList();
            ViewBag.indexEmployees = true;
            foreach (var item in userView)
            {
                item.Company = company;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim();

                var userViewName = userView.Where(s =>s.Name !=null && s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                var userViewCargo = userView.Where(s =>s.Cargo !=null && s.Cargo.ToLower().Contains(searchString.ToLower())).ToList();
                var userViewSetor = userView.Where(s =>s.Setor !=null && s.Setor.ToLower().Contains(searchString.ToLower())).ToList();
                var userViewEmail = userView.Where(s =>s.Email !=null && s.Email.ToLower().Contains(searchString.ToLower())).ToList();
                userView = userViewName.Union(userViewCargo).Union(userViewSetor).Union(userViewEmail).Union(userViewName).ToList();

            }
            var userList = userView.ToPagedList(page, 9);
            return View(nameof(Index), userList);


        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            UserViewModel userView = new UserViewModel();
            return PartialView("_createUserModal", userView);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel userView)
        {
            try
            {
                //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                //if (ModelState.IsValid)
                //{
                if (User.IsInRole("Company") || User.IsInRole("Admin"))
                {
                    RandomPasswordGenerator passwordGenerator = new RandomPasswordGenerator();
                    //TODO TEST IF COMPANY QUERY WORKS
                    string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    string randomPass = passwordGenerator.GeneratePassword();
                    //Person person_owner = _genericPersonService.Get(u => u.IdentityUserId == userId).First();
                    Company company = _genericCompanyService.Get(g => g.IdentityUserId == userId).First();
                    var countUsers = _genericPersonService.Get(m => m.CompanyId == company.Id).Count();
                    if (countUsers == 3){
                        ModelState.AddModelError("Não é possível cadastrar", "Limite de 3 usuários atingido.");
                        return StatusCode(409);
                    }
                    var user = new IdentityUser { UserName = userView.UserName, Email = userView.Email };
                    var result = await _userManager.CreateAsync(user, randomPass);
                    if (result.Succeeded)
                    {
                        var simpleUser = false;
                        if (company.Id == 0 && User.IsInRole("Admin") && userView.Admin)
                        {
                            _ = await _userManager.AddToRoleAsync(user, "Admin");
                            user.EmailConfirmed = true;
                        }
                        else if(User.IsInRole("Admin") && !userView.Admin)
                        {
                            _ = await _userManager.AddToRoleAsync(user, "Employee");
                            user.EmailConfirmed = true;
                        }
                        else
                        {
                            _ = await _userManager.AddToRoleAsync(user, "User");
                            simpleUser = true;
                        }

                        Person person = new Person { Name = userView.Name, Cargo = userView.Cargo, Setor = userView.Setor, CompanyId = company.Id, IdentityUserId = user.Id, Email = userView.Email };
                        _genericPersonService.Insert(person);
                        _genericPersonService.Save();
                        _logger.LogInformation("Novo usuário criado.");
                        Employee employee = _pedidoVendaRepository.GetEmployeesByCNPJ(company.Cnpj);
                        if (simpleUser)
                        {
                            await _emailSender.SendEmailAsync(user.Email, "Cadastro usuário", "O seu usuário foi criado com a senha: " + randomPass+"\nO Cadastro ainda está pendente de confirmação", null);
                            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, protocol: Request.Scheme);
                            await _emailSender.SendEmailAsync(/*employee.Email*/"pedrogustavosantana97@gmail.com", "Cadastro usuário",$"O Cliente {company.Name} criou o usuario {person.Name}.\n<a href=\""+ callbackUrl + "\">Confirmar</a> o cadastro" , null);
                        }else{
                            await _emailSender.SendEmailAsync(user.Email, "Cadastro usuário", "O seu usuário foi criado com a senha: " + randomPass, null);
                            await _emailSender.SendEmailAsync(employee.Email, "Cadastro usuário",$"O Cliente {company.Name} criou o usuario {person.Name}." , null);
                        }

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return StatusCode(409);
                    }

                    // If we got this far, something failed, redisplay form
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null){
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    return View("ConfirmEmail");
                }
                return View();
            }
            return StatusCode(500);
        }

        [HttpGet]
        public JsonResult getUserName()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (User.IsInRole("Employee"))
            {
                Employee employee = _genericEmployeeService.Get(e => e.IdentityUserId == userId).First();
                string json = "{" +
                   $"\"person\":\"{employee.Name}\"" +
                   "}";
                return Json(json);
            }
            else if (User.IsInRole("Company") || User.IsInRole("Admin"))
            {
                Company company = _genericCompanyService.Get(c => c.IdentityUserId == userId).First();
                string json = "{" +
                  $"\"person\":\"{company.Name}\"" +
                  "}";
                return Json(json);
            }

            else
            {
                Person person = _genericPersonService.Get(p => p.IdentityUserId == userId).First();
                Company company = _genericCompanyService.Get(c => c.Id == person.CompanyId).First();
                string json = "{" +
                    $"\"person\":\"{person.Name}\"," +
                    $"\"company\":\"{company.Name}\"" +
                    "}";
                return Json(json);
            }
        }
        // GET: UserController/Edit/5

        [Route("User/Edit/{id}")]
        public ActionResult Edit(int id)
        {

            Person person = _genericPersonService.Get(u => u.Id == id).First();
            UserViewEdit userViewEdit = _mapper.Map<UserViewEdit>(person);
            return PartialView("_userModelPartial", userViewEdit);

        }
        // POST: UserController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string IdentityUserId, string Name, string Id, string Setor, string Cargo, string Email, int CompanyId)
        {
            try
            {
                UserViewEdit userviewEdit = new UserViewEdit { Id = Convert.ToInt32(Id), IdentityUserId = IdentityUserId, Name = Name, Cargo = Cargo, Setor = Setor, Email = Email, CompanyId = CompanyId };
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Person newperson = _mapper.Map<Person>(userviewEdit);
                _genericPersonService.Update(newperson);
                _genericPersonService.Save();
                if (User.IsInRole("Company"))
                {
                    Company company = _genericCompanyService.Get(c => c.IdentityUserId == user).First();
                    Employee employee = _pedidoVendaRepository.GetEmployeesByCNPJ(company.Cnpj);
                    await _emailSender.SendEmailAsync(employee.Email, "Usuário Editado", "O Usuário " + newperson.Name + " foi editado pela companhia " + company.Name, null);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception x)
            {
                return StatusCode(500);
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Person person = _genericPersonService.Get(u => u.Id == id).First();
            UserViewModel userView = _mapper.Map<UserViewModel>(person);
            return View(userView);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                Person person = _genericPersonService.Get(u => u.Id == id).First();
                _ = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(person.IdentityUserId));
                _genericPersonService.Delete(id);
                _genericPersonService.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult Contato()
        {
            return View();
        }
    }
}