using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer(); //Timer for Time left
        System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer(); //Timer for falling words
        System.Windows.Threading.DispatcherTimer bkgrdTimer = new System.Windows.Threading.DispatcherTimer(); //Timer for background fading stars fields
        const int y_height = 10, x1_height = -400, x2_height = 400, die_height = 220, screenWidth = 525, screenHeight = 350, fallFrequency = 2;
        const double bkgrdTime = 0.005, second = 1;
        double FallingSpeed = 0.5; // Words falling speed
        int GameTime = 0;
        delegate void Start();
        Game game;
        List<TextBlock> words;
        Random r = new Random();
        Star[] stars = new Star[400];
        Image[] Lives = new Image[Game.Lives];
        
        //Initialization
        public MainWindow()
        {
            InitializeComponent();
            bkgrdsetup(screenWidth, screenHeight);
            NextLevel.Visibility = Visibility.Hidden;
        }

        //Setup Background
        private void bkgrdsetup(int width, int height)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star(r, width, height);
                stars[i].initialize(Canvas2D);
            }
        }

        //Setup Game Setting
        private void InitializeGame(int score)
        {
            game = new Game(score); //Game Setup
            Dead.Text = ""; //Health Bar
            TypeBox.Text = ""; //Type Area
            TimeCounter.Text = ""; //Reset Timer for Time Left
            words = new List<TextBlock>(Game.NoOfBlock); //Reset the number of words in game 
            for (int i = 0; i < Game.Lives; i++)
            {
                Lives[i] = LifeGrid.Children[i] as Image; //Health Bar Images
                Lives[i].Visibility = Visibility.Visible; 
            }
            Start start = new Start(Start_Game) + new Start(Start_Time) + new Start(Background_Timer); //Chain up all timer into delegate Start()
            start();
            TypeBox.Focus(); //Cursor on Typebox
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame(0);
            NextLevel.Visibility = Visibility.Hidden;
            ScoreCounter.Text = "0"; //Reset score to 0
            ((Button)sender).Visibility = Visibility.Hidden;
        }

        private void NextLevel_Click(object sender, RoutedEventArgs e)
        {
            InitializeGame(game.Score);
            if (FallingSpeed > 0.1)
                FallingSpeed -= 0.1; //Increase difficulties
            ((Button)sender).Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Hidden;
        }

        private void Background_Timer()
        {
            bkgrdTimer.Tick += bkgrdTimer_Tick;
            bkgrdTimer.Interval = TimeSpan.FromSeconds(bkgrdTime);
            bkgrdTimer.Start();
        }

        //For every Background Time Tick, update the Background Stars Field 
        private void bkgrdTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < stars.Length; i++)
                stars[i].update(Canvas2D);
        }

        private void Start_Time()
        {
            TimeRemaining.Visibility = Visibility.Visible;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(second);
            dispatcherTimer.Start();
        }

        // Every second past, check if game ends
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (game.TimeLeft > 0) {
                TimeCounter.Text = game.TimeLeft-- + "s";
            } else {
                TimeCounter.Text = "Time's up!";
                GameOver("Time");
            }
        }

        private void Start_Game()
        {
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromSeconds(FallingSpeed);
            gameTimer.Start();
        }

        //Foe every falling speed tick, trigger this call 
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // for every N falling occur, add new falling textblock with random word
            if (GameTime % fallFrequency == 0) {
                int counter = words.Count;
                words.Add(new TextBlock());
                words[counter].TextAlignment = TextAlignment.Center;
                words[counter].FontSize = 18;
                words[counter].Foreground = Brushes.White;
            }
            //Update falling words position
            for (int i = 0; i < words.Count; i++)
            {
                Thickness m = words[i].Margin;
                //New textblock setup
                if (words[i].Text == "") { 
                    TextBlockArea.Children.Add(words[i]);
                    words[i].Text = Game.wordList[r.Next(Game.NoOfBlock)];
                    m.Left = r.Next(x1_height, x2_height);
                    m.Top = 0;
                }
                m.Top += y_height; // Fall y_height unit every time
                words[i].Margin = m;
                // Check if words drop below the die_height
                if (game.CheckDie(words[i].Margin.Top, die_height))
                {
                    words[i].Visibility = Visibility.Hidden;
                    words.Remove(words[i--]);
                    Lives[--game.LivesCount].Visibility = Visibility.Hidden;
                    if (game.LivesCount == 0)
                        GameOver("Lives");
                }
            }
            GameTime++; // GameTime Counter
        }
        // Trigger this call when TypeBox's text is changed
        private void TypeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typedWord = TypeBox.Text;
            for (int i = 0; i < words.Count; i++)
            {
                //Highlight the targeted word(s)
                bool select = false;
                for (int j = 0; j < typedWord.Length; j++){
                    if (words[i].Text[j] == typedWord[j])
                        select = true;
                    else
                    {
                        select = false;
                        words[i].Foreground = Brushes.White;
                        break;
                    }
                }
                if (select)
                    words[i].Foreground = Brushes.SteelBlue;
                //Check if any targeted word typed completely
                if (words[i].Text == typedWord)
                {
                    Thickness m = words[i].Margin;
                    words[i].Margin = m;
                    ScoreCounter.Text = game.GetScore(words[i].Text.Length - (int)m.Top).ToString(); //Add Score
                    words[i].Visibility = Visibility.Hidden;
                    words.Remove(words[i--]); //Remove finished word
                    TypeBox.Text = ""; //Reset the TypeBox
                }
            }
        }
        //When game ends, trigger this call
        private void GameOver(string reason)
        {
            //stop all timers
            gameTimer.Tick -= gameTimer_Tick;
            gameTimer.Stop();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            dispatcherTimer.Stop();
            bkgrdTimer.Tick -= bkgrdTimer_Tick;
            bkgrdTimer.Stop();
            if (reason == "Time") //Reason: Time's up -> display score
            {
                MessageBox.Show(string.Format("Time's up! Your score is: {0}", game.Score), "Result");
                NextLevel.Visibility = Visibility.Visible;
            }
            else if (reason == "Lives") //Reason: Dead-> display score
            {
                Dead.Text = "DEAD";
                FallingSpeed = 0.5;
                MessageBox.Show(string.Format("You have run out of lives! Your score is: {0}", game.Score), "Result");
            }
            foreach (TextBlock tb in words)
                tb.Visibility = Visibility.Hidden;
            StartButton.Content = "Replay?"; // Replay option
            StartButton.Visibility = Visibility.Visible;
        }
    }
}
