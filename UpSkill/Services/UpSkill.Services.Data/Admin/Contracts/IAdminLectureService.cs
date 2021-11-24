using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Lecture;

namespace UpSkill.Services.Data.Admin.Contracts
{
    public interface IAdminLectureService
    {
        Task<int?> Create(LectureCreateServiceModel input);
    }
}
