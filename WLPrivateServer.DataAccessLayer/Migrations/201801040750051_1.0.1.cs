namespace WLPrivateServer.DataAccessLayer.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class _101 : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Characters", "Level", c => c.Int(nullable: false));
			AddColumn("dbo.Characters", "Rebirth", c => c.Boolean(nullable: false));
		}

		public override void Down()
		{
			DropColumn("dbo.Characters", "Rebirth");
			DropColumn("dbo.Characters", "Level");
		}
	}
}