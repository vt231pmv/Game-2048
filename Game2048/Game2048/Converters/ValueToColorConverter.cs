using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;


namespace Game2048.Converters
{
    public class ValueToColorConverter : IValueConverter
    {
        private static readonly SolidColorBrush tileEmptyBrush = CreateBrush(TileColorStorage.TitleEmpty);

        private readonly Dictionary<string, Brush> tileBrushes = new()
        {
            { "2", CreateBrush(TileColorStorage.Title2) },
            { "4", CreateBrush(TileColorStorage.Title4) },
            { "8", CreateBrush(TileColorStorage.Title8) },
            { "16", CreateBrush(TileColorStorage.Title16) },
            { "32", CreateBrush(TileColorStorage.Title32) },
            { "64", CreateBrush(TileColorStorage.Title64) },
            { "128", CreateBrush(TileColorStorage.Title128) },
            { "256", CreateBrush(TileColorStorage.Title256) },
            { "512", CreateBrush(TileColorStorage.Title512) },
            { "1024", CreateBrush(TileColorStorage.Title1024) },
            { "2048", CreateBrush(TileColorStorage.Title2048) }
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (tileBrushes.TryGetValue(value as string, out Brush brush))
                return brush;
            else
                return tileEmptyBrush;    
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private static SolidColorBrush CreateBrush(CustomColor colors)
        {
            return new SolidColorBrush(Color.FromArgb(255, colors.Red, colors.Green, colors.Blue));
        }
    }
}
