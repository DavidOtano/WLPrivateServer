using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WLPrivateServer.DataAccessLayer.Models
{
	public class User
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Username { get; set; }

		public string Password { get; set; }

		public virtual List<Character> Characters { get; set; }
	}
}