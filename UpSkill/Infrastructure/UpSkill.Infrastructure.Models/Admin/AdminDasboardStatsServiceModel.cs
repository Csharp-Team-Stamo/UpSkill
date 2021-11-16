namespace UpSkill.Infrastructure.Models.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AdminDasboardStatsServiceModel
    {
        public int ClientsNum { get; set; }
        public decimal Revenue { get; set; }
        public int CoursesNum { get; set; }
        public int CoachesNum { get; set; }
    }
}
