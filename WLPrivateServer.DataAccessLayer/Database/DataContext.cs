using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using WLPrivateServer.DataAccessLayer.Models;

namespace WLPrivateServer.DataAccessLayer
{
	public class DataContext : DbContext
	{
		public DataContext()
			: base("DefaultConnection")
		{
		}

		public DbSet<User> Users { get; set; }

		#region Static Implementation

		private static ThreadLocal<DataContext> db = new ThreadLocal<DataContext>(() => new DataContext());
		private static DataContext Db => db.Value;

		public static User GetUser(string username, string password)
		{
			var md5pass = string.Empty;

			using (var digest = MD5.Create())
				md5pass = string.Join("", digest.ComputeHash(Encoding.ASCII.GetBytes(password)).Select(x => x.ToString("X2")));

			return Db.Users.FirstOrDefault(x => x.Username == username && x.Password == md5pass);
		}

		public static User GetUser(int id)
		{
			return Db.Users.FirstOrDefault(x => x.Id == id);
		}

		public static void Save()
		{
			Db.SaveChanges();
		}

		#endregion Static Implementation
	}
}