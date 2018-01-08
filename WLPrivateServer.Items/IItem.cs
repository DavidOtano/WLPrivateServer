namespace WLPrivateServer.Items
{
	public interface IItem
	{
		int Id { get; }

		int CellWidth { get; }

		int CellHeight { get; }

		int Rank { get; }

		Gender GenderLimit { get; }

		int InventoryWidth { get; }

		int InventoryHeight { get; }

		bool Tradeable { get; }

		bool Discardable { get; }

		bool Stackable { get; }
	}
}