using System.Collections.Generic;
using WLPrivateServer.Items;

namespace WLPrivateServer.Characters
{
	public class Character
	{
		private readonly object syncRoot = new object();

		private int id;
		private string name;
		private string nickname;
		private int currentHP;
		private int currentSP;
		private int currentEXP;
		private int level;
		private int str;
		private int con;
		private int @int;
		private int wis;
		private int agi;
		private bool rebirth;
		private byte job;
		private int gold;

		private List<IEquippableItem> eq;

		public int Id
		{
			get
			{
				lock (syncRoot)
				{
					return id;
				}
			}
			set
			{
				lock (syncRoot)
				{
					id = value;
				}
			}
		}

		public string Name
		{
			get
			{
				lock (syncRoot)
				{
					return name;
				}
			}
			set
			{
				lock (syncRoot)
				{
					name = value;
				}
			}
		}

		public string NickName
		{
			get
			{
				lock (syncRoot)
				{
					return nickname;
				}
			}
			set
			{
				lock (syncRoot)
				{
					nickname = value;
				}
			}
		}

		public int CurrentHP
		{
			get
			{
				lock (syncRoot)
				{
					return currentHP;
				}
			}
			set
			{
				lock (syncRoot)
				{
					currentHP = value;
				}
			}
		}

		public int CurrentSP
		{
			get
			{
				lock (syncRoot)
				{
					return currentSP;
				}
			}
			set
			{
				lock (syncRoot)
				{
					currentSP = value;
				}
			}
		}

		public int CurrentEXP
		{
			get
			{
				lock (syncRoot)
				{
					return currentEXP;
				}
			}
			set
			{
				lock (syncRoot)
				{
					currentEXP = value;
				}
			}
		}

		public int Level
		{
			get
			{
				lock (syncRoot)
				{
					return level;
				}
			}
			set
			{
				lock (syncRoot)
				{
					level = value;
				}
			}
		}

		public int STR
		{
			get
			{
				lock (syncRoot)
				{
					return str;
				}
			}
			set
			{
				lock (syncRoot)
				{
					str = value;
				}
			}
		}

		public int CON
		{
			get
			{
				lock (syncRoot)
				{
					return con;
				}
			}
			set
			{
				lock (syncRoot)
				{
					con = value;
				}
			}
		}

		public int INT
		{
			get
			{
				lock (syncRoot)
				{
					return @int;
				}
			}
			set
			{
				lock (syncRoot)
				{
					@int = value;
				}
			}
		}

		public int WIS
		{
			get
			{
				lock (syncRoot)
				{
					return wis;
				}
			}
			set
			{
				lock (syncRoot)
				{
					wis = value;
				}
			}
		}

		public int AGI
		{
			get
			{
				lock (syncRoot)
				{
					return agi;
				}
			}
			set
			{
				lock (syncRoot)
				{
					agi = value;
				}
			}
		}

		public bool Rebirth
		{
			get
			{
				lock (syncRoot)
				{
					return rebirth;
				}
			}
			set
			{
				lock (syncRoot)
				{
					rebirth = value;
				}
			}
		}

		public byte Job
		{
			get
			{
				lock (syncRoot)
				{
					return job;
				}
			}
			set
			{
				lock (syncRoot)
				{
					job = value;
				}
			}
		}

		private int Gold
		{
			get
			{
				lock (syncRoot)
				{
					return gold;
				}
			}
			set
			{
				lock (syncRoot)
				{
					gold = value;
				}
			}
		}

		private List<IEquippableItem> EQ
		{
			get
			{
				lock (syncRoot)
				{
					return eq;
				}
			}
			set
			{
				lock (syncRoot)
				{
					eq = value;
				}
			}
		}
	}
}