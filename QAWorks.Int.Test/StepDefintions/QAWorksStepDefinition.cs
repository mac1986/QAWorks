using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace QAWorks.Int.Test
{
    [Binding]
    public sealed class QAWorksStepDefinition
    {
        private static WebDriverWrapper _webDriver ;

        [BeforeScenario]
        public static void SetUpWebDriver()
        {
            _webDriver = new WebDriverWrapper();
        }

        [AfterScenario]
        public static void destroyScenarioWebDriver()
        {
            try
            {
                _webDriver.Close();
                _webDriver = null;
            }
            catch
            {
            }
        }

        [After]
        public static void destroyWebDriver()
        {
            try
            {
                _webDriver.Close();
                _webDriver = null;
            }
            catch
            {
            }
        }

        [Given(@"I am on the QAWorks Site")]
        public void GivenIAmOnTheQAWorksSite()
        {
            var url = "http://qaworks.com/contact.aspx";
            _webDriver.OpenURL(url);
            _webDriver.SetPageMap(url);
            _webDriver.WaitForPageToLoad("Contact Us - QAWorks");
        }

        [Then(@"I should be able to contact QAWorks with the following information")]
        public void ThenIShouldBeAbleToContactQAWorksWithTheFollowingInformation(Table table)
        {
            var element = SetFieldValues(table);
            _webDriver.Submit();
        }

        [When(@"I try to contact QAWorks with the following information")]
        public void WhenITryToContactQAWorksWithTheFollowingInformation(Table table)
        {
            var element = SetFieldValues(table);
            _webDriver.Submit();
        }

        [Then(@"The page title changes to '(.*)'")]
        public void ThenThePageTitleChangesTo(string newPageTitle)
        {
            _webDriver.WaitForPageToLoad(newPageTitle);
        }

        public object SetFieldValues(Table table)
        {
            object element = null;
            foreach (var row in table.Rows)
            {
                var friendlyFieldName = row[0];
                var valueToSet = row[1];

                element = _webDriver.FindElementByFriendlyName(friendlyFieldName);
                _webDriver.SetText(element, valueToSet);
            }
            return element;
        }

        [Then(@"I get the validation errors")]
        public void ThenIGetTheValidationErrors(Table table)
        {
            object element = null;
            foreach (var row in table.Rows)
            {
                var friendlyFieldName = row[0];
                var valueToCompare = row[1];

                element = _webDriver.FindElementByFriendlyName(friendlyFieldName);
                Assert.AreEqual( valueToCompare, _webDriver.GetText(element), "Validation error not correct for "+ friendlyFieldName);
            }
        }

    }
}
