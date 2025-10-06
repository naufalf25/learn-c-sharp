using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoGame.WPFApp.Commands;
using UnoGame.WPFApp.Helper;

namespace UnoGame.WPFApp.ViewModels
{
    internal class PlayerCountViewModel : BaseViewModel
    {
        private readonly MainWindow _mainWindow;
        public List<int> PlayerOptions { get; } = [2, 3, 4];
        private int _selectedPlayerCount = 2;
        public int SelectedPlayerCount
        {
            get => _selectedPlayerCount;
            set => SetProperty(ref _selectedPlayerCount, value);
        }
        public ICommand ConfirmCommand { get; }

        public PlayerCountViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            ConfirmCommand = new RelayCommand(_ =>
            {
                SoundManager.PlaySound("click");
                NavigateToPlayerNaming();
            });
        }

        private void NavigateToPlayerNaming()
        {
            _mainWindow.NavigateToPlayerNaming(SelectedPlayerCount);
        }
    }
}
