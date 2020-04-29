using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using SelUICore.Utils.Selenium;
using SelUICore.Utils.Hooks;
using SelUICore.Utils.Extensions;


namespace SelUICore.Utils.Selenium
{
    public static class Settings
    {
        
        public static string weHighlightedColour = "arguments[0].style.border='5px solid green'";
        public static string baseUrl;
        public static string _offerCode = DateTime.Now.ToString("dd/MM/yyyy hh:mm");
        public static string _toDay = DateTime.Now.ToString("dd/MM/yyyy");
        
        public static int LongTimeout;
        public static int MediumTimeout;
        public static int ShortTimeout;

        [SetUp]
        public static void SetupTest(string ProjectConfigFilePath)
        {
            
            //Driver.InitChrome();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ProjectConfigFilePath);
            foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
            {
                string TagName = xmlNode.Name;
                switch (TagName)
                {
                    case "HostUrl":
                        baseUrl = xmlNode.InnerText;
                        break;
                    case "LongTimeout":
                        LongTimeout = Int16.Parse(xmlNode.InnerText);
                        break;
                    case "MediumTimeout":
                        MediumTimeout = Int16.Parse(xmlNode.InnerText);
                        break;
                    case "ShortTimeout":
                        ShortTimeout = Int16.Parse(xmlNode.InnerText);
                        break;

                }

            }



        }
        public static void SetupScenario()
        {
            WebElementExtensions._driver = Driver.CurrentDriver;
            WebDriverExtensions._driver = Driver.CurrentDriver;
        }
        public static void Teardown()
        {
            TestRunHooks.AfterTestRun();
        }



    }  
}


