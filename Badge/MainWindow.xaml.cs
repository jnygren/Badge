using System;
using System.Windows;
using System.Diagnostics;
using NLog;

namespace Badge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// c'tor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Window Loaded event handler
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// 'Go' button handler
        /// </summary>
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
                logger.Info("  --- Badge tracking entry complete. ---\r\n");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in HealtherTogether. {0}", ex.Message);
                statusPanel1.Content = string.Format("Error in HealtherTogether. {0}", ex.Message);
            }
        }


        /// <summary>
        /// 'File - View Log' menu item handler
        /// </summary>
        private void ViewLog_Click(object sender, RoutedEventArgs e)
        {
            NLog.Targets.FileTarget fileTarget = (NLog.Targets.FileTarget)LogManager.Configuration.FindTargetByName("file");
            string logfile = fileTarget.FileName.Render(new LogEventInfo());
            Process.Start(logfile);     // Open logfile in default viewer
        }


        /// <summary>
        /// 'Help - Help' menu item handler
        /// </summary>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Help is not yet implemented.", "Help");
        }


        /// <summary>
        /// 'Help - About' menu item handler
        /// </summary>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("About box is not yet implemented.", "About Badge");
        }


    }
}
