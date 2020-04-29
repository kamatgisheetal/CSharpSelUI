using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SelUICore.Utils.Selenium;
using static SelUICore.Utils.Selenium.Settings;

namespace SelUICore.Utils.Extensions
{
    public static class WebDriverExtensions
    {
        public static IWebDriver _driver = Driver.CurrentDriver;

        /*Web driver API to highlight the element on the page of web application
        Paratmeters: Selenium locator
        Return : locator 
        Author : Sheetal K
        Example : */

        public static object WdHighlight(this By locator)
        {
            var mylocator = _driver.FindElement(locator);
            var js = (IJavaScriptExecutor)_driver;
            return js.ExecuteAsyncScript(weHighlightedColour, mylocator);
        }

        /*Web driver API to find the element on the page of web application
        Paratmeters: Selenium locator,time 
        Return : locator 
        Author : Sheetal K
        Example : */
        public static IWebElement WdFindElement(this By locator, int sec = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(sec));
            return wait.Until(drv =>
            {
                try
                {
                    locator.WdHighlight();
                    return drv.FindElement(locator);
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

        }

        /*Web driver API to perform action on the element of web application
        Paratmeters: Selenium locator,string,integer,boolean
        Return : None 
        Author : Sheetal K
        Example : */
        public static void WdSendKeys(this By locator,string text, int sec=10,bool clearFirst = false)
        {
            if (clearFirst) locator.WdFindElement(sec).Clear();
            locator.WdFindElement(sec).SendKeys(text);
        }

        /*Web driver API to click the element based on index of web application
        Paratmeters: Selenium locator,integer,integer
        Return : None 
        Author : Sheetal K
        Example : */
        public static void WdClickbyIndex(this By locator, int index=0,int sec = 10)
        {
            var myLocator = _driver.FindElements(locator);
            myLocator[index].Click();
        }

        /*Web driver API to click the element of web application
        Paratmeters: Selenium locator,integer
        Return : None 
        Author : Sheetal K
        Example : */
        public static void WdClick(this By locator,int sec =10)
        {
            locator.WdFindElement(sec).Click();
        }
    }

    
}
