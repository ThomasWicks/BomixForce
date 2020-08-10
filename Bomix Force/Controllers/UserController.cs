using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Bomix_Force.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bomix_Force.Controllers
{
    public class UserController : Controller
    {
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IMapper _mapper;
        public UserController(IGenericRepository<Person> genericPersonService, IGenericRepository<Company> genericCompanyService, IMapper mapper)
        {
            _genericPersonService = genericPersonService;
            _mapper = mapper;
            _genericCompanyService = genericCompanyService;

        }

        // GET: UserController
        public ActionResult Index()
        {
            string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Person person = _genericPersonService.Get(u => u.UserId == user).First();
            IEnumerable<UserViewModel> userView = _mapper.Map<IEnumerable<UserViewModel>>(_genericPersonService.Get(p => p.CompanyId == person.CompanyId));
            return View(userView);
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
                person.Cpf =Int32.Parse( collection["Cpf"]);
                person.Tel =Int32.Parse(collection["Tel"]);
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
            return View();
        }

        // POST: UserController/Delete/5
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
