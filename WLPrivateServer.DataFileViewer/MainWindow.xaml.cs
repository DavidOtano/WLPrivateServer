using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WLPrivateServer.Items;
using WLPrivateServer.Items.Data;

namespace WLPrivateServer.DataFileViewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			ItemDataFile.LoadItems("C:\\Program Files (x86)\\Wonderland Online\\data\\item.dat");
		}

		private bool IsOnlyDigits(string text)
		{
			var digits = "0123456789";
			int count = 0;
			foreach (var ch in text)
				if (digits.Contains(ch))
					count++;

			return text.Length > 0 ? text.Length == count : false;
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			var search = searchText.Text;
			var searchId = IsOnlyDigits(search) ? int.Parse(search) : -1;

			switch (search)
			{
				case "b1":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit1) == (ushort)ItemRestrictions.Bit1);
					break;

				case "b2":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Untransferable) == (ushort)ItemRestrictions.Untransferable);
					break;

				case "b3":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Uncombinable) == (ushort)ItemRestrictions.Uncombinable);
					break;

				case "b4":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit4) == (ushort)ItemRestrictions.Bit4);
					break;

				case "b5":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.CannotBeSoldNPC) == (ushort)ItemRestrictions.CannotBeSoldNPC);
					break;

				case "b6":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.UnstorablePropsKeeper) == (ushort)ItemRestrictions.UnstorablePropsKeeper);
					break;

				case "b7":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit7) == (ushort)ItemRestrictions.Bit7);
					break;

				case "b8":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Undiscardable) == (ushort)ItemRestrictions.Undiscardable);
					break;

				case "b9":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit9) == (ushort)ItemRestrictions.Bit9);
					break;

				case "b10":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit10) == (ushort)ItemRestrictions.Bit10);
					break;

				case "b11":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit11) == (ushort)ItemRestrictions.Bit11);
					break;

				case "b12":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit12) == (ushort)ItemRestrictions.Bit12);
					break;

				case "b13":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit13) == (ushort)ItemRestrictions.Bit13);
					break;

				case "b14":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit14) == (ushort)ItemRestrictions.Bit14);
					break;

				case "b15":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit15) == (ushort)ItemRestrictions.Bit15);
					break;

				case "b16":
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => (x.Control & (ushort)ItemRestrictions.Bit16) == (ushort)ItemRestrictions.Bit16);
					break;

				default:
					itemList.ItemsSource = ItemDataFile.ItemList.Where(x => x.ItemName.Contains(search) || x.ItemDescription.Contains(search) || x.ItemID == searchId);
					break;
			}
		}
	}
}