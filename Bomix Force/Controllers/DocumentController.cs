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
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IGenericRepository<Document> _genericDocumentService;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IPedidoVendaRepository _pedidoVendaRepository;
        private IWebHostEnvironment _environment;

        public DocumentController(INonconformityRepository nonconformityRepository, IGenericRepository<Document> genericDocumentService, IGenericRepository<Company> genericCompanyService, IGenericRepository<Person> genericPersonService,
        IMapper mapper, SignInManager<IdentityUser> signInManager, IEmailSender emailSender, IWebHostEnvironment environment, UserManager<IdentityUser> userManager, ILogger<RegisterModel> logger, IPedidoVendaRepository pedidoVendaRepository)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _environment = environment;
            _genericDocumentService = genericDocumentService;
            _emailSender = emailSender;
            _nonconformityRepository = nonconformityRepository;
            _genericCompanyService = genericCompanyService;
            _genericPersonService = genericPersonService;
            _pedidoVendaRepository = pedidoVendaRepository;

        }
        // GET: DocumentController
        public ActionResult Index()
        {
            List<DocumentViewModel> documentView = new List<DocumentViewModel>();
            List<Document> documents = new List<Document>();
            var identityUser = _userManager.GetUserAsync(User);
            if (identityUser.Result.EmailConfirmed == true)
            {
                if (User.IsInRole("Admin") || User.IsInRole("Employee"))
                {

                    documents = _genericDocumentService.GetAll().ToList();
                    documentView = _mapper.Map<IEnumerable<DocumentViewModel>>(documents).ToList();
                }
                else if (User.IsInRole("Company")){

                }
                else if (User.IsInRole("User"))
                {
                    string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    documents = _genericDocumentService.Get(d => d.IdentityUserId == user).ToList();
                    documentView = _mapper.Map<IEnumerable<DocumentViewModel>>(documents).ToList();

                }
                foreach (var document in documentView)
                {
                    var path = Path.Combine(
                   Directory.GetCurrentDirectory(),
                   "wwwroot/answers", "DOC_" + document.Id.ToString() + ".pdf");
                    if (System.IO.File.Exists(path))
                        document.Status = "Concluido";
                    else
                        document.Status = "Análise";
                }

                return View(documentView);
            }
            else
            {
                return Redirect("~/Identity/Account/Manage");
            }

        }

        // GET: DocumentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DocumentController/Create
        public ActionResult Create()
        {
            DocumentViewModel documentViewModel = new DocumentViewModel();
            return View(documentViewModel);
        }

        // POST: DocumentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DocumentViewModel documentViewModel)
        {
            try
            {
                string user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                List<Document> documents = new List<Document>();

                List<string> documentos = new List<string>();
                if (documentViewModel.AF)
                {
                    Document document = new Document();
                    document.Type = "Alvará de funcionamento";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.AVCB)
                {
                    Document document = new Document();
                    document.Type = "AVCB – Auto de vistoria do Corpo de Bombeiros";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.AS)
                {
                    Document document = new Document();
                    document.Type = "Alvará sanitário";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.LA)
                {
                    Document document = new Document();
                    document.Type = "Licença ambiental";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.CISSO9001)
                {
                    Document document = new Document();
                    document.Type = "Certificado ISO 9001";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }

                if (documentViewModel.CFSSC22000)
                {
                    Document document = new Document();
                    document.Type = "Certificado FSSC 22000";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.LMG)
                {
                    Document document = new Document();
                    document.Type = "Laudo de migração";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.LMB)
                {
                    Document document = new Document();
                    document.Type = "Laudo microbiológico";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.ET)
                {
                    Document document = new Document();
                    document.Type = "Especificações técnicas";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.CND)
                {
                    Document document = new Document();
                    document.Type = "Certidões negativas de débitos";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.DAA)
                {
                    Document document = new Document();
                    document.Type = "Declaração ausência de alergênicos";
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                if (documentViewModel.OT)
                {
                    Document document = new Document();
                    document.Type = documentViewModel.Other;
                    document.PathExtFile = null;
                    document.Date = DateTime.Now;
                    document.IdentityUserId = user;
                    documents.Add(document);
                }
                Company company = new Company();
                if (User.IsInRole("Company"))
                {
                    company = _genericCompanyService.Get(u => u.IdentityUserId == user).First();

                }
                else if (User.IsInRole("User"))
                {
                    Person person = _genericPersonService.Get(p => p.IdentityUserId == user).First();
                    company = _genericCompanyService.Get(u => u.Id == person.CompanyId).First();

                }

                string Message = string.Format(
                    "Prezado(a), <br> " +
                    "o cliente <b>{0}</b> de cnpj: {1}, abriu um requerimento solicitando o(s) seguinte(s) documento(s): <br> \n ", company.Name, company.Cnpj);
                foreach (var documento in documents)
                {
                    Message += "<br> -" + documento.Type;
                    if (documento.Type == "Outros")
                    {
                        Message += "<br>" + "--Outros: " + documentViewModel.Other;
                    }
                    else if (documento.Type == "Especificações técnicas")
                    {
                        Message += "<br>" + "Tipo de balde: " + documentViewModel.BucketType + "<br>";
                        Message += "<br>" + "Tipo de tampa: " + documentViewModel.BucketLidType + "<br>";
                    }
                    else if (documento.Type == "Certidões negativas de débitos")
                    {
                        Message += "<br>" + "Tipo de certidão: " + documentViewModel.Debit + "<br>";
                    }
                    _genericDocumentService.Insert(documento);
                    _genericDocumentService.Save();
                }
                Employee employee = _pedidoVendaRepository.GetEmployeesByCNPJ(company.Cnpj);
                await _emailSender.SendEmailAsync(employee.Email, "Documento", Message, document.FilePath);
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


        [HttpPost]
        public async Task<ActionResult> Download(NonconformityViewModel file)
        {
            try
            {
                string wwwPath = _environment.WebRootPath;
                var path = wwwPath + "\\answers\\DOC_" + file.Id.ToString() + ".pdf";

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
                string wwwPath = _environment.WebRootPath;
                var path = wwwPath + "\\answers\\DOC_" + file.Id.ToString() + ".pdf";
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
