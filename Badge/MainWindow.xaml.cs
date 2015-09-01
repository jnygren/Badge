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

namespace Badge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            HealtherTogether ht = new HealtherTogether();

            statusPanel1.Content = "Updating HealtherTogether Badge tracking.";
            if (ht.Login())
            {
                ;
            }

            ht.Logoff();
            statusPanel1.Content = "Done!";
        }

    }
}
