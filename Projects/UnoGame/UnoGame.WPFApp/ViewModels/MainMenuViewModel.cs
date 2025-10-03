using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoGame.WPFApp.Commands;

namespace UnoGame.WPFApp.ViewModels
{
    internal class MainMenuViewModel : BaseViewModel
    {
        private readonly MainWindow _mainWindow;
        public ICommand StartGameCommand { get; }
        public ICommand ExitGameCommand { get; }

        public MainMenuViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            StartGameCommand = new RelayCommand(_ => NavigateToPlayerCount());
            ExitGameCommand = new RelayCommand(_ => App.Current.Shutdown());
        }

        private void NavigateToPlayerCount()
        {
            _mainWindow.NavigateToPlayerCount();
        }
    }
}
