using Game2048.Commands;
using Game2048.Data;
using Game2048.Models;
using Game2048.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.ViewModels
{
    public class StatisticsViewModel : ViewModel
    {
        public static NavigationCommand NavigateToMenuPage { get => new(NavigateToPage, new Uri("Views/Pages/MenuPage.xaml", UriKind.RelativeOrAbsolute)); }
        public static ObservableCollection<Player> StatisticsCollection { get => Statistics.Players; }
    }
}
