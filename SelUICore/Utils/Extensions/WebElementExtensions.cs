using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SelUICore.Utils.Selenium;
using static SelUICore.Utils.Selenium.Settings;

namespace SelUICore.Utils.Extensions
{
    public static class WebElementExtensions
    {
        //[ThreadStatic]
        //private static IWebDriver _driver = Driver.CurrentDriver;
        public static   IWebDriver _driver;


        /*Web element API to highlight the element of web application
         Paratmeters: IWebElement
         Return : None 
         Author : Sheetal K
         Example : */

        public static void WeHighlightElement(this IWebElement element)
        {
            var js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript(weHighlightedColour, element);
        }

        /*Web element API to check the element is enabled of web application
        Paratmeters: IWebElement,integer
        Return : Boolean 
        Author : Sheetal K
        Example : */

        public static bool WeElementEnabled(this IWebElement element,int sec =10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(sec));
            return wait.Until(d =>
            {
                try
                {
                    element.WeHighlightElement();
                    return element.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }

        /*Web element API to select dropdown by index the element of web application
        Paratmeters: IWebElement,string,integer
        Return : None 
        Author : Sheetal K
        Example : */

        public static void WeSelectDropdownOptionByIndex(this IWebElement element,string text,int sec=10)
        {
            element.WeElementEnabled(sec);
            new SelectElement(element).SelectByText(text);
        }

        /*Web element API to select dropdown by text the element of web application
        Paratmeters: IWebElement,string,integer
        Return : None 
        Author : Sheetal K
        Example : */
        public static void WeSelectDropdownOptionByText(this IWebElement element,string text,int sec=10)
        {
            element.WeElementEnabled(sec);
            new SelectElement(element).SelectByText(text);
        }


        /*Web element API to select dropdown by value the element of web application
        Paratmeters: IWebElement,string,integer
        Return : None 
        Author : Sheetal K
        Example : */
        public static void WeSelectDrodownOptionByValue(this IWebElement element,string value,int sec = 10)
        {
            element.WeElementEnabled(sec);
            new SelectElement(element).SelectByValue(value);
        }


        /*Web element API to get the attribute of the element of web application
        Paratmeters: IWebElement,string
        Return : String 
        Author : Sheetal K
        Example : */
        public static string WeGetAttribute(this IWebElement element,string attribute)
        {
            return element.GetAttribute(attribute);
            
        }
        
        /*Web element API to get the text displayed on the element of web application
        Paratmeters: IWebElement
        Return : String 
        Author : Sheetal K
        Example : */
        public static string WeGetText(this IWebElement element)
        {
            return element.Text;

        }


        public static bool WeElementIsDisplayed(this IWebElement element, int sec = 10)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(sec));
            return wait.Until(d =>
            {
                try
                {
                    element.WeHighlightElement();
                    return element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });

        }

        public static void WeSendKeys(this IWebElement element,string keys,int sec = 10, bool clearFirst =false)
        {
            element.WeElementIsDisplayed(sec);
            if (clearFirst) element.Clear();
            element.SendKeys(keys);
        }

        public static void WeElementToBeClickable(this IWebElement element,int sec = 10)
        {
            var wait = new WebDriverWait(_driver,TimeSpan.FromSeconds(sec));
            wait.Until(c => element.Enabled);
        }

        public static void WeClick(this IWebElement element, int sec = 10)
        {
            element.WeElementToBeClickable(sec);
            element.WeHighlightElement();
            element.Click();
        }
        public static void WeSwitchTo(this IWebElement iframe,int sec =10)
        {
            iframe.WeElementToBeClickable(sec);
            iframe.WeHighlightElement();
            _driver.SwitchTo().Frame(iframe);
        }

        public static void ClickElement(this IWebElement element, IWebDriver _driver, int Time)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(Time)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
            element.Click();
            //try
            //{
            //    new WebDriverWait(_driver, TimeSpan.FromSeconds(Time)).Until(ExpectedConditions.ElementToBeClickable(element));
            //    element.Click();
            //}
            //catch
            //{
            //    new WebDriverWait(_driver, TimeSpan.FromSeconds(Time)).Until(ExpectedConditions.ElementToBeClickable(element));
            //    element.Click();
            //}
        }

        public static void JavaScriptClick(this IWebElement webElement, IWebDriver _driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript("arguments[0].click();", webElement);
        }

    }
}
