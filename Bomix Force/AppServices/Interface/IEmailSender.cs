using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomix_Force.AppServices.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, List<IFormFile> attachment );
    }

}
