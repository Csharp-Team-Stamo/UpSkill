﻿namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.Course;
    using Microsoft.EntityFrameworkCore;
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

       public async Task<CourseDescriptionModel> GetByIdAsync(int courseId)
        {
            var course = await coursesRepository
                .All()
                .Select(x => new CourseDescriptionModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    AuthorFullName = x.AuthorFullName,
                    Company = x.CompanyName,
                    CreatorAvatarImgUrl = x.CreatorImageUrl,
                    CourseDescription = x.Description,
                    VideoUrl = x.VideoUrl,
                    SkillsLearn = x.SkillsLearn,
                    CourseDurationInHours = x.CourseDurationInHours,
                    LecturesCount = x.LecturesCount,
                })
                .FirstOrDefaultAsync(x => x.Id == courseId);

            return course;
        }

        public CoursesListingCatalogModel GetAllByOwnerId(string ownerId)
        {
            return new CoursesListingCatalogModel
            {
                OwnerId = ownerId,
                OwnerCourseCollectionIds = OwnerCourseCollectionIds(ownerId),
                Courses = coursesOwnerRepository
                .All()
                .Where(x => x.OwnerId == ownerId)
                .Select(x => new CourseInListCatalogModel
                {
                    Id = x.Course.Id,
                    AuthorFullName = x.Course.AuthorFullName,
                    Name = x.Course.Name,
                    CategoryName = x.Course.Category.Name,
                    CompanyLogoUrl = x.Course.CompanyLogoUrl,
                    ImageUrl = x.Course.ImageUrl,
                    PricePerPerson = x.Course.Price,
                }).ToList()
            };
        }

        public CoursesListingCatalogModel GetAll(string ownerId)
        {
            return new CoursesListingCatalogModel
            {
                OwnerId = ownerId,
                OwnerCourseCollectionIds = OwnerCourseCollectionIds(ownerId),
                Courses = coursesRepository.All().Select(x => new CourseInListCatalogModel
                {
                    Id = x.Id,
                    AuthorFullName = x.AuthorFullName,
                    Name = x.Name,
                    CategoryName = x.Category.Name,
                    CompanyLogoUrl = x.CompanyLogoUrl,
                    ImageUrl = x.ImageUrl,
                    LanguageName = x.Language.Name,
                    PricePerPerson = x.Price,
                }).ToList()
            };
        }

        public async Task AddCourseInOwnerCoursesCollectionAsync(int courseId, string ownerId)
        {
            var courseOwner = new CourseOwner { CourseId = courseId, OwnerId = ownerId };
            await coursesOwnerRepository.AddAsync(courseOwner);
            await coursesOwnerRepository.SaveChangesAsync();
        }

        public async Task RemoveCourseFromOwnerCoursesCollectionAsync(int courseId, string ownerId)
        {
            var courseTooRemove = await coursesOwnerRepository.All()
                .FirstOrDefaultAsync(x => x.CourseId == courseId && x.OwnerId == ownerId);
            coursesOwnerRepository.HardDelete(courseTooRemove);
            await coursesOwnerRepository.SaveChangesAsync();
        }

        private List<int> OwnerCourseCollectionIds(string ownerId)
        {
            return coursesOwnerRepository.All().Where(x => x.OwnerId == ownerId)
                .Select(x => x.CourseId)
                .ToList();
        }
    }
}
