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
using NLog;

namespace Badge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logger logger = LogManager.GetCurrentClassLogger();


        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            HealtherTogether ht = new HealtherTogether();

            statusPanel1.Content = "Updating HealtherTogether Badge tracking.";
            if (ht.Login())
            {
                ht.Track();
            }

            ht.Logoff();
            ht.Close();
            statusPanel1.Content = "Done!";
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in HealtherTogether. {0}", ex.Message);
            }
        }

    }
}
