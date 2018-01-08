namespace WLPrivateServer.DataAccessLayer.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class Initial : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Users",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Username = c.String(),
					Password = c.String(),
				})
				.PrimaryKey(t => t.Id);

			CreateTable(
				"dbo.Characters",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Name = c.String(),
					NickName = c.String(),
					CurrentHP = c.Int(nullable: false),
					CurrentSP = c.Int(nullable: false),
					STR = c.Int(nullable: false),
					CON = c.Int(nullable: false),
					INT = c.Int(nullable: false),
					WIS = c.Int(nullable: false),
					AGI = c.Int(nullable: false),
					Gold = c.Int(nullable: false),
					User_Id = c.Int(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Users", t => t.User_Id)
				.Index(t => t.User_Id);
		}

		public override void Down()
		{
			DropForeignKey("dbo.Characters", "User_Id", "dbo.Users");
			DropIndex("dbo.Characters", new[] { "User_Id" });
			DropTable("dbo.Characters");
			DropTable("dbo.Users");
		}
	}
}