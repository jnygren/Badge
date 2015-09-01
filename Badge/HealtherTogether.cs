using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Badge
{
    class HealtherTogether
    {
        private static IWebDriver driver = null;
        private static readonly string HealtherTogetherURL = "http://www.medtronichealthiertogether.com/members/healthyhabits/3";


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

                driver.FindElement(HTPage.SpouseLogin).Click();
            }

            return result;
        }


        public void Logoff()
        {
            if (driver != null)
            {

                driver.Quit();
                driver = null;
            }
        }
    }
}
