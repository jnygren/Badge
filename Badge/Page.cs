using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace Badge
{
    public class Page
    {
        private static IWebDriver _driver = null;
        private static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                    _driver = new FirefoxDriver();
                return _driver;
            }
            set { _driver = value; }
        }

        public const int Delay = 300;
        private static int DelayForValidation = 200;
        private static readonly int TimeoutForElementPresence = 30;
        public delegate By GetBy(string element);

        private static void Sleep(int delay = Delay)
        {
            if (Delay > 0)
                Thread.Sleep(Delay);
        }


        /// <summary>
        /// Open web page in browser
        /// </summary>
        /// <param name="url">URL of page to be opened.</param>
        public static void OpenUrl(string url)
        {
            Driver.Url = url;
        }

#if OLD
		public static string GetCurrentPageUrl()
		{
			int retryCount = 0;
			string url;

			do
			{
				Sleep();

				url = Config.GetDriver().Url;

				if (String.IsNullOrWhiteSpace(url) == false)
					return url;

			} while (++retryCount < 10);

			return url;
		}
#endif

        public static void EnterText(string field, string value)
        {
            EnterText(By.Name(field), value);
        }

        public static void EnterText(By field, string value)
        {
            if (WaitForTheElement(field))
            {
                IWebElement element = Driver.FindElement(field);
                element.Clear();
                element.SendKeys(value);
            }
            Sleep();
        }

#if OLD
        public static void EnterFilePath(string field, string value)
        {
            WaitForTheElement(By.Name(field));
            IWebElement element = Config.GetDriver().FindElement(By.Name(field));
            element.SendKeys(value);
            Sleep();
        }
#endif

        public static void Click(By by)
        {
            if (WaitForTheElement(by))
                Driver.FindElement(by).Click();
            Sleep();
        }

