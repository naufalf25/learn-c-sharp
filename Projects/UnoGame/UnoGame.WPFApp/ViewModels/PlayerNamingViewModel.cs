using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UnoGame.BackEnd.Enums;
using UnoGame.BackEnd.Interfaces;
using UnoGame.BackEnd.Models;
using UnoGame.WPFApp.Commands;

namespace UnoGame.WPFApp.ViewModels
{
    internal class PlayerNamingViewModel : BaseViewModel
    {
        private readonly MainWindow _mainWindow;
        public ObservableCollection<PlayerNameEntry> PlayerNames { get; } = new();
        public ICommand ConfirmCommand { get; }

        public PlayerNamingViewModel(MainWindow mainWindow, int playerCount)
        {
            _mainWindow = mainWindow;

            for (int i = 0; i < playerCount; i++)
            {
                PlayerNames.Add(new PlayerNameEntry(i));
            }

            ConfirmCommand = new RelayCommand(_ => ConfirmNames(), _ => CanConfirm());
        }

        private bool CanConfirm() => PlayerNames.All(p => !string.IsNullOrWhiteSpace(p.Name));

        private void ConfirmNames()
        {
            var players = PlayerNames
                .Select(p => new Player(p.Name, PlayerType.Human))
                .Cast<IPlayer>()
                .ToList();

            _mainWindow.NavigateToGame(players);
        }
    }

    internal class PlayerNameEntry : BaseViewModel
    {
        public string Label { get; }
        private string? _name;
        public string Name
        {
            get => _name ?? "";
            set => SetProperty(ref _name, value);
        }

        public PlayerNameEntry(int index)
        {
            Label = $"Player {index + 1}";
            Name = Label;
        }
    }
}
