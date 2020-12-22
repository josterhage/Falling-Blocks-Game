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
    /// Interaction logic for NameDialog.xaml
    /// </summary>
    public partial class NameDialog : Window
    {
        public NameDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GamePage.SetHSName(hsName.Text);
            this.Close();
        }

        private void HsName_TouchEnter(object sender, TouchEventArgs e)
        {
            GamePage.SetHSName(hsName.Text);
            this.Close();
        }
    }
}
