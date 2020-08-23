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
    public class UserController : Controller
    {
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly IMapper _mapper;
        public UserController(IGenericRepository<Company> genericCompanyService, IMapper mapper,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            IGenericRepository<ApplicationUser> userRepository)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _userManager = userManager;
            _genericCompanyService = genericCompanyService;

        }
        [Authorize]
        // GET: UserController
        public ActionResult Index()
        {
            var teste = User.IsInRole("Company");
            if (User.IsInRole("Admin"))
            {
                IEnumerable<UserViewModel> userView = _mapper.Map<IEnumerable<UserViewModel>>(_userManager.GetUserAsync(User));
                foreach (var item in userView)
                {
                    ApplicationUser person = _userRepository.Get(u => u.UserName == item.UserName).First();
                    item.Company = _genericCompanyService.Get(g => g.Id == person.CompanyId).First(); ;
                }

                return View(userView);
            }
            else if (User.IsInRole("Company"))
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ApplicationUser person = _userRepository.Get(u => u.Id == user).First();
                Company company = _genericCompanyService.Get(g => g.Id == person.CompanyId).First();

                IEnumerable<UserViewModel> userView = _mapper.Map<IEnumerable<UserViewModel>>(_userRepository.Get(g => g.CompanyId == person.CompanyId));
                foreach (var item in userView)
                {
                    item.Company = company;
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
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(string id)
        {
            ApplicationUser person = _userRepository.Get(u => u.Id == id).First();
            UserViewModel userView = _mapper.Map<UserViewModel>(person);
            return View(userView);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                ApplicationUser person = _userRepository.Get(u => u.Id == id).First();
                person.UserName = collection["Name"];
                person.PhoneNumber = collection["Tel"];
                _userRepository.Update(person);
                _userRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(string id)
        {
            ApplicationUser person = _userRepository.Get(u => u.Id == id).First();
            UserViewModel userView = _mapper.Map<UserViewModel>(person);
            return View(userView);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                ApplicationUser person = _userRepository.Get(u => u.Id == id).First();
                _userManager.DeleteAsync(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
