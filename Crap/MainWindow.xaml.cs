using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

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
        string ImagePath = "";
        int DiceOne = 0;
        int DiceTwo = 0;
        int CurrentDiceSum = 0;
        int PreviousDiceSum = 0;
        int Turn = 0;
        int Points = 0;
        bool bIsFirstTurn = true;
        bool bSnakeEyes = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            labelVersion.Content = "v" + Assembly.GetExecutingAssembly().GetName().Version;
            NewGame();
        }

        private void NewGame()
        {
            label.Content = "";
            DiceOne = DiceTwo = 0;
            CurrentDiceSum = PreviousDiceSum = 0;
            bIsFirstTurn = true;
            bSnakeEyes = false;
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
            if (DiceOne == 1 && DiceTwo == 1 && bIsFirstTurn) {
                bSnakeEyes = true;
            }
            SetDiceImage(DiceOne, 1);
            SetDiceImage(DiceTwo, 2);
            CurrentDiceSum = DiceOne + DiceTwo;
            Points = Points + CurrentDiceSum;
        }

        private void ShowAboutBox()
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
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

            if (!bSnakeEyes)
            {
                ImagePath = $"pack://application:,,,/img/{number}.png";
            }
            else
            {
                ImagePath = $"pack://application:,,,/img/1_snake_eye.png";
            }

            if (DiceId == 1)
            {
                DiceOneImg.Source = new BitmapImage(new Uri(ImagePath));
            }
            if (DiceId == 2)
            {
                DiceTwoImg.Source = new BitmapImage(new Uri(ImagePath));
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

        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            ShowAboutBox();
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