using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;

namespace SeleniumAutoFramework.Base
{
    //CLASS FOR HANDLING DRIVER CONTEXT AND CURRENT PAGES
    public class ParallelConfig
    {
        //FOR HANDLING DRIVER CONTEXT 
        public IWebDriver Driver { get; set; }

        //FOR HANDLING CURRENT PAGE CONTEXT
        public BasePage CurrentPage { get; set; }

        //FOR HANDLING SCREENSHOT CAPTURE 
        public MediaEntityModelProvider CaptureScreenshotAndReturnModel(string Name)
        {
            string screenshot = "";
            try
            {
                screenshot = ((ITakesScreenshot)Driver).GetScreenshot().AsBase64EncodedString;
                return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, Name).Build();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
