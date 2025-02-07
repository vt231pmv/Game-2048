using Game2048.Commands;
using Game2048.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.ViewModels
{
    public class MenuViewModel : ViewModel
    {
        public NavigationCommand NavigateToGamePage { get => new(NavigateToPage, new Uri("Views/Pages/GamePage.xaml", UriKind.RelativeOrAbsolute)); }

        public static NavigationCommand NavigateToStatisticsPage { get => new(NavigateToPage, new Uri("Views/Pages/StatisticsPage.xaml", UriKind.RelativeOrAbsolute)); }
        public static RelayCommand QuitApp { get => new(Quit); }
    }
}