#if OLD
		public static void Click(By by, int index)
		{
			WaitForTheElement(by);
			ICollection<IWebElement> elements = Config.GetDriver().FindElements(by);
			Assert.IsTrue(elements.Count > index, "Invalid index to Click");
			if (elements.Count > index)
			{
				elements.ElementAt(index).Click();
			}
			Sleep();
		}

        public static bool IsElementPresentInPages(By select, By link)
        {
            if (IsElementPresent(link))
            {
                return true;
            }
			if (IsElementPresent(select))
			{
				List<string> pages = GetOptionsInSelect(select);
				foreach (string page in pages)
				{
					SelectDropDownByVisibleText(select, page);
					if (IsElementPresent(link))
					{
						return true;
					}
				}
			}
            return false;
        }

        public static void ClickInPages(By select, By linkToClick)
        {
            if (IsElementPresent(linkToClick))
            {
                Click(linkToClick);
                return;
            }
            if (IsElementPresent(select))
            {
                List<string> pages = GetOptionsInSelect(select);
                foreach (string page in pages)
                {
                    SelectDropDownByVisibleText(select, page);
                    if (IsElementPresent(linkToClick))
                    {
                        Click(linkToClick);
                        return;
                    }
                }
            }
            Assert.Fail("Unable to find the given link");
            Sleep();
        }

        public static void ClickFirstPresent(params By[] elements)
        {
            IWebDriver driver = Config.GetDriver();
            foreach (By by in elements)
            {
                if(IsElementPresent(by))
                {
                    driver.FindElement(by).Click();
                    return;
                }
            }
            Sleep();
        }

        public static void CheckCheckbox(string field, bool check = true)
        {
            CheckCheckboxes(By.Name(field), check);
        }

        public static void CheckFirstCheckbox(By by, bool check = true)
        {
            IWebElement element = Config.GetDriver().FindElement(by);
            if (element.Selected != check)
            {
                element.Click();
            }
            Sleep();
        }

        public static void CheckCheckboxes(By by, bool check = true)
        {
            ICollection<IWebElement> elements = Config.GetDriver().FindElements(by);
            foreach (IWebElement element in elements)
            {
                if (element.Displayed && element.Selected != check)
                {
                    element.Click();
                }
            }
            Sleep();
        }

        public static void CheckCheckboxInPages(By select, By checkBox, bool check = true)
        {
            CheckCheckboxesInPages(select, checkBox, check, false);
        }

        public static void CheckCheckboxesInPages(By select, By checkBox, bool check = true)
        {
            CheckCheckboxesInPages(select, checkBox, check, true);
        }

        private static void CheckCheckboxesInPages(By select, By checkBox, bool check, bool allPages)
        {
            bool found = false;
            if (IsElementPresent(checkBox))
            {
                found = true;
                CheckCheckboxes(checkBox, check);
                if(!allPages)
                    return;
            }
            if (IsElementPresent(select))
            {
                List<string> pages = GetOptionsInSelect(select);
                foreach (string page in pages)
                {
                    SelectDropDownByVisibleText(select, page);
                    if (IsElementPresent(checkBox))
                    {
                        CheckCheckboxes(checkBox, check);
                        found = true;
                        if (!allPages)
                            return;
                    }
                }
            }
            if (found)
                return;
            Assert.Fail("Unable to find the given Checkbox: " + checkBox.ToString());
            Sleep();
        }


        public static void CheckMulitpleCheckboxes(GetBy getBy, bool check, params string[] ids)
        {
            IWebDriver driver = Config.GetDriver();
            foreach (string id in ids)
            {
                By by = getBy(id);
                IWebElement element = driver.FindElement(by);
                if (!element.Displayed)
                {
                    Assert.Fail("Element is hidden - " + by);
                    return;
                }
                if (element.Selected != check)
                {
                    element.Click();
                }
            }
            Sleep();
        }
              
        public static void CheckMulitpleCheckboxesInPages(By select, GetBy getBy, bool check, params string[] ids)
        {
            
            foreach (string id in ids)
            {
                By by = getBy(id);
                if (IsElementPresent(by))
                {                 
                    CheckCheckboxes(by, check);            
                }
                if (IsElementPresent(select))
                {
                    List<string> pages = GetOptionsInSelect(select);
                    foreach (string page in pages)
                    {
                        SelectDropDownByVisibleText(select, page);
                        if (IsElementPresent(by))
                        {
                            CheckCheckboxes(by, check); 
                        }
                    }
                }
   
            }
            Sleep();
        }
        public static bool IsSelected(By by)
        {
            IWebElement el = Config.GetDriver().FindElement(by);
            if (el.Displayed)
            {
                return el.Selected;
            }
            Assert.Fail("Element is hidden. " + by);
            return false;
        }

        public static bool IsSelectedInPages(By select, By elementToCheck)
        {
            if (IsElementPresent(elementToCheck))
                return Config.GetDriver().FindElement(elementToCheck).Selected;

            if (IsElementPresent(select))
            {
                List<string> pages = GetOptionsInSelect(select);
                foreach (string page in pages)
                {
                    SelectDropDownByVisibleText(select, page);

                    if (IsElementPresent(elementToCheck))
                        return Config.GetDriver().FindElement(elementToCheck).Selected;
                }

                Assert.Fail("Unable to find the given link");
                Sleep();
            }
            return false;
        }

        public static void SelectDropDownByVisibleText(string field, string text)
        {
            SelectDropDownByVisibleText(By.Name(field), text);
        }

        public static void SelectDropDownByVisibleText(By field, string text)
        {
            WaitForTheElement(field);
            new SelectElement(Config.GetDriver().FindElement(field)).SelectByText(text);
            Sleep();
        }

        public static void SelectDropDownByValue(string field, string value)
        {
            SelectDropDownByValue(By.Name(field), value);
        }

        public static void SelectDropDownByValue(By field, string value)
        {
            WaitForTheElement(field);
            new SelectElement(Config.GetDriver().FindElement(field)).SelectByValue(value);
            Sleep();
        }

        /// <summary>
        /// The index starts from 0. If the index is negative, corresponding element is checked from the last. 
        /// If index is -1, it would select the last element. If it is -2, it would select the last but one element and so on.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="index"></param>
        public static void SelectDropDownByIndex(string field, int index)
        {
            SelectDropDownByIndex(By.Name(field), index);
        }

        /// <summary>
        /// The index starts from 0. If the index is negative, corresponding element is checked from the last. 
        /// If index is -1, it would select the last element. If it is -2, it would select the last but one element and so on.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="index"></param>
        public static void SelectDropDownByIndex(By field, int index)
        {
            WaitForTheElement(field);
            SelectElement select = new SelectElement(Config.GetDriver().FindElement(field));
            if (index < 0)
                index += select.Options.Count;
            select.SelectByIndex(index);
            Sleep();
        }

