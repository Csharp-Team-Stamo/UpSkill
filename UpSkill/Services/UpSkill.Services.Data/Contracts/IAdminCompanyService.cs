using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Company;

namespace UpSkill.Services.Data.Contracts
{
    public interface IAdminCompanyService
    {
        Task<int?> Create(CompanyCreateInputModel input);
    }
}
