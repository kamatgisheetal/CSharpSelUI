using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using SelUICore.Utils.Selenium;

namespace SelUICore.Utils.Hooks
{
    [Binding]
    public static class SpecFlowHooks
    {
        [Before]
        [Scope (Tag = "Chrome")]
        public static void StartChromeDriver()
        {
            Driver.InitChrome();
        }
        [After]
        public static void StopWebDriver()
        {
            //Driver.CurrentDriver.Quit();
            //Driver.CurrentDriver.Close();
        }

        
    }
}
