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

namespace Crap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        const string WIN_TEXT = "You win!";
        const string LOSE_TEXT = "You lose!";
        const string SNAKEEYES_TEXT = "Snake eyes! ";
        int DiceOne = 0;
        int DiceTwo = 0;
        int CurrentDiceSum = 0;
        int PreviousDiceSum = 0;
        int Turn = 0;
        int Points = 0;
        bool bIsFirstTurn = true;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void NewGame()
        {
            label.Content = "";
            DiceOne = DiceTwo = 0;
            CurrentDiceSum = PreviousDiceSum = 0;
            bIsFirstTurn = true;
            SetDiceImage(0, 1);
            SetDiceImage(0, 2);
            Turn = 0;
            Points = 0;
            DisplayTurns();
            DisplayPoints();
            SetRollButtonState(true);
        }

        private void DoDiceRoll()
        {
            Random diceRoll = new Random();
            DiceOne = diceRoll.Next(1, 7);
            DiceTwo = diceRoll.Next(1, 7);
            SetDiceImage(DiceOne, 1);
            SetDiceImage(DiceTwo, 2);
            CurrentDiceSum = DiceOne + DiceTwo;
            Points = Points + CurrentDiceSum;
        }

        private void SetRollButtonState(bool inState)
        {
            btnRoll.IsEnabled = inState;
        }

        private void DisplayTurns()
        {
            LabelTurnsDisplay.Content = Turn;
        }

        private void DisplayPoints()
        {
            LabelPointsDisplay.Content = Points;
        }

        private void SetDiceImage(int number, int DiceId)
        {
            if (number < 1 || number > 6)
            {
                number = 0;
            }
            string imagePath = $"pack://application:,,,/img/{number}.png";

            if (DiceId == 1)
            {
                DiceOneImg.Source = new BitmapImage(new Uri(imagePath));
            }
            if (DiceId == 2)
            {
                DiceTwoImg.Source = new BitmapImage(new Uri(imagePath));
            }
        }

        private void CheckFirstTurn()
        {
            if ((CurrentDiceSum == 7) || (CurrentDiceSum == 11))
            {
                DisplayPoints();
                label.Content = WIN_TEXT;
                SetRollButtonState(false);
            }
            if (CurrentDiceSum == 2)
            {
                label.Content = SNAKEEYES_TEXT + LOSE_TEXT;
                SetRollButtonState(false);
        }
            if ((CurrentDiceSum == 3) || (CurrentDiceSum == 12))
            {
                label.Content = LOSE_TEXT;
                SetRollButtonState(false);
            }
        }

        private void GameLoop()
        {
            Turn++;
            DisplayTurns();
            if (bIsFirstTurn)
            {
                CheckFirstTurn();
                bIsFirstTurn = false;
                PreviousDiceSum = CurrentDiceSum;
            }
            else
            {
                if (PreviousDiceSum == CurrentDiceSum)
                {
                    DisplayPoints();
                    label.Content = WIN_TEXT;
                    SetRollButtonState(false);
                }
                if (CurrentDiceSum == 7)
                {
                    label.Content = LOSE_TEXT;
                    SetRollButtonState(false);
                }
            }
            DisplayPoints();
            PreviousDiceSum = CurrentDiceSum;
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            DoDiceRoll();
            GameLoop();
        }

        private void btnHowToPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}