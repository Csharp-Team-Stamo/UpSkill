namespace UpSkill.Infrastructure.Models.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MonthlyClient
    {
        public int Id { get; set; }
        public string MonthName { get; set; }
        public int ClientsNum { get; set; }
    }
}
