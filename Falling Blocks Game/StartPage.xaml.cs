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
using System.Xml;

namespace Falling_Blocks_Game
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private List<int> test = new List<int>();

        public struct namescores
        {
            public string rank;
            public string name;
            public string score;
        }

        public static namescores[] ns { get; private set; } = new namescores[10];

        public StartPage()
        {
            int i = 0;
            InitializeComponent();
            //LoadScores();
            XmlReader scorefile = XmlReader.Create("HighScores.xml");

            while (scorefile.Read())
            {
                if ((scorefile.NodeType == XmlNodeType.Element) && (scorefile.Name == "score"))
                {
                    if ((scorefile.HasAttributes) && i < 10)
                    {
                        ns[i].rank = scorefile.GetAttribute("rank");
                        ns[i].name = scorefile.GetAttribute("name");
                        ns[i].score = scorefile.GetAttribute("points");
                        i++;
                    }
                }
            }

            scorefile.Close();
        }

        //private void LoadScores()
        //{

        //}

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            GamePage gamePage = new GamePage();
            this.NavigationService.Navigate(gamePage);
        }

        private void HighScores_Click(object sender, RoutedEventArgs e)
        {
            ScoresWindow sw = new ScoresWindow();
            sw.ShowDialog();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            About abt = new About();
            abt.ShowDialog();
        }
    }
}
