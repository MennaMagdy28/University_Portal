namespace HUP.Core.Entities.Shared
{
	public abstract class BaseEntity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
	}
}