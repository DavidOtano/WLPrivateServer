using System;

namespace WLPrivateServer.Items.Implementation
{
	public abstract class Item : IItem
	{
		public abstract int Id { get; }

		public abstract int CellWidth { get; }

		public abstract int CellHeight { get; }

		public abstract int Rank { get; }

		public abstract int InventoryWidth { get; }

		public abstract int InventoryHeight { get; }

		public abstract bool Tradeable { get; }

		public abstract bool Discardable { get; }

		public abstract bool Stackable { get; }

		public Gender GenderLimit => throw new NotImplementedException();
	}
}