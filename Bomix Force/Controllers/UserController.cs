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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bomix_Force.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IMapper _mapper;
        public UserController(IGenericRepository<Person> genericPersonService, IGenericRepository<Company> genericCompanyService,
        IMapper mapper, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger)
        {
            _genericPersonService = genericPersonService;
            _mapper = mapper;
            _genericCompanyService = genericCompanyService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;

        }
        [Authorize]
        // GET: UserController
        public ActionResult Index()
        {
            var teste = User.IsInRole("Company");
            if (User.IsInRole("Admin"))
            {
                UserViewIndex userList = new UserViewIndex();
                userList.UserList = new List<UserViewModel>();
                List<Person> people = _genericPersonService.GetAll().ToList();
                userList.UserList = _mapper.Map<IEnumerable<UserViewModel>>(people);
                foreach (var item in userList.UserList)
                {
                    Person person = _genericPersonService.Get(u => u.Name == item.Name).First();
                    //item.Input.Company = _genericCompanyService.Get(g => g.Id == person.CompanyId).First(); ;
                }

                return View(userList);
            }
            else if (User.IsInRole("Company"))
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Person person = _genericPersonService.Get(u => u.UserId == user).First();
                Company company = _genericCompanyService.Get(g => g.Id == person.CompanyId).First();

                IEnumerable<UserViewModel> userView = _mapper.Map<IEnumerable<UserViewModel>>(_genericPersonService.Get(g => g.CompanyId == person.CompanyId));
                foreach (var item in userView)
                {
                    item.CompanyName = company.Name;
                }

                return View(userView);
            }
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            List<Company> company = new List<Company>();
            company = _genericCompanyService.GetAll().ToList();
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel userviewModel)
        {
            try
            {
                //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                //if (ModelState.IsValid)
                //{

                //TODO TEST IF COMPANY QUERY WORKS
                Company company = _genericCompanyService.Get(c => c.Name == userviewModel.CompanyName).First();
                var user = new IdentityUser { UserName = userviewModel.UserName, Email = userviewModel.Email };
                var result = await _userManager.CreateAsync(user, userviewModel.Password);

                if (result.Succeeded)
                {
                    Person person = new Person { Name = userviewModel.Name, Email = userviewModel.Email, Tel = userviewModel.Tel, CompanyId = company.Id, UserId = user.Id };
                    _genericPersonService.Insert(person);
                    _genericPersonService.Save();
                    _logger.LogInformation("Person = " + person.Tel);
                    _logger.LogInformation("Novo usuário criado.");


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
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            Person person = _genericPersonService.Get(u => u.Id == id).First();
            UserViewModel userView = _mapper.Map<UserViewModel>(person);
            return View(userView);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                Person person = _genericPersonService.Get(u => u.Id == id).First();
                person.Name = collection["Name"];
                person.Tel = Int32.Parse(collection["Tel"]);
                _genericPersonService.Update(person);
                _genericPersonService.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
