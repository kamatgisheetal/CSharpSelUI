using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Linq;
using SelUICore.Utils;
using SelUICore.Utils.Extensions;
using SelUICore.Utils.Selenium;

namespace SelUICore.Utils.Selenium
{
    public static class Driver
    {
        [ThreadStatic]
        public static IWebDriver CurrentDriver;

        static List<string> ForNameofCheckboxElement = new List<string>();
        
        static List<IWebElement> CheckboxElement = new List<IWebElement>();

        /*Selenium API to initialize chrome driver in the specified path
        Paratmeters: None
        Return : None 
        Author : Sheetal K
        Example : */
        public static void InitChrome()
        {
            CurrentDriver = new ChromeDriver(Path.GetFullPath(@"../../../../SelUICore/" + "_drivers"));
        }

        /*Selenium API to set driver timeout
        Paratmeters: integer
        Return : None 
        Author : Sheetal K
        Example : */
        public static void Timeout(int Time)
        {
            CurrentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Time);
            
        }

        public static Func<IWebDriver, bool> ElementIsVisible(IWebElement element)
        {
            return (driver) =>
            {
                try
                {
                    return element.Displayed && element.Enabled;
                }
                catch (Exception)
                {
                    // If element is null, stale or if it cannot be located
                    return false;
                }
            };
        }

        public static IWebElement ExplicitWait(IWebElement element, int Time)
        {
            try
            {
                new WebDriverWait(CurrentDriver, TimeSpan.FromSeconds(Time)).Until(ElementIsVisible(element));

                return element;
            }
            catch
            {
                return element;
            }
        }

        public static bool InvisibilityOfElement(By by, int Time)
        {
            try
            {
                new WebDriverWait(CurrentDriver, TimeSpan.FromSeconds(Time)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string WaitUntilStartWith(IWebElement element, string text, int time)
        {
            new WebDriverWait(CurrentDriver, TimeSpan.FromSeconds(time)).Until(e => element.Text.Trim().StartsWith(text));
            return element.Text;
        }

        public static void TeardownTest()
        {
            try
            {
                CurrentDriver.Close();
            }
            catch (Exception)
            {
                // Ignore errors if we are unable to close the browser
            }
        }

        private static ChromeOptions ChromeProfile
        {
            get
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("disable-extensions");
                options.AddArgument("disable-infobars");
                

                return options;
            }
        }

       

        public static void BrowserType(string browser)
        {
            switch (browser)
            {
                case "IE":
                    CurrentDriver = new InternetExplorerDriver();
                    break;
                case "Firefox":
                    CurrentDriver = new FirefoxDriver();
                    break;
                case "Chrome":
                    CurrentDriver = new ChromeDriver(ChromeProfile);
                    break;
                default:
                    CurrentDriver = new ChromeDriver(ChromeProfile);
                    break;
            }
            CurrentDriver.Manage().Window.Maximize();
        }

        public static bool ElementValidation(IWebElement element)
        {
            try
            {
                if (element.Displayed & element.Enabled)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static void VerifyThat(params Action[] myAsserts)
        {
            foreach (var myAssert in myAsserts)
            {
                try
                {
                    myAssert();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
            }
        }

        public static bool ValidateGivenDataPresent(List<IWebElement> lstSubMenu, List<string> searchMenuText)
        {
            if (lstSubMenu == null || searchMenuText == null)
                return false;

            int flagCount = 0;
            foreach (var menu1 in searchMenuText)
            {
                foreach (var menu2 in lstSubMenu)
                {
                    if (IsTextEqualsToExpected(menu1, menu2.Text))
                    {
                        flagCount++;
                        break;
                    }
                }
            }
            if (searchMenuText.Count == flagCount)
                return true;

            return false;
        }

        public static bool IsTextEqualsToExpected(string text1, string text2)
        {
            return text1.Trim().Replace(Environment.NewLine, string.Empty).Equals(text2.Trim().Replace(Environment.NewLine, string.Empty), StringComparison.OrdinalIgnoreCase);
        }

        public static List<string> ConvertCommaSeparatedStringToList(String stringvalue)
        {
            return stringvalue.Split(',').ToList();
        }

        

        public static string GetNewTabTilte(IWebDriver driver)
        {
            var BrowserTabs = CurrentDriver.WindowHandles;
            int i = BrowserTabs.Count;
            driver.SwitchTo().Window(BrowserTabs[i - 1]);
            string WinTitle = driver.Title;
            return WinTitle;
        }


        /// <summary>
        /// Select or unselct the checkbox by comparing label name, For property and ID of the element
        /// </summary>
        /// <param name="Labels">List of labels </param>
        /// <param name="Checkboxes">List of Checkbox</param>
        /// <param name="SplitCheckboxInputName">Checkbox name from Config file</param>
        /// <param name="Checked">Bool input to compare where checkbox is checked or not</param>
        public static void SelectCheckbox(IList<IWebElement> Labels, IList<IWebElement> Checkboxes, List<string> SplitCheckboxInputName, bool Checked)
        {
            //SplitCheckboxInputName = ConvertCommaSeparatedStringToList(CheckboxInputName); // CheckboxInputName.Split(',').ToList();
            ForNameofCheckboxElement.Clear();
            CheckboxElement.Clear();
            foreach (var text in SplitCheckboxInputName)
            {
                foreach (var element in Labels)
                {
                    if (element.Text == text)
                    {
                        ForNameofCheckboxElement.Add(element.GetAttribute("for").ToString());
                    }
                }
            }
            //Compare the For name with ID and add the matchable element in the Webelement list
            foreach (var text in ForNameofCheckboxElement)
            {
                foreach (var element in Checkboxes)
                {
                    if (element.GetAttribute("id") == text)
                    {
                        CheckboxElement.Add(element);
                    }
                }
            }

            foreach (var element in CheckboxElement)
            {
                if (element.Selected == Checked)
                {
                    element.ClickElement(CurrentDriver,Settings.ShortTimeout);
                    
                }
            }
        }


    }
}
