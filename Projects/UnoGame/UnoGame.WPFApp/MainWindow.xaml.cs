using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnoGame.BackEnd.Interfaces;
using UnoGame.WPFApp.Views;

namespace UnoGame.WPFApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        NavigateToMainMenu();
    }

    public void NavigateToMainMenu()
    {
        MainContent.Content = new MainMenuView(this);
    }

    public void NavigateToPlayerCount()
    {
        MainContent.Content = new PlayerCountView(this);
    }

    public void NavigateToPlayerNaming(int playerCount)
    {
        MainContent.Content = new PlayerNamingView(this, playerCount);
    }

    public void NavigateToGame(List<IPlayer> players)
    {
        MainContent.Content = new GameView(this, players);
    }

    public void NavigateToEndGame(IPlayer winnerPlayer)
    {
        //MainContent.Content = new EndGameView(this, winnerPlayer);
    }
}