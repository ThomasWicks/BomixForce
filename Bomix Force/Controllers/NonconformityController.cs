using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Bomix_Force.AppServices.Interface;
using Bomix_Force.Areas.Identity.Pages.Account;
using Bomix_Force.Data.Entities;
using Bomix_Force.Data.Enum;
using Bomix_Force.Models;
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
            var identityUser = _userManager.GetUserAsync(User);
            if (identityUser.Result.EmailConfirmed == true)
            {
                if (User.IsInRole("Admin") || User.IsInRole("Employee"))
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

                }
                else if (User.IsInRole("User"))
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    Person person = _genericPersonService.Get(p => p.IdentityUserId == user).First();
                    Company company = _genericCompanyService.Get(u => u.Id == person.CompanyId).First();

                    nonconformities = _nonconformityRepository.Get(n => n.CompanyId == company.Id).ToList();
                    nonconformityView = _mapper.Map<IEnumerable<NonconformityViewModel>>(nonconformities).ToList();
                }
                foreach (var nonConformity in nonconformityView)
                {
                    var path = Path.Combine(
                   Directory.GetCurrentDirectory(),
                   "wwwroot/answers", nonConformity.Id.ToString() + ".pdf");
                    if (System.IO.File.Exists(path))
                        nonConformity.Status = "Concluido";
                    else
                        nonConformity.Status = "Análise";
                }

                return View(nonconformityView);
            }
            else
            {
                return LocalRedirect("./Identity/Account/Manage");
            }

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
                Notify("É necessario a criação de um registro de não conformidade para cada tipo de Item ", "Aviso", NotificationType.warning);
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
                //string Message = string.Format(
                //    "Prezado(a), <br> " +
                //    "o cliente <b>{0}</b> de cnpj: {1}, abriu um registro de não conformidade em seu pedido:<br>" +
                //    "Nota Fiscal: {2}<br> " +
                //    "Lote: {3}<br>" +
                //    "Quantidade:{4}<br> " +
                //    "Item: {5}<br> " +
                //    "Descrição do problema: {6}\n ", company.Name, company.Cnpj, nonconformityViewModel.Nf, nonconformityViewModel.Lote, nonconformityViewModel.Quantity,
                //    nonconformityViewModel.SelectedItem, nonconformityViewModel.Description);

                string FilePath = ".\\Views\\Template Email\\RNC.html";
                StreamReader str = new StreamReader(FilePath);
                string msg = str.ReadToEnd();
                msg = msg.Replace("NomeCliente", company.Name);
                msg = msg.Replace("CnpjCliente", company.Cnpj);
                msg = msg.Replace("NF", nonconformityViewModel.Nf);
                msg = msg.Replace("Lotes", nonconformityViewModel.Lote);
                msg = msg.Replace("Qtd", nonconformityViewModel.Quantity.ToString());
                msg = msg.Replace("Items", nonconformityViewModel.SelectedItem);
                msg = msg.Replace("Descricao", nonconformityViewModel.Description);

                str.Close();
                await _emailSender.SendEmailAsync("bomixforcedev@gmail.com", "Registro de não conformidade", msg, nonconformityViewModel.FilePath);
                Nonconformity nonconformity = _mapper.Map<Nonconformity>(nonconformityViewModel);
                nonconformity.Company = company;
                var values = Enum.GetValues(typeof(ItemEnum));
                int index = 0;
                foreach (var item in values)
                {
                    if (item.ToString() == nonconformityViewModel.SelectedItem)
                    {
                        nonconformity.ItemEnum = index;
                    }
                    index++;
                }
                _nonconformityRepository.Insert(nonconformity);
                _nonconformityRepository.Save();
                Notify("Registro enviado com sucesso", "Não Conformidade");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Notify("Não foi possivel criar o registro", "Não Conformidade", NotificationType.error);
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

        [HttpPost]
        public async Task<ActionResult> Download(NonconformityViewModel file)
        {
            try
            {
                var path = Path.Combine(
                       Directory.GetCurrentDirectory(),
                       "wwwroot/answers", file.Id.ToString() + ".pdf");

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;
                return File(memory, "application/pdf", Path.GetFileName(path));
            }
            catch (Exception x)
            {
                Notify("O arquivo não está disponivél", "Erro", NotificationType.error);
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Upload(NonconformityViewModel file)
        {
            try
            {
                var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/answers",
                file.Id.ToString() + ".pdf");
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.FilePath[0].CopyToAsync(stream);
                    }
                    Notify("O arquivo foi atualizado com sucesso", "Sucesso", NotificationType.success);
                }
                else
                {
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.FilePath[0].CopyToAsync(stream);
                    }
                    Notify("O arquivo foi enviado com sucesso", "Sucesso", NotificationType.success);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception x)
            {
                Notify("Um erro aconteceu ao enviar o arquivo, por favor verifique se um arquivo foi selecionado e  tente novamente", "Erro", NotificationType.error);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
