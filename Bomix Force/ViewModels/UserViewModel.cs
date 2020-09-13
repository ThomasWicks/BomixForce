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
        public int PhoneNumber { get; set; }
        [Display(Name = "Cargo")]
        [Required]
        public string Cargo { get; set; }
        [Display(Name = "Setor")]
        [Required]
        public string Setor { get; set; }
        public Company Company { get; set; }
        public List<SelectListItem> Setores { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Propriétario", Text = "Propriétario" },
            new SelectListItem { Value = "Diretor", Text = "Diretor" },
            new SelectListItem { Value = "Gerente", Text = "Gerente"  },
            new SelectListItem { Value = "Coordenador", Text = "Coordenador"  },
            new SelectListItem { Value = "Supervisor", Text = "Supervisor"  },
            new SelectListItem { Value = "Analista", Text = "Analista"  },
            new SelectListItem { Value = "Assistente/Secretária", Text = "Assistente/Secretária"  },
            new SelectListItem { Value = "Genérico Industrial", Text = "Genérico Industrial"  },
            new SelectListItem { Value = "Genérico ADM", Text = "Genérico ADM"  },
        };
        public List<SelectListItem> Cargos { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Comercial", Text = "Comercial" },
            new SelectListItem { Value = "PCP/S$OP", Text = "PCP/S$OP" },
            new SelectListItem { Value = "Qualidade", Text = "Qualidade"  },
            new SelectListItem { Value = "Compras", Text = "Compras"  },
            new SelectListItem { Value = "Suprimentos", Text = "Suprimentos"  },
            new SelectListItem { Value = "Genérico Industrial", Text = "Genérico Industrial"  },
            new SelectListItem { Value = "Genérico ADM", Text = "Genérico ADM"  },
            new SelectListItem { Value = "Produção", Text = "Produção"  },
            new SelectListItem { Value = "Financeiro", Text = "Financeiro"  },
            new SelectListItem { Value = "Logística", Text = "Logística"  },
            new SelectListItem { Value = "Marketing", Text = "Marketing"  },
        };
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


        public List<SelectListItem> Setores { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Propriétario", Text = "Propriétario" },
            new SelectListItem { Value = "Diretor", Text = "Diretor" },
            new SelectListItem { Value = "Gerente", Text = "Gerente"  },
            new SelectListItem { Value = "Coordenador", Text = "Coordenador"  },
            new SelectListItem { Value = "Supervisor", Text = "Supervisor"  },
            new SelectListItem { Value = "Analista", Text = "Analista"  },
            new SelectListItem { Value = "Assistente/Secretária", Text = "Assistente/Secretária"  },
            new SelectListItem { Value = "Genérico Industrial", Text = "Genérico Industrial"  },
            new SelectListItem { Value = "Genérico ADM", Text = "Genérico ADM"  },
        };
        public List<SelectListItem> Cargos { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Comercial", Text = "Comercial" },
            new SelectListItem { Value = "PCP/S$OP", Text = "PCP/S$OP" },
            new SelectListItem { Value = "Qualidade", Text = "Qualidade"  },
            new SelectListItem { Value = "Compras", Text = "Compras"  },
            new SelectListItem { Value = "Suprimentos", Text = "Suprimentos"  },
            new SelectListItem { Value = "Genérico Industrial", Text = "Genérico Industrial"  },
            new SelectListItem { Value = "Genérico ADM", Text = "Genérico ADM"  },
            new SelectListItem { Value = "Produção", Text = "Produção"  },
            new SelectListItem { Value = "Financeiro", Text = "Financeiro"  },
            new SelectListItem { Value = "Logística", Text = "Logística"  },
            new SelectListItem { Value = "Marketing", Text = "Marketing"  },
        };
    }
}