#endif


        /// <summary>
        /// Wait for element to be rendered on page
        /// </summary>
        /// <param name="by">Element of interest</param>
        /// <param name="timeout">Time in which element is to be found</param>
        /// <returns>'True' if element is found.</returns>
        public static bool WaitForTheElement(By by, int timeout = 45)
        {
            bool found = false;

            for (int i = 0; i <= timeout; i++)
            {
                if (Page.IsElementPresent(by))
                {
                    found = true;
                    break;
                }
                Thread.Sleep(1000);
            }
            //Assert.Fail("Unable to find the element " + by.ToString() + " even after waiting " + timeout + " seconds");
            return found;
        }

#if OLD
        public static void WaitForTheElementAbsence(By by, int timeout = 45)
        {
            for (int i = 0; i <= timeout; i++)
            {
                if (!Page.IsElementPresent(by))
                    return;
                Thread.Sleep(1000);
            }
            Assert.Fail("Element " + by.ToString() + " is present even after waiting " + timeout + " seconds");
        }

        public static void WaitForTheElements(params By[] by)
        {
            for (int i = 0; i <= TimeoutForElementPresence; i++)
            {
                foreach(By byElement in by)
                {
                    if(Page.IsElementPresent(byElement))
                        return;
                }
                Thread.Sleep(1000);
            }
            Assert.Fail("Unable to find the element " + by[0].ToString() + " even after waiting " + TimeoutForElementPresence + " seconds");
        }

        public static void MouseOver(By by)
        {
            IWebDriver driver = Config.GetDriver();
            IWebElement element = driver.FindElement(by);
            Actions builder = new Actions(driver);
            builder.MoveToElement(element).Build().Perform();
            Sleep();
        }

        public static string GetCurrentWindow()
        {
            return Config.GetDriver().CurrentWindowHandle;
        }

        public static void SwitchToWindow(string window)
        {
            Config.GetDriver().SwitchTo().Window(window);
            Sleep();
        }

        public static void SwitchToWindowOtherThan(string window)
        {
            IWebDriver driver = Config.GetDriver();
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 3, 0));
            wait.Until<bool>(d => { return d.WindowHandles.Count > 1; });

            foreach (string handle in driver.WindowHandles)
            {
                if (!handle.Equals(window))
                {
                    driver.SwitchTo().Window(handle);
                    break;
                }
            }
            Sleep();
        }

        public static void CloseCurrentWindow()
        {
            Config.GetDriver().Close();
        }

		public static void CloseAllWindowsOtherThanMain()
		{
			string mainWindow;
			IWebDriver driver = Config.GetDriver();

			do
			{
				mainWindow = GetCurrentWindow();

				if (driver.WindowHandles.Count == 1)
					break;

				SwitchToWindowOtherThan(mainWindow);

				if (GetCurrentWindow() == mainWindow)
					break;

				CloseCurrentWindow();
				SwitchToWindow(mainWindow);

			} while (true);
		}
#endif

        public static void CloseBrowser()
        {
                Driver.Quit();
                _driver = null;
        }

#if OLD
        public static string GetSelectedValue(string field)
        {
            return GetSelectedValue(By.Name(field));
        }

        public static string GetSelectedValue(By field)
        {
            IWebElement el = Config.GetDriver().FindElement(field);
            if (!el.Displayed)
            {
                Assert.Fail("The element is not visible: " + field);
            }
            return new SelectElement(el).SelectedOption.Text;
        }
#endif

        public static bool IsElementPresent(By by)
        {
            return Driver.FindElements(by).Where(e => e.Displayed).Count() > 0;
        }

