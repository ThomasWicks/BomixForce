using Bomix_Force.Areas.Identity.Pages.Account;
using Bomix_Force.Data.Context;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Bomix_Force.AppServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.ViewModels
{

    public class UserViewModel
    {
        public int Id { get; set; }
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
        public string Name { get; set; }
        [Required]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public int Tel { get; set; }
        [Required]
        public string Endereço { get; set; }
        [Required]
        public string Cargo { get; set; }
        public string Setor { get; set; }
        public Company Company { get; set; }
    }
    public class UserViewIndex
    {
        public IEnumerable<UserViewModel> UserList { get; set; }
        public UserViewModel User { get; set; }
        public UserViewEdit UserViewEdit { get; set; }
    }
    public class UserViewEdit
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string UserID { get; set; }

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
        public string Name { get; set; }
        [Required]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public int Tel { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        public string Cargo { get; set; }
        public string Setor { get; set; }
        public int? CompanyId { get; set; }
    }
}
