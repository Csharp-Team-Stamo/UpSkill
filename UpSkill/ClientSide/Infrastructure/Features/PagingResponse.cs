
namespace UpSkill.ClientSide.Infrastructure.Features
{
    using System.Collections.Generic;
    using UpSkill.Infrastructure.Common.Pagination;

    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }
    }
}
