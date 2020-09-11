using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Bomix_Force.AppServices;
using Bomix_Force.AppServices.Interface;
using Bomix_Force.Data.Context;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Bomix_Force.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using IEmailSender = Bomix_Force.AppServices.Interface.IEmailSender;

namespace Bomix_Force.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            //IEmailSender emailSender,
            IEmailSender emailSender,
            IGenericRepository<Company> genericCompanyService,
            IGenericRepository<Person> genericPersonService
              )
           
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _genericCompanyService = genericCompanyService;
            _genericPersonService = genericPersonService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Usuário")]
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "A {0} precisa ser pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Senha")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "As senhas não correspondem.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Nome da Empresa")]
            public string CompanyName { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            [Display(Name = "Telefone")]
            [DataType(DataType.PhoneNumber)]
            public int Tel { get; set; }
            [Required]
            public string Endereço { get; set; }


        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            List<Company> company = new List<Company>();
            company = _genericCompanyService.GetAll().ToList();
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var context = new ModelContext();
                //TODO TEST IF COMPANY QUERY WORKS
                Company company = _genericCompanyService.Get(c => c.Name == Input.CompanyName).First();
                var user = new IdentityUser { UserName = Input.UserName, Email = Input.Email, PhoneNumber = Input.Tel.ToString() };
                var result = await _userManager.CreateAsync(user, Input.Password);
                
                if (result.Succeeded)
                {
                    Person person = new Person { Name = Input.Name, CompanyId = company.Id, IdentityUserId = user.Id };
                    _genericPersonService.Insert(person);
                    _genericPersonService.Save();
                    _logger.LogInformation("Person = " + user.PhoneNumber);
                    _logger.LogInformation("Novo usuário criado.");


                    return Page();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
