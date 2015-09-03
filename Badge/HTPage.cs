using System;
using OpenQA.Selenium;

namespace Badge
{
    public class HTPage
    {
        // Home page
        public static By BtnEmpLogin = By.Id("login-text");
        public static By BtnSpouseLogin = By.Id("spouse-text");

        // Login page
        public static By TxtUsername = By.Id("username");
        public static By TxtPassword = By.Id("passwd");
        public static By BtnLogin = By.Id("login-btn");

        // Header
        public static By DDUserLinks = By.Id("user-links-trigger");

        // Dropdown
        public static By LinkLogout = By.XPath("//a[@href='/logout/']");


    }
}
