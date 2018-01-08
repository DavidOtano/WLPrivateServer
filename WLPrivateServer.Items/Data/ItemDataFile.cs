using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WLPrivateServer.Items.Data
{
	public class ItemWrapper
	{
		private ItemInfo info;

		public ItemWrapper(ItemInfo info)
		{
			this.info = info;
		}

		private string itemName;
		private string itemDescription;

		public byte ItemNameLength => info.ItemNameLength;

		public string ItemName
		{
			get
			{
				if (itemName == null)
					itemName = Encoding.ASCII.GetString(info.ItemName.Take(ItemNameLength).ToArray());

				return itemName;
			}
		}

		public byte[] OriginalItemName => info.ItemName;

		public byte ItemDescriptionLength => info.ItemDescriptionLength;

		public string ItemDescription
		{
			get
			{
				if (itemDescription == null)
					itemDescription = Encoding.ASCII.GetString(info.ItemDescription.Take(ItemDescriptionLength).ToArray());

				return itemDescription;
			}
		}

		public byte[] OriginalItemDescription => info.ItemDescription;

		public byte ItemType
		{
			get
			{
				return info.ItemType;
			}
			set
			{
				info.ItemType = value;
			}
		}

		public UInt16 ItemID
		{
			get
			{
				return info.ItemID;
			}
			set
			{
				info.ItemID = value;
			}
		}

		public UInt16 IconNum
		{
			get
			{
				return info.IconNum;
			}
			set
			{
				info.IconNum = value;
			}
		}

		public UInt16 LargeIconNum
		{
			get
			{
				return info.LargeIconNum;
			}
			set
			{
				info.LargeIconNum = value;
			}
		}

		public UInt16[] EquipImageNum
		{
			get
			{
				return info.EquipImageNum;
			}
			set
			{
				info.EquipImageNum = value;
			}
		}

		public UInt16[] StatusType
		{
			get
			{
				return info.StatusType;
			}
			set
			{
				info.StatusType = value;
			}
		}

		public byte[] Bytes1 => info.Bytes1;
		public UInt32[] StatusUp => info.StatusUp;
		public byte Byte2 => info.Byte2;
		public byte Grade => info.Grade;
		public byte EquipPos => info.EquipPos;
		public byte Byte3 => info.Byte3;
		public UInt32[] ColorDef => info.ColorDef;
		public byte Unused => info.Unused;
		public byte Level => info.Level;
		public UInt32 BuyingPrice => info.BuyingPrice;
		public UInt32 SellingPrice => info.SellingPrice;
		public byte EquipLimit => info.EquipLimit;

		public UInt16 Control
		{
			get
			{
				return info.Control;
			}
			set
			{
				info.Control = value;
			}
		}

		public UInt32 Long1 => info.Long1;
		public byte SetID => info.SetID;
		public UInt32 AntiSeal => info.AntiSeal;
		public UInt16 SkillID => info.SkillID;
		public UInt16[] MaterialTypes => info.MaterialTypes;
		public byte[] InTentSize => info.InTentSize;
		public UInt16 Word3 => info.Word3;
		public byte CellWidth => info.CellWidth;
		public byte CellHeight => info.CellHeight;
		public byte Byte7 => info.Byte7;
		public UInt16[] InTentImages => info.InTentImages;
		public UInt16 NpcID => info.NpcID;
		public byte[] Bytes8 => info.Bytes8;
		public UInt16 Duration => info.Duration;
		public UInt16[] Words5 => info.Words5;
		public UInt32[] Longs3 => info.Longs3;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
	public struct ItemInfo
	{
		public byte ItemNameLength;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
		public byte[] ItemName;

		public byte ItemType;
		public UInt16 ItemID;
		public UInt16 IconNum;
		public UInt16 LargeIconNum;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public UInt16[] EquipImageNum;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public UInt16[] StatusType;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] Bytes1;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public UInt32[] StatusUp;

		public byte Byte2;
		public byte Grade;
		public byte EquipPos;
		public byte Byte3;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public UInt32[] ColorDef;

		public byte Unused;
		public byte Level;
		public UInt32 BuyingPrice;
		public UInt32 SellingPrice;
		public byte EquipLimit;
		public UInt16 Control;
		public UInt32 Long1;
		public byte SetID;
		public UInt32 AntiSeal;
		public UInt16 SkillID;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
		public UInt16[] MaterialTypes;

		public byte ItemDescriptionLength;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 254)]
		public byte[] ItemDescription;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] InTentSize;

		public UInt16 Word3;
		public byte CellWidth;
		public byte CellHeight;
		public byte Byte7;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public UInt16[] InTentImages;

		public UInt16 NpcID;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte[] Bytes8;

		public UInt16 Duration;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public UInt16[] Words5;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
		public UInt32[] Longs3;
	}

	public class ItemNotFoundException : Exception
	{
		public ItemNotFoundException()
		{
		}

		public ItemNotFoundException(string message)
			: base(message)
		{
		}

		public ItemNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}

	public static class ItemDataFile
	{
		public static ObservableCollection<ItemWrapper> ItemList { get; private set; }
		public static readonly object SyncRoot;

		static ItemDataFile()
		{
			ItemList = new ObservableCollection<ItemWrapper>();
			SyncRoot = new object();
		}

		private static void DecodeItem32(ref UInt32 val)
		{
			val = Convert.ToUInt32((val ^ 0x0B80F4B4) - 9);
		}

		private static UInt32 EncodeItem32(UInt32 val)
		{
			return Convert.ToUInt32((val + 9) ^ 0x0B80F4B4);
		}

		private static void DecodeItem16(ref UInt16 val)
		{
			val = Convert.ToUInt16((val ^ 0xEFC3) - 9);
		}

		private static UInt16 EncodeItem16(UInt16 val)
		{
			return Convert.ToUInt16((val + 9) ^ 0xEFC3);
		}

		private static void DecodeItem8(ref byte val)
		{
			val = Convert.ToByte((val ^ 0x9A) - 9);
		}

		private static byte EncodeItem8(byte val)
		{
			return Convert.ToByte((val + 9) ^ 0x9A);
		}

		private static bool bit_set(UInt16 value, UInt16 index)
		{
			return (value & (1 << index)) != 0;
		}

		private static T ReadFromItems<T>(Stream fs)
		{
			byte[] buffer = new byte[Marshal.SizeOf(typeof(T))];
			fs.Read(buffer, 0, Marshal.SizeOf(typeof(T)));

			GCHandle Handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
			T RetVal = (T)Marshal.PtrToStructure(Handle.AddrOfPinnedObject(), typeof(T));
			Handle.Free();

			return RetVal;
		}

		public static ItemWrapper GetItemByID(UInt16 itemId)
		{
			lock (SyncRoot)
			{
				return ItemList.FirstOrDefault(x => x.ItemID == itemId) ?? throw (new ItemNotFoundException("Item => " + itemId.ToString() + " could not be found."));
			}
		}

		public static ItemWrapper GetItemByName(string name)
		{
			lock (SyncRoot)
			{
				return ItemList.FirstOrDefault(x => x.ItemName == name) ?? throw (new ItemNotFoundException("Item => \"" + name + "\" could not be found."));
			}
		}

		public static bool LoadItems(string path)
		{
			try
			{
				using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					long FileLen = fs.Length;

					if (ItemList.Count > 0) { lock (SyncRoot) { ItemList.Clear(); } }

					while (FileLen >= Marshal.SizeOf(typeof(ItemInfo)))
					{
						ItemInfo info = ReadFromItems<ItemInfo>(fs);

						DecodeItem8(ref info.ItemType);
						DecodeItem16(ref info.ItemID);
						DecodeItem16(ref info.IconNum);
						DecodeItem16(ref info.LargeIconNum);
						DecodeItem16(ref info.EquipImageNum[0]);
						DecodeItem16(ref info.EquipImageNum[1]);
						DecodeItem16(ref info.EquipImageNum[2]);
						DecodeItem16(ref info.EquipImageNum[3]);
						DecodeItem16(ref info.StatusType[0]);
						DecodeItem16(ref info.StatusType[1]);
						DecodeItem8(ref info.Bytes1[0]);
						DecodeItem8(ref info.Bytes1[1]);
						DecodeItem32(ref info.StatusUp[0]);
						DecodeItem32(ref info.StatusUp[1]);
						DecodeItem8(ref info.Byte2);
						DecodeItem8(ref info.Grade);
						DecodeItem8(ref info.EquipPos);
						DecodeItem8(ref info.Byte3);
						DecodeItem32(ref info.ColorDef[0]);
						DecodeItem32(ref info.ColorDef[1]);
						DecodeItem32(ref info.ColorDef[2]);
						DecodeItem32(ref info.ColorDef[3]);
						DecodeItem32(ref info.ColorDef[4]);
						DecodeItem32(ref info.ColorDef[5]);
						DecodeItem32(ref info.ColorDef[6]);
						DecodeItem32(ref info.ColorDef[7]);
						DecodeItem32(ref info.ColorDef[8]);
						DecodeItem32(ref info.ColorDef[9]);
						DecodeItem32(ref info.ColorDef[10]);
						DecodeItem32(ref info.ColorDef[11]);
						DecodeItem32(ref info.ColorDef[12]);
						DecodeItem32(ref info.ColorDef[13]);
						DecodeItem32(ref info.ColorDef[14]);
						DecodeItem32(ref info.ColorDef[15]);
						DecodeItem8(ref info.Unused);
						DecodeItem8(ref info.Level);
						DecodeItem32(ref info.BuyingPrice);
						DecodeItem32(ref info.SellingPrice);
						DecodeItem8(ref info.EquipLimit);
						DecodeItem16(ref info.Control);
						DecodeItem32(ref info.Long1);
						DecodeItem8(ref info.SetID);
						DecodeItem32(ref info.AntiSeal);
						DecodeItem16(ref info.SkillID);
						DecodeItem16(ref info.MaterialTypes[0]);
						DecodeItem16(ref info.MaterialTypes[1]);
						DecodeItem16(ref info.MaterialTypes[2]);
						DecodeItem16(ref info.MaterialTypes[3]);
						DecodeItem16(ref info.MaterialTypes[4]);
						DecodeItem8(ref info.InTentSize[0]);
						DecodeItem8(ref info.InTentSize[1]);
						DecodeItem8(ref info.InTentSize[2]);
						DecodeItem16(ref info.Word3);
						DecodeItem8(ref info.CellWidth);
						DecodeItem8(ref info.CellHeight);
						DecodeItem8(ref info.Byte7);
						DecodeItem16(ref info.InTentImages[0]);
						DecodeItem16(ref info.InTentImages[1]);
						DecodeItem16(ref info.NpcID);
						DecodeItem8(ref info.Bytes8[0]);
						DecodeItem8(ref info.Bytes8[1]);
						DecodeItem8(ref info.Bytes8[2]);
						DecodeItem8(ref info.Bytes8[3]);
						DecodeItem8(ref info.Bytes8[4]);
						DecodeItem8(ref info.Bytes8[5]);
						DecodeItem16(ref info.Duration);
						DecodeItem16(ref info.Words5[0]);
						DecodeItem16(ref info.Words5[1]);
						DecodeItem16(ref info.Words5[2]);
						DecodeItem16(ref info.Words5[3]);
						DecodeItem32(ref info.Longs3[0]);
						DecodeItem32(ref info.Longs3[1]);
						DecodeItem32(ref info.Longs3[2]);
						DecodeItem32(ref info.Longs3[3]);
						DecodeItem32(ref info.Longs3[4]);

						info.ItemName = info.ItemName.Reverse().ToArray();
						info.ItemDescription = info.ItemDescription.Reverse().ToArray();

						lock (SyncRoot)
						{
							ItemList.Add(new ItemWrapper(info));
						}

						FileLen -= Marshal.SizeOf(typeof(ItemInfo));
					}

					fs.Close();
					fs.Dispose();
				}
				return true;
			}
			catch (Exception) { return false; }
		}

		public static void WriteDataFile()
		{
			using (var fs = new FileStream("C:\\Program Files (x86)\\Wonderland Online\\data\\item.dat", FileMode.OpenOrCreate, FileAccess.Write))
			using (var writer = new BinaryWriter(fs))
			{
				foreach (var info in ItemList)
				{
					writer.Write(info.ItemNameLength);
					writer.Write(info.OriginalItemName.Reverse().ToArray());
					writer.Write(EncodeItem8(info.ItemType));
					writer.Write(EncodeItem16(info.ItemID));
					writer.Write(EncodeItem16(info.IconNum));
					writer.Write(EncodeItem16(info.LargeIconNum));
					writer.Write(EncodeItem16(info.EquipImageNum[0]));
					writer.Write(EncodeItem16(info.EquipImageNum[1]));
					writer.Write(EncodeItem16(info.EquipImageNum[2]));
					writer.Write(EncodeItem16(info.EquipImageNum[3]));
					writer.Write(EncodeItem16(info.StatusType[0]));
					writer.Write(EncodeItem16(info.StatusType[1]));
					writer.Write(EncodeItem8(info.Bytes1[0]));
					writer.Write(EncodeItem8(info.Bytes1[1]));
					writer.Write(EncodeItem32(info.StatusUp[0]));
					writer.Write(EncodeItem32(info.StatusUp[1]));
					writer.Write(EncodeItem8(info.Byte2));
					writer.Write(EncodeItem8(info.Grade));
					writer.Write(EncodeItem8(info.EquipPos));
					writer.Write(EncodeItem8(info.Byte3));
					writer.Write(EncodeItem32(info.ColorDef[0]));
					writer.Write(EncodeItem32(info.ColorDef[1]));
					writer.Write(EncodeItem32(info.ColorDef[2]));
					writer.Write(EncodeItem32(info.ColorDef[3]));
					writer.Write(EncodeItem32(info.ColorDef[4]));
					writer.Write(EncodeItem32(info.ColorDef[5]));
					writer.Write(EncodeItem32(info.ColorDef[6]));
					writer.Write(EncodeItem32(info.ColorDef[7]));
					writer.Write(EncodeItem32(info.ColorDef[8]));
					writer.Write(EncodeItem32(info.ColorDef[9]));
					writer.Write(EncodeItem32(info.ColorDef[10]));
					writer.Write(EncodeItem32(info.ColorDef[11]));
					writer.Write(EncodeItem32(info.ColorDef[12]));
					writer.Write(EncodeItem32(info.ColorDef[13]));
					writer.Write(EncodeItem32(info.ColorDef[14]));
					writer.Write(EncodeItem32(info.ColorDef[15]));
					writer.Write(EncodeItem8(info.Unused));
					writer.Write(EncodeItem8(info.Level));
					writer.Write(EncodeItem32(info.BuyingPrice));
					writer.Write(EncodeItem32(info.SellingPrice));
					writer.Write(EncodeItem8(info.EquipLimit));
					writer.Write(EncodeItem16(info.Control));
					writer.Write(EncodeItem32(info.Long1));
					writer.Write(EncodeItem8(info.SetID));
					writer.Write(EncodeItem32(info.AntiSeal));
					writer.Write(EncodeItem16(info.SkillID));
					writer.Write(EncodeItem16(info.MaterialTypes[0]));
					writer.Write(EncodeItem16(info.MaterialTypes[1]));
					writer.Write(EncodeItem16(info.MaterialTypes[2]));
					writer.Write(EncodeItem16(info.MaterialTypes[3]));
					writer.Write(EncodeItem16(info.MaterialTypes[4]));
					writer.Write(info.ItemDescriptionLength);
					writer.Write(info.OriginalItemDescription.Reverse().ToArray());
					writer.Write(EncodeItem8(info.InTentSize[0]));
					writer.Write(EncodeItem8(info.InTentSize[1]));
					writer.Write(EncodeItem8(info.InTentSize[2]));
					writer.Write(EncodeItem16(info.Word3));
					writer.Write(EncodeItem8(info.CellWidth));
					writer.Write(EncodeItem8(info.CellHeight));
					writer.Write(EncodeItem8(info.Byte7));
					writer.Write(EncodeItem16(info.InTentImages[0]));
					writer.Write(EncodeItem16(info.InTentImages[1]));
					writer.Write(EncodeItem16(info.NpcID));
					writer.Write(EncodeItem8(info.Bytes8[0]));
					writer.Write(EncodeItem8(info.Bytes8[1]));
					writer.Write(EncodeItem8(info.Bytes8[2]));
					writer.Write(EncodeItem8(info.Bytes8[3]));
					writer.Write(EncodeItem8(info.Bytes8[4]));
					writer.Write(EncodeItem8(info.Bytes8[5]));
					writer.Write(EncodeItem16(info.Duration));
					writer.Write(EncodeItem16(info.Words5[0]));
					writer.Write(EncodeItem16(info.Words5[1]));
					writer.Write(EncodeItem16(info.Words5[2]));
					writer.Write(EncodeItem16(info.Words5[3]));
					writer.Write(EncodeItem32(info.Longs3[0]));
					writer.Write(EncodeItem32(info.Longs3[1]));
					writer.Write(EncodeItem32(info.Longs3[2]));
					writer.Write(EncodeItem32(info.Longs3[3]));
					writer.Write(EncodeItem32(info.Longs3[4]));
				}
			}
		}

		public static void UnloadItems()
		{
			lock (SyncRoot) { ItemList.Clear(); }
		}
	}
}