using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.Xml;

namespace Falling_Blocks_Game
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private Block block = null;
        private Block nblock = null;
        private DispatcherTimer dispatcherTimer = null;
        private Field field = null;
        static string highscorename;
        static int Level;
        static int Score;
        private const int fallrate = 8;
        Random rand = new Random();
        Square.SquareTypes nextBlock;
        //private bool first = true;

        public GamePage()
        {
            InitializeComponent();

            nextBlock = (Square.SquareTypes)rand.Next(0, 7);
            block = new Block(nextBlock, gameField, 0, 6);
            nextBlock = (Square.SquareTypes)rand.Next(0, 7);
            nblock = new Block(nextBlock, nextBox, 0, 0);

            field = new Field();

            Level = 1;
            scoreBox.Text = Score.ToString();
            levelBox.Text = Level.ToString();

            this.dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(2750000 - (Level * fallrate * 25000));
            dispatcherTimer.Start();

            gameField.Focus();
        }

        private void GameQuit_Click(object sender, RoutedEventArgs e)
        {
            GameQuit();
        }

        private void GameQuit()
        {
            dispatcherTimer.Stop();
            Rectangle grayBox = new Rectangle();
            grayBox.Fill = new SolidColorBrush(Colors.DarkGray);
            grayBox.Stroke = new SolidColorBrush(Colors.DarkGray);
            grayBox.Width = 240;
            grayBox.Height = 480;
            Canvas.SetTop(grayBox, 0);
            Canvas.SetLeft(grayBox, 0);
            gameField.Children.Add(grayBox);
            string messageBoxText = "Are you sure you want to quit?";
            string messageBoxCaption = "Please keep playing...";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(messageBoxText, messageBoxCaption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                gameField.Children.Clear();
                this.NavigationService.Navigate(new StartPage());
                Score = 0;
                Level = 1;
            }

            dispatcherTimer.Start();
            //gameField.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            block.RotateBlock(true);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            block.RotateBlock(false);
        }

        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (field.CanShift(false, block))
                    {
                        block.ShiftBlock(false);
                    }
                    break;
                case Key.Right:
                    if (field.CanShift(true, block))
                    {
                        block.ShiftBlock(true);
                    }
                    break;
                case Key.Down:
                    if (CanFall())
                    {
                        block.Fall();
                    }
                    else
                    {
                        field.AddBlock(block);

                        scoreBox.Text = Score.ToString();
                        levelBox.Text = Level.ToString();
                        dispatcherTimer.Interval = new TimeSpan(2750000 - (Level * fallrate * 25000));


                        block = new Block(nextBlock, gameField, 0, 6);
                        nextBlock = (Square.SquareTypes)rand.Next(0, 6);
                        for (int i = 0; i < 4; i++)
                        {
                            nblock.Squares[i].DeleteSquare();
                        }
                        nblock = new Block(nextBlock, nextBox, 0, 0);
                    }
                    break;
                case Key.Up:
                    lock (block)
                    {
                        block.RotateBlock(true);
                    }
                    break;
                case Key.Escape:
                    GameQuit();
                    break;
                case Key.Space:
                    DropBlock();
                    break;
                default:
                    break;
            }
        }

        private void DropBlock()
        {
            lock (block)
            {
                while (CanFall())
                {
                    block.Fall();
                }
            }

            field.AddBlock(block);

            scoreBox.Text = Score.ToString();
            levelBox.Text = Level.ToString();
            dispatcherTimer.Interval = new TimeSpan(2750000 - (Level * fallrate * 25000));

            block = new Block(nextBlock, gameField, 0, 6);
            nextBlock = (Square.SquareTypes)rand.Next(0, 7);
            for (int i = 0; i < 4; i++)
            {
                nblock.Squares[i].DeleteSquare();
            }
            nblock = new Block(nextBlock, nextBox, 0, 0);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lock (block)
            {
                if (CanFall())
                {
                    block.Fall();
                }
                else
                {
                    if (!field.AddBlock(block))
                    {
                        YouLose();
                    }

                    scoreBox.Text = Score.ToString();
                    levelBox.Text = Level.ToString();
                    dispatcherTimer.Interval = new TimeSpan(2750000 - (Level * fallrate * 25000));

                    block = new Block(nextBlock, gameField, 0, 6);
                    nextBlock = (Square.SquareTypes)rand.Next(0, 7);
                    for (int i = 0; i < 4; i++)
                    {
                        nblock.Squares[i].DeleteSquare();
                    }
                    nblock = new Block(nextBlock, nextBox, 0, 0);
                }
            }

            gameField.Focus();

            CommandManager.InvalidateRequerySuggested();
        }

        private void YouLose()
        {
            dispatcherTimer.Stop();

            string messageBoxText = $"Game over, you got {Score.ToString()} points!";
            string messageBoxCaption = "Game over!";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Stop;

            MessageBox.Show(messageBoxText, messageBoxCaption, button, icon);

            CheckHighScore();

            gameField.Children.Clear();
            Level = 1;
            Score = 0;
            this.NavigationService.Navigate(new StartPage());

        }

        private void CheckHighScore()
        {
            int rank = 10;
            for (int i = 0; i < 10; i++)
            {
                int sc = Int32.Parse(StartPage.ns[i].score);

                if (Int32.Parse(StartPage.ns[i].score) < Score)
                {
                    rank--;
                }
            }

            if (rank < 10)
            {
                UpdateHighScores(rank);
            }
        }

        private void UpdateHighScores(int rank)
        {
            NameDialog nd = new NameDialog();
            nd.ShowDialog();
            for (int r = 9; r > rank; r--)
            {
                StartPage.ns[r].name = StartPage.ns[r - 1].name;
                StartPage.ns[r].score = StartPage.ns[r - 1].score;
            }
            StartPage.ns[rank].name = highscorename;
            StartPage.ns[rank].score = Score.ToString();

            WriteHSXML();
        }

        private void WriteHSXML()
        {
            XmlDocument hsFile = new XmlDocument();

            hsFile.Load("HighScores.xml");
            XmlNode root = hsFile.DocumentElement;

            for (int i = 0; i < 10; i++)
            {
                XmlNode node = root.SelectSingleNode($"score[@rank=\"{(i + 1).ToString()}\"]");
                node.Attributes["name"].Value = StartPage.ns[i].name;
                node.Attributes["points"].Value = StartPage.ns[i].score;
            }

            hsFile.Save("HighScores.xml");
        }

        private bool CanFall()
        {
            if ((block.Row + block.BottomRow) < 23)
            {
                return !field.CheckBeneath(block);
            }
            else
            {
                return false;
            }

        }

        public static void NextLevel()
        {
            Level++;
        }

        internal static void SetLevel(int v)
        {
            Level = v;
        }

        public static void GotLine()
        {
            Score += (Level * 100);
        }

        public static void SetHSName(string name)
        {
            highscorename = name;
        }
    }
}
