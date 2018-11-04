using System;
using System.Collections.Generic;
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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
        int TimeLeft, GameTime, score, LowerCounter, HigherCounter, LivesCount;
        const int NoOfBlock = 7, y_height = 10, x1_height = 10, x2_height = 434, die_height = 220, lives = 3;
        TextBlock[] words = new TextBlock[NoOfBlock];
        Image[] Lives = new Image[lives];
        int[] x_axis = new int[NoOfBlock];
        int[] y_axis = new int[NoOfBlock];
        List<string> wordList = new List<string>(100) { "Apple", "Orange", "Banana", "One", "Two", "Three", "Eric" };


        //Initialization
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            TimeLeft = 30;
            GameTime = 0;
            score = 0;
            LowerCounter = 0;
            HigherCounter = 0;
            LivesCount = lives;
            Random rand = new Random();
            for (int i = 0; i < NoOfBlock; i++)
            {
                TextBlock TB = new TextBlock();
                x_axis[i] = rand.Next(x1_height, x2_height);
                y_axis[i] = y_height;
                TextBlockArea.Children.Add(TB);
                words[i] = TextBlockArea.Children[i] as TextBlock;
                words[i].Visibility = Visibility.Hidden;
            }
            for (int i = 0; i < lives; i++) { 
                Lives[i] = LifeGrid.Children[i] as Image;
                Lives[i].Visibility = Visibility.Visible;
            }
        }

        private void Start_Time()
        {
            TimeRemaining.Visibility = Visibility.Visible;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        
        // Every seconde past, trigger this call
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (TimeLeft > 0) { 
                TimeCounter.Text = TimeLeft-- + "s";
            } else
            {
                dispatcherTimer.Tick -= dispatcherTimer_Tick;
                dispatcherTimer.Stop();
                TimeCounter.Text = "Time's up!";
                GameOver("Time");
            }
        }

        private void GameOver(string reason) {
            if (reason == "Time")
                MessageBox.Show(string.Format("Time's up! Your score is: {0}", score));
            else if (reason == "Lives")
                    MessageBox.Show(string.Format("You have run out of lives! Your score is: {0}", score));
            StartButton.Content = "Replay?";
            StartButton.Visibility = Visibility.Visible;
            gameTimer.Tick -= gameTimer_Tick;
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
        }

        private void Start_Game()
        {
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromSeconds(.1);
            gameTimer.Start();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            if (TimeLeft > 0)
            {
                if (GameTime % 4 == 0 && HigherCounter <7)
                    HigherCounter++;
                for (int i = LowerCounter; i < HigherCounter; i++)
                { 
                    words[i].Visibility = Visibility.Visible;
                    words[i].Text = wordList[i];
                    words[i].Margin = new Thickness(x_axis[i], y_axis[i], 0, 0);
                    words[i].TextAlignment = TextAlignment.Center;
                    words[i].FontSize = 12;
                    y_axis[i] += 10;
                    if (y_axis[i] >= die_height)
                    {
                        y_axis[i] = y_height;
                        words[i].Visibility = Visibility.Hidden;
                        LowerCounter++;
                        Lives[--LivesCount].Visibility = Visibility.Hidden;
                        if (LivesCount == 0)
                            GameOver("Lives");
                    }
                }
                GameTime++;
            } else
            {
                gameTimer.Tick -= gameTimer_Tick;
                gameTimer.Stop();
            }
        }

        private void TypeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typedWord = TypeBox.Text;
            for (int i = LowerCounter; i < HigherCounter; i++)
            {
                if (wordList[i] == typedWord)
                {
                    score += 200-y_axis[i];
                    ScoreCounter.Text = score.ToString();
                    y_axis[i] = y_height;
                    words[i].Visibility = Visibility.Hidden;
                    LowerCounter++;
                    TypeBox.Text = "";
                }
            }
        }
        
        delegate void Start();

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame();
            ScoreCounter.Text = "0";
            TimeCounter.Text = "";
            ((Button)sender).Visibility = Visibility.Hidden;
            Start start = new Start(Start_Game) + new Start(Start_Time);
            start();
        }
    }
}
