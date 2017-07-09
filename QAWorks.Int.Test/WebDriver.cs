using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Threading;

namespace QAWorks.Int.Test
{
    public class WebDriverWrapper
    {
        private IWebDriver _IEWebDriver;
        private PageMap _pageMap = null;
        public WebDriverWrapper()
        {
            _IEWebDriver = new OpenQA.Selenium.IE.InternetExplorerDriver();
        }

        public bool OpenURL(string url)
        {
            _IEWebDriver.Url = url ;
            return true;
        }

        internal void Close()
        {
            _IEWebDriver.Close();
        }

        internal void SetPageMap(string url)
        {
            //Does the url start http://
            var starturl = url.IndexOf("://") ;
            if (starturl > 0)
                starturl = starturl + 3;
            var filename = ".\\content\\" + url.Substring(starturl).Replace('/', '.').Replace('\\', '.')+".XML";

            XmlSerializer serializer = new XmlSerializer(typeof(PageMap));
            using (StreamReader reader = new StreamReader(filename))
            {
                _pageMap = (PageMap)serializer.Deserialize(reader);
                reader.Close();
            }
        }

        public object FindElementByFriendlyName(string friendlyName)
        {
            var selector = _pageMap.CSSSelectors.First(c => c.FriendlyFieldName == friendlyName);

            IWebElement element = null;
            switch (selector.CSSSelectorType.ToLower())
            {
                case "id":
                    element = _IEWebDriver.FindElement(By.Id(selector.CSSSelectorValue));
                    break;
                    //Implement other selector Types here as needed
                default:
                    break;
            }
            if (element == null)
                throw new InternalTestFailureException("CSS Selector Type not recognised");

            return element;
        }

        public object SetText(object elementObj, string text)
        {
            if (elementObj != null)
            {
                var element = elementObj as IWebElement;
                if (string.IsNullOrEmpty(text))
                    element.Clear();
                else
                    element.SendKeys(text);
            }
            else
            {
                throw new InternalTestFailureException("Textual element not found");
            }

            return elementObj;
        }

        internal string GetText(object elementObj)
        {
            if (elementObj != null)
            {
                var element = elementObj as IWebElement;
                if (element.GetCssValue("visibility") != "hidden")
                {
                    return element.Text;
                }
            }
            throw new InternalTestFailureException("Textual element not found");
        }

        public bool Submit()
        {
            var windowHandle = _IEWebDriver.CurrentWindowHandle;
            var element = FindElementByFriendlyName("submit") as IWebElement;
            if (element != null)
            {
                element.Click();
                Thread.Sleep(1000);
            }
            else
            {
                throw new InternalTestFailureException("Submission button not found");
            }
            return true;
        }

        public void WaitForPageToLoad(string expextedPageTitle)
        {            
            var wait = new WebDriverWait(_IEWebDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.StartsWith(expextedPageTitle, StringComparison.OrdinalIgnoreCase));
        }
    }
}
