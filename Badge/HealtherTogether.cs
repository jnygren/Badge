using System;
using NLog;                         // Note: 'NLog Configuration' is not needed.

namespace Badge
{
    class HealtherTogether
    {
        private static readonly string HealtherTogetherURL = "https://www.medtronichealthiertogether.com/";
        private static readonly string HealtherTogetherURL2 = "http://www.medtronichealthiertogether.com/members/healthyhabits/3";
        private static readonly string TEMP_Username = "username";
        private static readonly string TEMP_Password = "password";
        private Logger logger = LogManager.GetCurrentClassLogger();


        public HealtherTogether()
        {
            Page.OpenUrl(HealtherTogetherURL);
        }


        /// <summary>
        /// Login to HealthierTogether
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            bool result = true;

            logger.Debug("{0}", "In HealtherTogether.Login()");

            // Home Page
            if (Page.WaitForTheElement(HTPage.hdrWelcome))
                logger.Info("Successfully reached 'Home' page.");
            //Page.Click(HTPage.BtnEmpLogin);
            Page.Click(HTPage.BtnSpouseLogin);

            // Login Page
            if (Page.WaitForTheElement(HTPage.hdrLogin))
                logger.Info("Successfully reached 'Login' page.");
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
            // Members Page
            //if (Page.WaitForTheElement(HTPage.titleMembers))
            if (Page.GetTitle().Equals("Members"))
                logger.Info("Successfully reached 'Members' page.");
            Page.Click(HTPage.LinkTrack);


            // Data Tracking page
            if (Page.WaitForTheElement(HTPage.H2Tracking))
            {
                logger.Info("Successfully reached 'Data Tracking' page.");
                Page.EnterText(HTPage.InpStressReduction, "1");
                Pause(1000);
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
            if (Page.WaitForTheElement(HTPage.DDUserLinks))
            {
                Page.Click(HTPage.DDUserLinks);
                if (Page.WaitForTheElement(HTPage.LinkLogout))
                    Page.Click(HTPage.LinkLogout);
                Pause();
                if (Page.WaitForTheElement(HTPage.hdrWelcome))
                    logger.Info("Successfully logged off from HealtherTogether.");
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
