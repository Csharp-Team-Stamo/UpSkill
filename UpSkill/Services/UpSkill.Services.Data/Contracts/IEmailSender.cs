﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;

namespace UpSkill.Services.Data.Contracts
{
    public interface IEmailSender
    {
        Task<Response> SendMailAsync(string subject, string toEmail, string toFullName, string plainTextMessage, string htmlContent);

        Task<Response> SendMailAsync(string subject, string toEmail, string toFullName, string plainTextMessage);
    }
}
