namespace WLPrivateServer.Items
{
	public interface IEquippableItem : IItem, IDamageableItem
	{
		int Level { get; }
		int HP { get; }
		int SP { get; }
		int ATK { get; }
		int DEF { get; }
		int MAT { get; }
		int MDF { get; }
		int SPD { get; }
		int Crit { get; }
	}
}