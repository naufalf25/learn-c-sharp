using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UnoGame.BackEnd.Interfaces;
using UnoGame.WPFApp.ViewModels;

namespace UnoGame.WPFApp.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView(MainWindow mainWindow, List<IPlayer> players)
        {
            InitializeComponent();
            DataContext = new GameViewModel(mainWindow, players);
            var vm = DataContext as GameViewModel;
            if (vm?.GameLog != null)
            {
                vm.GameLog.CollectionChanged += GameLog_CollectionChanged;
            }
        }

        private void GameLog_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (GameLogListBox.Items.Count > 0)
                    {
                        GameLogListBox.ScrollIntoView(GameLogListBox.Items[GameLogListBox.Items.Count - 1]);
                    }
                }), DispatcherPriority.Background);
            }
        }
    }
}
