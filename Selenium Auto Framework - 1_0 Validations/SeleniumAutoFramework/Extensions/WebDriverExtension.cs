
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;

namespace SeleniumAutoFramework.Extensions;
public static class WebDriverExtension
{
    /// <summary>
    /// Used to take screenshot during execution and attach it to the extent report
    /// </summary>
    public static void CaptureScreen(this IWebDriver Driver, ScenarioContext _scenarioContext)
    {
        ((ExtentTest)_scenarioContext["StepNode"]).AddScreenCaptureFromBase64String(((ITakesScreenshot)Driver).GetScreenshot().AsBase64EncodedString);
        LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "Screenshot captured");
    }
    /// <summary>
    /// Launch the given URL to the browser
    /// </summary>
    public static void LaunchUrl(this IWebDriver Driver, string url)
    {
        try
        {
            Driver.Navigate().GoToUrl(url);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"NAVIGATING URL [{url}]");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Validate the page is displayed or not by verifying URL,Title and one WebElement from that page
    /// </summary>
    public static void PageValidate(this IWebDriver Driver, string url, string title, By elementTOfind, [CallerArgumentExpression("elementTOfind")] string locator = null)
    {
        var className = new StackTrace(1).GetFrame(0).GetMethod().ReflectedType.Name;
        Driver.WaitForNextPage();
        if (title != "none")
            Assert.IsTrue(Driver.WaitForTitleContains(title), $"Title  NOT matched in [{className}]");
        else LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{className}] [TITLE] not validated !");
        if (url != "none")
            Assert.IsTrue(Driver.Url.Contains(url), $"Url not matched in [{className}]");
        else LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{className}] [URL] not validated !");
        Assert.IsTrue(Driver.WaitForElementToPresent(elementTOfind, locator: locator), $"[{className}] is not displayed");
        LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{className}] is displayed");
    }
    /// <summary>
    /// Wait till the URL contains given string.
    /// </summary>
    public static void WaitTillUrlContains(this IWebDriver Driver, string expectedURL)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(300));
            wait.Until(ExpectedConditions.UrlContains(expectedURL));
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Wait Completed for [URL] contains [{expectedURL}]");
        }
        catch (TimeoutException)
        {
            Assert.Fail("Wait Timeout Exceeds");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Returns the selected value from the dropdown.
    /// </summary>
    public static string GetSelectedValueInDropDown(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            IWebElement element = Driver.WaitForElement(by);
            SelectElement DrpDownList = new(element);
            string value = DrpDownList.AllSelectedOptions.FirstOrDefault().ToString();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Selected value in [{locator.Replace("slct", "")}] is [{value}]");
            return value;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Can't able to get selected value from dropdown \"{locator.Replace("slct", "")}\"--\n" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// Returns the selected text from the dropdown.
    /// </summary>
    public static string GetSelectedTextValueInDropDown(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            IWebElement element = Driver.WaitForElement(by);
            SelectElement DrpDownList = new(element);
            string text = DrpDownList.AllSelectedOptions.FirstOrDefault().Text;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Selected text in [{locator.Replace("slct", "")}] is [{text}]");
            return text;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Can't able to get selected value from dropdown \"{locator.Replace("slct", "")}\"--\n" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// Returns the list of all selected options from the dropdown.
    /// </summary>
    public static IList<IWebElement> GetDropDownListValues(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            var element = Driver.WaitForElement(by);
            SelectElement DrpDownList = new(element);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"List of all selected value in [{locator.Replace("slct", "")}] is found");
            return DrpDownList.AllSelectedOptions;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Can't able to get values from dropdown \"{locator.Replace("slct", "")}\"--\n" + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Returns text from each option in select tag
    /// </summary>
    public static string[] GetAllTextFromSelectTag(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            var element = Driver.WaitForElement(by);
            SelectElement DrpDownList = new(element);
            var options = DrpDownList.Options;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Text from each option in [{locator.Replace("slct", "")}] select tag is found");
            string[] text = new string[DrpDownList.Options.Count];
            for (int i = 0; i < options.Count; i++)
            {
                text[i] = options[i].Text;
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"{i + 1}. {text[i]}");
            }
            return text;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Can't able to get text from each option tags \"{locator.Replace("slct", "")}\"--\n" + ex.Message);
            return null;
        }
    }

    /// <summary>
    /// Wait till overlaying element to disappear in page
    /// </summary>
    public static void OverLayToDisappear(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(600));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Overlaying element [{locator.Replace("img", "")}] disappeared");
        }
        catch (TimeoutException)
        {
            Assert.Fail($"Overlay Timeout for \"{locator.Replace("img", "")}\"");
        }
        catch (Exception ex)
        {
            Assert.Fail($"overlay wait failed for \"{locator.Replace("img", "")}\"--\n" + ex.Message);
        }
    }
    /// <summary>
    /// Wait till overlay to disappear, At first it check the overlaying element is presnet or not, 
    /// If present then wait till the overlaying element to disappear.
    /// </summary>
    public static void WaitTillOverlayDisappears(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(3));
            bool overlayPresent = wait.Until(ExpectedConditions.ElementIsVisible(by)).Displayed;
            if (overlayPresent)
            {
                wait = new(Driver, TimeSpan.FromSeconds(300));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Overlaying element [{locator.Replace("img", "")}] disappeared");
            }
            else
            {
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Overlaying element [{locator.Replace("img", "")}] not appeared");
            }
        }
        catch (Exception)
        {

        }
    }
    /// <summary>
    /// Sending an empty response to server wait for it to receive, so that we can confirm previous call was finished
    /// </summary>
    public static void WaitForResponse(this IWebDriver Driver)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(120));
            string Url = Driver.Url;
            string response = "var xhr=new XMLHttpRequest();" +
                 "xhr.open('GET','" + Url + "', false);" +
                 "xhr.send(null);" +
                 "return xhr.status;";
            Thread.Sleep(500);
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript(response));
            Thread.Sleep(500);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "Wait for response done");
        }
        catch (Exception)
        {
            Console.WriteLine("Respone Was Failed");
        }
    }
    /// <summary>
    /// Wait for the page load to completely
    /// </summary>
    /// <returns>True, If waiting is completed</returns>
    public static bool WaitForNextPage(this IWebDriver Driver)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(120));
            Stopwatch sw = new();
            bool result = false;
            int tries = 0;
            for (tries = 0; tries <= 3 && sw.Elapsed <= TimeSpan.FromSeconds(1); tries++)
            {
                sw.Start();
                try { result = wait.Until(Driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete")); }
                catch { }
                sw.Stop();
            }
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "Page loading complete");
            return result;
        }
        catch (Exception ex)
        {
            Assert.Fail("wait for next page failed --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Clicks the WebElement, will wait if the element is not clickable 
    /// </summary>
    /// <returns>True, If the webelement is clicked </returns>
    public static bool WaitAndClick(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(new SystemClock(), Driver, TimeSpan.FromSeconds(20.0), TimeSpan.FromMilliseconds(25.0));
            wait.Until(ExpectedConditions.ElementToBeClickable(by)).Click();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator.Replace("btn", "")}] button is clicked");
            return true;
        }
        catch (Exception ex)
        {
            //Assert.Fail($"Element \"{locator.Replace("btn", "")}\" was NOT clicked --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Clicks the webelement, will wait if the element is not clickable 
    /// </summary>
    /// <returns>True, If the webelement is clicked </returns>
    public static bool WaitAndClick(this IWebDriver Driver, IWebElement element, [CallerArgumentExpression("element")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(new SystemClock(), Driver, TimeSpan.FromSeconds(20.0), TimeSpan.FromMilliseconds(25.0));
            wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator.Replace("btn", "")}] button is clicked");
            return true;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element \"{locator.Replace("btn", "")}\" was NOT clicked --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Highlights the WebElement
    /// </summary>
    public static void Highlight(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementExists(by));

            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", element);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator}] highlighted");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Field \"{locator}\" is Not Interactable --\n" + ex.Message);
        }
    }
    /// <summary>
    /// Unhighlights the WebElement
    /// </summary>
    public static void UnHighlight(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(1));
            var element = wait.Until(ExpectedConditions.ElementExists(by));

            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].setAttribute('style', 'background: ; border: 0px;');", element);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator}] unhighlighted");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Field \"{locator}\" is Not Interactable --\n" + ex.Message);
        }
    }
    /// <summary>
    /// Clicks the webelement using javascript executor, will wait if the element is not clickable 
    /// </summary>
    /// <returns>True, If the webelement is clicked </returns>
    public static void JSClick(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(new SystemClock(), Driver, TimeSpan.FromSeconds(20.0), TimeSpan.FromMilliseconds(25.0));
            var element = wait.Until(ExpectedConditions.ElementExists(by));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)Driver;
            jse.ExecuteScript("arguments[0].click()", element);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator.Replace("btn", "")}] button is clicked");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element \"{locator}\" is Not clicked --\n" + ex.Message);
        }
    }
    /// <summary>
    /// Clicks the webelement using javascript executor, will wait if the element is not clickable 
    /// </summary>
    /// <returns>True, If the webelement is clicked </returns>
    public static void JSClick(this IWebDriver Driver, IWebElement element, [CallerArgumentExpression("element")] string locator = null)
    {
        try
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)Driver;
            jse.ExecuteScript("arguments[0].click()", element);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator.Replace("btn", "")}] button is clicked");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element \"{locator}\" is Not clicked --\n" + ex.Message);
        }
    }
    /// <summary>
    /// Clears the field by backspacing and send text to webelement and then press tab key
    /// </summary>
    /// <returns>True,If text and tab entered</returns>
    public static bool ClearAndSendTextAndPressTab(this IWebDriver Driver, By by, string str, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            while (!element.GetAttribute("value").Equals(""))
            {
                element.SendKeys(Keys.Backspace);
            }
            element.SendKeys(str);
            element.SendKeys(Keys.Tab);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Clear & Entered [{str}] to text field [{locator.Replace("txt", "")}] and pressed Tab");
            return true;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Can't able to send input and tab for element \"{locator}\" --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Clears the field and send text to webelement
    /// </summary>
    /// <returns>True,If text entered</returns>
    public static bool ClearAndSend(this IWebDriver Driver, By by, string str, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(new SystemClock(), Driver, TimeSpan.FromSeconds(20.0), TimeSpan.FromMilliseconds(25.0));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            element.Clear();
            element.SendKeys(str);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Clear & Entered [{str}] to text field [{locator.Replace("txt", "")}]");
            return true;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Can't able to send input and tab for element \"{locator.Replace("txt", "")}\" --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Send text to webelement 
    /// </summary>
    /// <returns>True,If text</returns>
    public static bool SendKeysOrText(this IWebDriver Driver, By by, string str, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(new SystemClock(), Driver, TimeSpan.FromSeconds(20.0), TimeSpan.FromMilliseconds(25.0));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            element.SendKeys(str);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Entered [{str}] to text field [{locator.Replace("txt", "")}]");
            return true;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Can't able to send input for element \"{locator.Replace("txt", "")}\" --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Change the value present in value attribute in html
    /// </summary>
    /// <returns>True, If value set</returns>
    public static bool SetForValueAttribute(this IWebDriver Driver, By by, string str, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript($"arguments[0].setAttribute('value','{str}');", WaitForElement(Driver, by));
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator}] value attribute is changed to [{str}]");
            return true;
        }
        catch (Exception ex)
        {
            Assert.Fail($"can't able to set value for attribute to element \"{locator}\" --\n " + ex.Message);
            return false;
        }

    }
    /// <summary>
    /// Clicks the Radio button
    /// </summary>
       public static void ClickRadioButton(this IWebDriver Driver, By by, string buttonName, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            IList<IWebElement> element = Driver.ListOfElements(by);
            bool IsClicked = false;
            for (int i = 0; i <= 1; i++)
            {
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "Radio button Value in Application: " + element[i].Text);
                if (element[i].Text == buttonName)

                {
                    Driver.MoveToTheElementAndClick(element[i]);
                    IsClicked = true;
                    LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Radio button [{locator.Replace("rb", "")}] : [{buttonName}] is clicked");
                    break;
                }
            }
            if (!IsClicked)
            {
                Assert.Fail($"Please Check Button Name");
            }
        }
        catch (Exception ex)
        {
            Assert.Fail($"click radio button failed for \"{locator}\" with text \"{buttonName}\" --\n" + ex.Message);
        }
    }
    /// <summary>
    /// Scrolls the element to center of the page.
    /// </summary>
    public static void ScrollToCenter(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({block:'center' , inline: 'nearest'});", WaitForElement(Driver, by));
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Scrolled to [{locator}]");
        }
        catch (Exception ex)
        {
            Assert.Fail($"scroll to center failed for \"{locator}\"--\n " + ex.Message);
        }
    }
    /// <summary>
    /// Scroll the webpage by pixel, use positive value for down scroll or wise-versa.
    /// </summary>
    public static void ScrollByPixel(this IWebDriver Driver, int pixel)
    {
        try
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollBy(0," + pixel + ")");
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Scrolled by [{pixel}] pixel");
        }
        catch (Exception ex)
        {
            Assert.Fail($"scroll by pixel failed --\n " + ex.Message);
        }
    }
    /// <summary>
    /// Scroll to bottom of the page
    /// </summary>
    /// <param name="Driver"></param>
    public static void ScrollToBottom(this IWebDriver Driver)
    {
        try
        {
            long scrollHeight = 0;
            do
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                if (newScrollHeight == scrollHeight)
                {
                    break;
                }
                else
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(4000);
                }
            } while (true);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Scrolled to bottom");
        }
        catch (Exception ex)
        {
            Assert.Fail("scroll to bottom failed --\n" + ex.Message);
        }
    }
    /// <summary>
    /// Mouse hover to webelement and then double click
    /// </summary>
    public static void MoveToTheElementAndDoubleClick(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            Actions act = new(Driver);
            act.MoveToElement(element).Build().Perform();
            act.DoubleClick(element).Build().Perform();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator.Replace("btn", "")}] is double clicked");
        }
        catch (NoSuchElementException)
        {
            Assert.Fail($"No Such Element -- \"{locator}\"");
        }
        catch (Exception ex)
        {
            Assert.Fail($"double click failed for \"{locator}\" --\n" + ex.Message);
        }
    }
    /// <summary>
    /// Mouse hover to webelement and then click
    /// </summary>
    public static void MoveToTheElementAndClick(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            Actions act = new(Driver);
            act.MoveToElement(element).Build().Perform();
            act.Click(element).Build().Perform();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Mouse clicked on [{locator.Replace("btn", "")}] ");
        }
        catch (NoSuchElementException)
        {
            Assert.Fail($"No Such Element -- \"{locator}\"");
        }
        catch (Exception ex)
        {
            Assert.Fail($"move to element and click failed for  \"{locator}\" --\n" + ex.Message);
        }
    }
    /// <summary>
    /// Mouse hover to webelement and then click
    /// </summary>
    public static void MoveToTheElementAndClick(this IWebDriver Driver, IWebElement element, [CallerArgumentExpression("element")] string locator = null)
    {
        try
        {
            Actions act = new(Driver);
            act.MoveToElement(element).Build().Perform();
            act.Click(element).Build().Perform();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Mouse clicked on [{locator.Replace("btn", "")}] ");
        }
        catch (NoSuchElementException)
        {
            Assert.Fail($"No Such Element --\"{locator}\"");
        }
        catch (Exception ex)
        {
            Assert.Fail($" Mouse click failed for element \"{locator}\" --\n" + ex.Message);
        }
    }
    /// <summary>
    /// Mouse hover to webelement
    /// </summary>
    public static void MoveToTheElement(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            Actions act = new(Driver);
            act.MoveToElement(element).Build().Perform();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Move to [{locator.Replace("btn", "")}]");
        }
        catch (NoSuchElementException)
        {
            Assert.Fail($"No Such Element --\"{locator}\"");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Move to the element \"{locator}\" failed  --\n " + ex.Message);
        }
    }
    /// <summary>
    /// Mouse hover to webelement
    /// </summary>
    public static void MoveToTheElement(this IWebDriver Driver, IWebElement element, [CallerArgumentExpression("element")] string locator = null)
    {
        try
        {
            Actions act = new(Driver);
            act.MoveToElement(element).Build().Perform();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Move to [{locator}]");
        }
        catch (NoSuchElementException)
        {
            Assert.Fail($"No Such Element --\"{locator}\"");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Move to the element \"{locator}\" failed  --\n " + ex.Message);
        }
    }
    /// <summary>
    /// Choose webelement from select tag by using text
    /// </summary>
    public static void SelectValueFromDropDownListUsingText(this IWebDriver Driver, By by, string value, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            SelectElement DrpDownList = new(element);
            DrpDownList.SelectByText(value);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Selected text [{value}] from dropdown [{locator.Replace("slct", "")}]");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Failed to select from drop down \"{locator.Replace("slct", "")}\" with text \"{value}\"" + ex.Message);
        }
    }
    /// <summary>
    /// Choose webelement from select tag by using index
    /// </summary>
    public static void SelectValueFromDropDownListUsingIndex(this IWebDriver Driver, By by, int value, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            SelectElement DrpDownList = new(element);
            DrpDownList.SelectByIndex(value);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Selected index [{value}] from dropdown [{locator.Replace("slct", "")}]");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Failed to select from drop down \"{locator.Replace("slct", "")}\" with index \"{value}\"" + ex.Message);
        }
    }
    /// <summary>
    /// Choose webelement from select tag by using value
    /// </summary>
    public static void SelectValueFromDropDownListUsingValue(this IWebDriver Driver, By by, string value, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            SelectElement DrpDownList = new(element);
            DrpDownList.SelectByValue(value);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Selected value [{value}] from dropdown [{locator.Replace("slct", "")}]");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Failed to select from drop down \"{locator.Replace("slct", "")}\" with value \"{value}\"" + ex.Message);
        }
    }
    /// <summary>
    /// Check if webelement is displayed or not under given time
    /// </summary>
    /// <returns>True, If the webelement is displayed</returns>
    public static bool ElementDisplayed(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            bool displayed = element.Displayed;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator}] displayed [{displayed}]");
            return displayed;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    /// <summary>
    /// Check if webelement is selected or not under given time
    /// </summary>
    /// <returns>True, If the webelement is selected</returns>
    public static bool ElementSelected(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            bool IsSelected = element.Selected;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator}] selected [{IsSelected}]");
            return IsSelected;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element \"{locator}\" Not selected --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Check if webelement is enabled or not under given time
    /// </summary>
    /// <returns>True, If the webelement is enabled</returns>
    public static bool ElementEnabled(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            bool IsEnabled = element.Enabled;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{locator}] enabled [{IsEnabled}]");
            return IsEnabled;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element \"{locator}\" Not enabled --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Finds the attribute value of a weblement for given attribute
    /// </summary>
    public static string GetAttribute(this IWebDriver Driver, By by, string attributeName, [CallerArgumentExpression("by")] string locator = null)
    {
        IWebElement element = null;
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(30));
            element = wait.Until(ExpectedConditions.ElementExists(by));
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element \"{locator}\" is Not Present --\n" + ex.Message);
            return null;
        }
        try
        {
            string value = element.GetAttribute(attributeName);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"For [{locator}] : Attribute: [{attributeName}] :: Value: [{value}]");
            return value;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Get attribute failed for \"{locator}\" --\n" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// Wait for a webelement to get refreshed 
    /// </summary>
    public static void WaitForStale(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.StalenessOf(Driver.FindElement(by)));
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Stale completed for [{locator}]");
        }
        catch (Exception)
        {
        }
    }
    /// <summary>
    /// Finds list of webelements with matching locator
    /// </summary>
    /// <returns> List of webelements</returns>
    public static IList<IWebElement> ListOfElements(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            var elements = Driver.FindElements(by);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Elements found for [{locator}]");
            return elements;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Elements Not Present for locator \"{locator}\"--\n" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// Get texts from all webelements located by given locator
    /// </summary>
    /// <returns>Texts found from each webelement</returns>
    public static string[] GetTextsFromList(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            IList<IWebElement> element = Driver.ListOfElements(by);
            string[] txt = new string[element.Count];
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Text from List [{locator}] are below :");
            for (int i = 0; i < txt.Length; i++)
            {
                txt[i] = element[i].Text;
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[{txt[i]}");
            }
            return txt;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Get text from list \"{locator}\" failed  __\n" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// Wait for title to contain given string
    /// </summary>
    /// <returns></returns>
    public static bool WaitForTitleContains(this IWebDriver Driver, string title)
    {
        try
        {
            WebDriverWait wait = new(new SystemClock(), Driver, TimeSpan.FromSeconds(20.0), TimeSpan.FromMilliseconds(25.0));
            bool result = wait.Until(ExpectedConditions.TitleContains(title));
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Title contains [{title}] : [{result}] ");
            return result;
        }
        catch
        {
            try
            {
                Driver.WaitForNextPage();
                WebDriverWait wait = new(new SystemClock(), Driver, TimeSpan.FromSeconds(20.0), TimeSpan.FromMilliseconds(25.0));
                bool result = wait.Until(ExpectedConditions.TitleContains(title));
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Title contains [{title}] : [{result}] ");
                return result;
            }
            catch (Exception ex)
            {
                Assert.Fail($"Ttile not contains \"{title}\" --\n" + ex.Message);
                return false;
            }
        }
    }
    /// <summary>
    /// Finds the webelement with given attribute and its value from list of webelements
    /// </summary>
    public static IWebElement GetElementFromListWithAttribute(this IWebDriver Driver, By by, string attributeName, string value, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            IList<IWebElement> lst = Driver.ListOfElements(by);
            IWebElement requiredElement = null;
            foreach (IWebElement item in lst)
            {
                if (item.GetAttribute(attributeName) == value)
                {
                    requiredElement = item;
                    break;
                }
            }
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element from list [{locator}] with attribute [{attributeName}] is found ");
            return requiredElement;
        }
        catch (Exception ex)
        {

            Assert.Fail($"Get Element From List \"{locator}\" with Attribute --\n" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// Check the checkbox
    /// </summary>
    public static bool CheckTheBox(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        var element = WaitForElement(Driver, by);
        try
        {
            if (!element.Selected)
            {
                element.Click();
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"CheckBox [{locator}] is checked");
                return true;
            }
            else
            {
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"CheckBox [{locator}] is already checked");
                return true;
            }
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element check failed for \"{locator}\"--\n" + ex.Message);
            return false;
        }
    }

    /// <summary>
    /// UnCheck the checkbox
    /// </summary>
    public static bool UncheckTheBox(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        var element = WaitForElement(Driver, by);
        try
        {
            if (element.Selected)
            {
                element.Click();
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"CheckBox [{locator}] is un-checked");
                return true;
            }
            else
            {
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"CheckBox [{locator}] is already un-checked");
                return true;
            }
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element check failed for \"{locator}\"--\n" + ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Wait for webelement to enable within the given time
    /// </summary>
    public static bool WaitForElementToEnable(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(60));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            bool IsEnabled = element.Enabled;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element [{locator}] to enable wait completed");
            return IsEnabled;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Wait for element \"{locator}\" to enable failed--\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Get the text present in the webelement
    /// </summary>
    public static string GetTextFromElement(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(7));
            string value = wait.Until(ExpectedConditions.ElementIsVisible(by)).Text;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element [{locator}] : Text [{value}]");
            return value;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Get text from element \"{locator}\" failed ! --\n " + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// Clicks the webelement from a dropdown with matching text
    /// </summary>
    public static bool SelectFromList(this IWebDriver Driver, By by, String matchingText, [CallerArgumentExpression("by")] string locator = null)
    {
        IList<IWebElement> lst = Driver.ListOfElements(by);
        try
        {
            bool IsSelected = false;
            foreach (IWebElement item in lst)
            {
                if (item.Text.ToLower().Equals(matchingText.ToLower()))
                {
                    Driver.WaitAndClick(item);
                    IsSelected = true;
                    break;
                }
            }
            //LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element from list [{locator}] with [{matchingText}] is selected");
            //if (!IsSelected)
            //{
            //    Assert.Fail($"Select from list \"{locator}\" with match text \"{matchingText}\" failed --\n");
            //}
            return IsSelected;
        }
        catch (Exception ex)
        {
            //Assert.Fail($"Select from list \"{locator}\" with match text \"{matchingText}\" failed --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Clicks the webelement from a dropdown with matching text using javascript executor
    /// </summary>
    public static bool SelectFromListUsingJS(this IWebDriver Driver, By by, string matchingText, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(20));
            var lst = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
            bool IsSelected = false;
            foreach (IWebElement item in lst)
            {
                if (item.Text.ToLower().Equals(matchingText.ToLower()))
                {
                    Driver.JSClick(item);
                    IsSelected = true;
                    LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element from list [{locator}] with [{matchingText}] is selected");
                    break;
                }
            }
            //if (!IsSelected)
            //{
            //    Assert.Fail($"Select from list \"{locator}\" with match text \"{matchingText}\" failed --\n");
            //}
            return IsSelected;
        }
        catch (Exception ex)
        {
            //Assert.Fail($"Select from list \"{locator}\" with match text \"{matchingText}\" failed --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Find and right click on the webelement
    /// </summary>
    public static void RightClickOnElement(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(by));
            Actions act = new(Driver);
            act.ContextClick(element).Build().Perform();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Right Clicked on [{locator}]");
        }
        catch (NoSuchElementException)
        {
            Assert.Fail($"No Such Element-- \"{locator}\"");
        }
        catch (Exception ex)
        {
            Assert.Fail($"Double click failed for element \"{locator}\"--\n" + ex.Message);
        }
    }
    /// <summary>
    /// Checks the URL contains given string
    /// </summary>
    public static bool VerifyUrlContains(this IWebDriver Driver, string expectedURL)
    {
        try
        {
            string actualURL = Driver.Url;
            if (actualURL.Contains(expectedURL))
            {
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"URL contains [{expectedURL}] is [true]");
                return true;
            }
            else
            {
                LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"URL contains [{expectedURL}] is [true]");
                return false;
            }
        }
        catch (Exception ex)
        {
            Assert.Fail($"Title Not contains {expectedURL} --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Opens the last downloaded file in current execution
    /// </summary>
    public static void OpenLastDownloadedFile(this IWebDriver Driver)
    {
        try
        {
            BrowserType browserType = (Config.Settings.Config_BrowserType);
            Driver.SwitchTo().NewWindow(WindowType.Tab);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Switched to new tab");
            IWebElement LastDownloadedFile;
            switch (browserType)
            {
                case BrowserType.Chrome:
                    Driver.Navigate().GoToUrl("chrome://downloads/");
                    Driver.WaitForNextPage();
                    LastDownloadedFile = (IWebElement)((IJavaScriptExecutor)Driver).ExecuteScript("return document.querySelector('downloads-manager').shadowRoot.querySelector('div iron-list downloads-item').shadowRoot.querySelector('[id=file-link]');");
                    LastDownloadedFile.Click();
                    LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Last downloaded file clicked");
                    break;
                case BrowserType.Edge:
                    Driver.Navigate().GoToUrl("edge://downloads/");
                    LastDownloadedFile = Driver.FindElement(By.XPath("//div[@class='downloads-list']//div[contains(@id,'downloads-item')]//button[contains(@id,'open_file')]"));
                    LastDownloadedFile.Click();
                    LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Last downloaded file clicked");
                    break;
                default:
                    break;
            }
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            Thread.Sleep(1000);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Close all tabs and windows except the last one
    /// </summary>
    public static void CloseAllTabs(this IWebDriver Driver)
    {
        try
        {
            while (Driver.WindowHandles.Count > 1)
            {
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                Driver.Close();
            }
            Driver.SwitchTo().Window(Driver.WindowHandles.First()); //TO GIVE CONTROL TO ORIGINAL TAB
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Tabs closed !");
        }
        catch (Exception ex)
        {
            Assert.Fail("Close all tab failed--\n" + ex.Message);
        }
    }
    /// <summary>
    /// Wait for webelement to display in webpage
    /// </summary>
    public static bool WaitForElementToPresent(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(60));
            bool IsDisplayed = wait.Until(ExpectedConditions.ElementIsVisible(by)).Displayed;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element [{locator}] present wait done ");
            return IsDisplayed;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Failed to wait for element to present \"{locator}\" failed--\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Finds the webelement with given locator
    /// </summary>
    public static IWebElement WaitForElement(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(30));
            IWebElement element = wait.Until(ExpectedConditions.ElementExists(by));
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element [{locator}] found");
            return element;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element \"{locator}\" is Not Present --\n" + ex.Message);
            return null;
        }
    }
    /// <summary>
    /// Wait for a webelement to be clickable
    /// </summary>
    public static bool WaitTillElementIsClickable(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(120));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element [{locator}] clickable wait done ");
            return true;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element Clickable wait failed for \"{locator}\" --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Wait for a webelement to invisible
    /// </summary>
    public static bool WaitTillElementIsInvisible(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(120));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element [{locator}] disappeared");
            return true;
        }
        catch (Exception ex)
        {
            Assert.Fail($"Element visible wait failed for \"{locator}\" --\n" + ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Switch to alert popup
    /// </summary>
    /// <param name="Driver"></param>
    public static void SwitchToAlert(this IWebDriver Driver)
    {
        try
        {
            IAlert alert = Driver.SwitchTo().Alert();
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Accepts alert popup
    /// </summary>
    public static void AcceptAlert(this IWebDriver Driver)
    {
        string text = "";
        try
        {
            IAlert alert = Driver.SwitchTo().Alert();
            text = alert.Text;
            alert.Accept();
            Console.WriteLine("The alert " + text + " is accepted");
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "Alert accepted");
        }
        catch (NoAlertPresentException)
        {
            Console.WriteLine("There is No Alert Present");
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "No alert present !");
        }
        catch (WebDriverException ex)
        {
            Console.WriteLine("WebDriverException : " + ex.Message);
        }
    }
    /// <summary>
    /// Dismiss alert popup
    /// </summary>
    public static void DismissAlert(this IWebDriver Driver)
    {
        try
        {
            IAlert alert = Driver.SwitchTo().Alert();
            string text = alert.Text;
            alert.Dismiss();
            Console.WriteLine("The alert " + text + " is dismissed");
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Alert dismissed");
        }
        catch (NoAlertPresentException)
        {
            Console.WriteLine("There is No Alert Present");
        }
        catch (WebDriverException ex)
        {
            Console.WriteLine("WebDriverException : " + ex.Message);
        }
    }
    /// <returns>Text content present in alert popup</returns>
    public static string GetAlertText(this IWebDriver Driver)
    {
        string text = "";
        try
        {
            IAlert alert = Driver.SwitchTo().Alert();
            text = alert.Text;
            Console.WriteLine("The alert text is " + text);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "The alert text is " + text);
        }
        catch (NoAlertPresentException)
        {
            Console.WriteLine("There is No Alert Present");
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), "There is No Alert Present");
        }
        catch (WebDriverException ex)
        {
            Console.WriteLine("WebDriverException : " + ex.Message);
        }
        return text;
    }
    /// <summary>
    /// Enter text to alert popup input field
    /// </summary>
    public static void TypeAlert(this IWebDriver Driver, string data)
    {
        try
        {
            Driver.SwitchTo().Alert().SendKeys(data);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Entered [{data}] to Alert");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Driver controll moves to tab or window with given title
    /// </summary>
    public static void SwitchToWindow(this IWebDriver Driver, string title)
    {
        try
        {
            IList<string> allWindows = new List<string>(Driver.WindowHandles);
            string mainwindow = Driver.CurrentWindowHandle;
            foreach (string eachWindow in allWindows)
            {
                Driver.SwitchTo().Window(eachWindow);
                if (Driver.Title.Equals(title))
                {
                    Console.WriteLine("Switched to the Window with Title: " + title);
                    LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Switched to tab with title[{title}]");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Window with Title: " + title + " not found " + ex.Message);
        }
    }
    /// <summary>
    /// Switch to frame by finding its webelement
    /// </summary>
    public static void SwitchToFrame(this IWebDriver Driver, IWebElement element)
    {
        try
        {
            Driver.SwitchTo().Frame(element);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Switched to frame [{element}]");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Switch to frame by using index
    /// </summary>
    public static void SwitchToFrame(this IWebDriver Driver, int index)
    {
        try
        {
            Driver.SwitchTo().Frame(index);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Switched to frame by index[{index}]");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Switch to frame using id or name
    /// </summary>
    public static void SwitchToFrame(this IWebDriver Driver, string idOrName)
    {
        try
        {
            Driver.SwitchTo().Frame(idOrName);
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Switched to frame [{idOrName}]");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Switch the Driver controll to default frame in a webpage
    /// </summary>
    public static void SwitchToDefaultContent(this IWebDriver Driver)
    {
        try
        {
            Driver.SwitchTo().DefaultContent();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Switched to default frame");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    public static bool VerifyUrl(this IWebDriver Driver, string url)
    {
        try
        {
            if (Driver.Url.Equals(url))
            {
                Console.WriteLine("The url: " + url + " matched successfully");
                return true;
            }
            else
            {
                Console.WriteLine("The url: " + url + " didnt match");
            }
            return false;
        }
        catch (Exception ex)
        {
            Assert.Fail("Url verify failed--\n" + ex.Message);
            return false;
        }
    }
    public static bool VerifyTitle(this IWebDriver Driver, string title)
    {
        try
        {
            if (Driver.Title.Equals(title))
            {
                Console.WriteLine("Page Title: " + title + " matched successfully");
                return true;
            }
            else
            {
                Console.WriteLine("Page Title: " + title + " didnt match");
            }
            return false;
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
            return false;
        }
    }
    /// <summary>
    /// Close the current active tab or window
    /// </summary>
    public static void CloseTab(this IWebDriver Driver)
    {
        try
        {
            Driver.Close();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"[Window] or [Tab] closed");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Refresh the webpage
    /// </summary>
    public static void Refresh(this IWebDriver Driver)
    {
        try
        {
            Driver.Navigate().Refresh();
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Page Refreshed !");
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
    /// <summary>
    /// Verify a WebElement is displayed or not, waits for only 1 second
    /// </summary>
    /// <returns>True, If the WebElement is displayed</returns>
    public static bool IsDisplayed(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(1));
            bool Displayed = wait.Until(ExpectedConditions.ElementIsVisible(by)).Displayed;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element [{locator}] displayed [{Displayed}]");
            return Displayed;
        }
        catch (Exception)
        {
            return false;
        }
    }
    /// <summary>
    /// Verify a WebElement is selected or not, waits for only 1 second
    /// </summary>
    /// <returns>True, If the WebElement is selected</returns>
    public static bool IsSelected(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            WebDriverWait wait = new(Driver, TimeSpan.FromSeconds(1));
            bool Selected = wait.Until(ExpectedConditions.ElementIsVisible(by)).Selected;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element [{locator}] selected [{Selected}]");
            return Selected;
        }
        catch (Exception)
        {
            return false;
        }
    }
    /// <summary>
    /// Verify a WebElement is enabled or not, waits for only 1 second
    /// </summary>
    /// <returns>True, If the WebElement is enabled</returns>
    public static bool IsEnabled(this IWebDriver Driver, By by, [CallerArgumentExpression("by")] string locator = null)
    {
        try
        {
            //WebDriverWait wait =;
            var Enabled = new WebDriverWait(Driver, TimeSpan.FromSeconds(1)).Until(ExpectedConditions.ElementIsVisible(by)).Enabled;
            LogHelper.LogFile(LoggingStep.GetFeatureFileName(), $"Element [{locator}] enabled [{Enabled}]");
            return Enabled;
        }
        catch (Exception)
        {
            return false;
        }
    }
}