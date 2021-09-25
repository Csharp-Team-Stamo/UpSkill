using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpSkill.Infrastructure.Data.Models
{
    public class Course
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string CoachId { get; set; }
        public Coach Coach { get; set; }

        public IQueryable<Employee> Students { get; set; }
    }
}
