namespace UpSkill.Infrastructure.Common.Pagination
{
    using System.Collections.Generic;
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
