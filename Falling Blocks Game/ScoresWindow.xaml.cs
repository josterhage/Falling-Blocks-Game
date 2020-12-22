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
using System.Windows.Shapes;

namespace Falling_Blocks_Game
{
    /// <summary>
    /// Interaction logic for ScoresWindow.xaml
    /// </summary>
    public partial class ScoresWindow : Window
    {
        public ScoresWindow()
        {
            InitializeComponent();
            nameFirst.Text = StartPage.ns[0].name;
            nameSecond.Text = StartPage.ns[1].name;
            nameThird.Text = StartPage.ns[2].name;
            nameFourth.Text = StartPage.ns[3].name;
            nameFifth.Text = StartPage.ns[4].name;
            nameSixth.Text = StartPage.ns[5].name;
            nameSeventh.Text = StartPage.ns[6].name;
            nameEighth.Text = StartPage.ns[7].name;
            nameNinth.Text = StartPage.ns[8].name;
            nameTenth.Text = StartPage.ns[9].name;
            scoreFirst.Text = StartPage.ns[0].score;
            scoreSecond.Text = StartPage.ns[1].score;
            scoreThird.Text = StartPage.ns[2].score;
            scoreFourth.Text = StartPage.ns[3].score;
            scoreFifth.Text = StartPage.ns[4].score;
            scoreSixth.Text = StartPage.ns[5].score;
            scoreSeventh.Text = StartPage.ns[6].score;
            scoreEighth.Text = StartPage.ns[7].score;
            scoreNinth.Text = StartPage.ns[8].score;
            scoreTenth.Text = StartPage.ns[9].score;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
