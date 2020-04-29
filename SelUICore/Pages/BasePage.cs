using System;
using System.Collections.Generic;
using System.Text;
using SelUICore.Utils.Selenium;
using OpenQA.Selenium;
using static SelUICore.Utils.Selenium.Settings;
using NUnit.Framework;



namespace SelUICore.Pages
{
    public static  class BasePage
    {
        
        
        /*API to navigate to the base URL of the web application
         Paratmeters: None
         Return : None 
         Author : Sheetal K*/

        public static void NavigateBaseUrl()
        {
            Driver.CurrentDriver.Navigate().GoToUrl(baseUrl);
            Driver.CurrentDriver.Manage().Window.Maximize();
            
        }

        /*API to validate page tilte of the web application
         Paratmeters: String
         Return : None 
         Author : Sheetal K*/
        public static void ValidatePageTitle(string expectedTitle)
        {
            var titleToValidate = GetTitle();
           
            Assert.IsTrue(titleToValidate.Contains(expectedTitle), " ::The actual page title is incorrect");
        }

        /*API to get the page title of the web application
         Paratmeters: None
         Return : None 
         Author : Sheetal K*/

        public static string GetTitle()
        {
            return Driver.CurrentDriver.Title;
        }

        /*API to get the URL of the web application
        Paratmeters: None
        Return : None 
        Author : Sheetal K*/

        public static string GetUrl()
        {
            return Driver.CurrentDriver.Url;
        }

        /*API to get the page source of the web application
        Paratmeters: None
        Return : None 
        Author : Sheetal K*/

        public static string GetPageSource()
        {
            return Driver.CurrentDriver.PageSource;
        }
    }
}
