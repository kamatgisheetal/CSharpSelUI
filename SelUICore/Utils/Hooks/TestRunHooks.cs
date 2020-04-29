using System;
using System.Collections.Generic;
using System.Text;
using SelUICore.Utils.Selenium;
using TechTalk.SpecFlow;

namespace SelUICore.Utils.Hooks
{
    internal static class TestRunHooks
    {
        [AfterTestRun]
        internal static void AfterTestRun()
        {
            Driver.CurrentDriver.Quit();
        }


    }
}
