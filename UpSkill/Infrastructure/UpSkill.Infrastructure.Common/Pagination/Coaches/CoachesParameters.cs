namespace UpSkill.Infrastructure.Common.Pagination.Coaches
{
   public class CoachesParameters
    {
        private int _pageSize = 15;
        public int StartIndex { get; set; }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
    }
}
