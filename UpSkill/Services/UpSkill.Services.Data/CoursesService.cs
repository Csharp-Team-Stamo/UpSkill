namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Contracts;
    using Infrastructure.Models.Course;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<CourseOwner> coursesOwnerRepository;

        public CoursesService(IDeletableEntityRepository<Course> coursesRepository, IDeletableEntityRepository<CourseOwner> coursesOwnerRepository)
        {
            this.coursesRepository = coursesRepository;
            this.coursesOwnerRepository = coursesOwnerRepository;
        }

        public CoursesListingCatalogModel GetAll(string ownerId)
        {
            var courses = new CoursesListingCatalogModel
            {
                OwnerId = ownerId,
                OwnerCourseCollectionIds = OwnerCourseCollectionIds(ownerId),
                Courses = coursesRepository.All().Select(x => new CourseInListCatalogModel
                {
                    Id = x.Id,
                    AuthorFullName = x.AuthorFullName,
                    CategoryName = x.Category.Name,
                    CompanyName = x.CompanyName,
                    CompanyLogoUrl = x.CompanyLogoUrl,
                    ImageUrl = x.ImageUrl,
                    LanguageName = x.Language.Name,
                    PricePerPerson = x.Price,
                }).ToList()
            };

            return null;
        }

        private List<int> OwnerCourseCollectionIds(string ownerId)
        {
            return coursesOwnerRepository.All().Where(x => x.OwnerId == ownerId)
                .Select(x => x.CourseId)
                .ToList();
        }
    }
}
