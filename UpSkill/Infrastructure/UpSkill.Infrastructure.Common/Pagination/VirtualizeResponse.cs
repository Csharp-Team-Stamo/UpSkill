namespace UpSkill.Infrastructure.Common.Pagination
{
    using System.Collections.Generic;

    public class VirtualizeResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalSize { get; set; }
    }
}
