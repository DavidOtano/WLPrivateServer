using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WLPrivateServer.DataAccessLayer.Models
{
	public class Character
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }

		public string NickName { get; set; }

		public int CurrentHP { get; set; }

		public int CurrentSP { get; set; }

		public int STR { get; set; }

		public int CON { get; set; }

		public int INT { get; set; }

		public int WIS { get; set; }

		public int AGI { get; set; }

		public int Gold { get; set; }

		public int Level { get; set; }

		public bool Rebirth { get; set; }
	}
}