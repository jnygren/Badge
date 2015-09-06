using System;
using NLog;                         // Note: 'NLog Configuration' is not needed.

namespace Badge
{
    class HealtherTogether
    {
        private static readonly string HealtherTogetherURL = "https://www.medtronichealthiertogether.com/";
        private static readonly string HealtherTogetherURL2 = "http://www.medtronichealthiertogether.com/members/healthyhabits/3";
        private static readonly string TEMP_Username = "JNygren";
        private static readonly string TEMP_Password = "VMN6p*B6";
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
            //Page.Click(HTPage.BtnEmpLogin);
            Page.Click(HTPage.BtnSpouseLogin);

            // Login Page
            if (Page.WaitForTheElement(HTPage.BtnLogin))
            {
                Page.EnterText(HTPage.TxtUsername, TEMP_Username);
                Page.EnterText(HTPage.TxtPassword, TEMP_Password);
                Page.Click(HTPage.BtnLogin);

            }
            else
                result = false;

            return result;
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
            }
        }


        /// <summary>
        /// Close browser
        /// </summary>
        public void Close()
        {
            Page.CloseBrowser();
        }

    }
}
