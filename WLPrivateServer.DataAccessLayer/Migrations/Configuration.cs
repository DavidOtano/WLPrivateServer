namespace WLPrivateServer.DataAccessLayer.Migrations
{
	using System.Collections.Generic;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;
	using WLPrivateServer.DataAccessLayer.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<WLPrivateServer.DataAccessLayer.DataContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(WLPrivateServer.DataAccessLayer.DataContext context)
		{
			User user = null;

			if (!context.Users.Any(x => x.Username == "johndoe"))
			{
				user = new User()
				{
					Username = "johndoe",
					Password = string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("password")).Select(x => x.ToString("X2"))),
				};

				context.Users.Add(user);
			}
			else
				user = context.Users.FirstOrDefault(x => x.Username == "johndoe");

			user.Characters = new List<Character>(new Character[]
			{
				new Character
				{
					Name = "John",
					NickName = "Doe",
					Level = 1,
					Rebirth = false,
					STR = 1,
					CON = 1,
					INT = 1,
					WIS = 1,
					AGI = 1,
					Gold = 1000000,
					CurrentHP = 10,
					CurrentSP = 10
				}
			});
		}
	}
}