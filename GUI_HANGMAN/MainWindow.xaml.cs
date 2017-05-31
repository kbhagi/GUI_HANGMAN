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
using Games;
using System.Diagnostics;

namespace GUI_HANGMAN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HangMan hangman;
        public MainWindow()
        {
            InitializeComponent();
            hangman = new HangMan(new Player(), 6);
            hangman.DuplicateGuess +=Hangman_DuplicateGuess;
            hangman.IncorrectGuess += Hangman_IncorrectGuess;
            hangman.CorrectGuess += Hangman_CorrectGuess;
            hangman.GameLost += Hangman_GameLost;
            hangman.GameWon += Hangman_GameWon;
            DisplayWord(hangman.WordToDisplay);
        }
        private void Hangman_GameWon(object sender, char e)
        {
            message.Content = "You win!";
            DisplayWord(hangman.Word);
        }
        private void Hangman_DuplicateGuess(object sender,char e)
        {
            message.Content = $"You already guessed '{e}!";
        }
        private void Hangman_IncorrectGuess(object sender, char e)
        {
            message.Content = $"There is not '{e}'!";
            DisplayImage();
        }
        private void Hangman_GameLost(object sender, char e)
        {
            message.Content = $"There is no '{e}'! You lose!";
            DisplayImage();
            DisplayWord(hangman.Word);
        }

        private void Hangman_CorrectGuess(object sender, char e)
        {
            message.Content = $"Correct! Guess again.";
            DisplayWord(hangman.WordToDisplay);
        }
        
    
        private void DisplayWord(string word)
        {
            this.word.Content = new TextBlock()
            { Text = string.Join(" ", (IEnumerable<char>)word) };
        }
        private void DisplayImage()
        {
            int imageNumber = hangman.IncorrectGuesses;
            Debug.Assert(imageNumber >= 0);
            Debug.Assert(imageNumber <= 7);
            image.Source = new BitmapImage(new Uri($@"pack://application:,,,/GUI_HANGMAN;component/Images/{imageNumber}.gif"));
        }
        protected override void OnTextInput(TextCompositionEventArgs e)
        {
           if(hangman.Outcome==Outcome.InProgress && !string.IsNullOrEmpty(e.Text))
            hangman.Guess(char.ToLower(e.Text[0]));
            base.OnTextInput(e);
        }


    }
}
