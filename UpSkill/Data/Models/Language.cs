namespace UpSkill.Data.Models
{
    using Common.Models;

    public class Language : BaseDeletableModel<int>
    {
        public string Name { get; init; }
    }
}
