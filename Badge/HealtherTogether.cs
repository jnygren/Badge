using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Badge
{
    class HealtherTogether
    {
        private static IWebDriver driver = null;
        private static readonly string HealtherTogetherURL = "https://www.medtronichealthiertogether.com/";
        private static readonly string TEMP_Username = "JNygren";
        private static readonly string TEMP_Password = "VMN6p*B6";


        public HealtherTogether()
        {
            driver = new FirefoxDriver();
        }


        public bool Login()
        {
            bool result = true;

            if (driver == null)
                result = false;
            else
            {
                driver.Url = HealtherTogetherURL;

                // Home Page
                driver.FindElement(HTPage.BtnSpouseLogin).Click();

                // Login Page
                IWebElement btnLogin = driver.FindElement(HTPage.BtnLogin);
                driver.FindElement(HTPage.TxtUsername).SendKeys(TEMP_Username);
                driver.FindElement(HTPage.TxtPassword).SendKeys(TEMP_Password);
                btnLogin.Click();
            }

            return result;
        }


        public void Logoff()
        {
            if (driver != null)
            {
                IWebElement dd = driver.FindElement(HTPage.DDUserLinks);
                dd.Click();
                driver.FindElement(HTPage.LinkLogout).Click();

                driver.Quit();
                driver = null;
            }
        }

    }
}
