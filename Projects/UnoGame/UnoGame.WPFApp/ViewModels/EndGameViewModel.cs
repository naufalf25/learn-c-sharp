using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoGame.BackEnd.Interfaces;
using UnoGame.WPFApp.Commands;

namespace UnoGame.WPFApp.ViewModels
{
    class EndGameViewModel : BaseViewModel
    {
        private readonly MainWindow _mainWindow;

        public string WinnerName { get; }
        public ICommand BackToMainMenuCommand { get; }
        public ICommand ExitGameCommand { get; }

        public EndGameViewModel(MainWindow mainWindow, IPlayer winner)
        {
            _mainWindow = mainWindow;
            WinnerName = winner.Name;

            BackToMainMenuCommand = new RelayCommand(_ =>
            {
                _mainWindow.NavigateToMainMenu();
            });

            ExitGameCommand = new RelayCommand(_ =>
            {
                App.Current.Shutdown();
            });
        }
    }
}
