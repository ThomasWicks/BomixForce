﻿using Bomix_Force.Areas.Identity.Pages.Account;
using Bomix_Force.Data.Context;
using Bomix_Force.Data.Entities;
using Bomix_Force.Repo.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.ViewModels
{
 
    public class UserViewModel
    {
            public Guid Id { get; set; }
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
    public class UserViewIndex
    {
        public IEnumerable<UserViewModel> UserList { get; set; }
        public UserViewModel User { get; set; }
    }
    
}