#if OLD
        public static int GetCount(By by)
        {
            return Config.GetDriver().FindElements(by).Where(e => e.Displayed).Count();
        }

        public static void VerifyElementPresence(By by, string message = "")
        {
            WaitForTheElement(by);
            Assert.IsTrue(GetCount(by) > 0, message);
        }

        public static void VerifyElementAbsence(By by, string message = "")
        {
            WaitForTheElementAbsence(by);
            Assert.IsTrue(GetCount(by) == 0, message);
        }

        public static void VerifyElementPresenceInPages(By select, By elementToBeSearched, string message = "")
        {
            VerifyElementPresenceAbsenceInPages(select, elementToBeSearched, message, true);
        }

        public static void VerifyElementAbsenceInPages(By select, By elementToBeSearched, string message = "")
        {
            VerifyElementPresenceAbsenceInPages(select, elementToBeSearched, message, false);
        }

        public static void VerifyElementPresenceAbsenceInPages(By select, By elementToBeSearched, string message, bool shouldBePresent)
        {
            Thread.Sleep(DelayForValidation);
            bool present = false;
            if (IsElementPresent(select))
            {
                List<string> pages = GetOptionsInSelect(select);
                foreach (string page in pages)
                {
                    SelectDropDownByVisibleText(select, page);
                    if (IsElementPresent(elementToBeSearched))
                    {
                        present = true;
                        break;
                    }
                }
                if (shouldBePresent != present)
                    Assert.Fail(message);
            }
            else
            {
                if (shouldBePresent)
                    Page.VerifyElementPresence(elementToBeSearched, message);
                else
                    Page.VerifyElementAbsence(elementToBeSearched, message);
            }
        }

        /// <summary>
        /// Verfies if the table of data on UI has the expected columns
        /// </summary>
        /// <param name="table">Entire table</param>
        /// <param name="columnHeaderPath">Xpath to extract the column headers</param>
        /// <param name="expectedColumns">List of expected of column headers</param>
        public static void VerifyListColumns(By table, By columnHeaderPath, List<string> expectedColumns)
        {
            WaitForTheElement(table);
            IWebElement element = Config.GetDriver().FindElement(table);
            ICollection<IWebElement> options = element.FindElements(columnHeaderPath);
            foreach (IWebElement col in options.ToList())
            {
                Assert.True(expectedColumns.Contains(col.Text));
            }
        }

        /// <summary>
        /// Column Number starts with 0.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnNum"></param>
        public static void VerifyColumnsIsSorted(By table, int columnNum, bool isAscending = true)
        {
            WaitForTheElement(table);
            IWebElement element = Config.GetDriver().FindElement(table);
            List<IWebElement> columns = element.FindElements(By.XPath(".//td[" + columnNum.ToString() + "]")).ToList();
            if (columns.Count > 2)
            {
                for (int i = 2; i < columns.Count; i++)
                {
                    Assert.True(string.Compare(columns[i].Text, columns[i - 1].Text) * (isAscending ? 1 : -1) >= 0);
                }
            }
        }

        public static string GetText(By by)
        {
            if(IsElementPresent(by))
                return Config.GetDriver().FindElement(by).Text;
            Assert.Fail("Element is not present - " + by);
            return null;
        }

        public static string GetUrl(By by)
        {
            if (IsElementPresent(by))
                return Config.GetDriver().FindElement(by).GetAttribute("href");
            Assert.Fail("Element is not present - " + by);
            return null;
        }

        public static string GetImage(By by)
        {
            if (IsElementPresent(by))
                return Config.GetDriver().FindElement(by).GetAttribute("src");
            Assert.Fail("Element is not present - " + by);
            return null;
            
        }

        public static string GetPageCookies(string[] cookieNames)
        {
            IOptions options = Config.GetDriver().Manage();
            
            string cookies = string.Empty;
            foreach (string cookieName in cookieNames)
            {
                string cookie = options.Cookies.GetCookieNamed(cookieName).ToString();
                string pattern = "(" + cookieName + "=[a-z0-9-]+;)";
                MatchCollection matches =  Regex.Matches(cookie, pattern);
                foreach (Match match in matches)
                {
                    cookies += match.Groups[1].Value;
                }
            }

            return cookies;
        }

        public static Stream GetFile(string domain, string fileUrl, string[] cookies)
        {
            HttpWebRequest request;
            Uri uri = new Uri(domain);
            request = (HttpWebRequest)HttpWebRequest.Create(fileUrl);
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            request.Accept = "*/*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/37.0.2062.120 Safari/537.36";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.SetCookies(uri, Page.GetPageCookies(cookies));
            return request.GetResponse().GetResponseStream();
        }
        public static string GetTextInPages(By select, By textElement)
        {
            if (IsElementPresent(textElement))
            {
                return GetText(textElement);
            }

            if (IsElementPresent(select))
            {
                List<string> pages = GetOptionsInSelect(select);
                foreach (string page in pages)
                {
                    SelectDropDownByVisibleText(select, page);
                    if (IsElementPresent(textElement))
                    {
                        return GetText(textElement);
                    }
                }
            }
            Assert.Fail("Unable to find the given text");
            return null;
        }

        public static string GetValue(By by, string attribute = "value")
        {
            IWebElement el = Config.GetDriver().FindElement(by);
            if(el.Displayed) 
                return el.GetAttribute(attribute);
            Assert.Fail("Element is hidden - " + by);
            return null;
        }

        public static string GetValue(string name, string attribute = "value")
        {
            return GetValue(By.Name(name), attribute);
        }

        public static List<string> GetTextList(By by)
        {
            return Config.GetDriver().FindElements(by).Where(el => el.Displayed).Select(element => element.Text).ToList();
        }

        public static List<string> GetOptionsInSelect(By by)
        {
            IWebElement element = Config.GetDriver().FindElement(by);
            if (element.Displayed)
            {
                ICollection<IWebElement> options = element.FindElements(By.TagName("option"));
                return options.Select(option => option.Text).ToList();
            }
            Assert.Fail("Element is hidden - " + by);
            return null;
        }

        public static void VerifyControlsCheckedInPages(By select, By control, bool check, string message = "")
        {
            Thread.Sleep(DelayForValidation);
            if (IsElementPresent(select))
            {
                List<string> pages = GetOptionsInSelect(select);
                foreach (string page in pages)
                {
                    SelectDropDownByVisibleText(select, page);
                    if (IsElementPresent(control))
                    {
                        if (IsSelected(control) == check)
                            return;
                        Assert.Fail(message);
                    }
                }
                Assert.Fail(message);
            }
            else
            {
                Assert.True(IsSelected(control) == check, message);
            }
        }

        public static void VerifyCheckboxesChecked(GetBy getBy, bool check, params string[] ids)
        {
            Thread.Sleep(DelayForValidation);
            IWebDriver driver = Config.GetDriver();
            foreach (string id in ids)
            {
                Assert.AreEqual(check, IsSelected(getBy(id)), id + " is expected to be " + (check ? "" : "not ") + "selected.");
            }
        }

        public static void VerifyCheckboxesChecked(By by, bool check)
        {
            Thread.Sleep(DelayForValidation);
            foreach (IWebElement element in Config.GetDriver().FindElements(by).Where(el => el.Displayed))
            {
                Assert.IsTrue(element.Selected == check, string.Format("Checkbox is expected to be {0}checked", check ? "" : "not "));
            }
        }

        public static void VerifyCheckboxesChecked(string name, bool check)
        {
            VerifyCheckboxesChecked(By.Name(name), check);
        }

        public static void VerifyText(By by, string expectedText)
        {
            WaitForTheElement(by);
            string actualText = GetText(by);
            Assert.IsTrue(expectedText.Equals(actualText), string.Format("Expected Text: {0}, Actual Text: {1}", expectedText, actualText));
        }

		public static void VerifyPartialText(By by, string expectedText)
		{
			WaitForTheElement(by);
			string actualText = GetText(by);
			Assert.IsTrue(actualText.Contains(expectedText), string.Format("Expected Text: {0}, Actual Text: {1}", expectedText, actualText));
		}

		public static void VerifyTextInPDF(PDFTestDocument pdfDoc, string textToCheck)
		{
			Assert.IsTrue(pdfDoc.Contains(textToCheck), String.Format(@"Expected the text: ""{0}"" in ""{1}"" but was not present", textToCheck, pdfDoc.PdfContent));
		}

        public static void VerifyTextForInput(By by, string expectedText)
        {
            Thread.Sleep(DelayForValidation);
            string actualText = GetValue(by);
            Assert.IsTrue(expectedText.Equals(actualText), string.Format("Expected Text: {0}, Actual Text: {1}", expectedText, actualText));
        }

		public static void SelectDateByValue(string ddlDay, string ddlMonth, string ddlYear, DateTime date)
		{
			Page.SelectDropDownByValue(ddlDay, date.Day.ToString());
			Page.SelectDropDownByValue(ddlMonth, date.Month.ToString());
			Page.SelectDropDownByValue(ddlYear, date.Year.ToString());			
		}

		public static void SelectDateByVisibleText(string ddlDay, string ddlMonth, string ddlYear, DateTime date)
		{
			Page.SelectDropDownByVisibleText(ddlDay, date.Day.ToString());
			Page.SelectDropDownByVisibleText(ddlMonth, date.Month.ToString());
			Page.SelectDropDownByVisibleText(ddlYear, date.Year.ToString());
		}

        public static bool ValidateGridCount(By pageIndex, By lblCount, By grid)
        {
            int gridCount = 0;
            if (IsElementPresent(pageIndex))
            {
                List<string> indexList = Page.GetOptionsInSelect(pageIndex);
                gridCount = (indexList.Count - 1) * GetCount(grid);
                SelectDropDownByVisibleText(pageIndex, indexList.Last());
                gridCount += GetCount(grid);
            }
            else
            {
                gridCount = GetCount(grid);
            }
            if (gridCount == int.Parse(GetText(lblCount)))
            {
                return true;
            }
            return false;
        }
#endif
    }
}
