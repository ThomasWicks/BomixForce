using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bomix_Force.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bomix_Force.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;

        public ChangePasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo é obrigatório")]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = "O campo é obrigatório")]
            [LowerCase(ErrorMessage ="A senha deve conter ao menos uma letra minúscula")]
            [UpperCase(ErrorMessage ="A senha deve conter ao menos uma letra maiúscula")]
            [Number(ErrorMessage ="A senha deve conter ao menos um número")]
            [SpecialChars(ErrorMessage ="A senha deve ter ao menos uma carácter especial (@, !, #, etc...)")]
            [StringLength(100, ErrorMessage = "A senha deve ter ao menos 6 caractéres", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nova Senha")]
            public string NewPassword { get; set; }
            [Required(ErrorMessage = "O campo é obrigatório")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirme a senha")]

            [Compare("NewPassword", ErrorMessage = "As senhas não coincidem")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }
            ViewData["emailConfirmed"] = user.EmailConfirmed ? "" : "Este é o seu primeiro acesso ou sua senha expirou, uma nova senha deve ser criada.";

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                Notify("Usuário não encontrado no sistema.", "Atenção", NotificationType.warning);
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (Input.OldPassword.Equals(Input.NewPassword))
            {
                Notify("Escolha uma senha diferente da atual.", "Atenção", NotificationType.warning);
                return Page();
            }
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (changePasswordResult.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, code);
            }
            else if (!changePasswordResult.Succeeded)
            {

                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError("changepasswordError", error.Description);
                }
                Notify("Não foi possível alterar a senha", "Atenção", NotificationType.error);
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("Usuário alterou a senha.");
            StatusMessage = "Sua senha foi alterada com sucesso.";
            Notify("Sua senha foi alterada com sucesso!", "Alteração", NotificationType.success);
            return Redirect("~/");
        }
        public void Notify(string message, string title = "Sweet Alert Toastr Demo",
                                   NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                message = message,
                title = title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = GetProvider()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }

        private string GetProvider()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProvider"];

            return value;
        }
    }
}

public class SpecialChars : RegularExpressionAttribute
{
    public SpecialChars()
        : base("^(?=.*[!@#$%^&+=]).*$")
    {
    }
}

public class UpperCase : RegularExpressionAttribute
{
    public UpperCase()
        : base("^(?=.*[A-Z]).*$")
    {
    }
}

public class LowerCase : RegularExpressionAttribute
{
    public LowerCase()
        : base("^(?=.*[a-z]).*$")
    {
    }
}
public class Number: RegularExpressionAttribute
{
    public Number()
        : base("^(?=.*[0-9]).*$")
    {
    }
}
