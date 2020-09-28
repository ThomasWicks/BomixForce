using System;
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
using Bomix_Force.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Bomix_Force.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Employee> _genericEmployeeService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IMapper _mapper;
        public UserController(IGenericRepository<Person> genericPersonService,
            IGenericRepository<Company> genericCompanyService, IGenericRepository<Employee> genericEmployeeService,
        IMapper mapper, SignInManager<IdentityUser> signInManager, IEmailSender emailSender, UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger)
        {
            _genericPersonService = genericPersonService;
            _mapper = mapper;
            _genericEmployeeService = genericEmployeeService;
            _genericCompanyService = genericCompanyService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;

        }
        [Authorize]
        // GET: UserController
        public ActionResult Index(string searchString, int? pageNumber)
        {
            ViewBag.searchString = searchString;
            int page = (pageNumber ?? 1);
            List<UserViewModel> userView = new List<UserViewModel>();
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                List<Employee> employees = _genericEmployeeService.GetAll().ToList();
                List<Person> people = _genericPersonService.GetAll().ToList();
                List<Company> Company = _genericCompanyService.GetAll().ToList();
                userView = _mapper.Map<IEnumerable<UserViewModel>>(Company).ToList();
                userView.AddRange(_mapper.Map<IEnumerable<UserViewModel>>(people).ToList());
                userView.AddRange(_mapper.Map<IEnumerable<UserViewModel>>(employees).ToList());
                if (!String.IsNullOrEmpty(searchString))
                {

                    var userViewCompany = userView.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                    var userViewTel = userView.Where(s => s.PhoneNumber.ToString().ToLower().Contains(searchString.ToLower())).ToList();
                    var userViewEmail = userView.Where(s => s.Email.ToLower().Contains(searchString.ToLower())).ToList();
                    userView = userViewCompany.Union(userViewTel).Union(userViewEmail).ToList();

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

                    var userViewName = userView.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
                    var userViewCargo = userView.Where(s => s.Cargo.ToLower().Contains(searchString.ToLower())).ToList();
                    var userViewSetor = userView.Where(s => s.Setor.ToLower().Contains(searchString.ToLower())).ToList();
                    var userViewEmail = userView.Where(s => s.Email.ToLower().Contains(searchString.ToLower())).ToList();
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
        public async Task<ActionResult> Create(UserViewModel userVIew)
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
                    var user = new IdentityUser { UserName = userVIew.UserName, Email = userVIew.Email };
                    var result = await _userManager.CreateAsync(user, randomPass);

                    if (company.Id == 0 && User.IsInRole("Admin"))
                        _ = await _userManager.AddToRoleAsync(user, "Admin");
                    else
                        _ = await _userManager.AddToRoleAsync(user, "User");


                    if (result.Succeeded)
                    {
                        Person person = new Person { Name = userVIew.Name, Cargo = userVIew.Cargo, Setor = userVIew.Setor, CompanyId = company.Id, IdentityUserId = user.Id, Email = userVIew.Email };
                        _genericPersonService.Insert(person);
                        _genericPersonService.Save();
                        _logger.LogInformation("Novo usuário criado.");
                        await _emailSender.SendEmailAsync(user.Email, "Cadastro usuário", "O seu usuário foi criado com a senha: " + randomPass);
                        //await _emailSender.SendEmailAsync("thomaswicks96@gmail.com", "Usuário criado", "O usuário " + person.Name + " foi criado com sucesso");


                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    //}

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
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: UserController/Edit/5
        [Route("User/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            if (User.IsInRole("Company"))
            {
                Person person = _genericPersonService.Get(u => u.Id == id).First();
                UserViewEdit userViewEdit = _mapper.Map<UserViewEdit>(person);
                return PartialView("_userModelPartial", userViewEdit);
            }
            else if (User.IsInRole("Employee") || User.IsInRole("Admin"))
            {
                Company company = _genericCompanyService.Get(u => u.Id == id).First();
                UserViewEdit userViewEdit = _mapper.Map<UserViewEdit>(company);
                return PartialView("_userModelPartial", userViewEdit);
            }
            return RedirectToAction(nameof(Index));
        }
        // POST: UserController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(UserViewEdit userviewEdit, string selectCargo, string selectSetor)
        {
            try
            {
                userviewEdit.Cargo = selectCargo;
                userviewEdit.Setor = selectSetor;
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Person newperson = _mapper.Map<Person>(userviewEdit);
                _genericPersonService.Update(newperson);
                _genericPersonService.Save();
                if (User.IsInRole("Company"))
                {
                    Company company = _genericCompanyService.Get(c => c.IdentityUserId == user).First();
                    await _emailSender.SendEmailAsync("rubemdealmeida@hotmail.com", "Usuário Editado", "O Usuário " + newperson.Name + " foi editado pela companhia " + company.Name);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception x)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            Person person = _genericPersonService.Get(u => u.Id == id).First();
            UserViewModel userView = _mapper.Map<UserViewModel>(person);
            return View(userView);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _genericPersonService.Delete(id);
                _genericPersonService.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}