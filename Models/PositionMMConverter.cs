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

                switch (targetCase)
                {
                    case "mm":
                        return ((Int64)value / 10000000).ToString();
                    case "mkm":
                        return (((Int64)value % 10000000) / 10000).ToString();
                    case "nm":
                        return ((((Int64)value % 10000000) % 10000) / 10).ToString();
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
