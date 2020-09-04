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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using IEmailSender = Bomix_Force.AppServices.Interface.IEmailSender;
using Microsoft.AspNetCore.Identity;
using Bomix_Force.AppServices;

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
        private readonly IEmailSender _emailSender;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IMapper _mapper;
        public UserController(IGenericRepository<Person> genericPersonService, IGenericRepository<Company> genericCompanyService,
        IMapper mapper, SignInManager<IdentityUser> signInManager, IEmailSender emailSender, UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger)
        {
            _genericPersonService = genericPersonService;
            _mapper = mapper;
            _genericCompanyService = genericCompanyService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;

        }
        [Authorize]
        // GET: UserController
        public ActionResult Index(string selectType, string searchString)
        {
            UserViewIndex userList = new UserViewIndex
            {
                UserList = new List<UserViewModel>()
            };
            var teste = User.IsInRole("Company");
            if (User.IsInRole("Admin"))
            {

                List<Person> people = _genericPersonService.GetAll().ToList();
                userList.UserList = _mapper.Map<IEnumerable<UserViewModel>>(people);
                foreach (var item in userList.UserList)
                {
                    Person person = _genericPersonService.Get(u => u.Name == item.Name).First();
                    item.Company = _genericCompanyService.Get(g => g.Id == person.CompanyId).First(); ;
                }


            }
            else if (User.IsInRole("Company"))
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Person person = _genericPersonService.Get(u => u.UserId == user).First();
                Company company = _genericCompanyService.Get(g => g.Id == person.CompanyId).First();

                IEnumerable<Person> people = _genericPersonService.Get(g => g.CompanyId == person.CompanyId);
                userList.UserList = _mapper.Map<IEnumerable<UserViewModel>>(people);
                foreach (var item in userList.UserList)
                {
                    item.Company = company;
                }
            }
            else if (User.IsInRole("User"))
            {
                //todo user can't see user page
                return View();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                if (selectType == "Name")
                {
                    userList.UserList = userList.UserList.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
                }
                else if(selectType == "Company")
                {
                    userList.UserList = userList.UserList.Where(s => s.Company.Name.ToLower().Contains(searchString.ToLower()));
                }
                else if (selectType == "Cargo")
                {
                    userList.UserList = userList.UserList.Where(s => s.Cargo.ToLower().Contains(searchString.ToLower()));
                }
                else if (selectType == "Setor")
                {
                    userList.UserList = userList.UserList.Where(s => s.Setor.ToLower().Contains(searchString.ToLower()));
                }
            }
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
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewIndex userviewIndex)
        {
            try
            {
                //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                //if (ModelState.IsValid)
                //{
                if (User.IsInRole("Company"))
                {
                    //TODO TEST IF COMPANY QUERY WORKS
                    string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    Person person_owner = _genericPersonService.Get(u => u.UserId == userId).First();
                    Company company = _genericCompanyService.Get(g => g.Id == person_owner.CompanyId).First();
                    UserViewModel userviewModel = userviewIndex.User;
                    var user = new IdentityUser { UserName = userviewModel.UserName, Email = userviewModel.Email };
                    var result = await _userManager.CreateAsync(user, userviewModel.Password);

                    if (result.Succeeded)
                    {
                        Person person = new Person { Name = userviewModel.Name, Email = userviewModel.Email, Cargo = userviewModel.Cargo, Setor = userviewModel.Setor, Tel = userviewModel.Tel, CompanyId = company.Id, UserId = user.Id };
                        _genericPersonService.Insert(person);
                        _genericPersonService.Save();
                        _logger.LogInformation("Person = " + person.Tel);
                        _logger.LogInformation("Novo usuário criado.");
                        await _emailSender.SendEmailAsync("thomaswicks96@gmail.com", "Usuário criado", "O usuário " + person.Name + " foi criado com sucesso");


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
            Person person = _genericPersonService.Get(u => u.Id == id).First();
            UserViewIndex userView = new UserViewIndex
            {
                UserViewEdit = _mapper.Map<UserViewEdit>(person)
            };
            return PartialView("_userModelPartial", userView.UserViewEdit);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(UserViewEdit userviewEdit)
        {
            try
            {

                Person newperson = _mapper.Map<Person>(userviewEdit);
                _genericPersonService.Update(newperson);
                _genericPersonService.Save();
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
