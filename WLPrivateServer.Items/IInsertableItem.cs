namespace WLPrivateServer.Items
{
	public interface IInsertableItem : IItem
	{
		int Increase { get; }
	}
}