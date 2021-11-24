namespace UpSkill.Infrastructure.Models.Lecture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LectureCreateServiceModel
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }
    }
}
