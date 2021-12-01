namespace UpSkill.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Owner;

    public class OwnerService : IOwnerService
    {
        private readonly IDeletableEntityRepository<Owner> ownerRepository;
        private readonly IDeletableEntityRepository<EmployeeCourse> coursesRepo;
        private readonly IDeletableEntityRepository<LiveSession> sessionRepo;

        public OwnerService(
            IDeletableEntityRepository<Owner> ownerRepository,
            IDeletableEntityRepository<EmployeeCourse> coursesRepo,
            IDeletableEntityRepository<LiveSession> sessionRepo
            )
        {
            this.ownerRepository = ownerRepository;
            this.coursesRepo = coursesRepo;
            this.sessionRepo = sessionRepo;
        }

        public string GetId(string userId)
        {
            return ownerRepository.All().FirstOrDefault(x => x.UserId == userId)?.Id;
        }

        public async Task<OwnerInvoiceDetailsModel> GetInvoiceInfo(string ownerId, int monthNum)
        {
            var invoiceInfo = new OwnerInvoiceDetailsModel();

            invoiceInfo.MonthlyCoursesToPay = await this.coursesRepo
                .All()
                .Where(ec =>
                            ec.Student.Owner.Id == ownerId &&
                            ec.CreatedOn.Month == monthNum)
                .Select(ec => new CourseInvoiceModel
                {
                    CourseId = ec.Course.Id,
                    EmployeeId = ec.Student.Id,
                    CourseName = ec.Course.Name,
                    IssueDate = ec.CreatedOn,
                    Price = ec.Course.Price
                })
                .ToListAsync();

            invoiceInfo.MonthlySessionsToPay = await this.sessionRepo
                .All()
                .Where(s =>
                            s.Student.Owner.Id == ownerId &&
                            s.End.Month == monthNum)
                .Select(s => new LiveSessionInvoiceModel
                {
                    SessionId = s.Id,
                    EmployeeId = s.Student.Id,
                    CoachName = s.Coach.FullName,
                    Price = s.Price,
                    IssueDate = s.End
                })
                .ToListAsync();

            invoiceInfo.TotalMonthlyAmount = invoiceInfo.MonthlyCoursesToPay.Sum(c => c.Price);
            invoiceInfo.TotalMonthlyAmount += invoiceInfo.MonthlySessionsToPay.Sum(s => s.Price);
            
            return invoiceInfo;
        }

        public async Task<string> GetOwnerId(string userId)
        {
            var owner = await this.ownerRepository
                .All()
                .SingleOrDefaultAsync(u => u.UserId == userId);

            return owner.Id;
        }
    }
}
