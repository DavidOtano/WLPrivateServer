namespace WLPrivateServer.Items
{
	public interface IForgeableItem : IEquippableItem
	{
		int Forges { get; }
	}
}