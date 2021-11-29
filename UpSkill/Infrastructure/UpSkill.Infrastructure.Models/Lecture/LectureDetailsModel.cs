using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSkill.Infrastructure.Models.Lecture
{
    public class LectureDetailsModel
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        //public Course Course { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public ICollection<string> Resources { get; set; } = new List<string>();
    }
}
