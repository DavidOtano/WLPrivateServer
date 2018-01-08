namespace WLPrivateServer.Items
{
	public interface ITentItem : IItem
	{
		int Width { get; }
		int Height { get; }
		int Depth { get; }
	}
}