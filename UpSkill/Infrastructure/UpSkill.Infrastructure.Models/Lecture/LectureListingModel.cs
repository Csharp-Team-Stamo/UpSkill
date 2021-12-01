namespace UpSkill.Infrastructure.Models.Lecture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LectureListingModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
    }
}
