﻿using System;
using System.Configuration;
using NLog;                         // Note: 'NLog Configuration' is not needed.

namespace Badge
{
    class HealtherTogether
    {
        private static readonly string HealtherTogetherURL = "https://www.medtronichealthiertogether.com/";
        private static readonly string HealtherTogetherURL2 = "http://www.medtronichealthiertogether.com/members/healthyhabits/3";
        private string TEMP_Username = "username";
        private string TEMP_Password = "password";
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// c'tor
        /// </summary>
        public HealtherTogether()
        {
            // Retrieve Username/Password from 'App.config'.
            if (string.IsNullOrEmpty(TEMP_Username = ConfigurationManager.AppSettings["HTUsername"]))
                TEMP_Username = "(Username not configured.)";
            if (string.IsNullOrEmpty(TEMP_Password = ConfigurationManager.AppSettings["HTPassword"]))
                TEMP_Password = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

            Page.OpenUrl(HealtherTogetherURL);
        }


        /// <summary>
        /// Login to HealthierTogether
        /// </summary>
        public bool Login()
        {
            bool result = true;

            logger.Debug("In HealtherTogether.Login()");

            // Home Page
            if (Page.WaitForTheElement(HTPage.hdrWelcome))
                logger.Info("  Successfully reached 'Home' page.");
            //Page.Click(HTPage.BtnEmpLogin);
            Page.Click(HTPage.BtnSpouseLogin);

            // Login Page
            if (Page.WaitForTheElement(HTPage.hdrLogin))
                logger.Info("  Successfully reached 'Login' page.");
            if (Page.WaitForTheElement(HTPage.BtnLogin))
            {
                Page.EnterText(HTPage.TxtUsername, TEMP_Username);
                Page.EnterText(HTPage.TxtPassword, TEMP_Password);
                Page.Click(HTPage.BtnLogin);
            }
            else
                result = false;
            Pause();

            return result;
        }


        /// <summary>
        /// Track your activity. Enter daily numbers.
        /// </summary>
        public void Track()
        {
            logger.Debug("In HealtherTogether.Track()");

            // Members Page
            //if (Page.WaitForTheElement(HTPage.titleMembers))
            if (Page.GetTitle().Equals("Members"))
                logger.Info("  Successfully reached 'Members' page.");
            // Tracking tab
            Page.Click(HTPage.LinkTrack);


            // Data Tracking page
            if (Page.WaitForTheElement(HTPage.H2Tracking))
            {
                logger.Info("  Successfully reached 'Data Tracking' page.");
                // Enter values for 'Exercise', 'Stress Reduction', ...
                Page.EnterText(HTPage.InpStressReduction, "1");
                Pause(1000);

                // Save entries (or not). User remains on tracking tab.
                //Page.Click(HTPage.BtnReset);
                Page.Click(HTPage.BtnSave);
                Pause();
            }

            //if (Page.WaitForTheElement(HTPage.??))
            //    logger.Info("Successfully entered daily activity.");
        }


        /// <summary>
        /// Logoff HealthierTogether web site
        /// </summary>
        public void Logoff()
        {
            logger.Debug("In HealtherTogether.Logoff()");

            // Dropdown menu of user links
            if (Page.WaitForTheElement(HTPage.DDUserLinks))
            {
                Page.Click(HTPage.DDUserLinks);
                if (Page.WaitForTheElement(HTPage.LinkLogout))
                    Page.Click(HTPage.LinkLogout);
                Pause();
                if (Page.WaitForTheElement(HTPage.hdrWelcome))
                    logger.Info("  Successfully logged off from HealtherTogether.");
            }
        }


        /// <summary>
        /// Close browser
        /// </summary>
        public void Close()
        {
            Page.CloseBrowser();
        }


        /// <summary>
        /// Pause for some time before continuing
        /// </summary>
        /// <param name="length">Length of pause in ms. (Default = 2 seconds)</param>
        private void Pause(int length = 2000)
        {
            System.Threading.Thread.Sleep(length);
        }
    }
}
