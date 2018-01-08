using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using WLPrivateServer.Items;
using WLPrivateServer.Items.Data;

namespace WLPrivateServer.DataFileViewer.Converters
{
	public class ObjectToTextValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return string.Empty;

			var properties = value.GetType().GetProperties().Where(x => x.PropertyType.IsPublic);

			var text = new StringBuilder();

			foreach (var prop in properties)
			{
				if (prop.PropertyType.IsArray)
				{
					text.AppendFormat("{0}: ", prop.Name);

					var enumerable = prop.GetValue(value) as IEnumerable;

					foreach (var obj in enumerable)
					{
						text.AppendFormat("{0}, ", obj.ToString());
					}

					text.Length -= 2;

					text.Append(Environment.NewLine);
				}
				else text.AppendFormat("{0}: {1}{2}", prop.Name, prop.GetValue(value), Environment.NewLine);

				if (prop.Name == "Control")
				{
					var val = (ushort)prop.GetValue(value);

					text.AppendFormat("{0}{0}{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Bit1) == (ushort)ItemRestrictions.Bit1)
						text.AppendFormat("Untradeable.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Untransferable) == (ushort)ItemRestrictions.Untransferable)
						text.AppendFormat("Non Recyclable.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Uncombinable) == (ushort)ItemRestrictions.Uncombinable)
						text.AppendFormat("Non Compoundable.{0}", Environment.NewLine);

					if ((value as ItemWrapper).ItemType > 17)
					{
						if ((val & (ushort)ItemRestrictions.Bit4) == (ushort)ItemRestrictions.Bit4)
							text.AppendFormat("Stackable.{0}", Environment.NewLine);
					}

					if ((val & (ushort)ItemRestrictions.CannotBeSoldNPC) == (ushort)ItemRestrictions.CannotBeSoldNPC)
						text.AppendFormat("Unsellable.{0}", Environment.NewLine);

					if ((value as ItemWrapper).ItemType > 17)
					{
						if ((val & (ushort)ItemRestrictions.UnstorablePropsKeeper) == (ushort)ItemRestrictions.UnstorablePropsKeeper)
							text.AppendFormat("Visible Map Item.{0}", Environment.NewLine);
					}
					else
					{
						if ((val & (ushort)ItemRestrictions.UnstorablePropsKeeper) == (ushort)ItemRestrictions.UnstorablePropsKeeper)
							text.AppendFormat("Pet Item.{0}", Environment.NewLine);
					}

					if ((val & (ushort)ItemRestrictions.Bit7) == (ushort)ItemRestrictions.Bit7)
						text.AppendFormat("Quest/Event Item.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Undiscardable) == (ushort)ItemRestrictions.Undiscardable)
						text.AppendFormat("Undiscardable.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Bit9) == (ushort)ItemRestrictions.Bit9)
						text.AppendFormat("Click to Use.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Bit10) == (ushort)ItemRestrictions.Bit10)
						text.AppendFormat("Unforgeable.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Bit11) == (ushort)ItemRestrictions.Bit11)
						text.AppendFormat("Obtained In Event.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Bit12) == (ushort)ItemRestrictions.Bit12)
						text.AppendFormat("Bit12 = 1.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Bit13) == (ushort)ItemRestrictions.Bit13)
						text.AppendFormat("Bit13 = 1.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Bit14) == (ushort)ItemRestrictions.Bit14)
						text.AppendFormat("Bit14 = 1.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Bit15) == (ushort)ItemRestrictions.Bit15)
						text.AppendFormat("Bit15 = 1.{0}", Environment.NewLine);

					if ((val & (ushort)ItemRestrictions.Bit16) == (ushort)ItemRestrictions.Bit16)
						text.AppendFormat("Bit16 = 1.{0}", Environment.NewLine);

					text.AppendFormat("{0}{0}{0}", Environment.NewLine);
					text.AppendFormat("{0} Bits: {1}{2}", prop.Name, System.Convert.ToString((UInt16)prop.GetValue(value), 2), Environment.NewLine);
				}
			}

			return text.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}