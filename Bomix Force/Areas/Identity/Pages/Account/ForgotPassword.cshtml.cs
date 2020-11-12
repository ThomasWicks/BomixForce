using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Bomix_Force.Controllers;
using Bomix_Force.AppServices.Interface;
using Bomix_Force.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Components;
using Bomix_Force.Repo.Interface;
using Bomix_Force.Data.Entities;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Bomix_Force.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IGenericRepository<Company> _genericCompanyService;
        private readonly IGenericRepository<Person> _genericPersonService;
        private readonly IGenericRepository<Employee> _genericEmployeeService;
        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender, IGenericRepository<Person> genericPersonService,
            IGenericRepository<Employee> genericEmployeeService, IGenericRepository<Company> genericCompanyService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _genericCompanyService = genericCompanyService;
            _genericPersonService = genericPersonService;
            _genericEmployeeService = genericEmployeeService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "O campo é obrigatório")]
            public string UserName { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(Input.UserName);
                    string Email = string.Empty;
                    if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                    {
                        // Don't reveal that the user does not exist or is not confirmed
                        //return RedirectToPage("./ForgotPasswordConfirmation");
                        //var teste = new BaseController();
                        Notify("Usuário não encontrado no sistema.", "Erro ao buscar o usuário", NotificationType.warning);
                        return Page();
                    }

                    List<Company> company = _genericCompanyService.Get(g => g.IdentityUserId == user.Id).ToList();
                    if (company.Count() == 0)
                    {
                        List<Person> person = _genericPersonService.Get(g => g.IdentityUserId == user.Id).ToList();
                        if (person.Count() == 0)
                        {
                            List<Employee> employee = _genericEmployeeService.Get(g => g.IdentityUserId == user.Id).ToList();
                            Email = employee.First().Email;
                        }
                        else
                        {
                            Email = person.First().Email;
                        }
                    }
                    else
                    {
                        Email = company.First().Email;
                    }
                    // For more information on how to enable account confirmation and password reset please 
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme);
                    await _emailSender.SendEmailAsync(
                        Email,
                        "Redefinição de senha",
                        $"Para redefinir a senha <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clique aqui</a>.", null);
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                return Page();
            }
            catch (Exception e)
            {
                Notify("Usuário não encontrado no sistema.", "Erro ao buscar o usuário", NotificationType.warning);
                return Page();
            }

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
