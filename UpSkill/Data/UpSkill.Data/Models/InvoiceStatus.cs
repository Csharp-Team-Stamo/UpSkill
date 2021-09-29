namespace UpSkill.Data.Models
{
	using UpSkill.Data.Common.Models;

	public class InvoiceStatus : BaseModel<int>
	{
		public string Name { get; init; }
	}
}
