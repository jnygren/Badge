using System;
using OpenQA.Selenium;

namespace Badge
{
    public class HTPage
    {
        // Home page
        public static By hdrWelcome = By.XPath("//div[@class='welcome-header']/h1[contains(text(), 'Welcome')]");
        public static By BtnEmpLogin = By.Id("login-text");
        public static By BtnSpouseLogin = By.Id("spouse-text");

        // Login page
        public static By hdrLogin = By.Id("Login");
        public static By TxtUsername = By.Id("username");
        public static By TxtPassword = By.Id("passwd");
        public static By BtnLogin = By.Id("login-btn");

        // Members page
        public static By titleMembers = By.XPath("/html/head/title[contains(text(), 'Members')]");
        //public static By titleMembers = By.XPath("/html/title");

        // Data Tracking page
        public static By H2Tracking = By.XPath("//h2[contains(text(), 'Tracking')]");
        public static By InpStressReduction = By.Id("42737|35-text");
        public static By BtnReset = By.Id("tracker-reset");
        public static By BtnSave = By.Id("tracker-submit");

        // Header
        public static By LinkTrack = By.XPath("//li[@id='nav_members_track']/a");
        public static By DDUserLinks = By.Id("user-links-trigger");

        // Dropdown
        public static By LinkLogout = By.XPath("//a[@href='/logout/']");


    }
}
