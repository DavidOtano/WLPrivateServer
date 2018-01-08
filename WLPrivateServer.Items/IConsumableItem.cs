namespace WLPrivateServer.Items
{
	public interface IConsumableItem : IItem
	{
		int HP { get; }
		int SP { get; }
	}
}