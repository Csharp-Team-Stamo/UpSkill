namespace UpSkill.Data.Models
{
	using UpSkill.Data.Common.Models;

	public class Administrator : BaseModel<string>
	{
		public string UserId { get; init; }
		public ApplicationUser User { get; init; }
	}
}
