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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bomix_Force.ViewModels
{

    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não correspondem.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Telefone")]
        public int PhoneNumber { get; set; }
        [Display(Name = "Cargo")]
        [Required]
        public string Cargo { get; set; }
        [Display(Name = "Setor")]
        [Required]
        public string Setor { get; set; }
        public Company Company { get; set; }
        public string Cnpj { get; set; }
    }
    public class UserViewEdit
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public string UserID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        [Required]
        public string Cargo { get; set; }
        [Required]
        public string Setor { get; set; }
        [Required]
        public int? CompanyId { get; set; }


    }
}