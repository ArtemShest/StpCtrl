using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StpCtrl.Models
{
    public class PositionMMConverter : MarkupExtension, IValueConverter
    {
        public static readonly PositionMMConverter Instance = new();
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Int64 curPosition && parameter is string targetCase 
                && targetType.IsAssignableTo(typeof(string)))
            {
                Int64 mm, mkm, nm;
                switch (targetCase)
                {
                    case "mm":
                        mm = ((Int64)value / 10000000);
                        return mm;
                    case "mkm":
                        mkm = (((Int64)value % 10000000) / 10000);
                        return mkm.ToString();
                    case "nm":
                        nm = ((((Int64)value % 10000000) % 10000) / 10);
                        return nm.ToString();
                    case "fullmm":
                        string mmString = ((Int64)value / 10000000).ToString() + '.';
                        mkm = Math.Abs(((Int64)value % 10000000) / 10000);
                        if (mkm < 100) mmString += "0";
                        if (mkm < 10) mmString += "0";
                        mmString += mkm.ToString();
                        nm = Math.Abs((((Int64)value % 10000000) % 10000) / 10);
                        if (nm < 100) mmString += "0";
                        if (nm < 10) mmString += "0";
                        mmString += nm.ToString();
                        return mmString;
                }
            }
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;

    }

}
