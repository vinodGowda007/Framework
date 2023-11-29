using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SeleniumAutoFramework.Extensions
{
    public static class WebElementExtension
    {

        //USED TO FIND ELEMENTS INSIDE ELEMENTS 
        //FIND LIST OF WEBELEMENTS INSIDE LIST OF WEBELEMENTS
        public static IList<IWebElement> FindElementsInsideElements(this IList<IWebElement> element, By by)
        {
            IList<IWebElement> lstOfInsideElements = new List<IWebElement>();
            for (int i = 0; i < element.Count; i++)
            {
                lstOfInsideElements.Add(element[i].FindElement(by));
            }
            return lstOfInsideElements;
        }
        //USED TO FIND ELEMENTS INSIDE ONE PARENT ELEMENT
        public static IList<IWebElement> FindElementsInsideOneElement(this IWebElement element, By by)
        {
            IList<IWebElement> lstOfInsideElements = element.FindElements(by);
            return lstOfInsideElements;
        }



        //OLD METHODS



        //TO RETRIEVE VALUE SELECTED IN A DROPDOWN
        public static string GetSelectedValueInDropDown(this IWebElement element)
        {
            SelectElement DrpDownList = new SelectElement(element);
            return DrpDownList.AllSelectedOptions.FirstOrDefault().ToString();
        }


        //TO RETRIEVE ALL VALUES FROM DROP DOWN LIST
        public static IList<IWebElement> GetDropDownListValues(this IWebElement element)
        {
            SelectElement DrpDownList = new SelectElement(element);
            return DrpDownList.AllSelectedOptions;
        }

        //TO RETRIEVE TEXT FROM A ELEMENT
        public static string GetElementText(this IWebElement element)
        {
            return element.Text;
        }

        //TO SELECT VALUE FROM DROP DOWN LIST
        public static void SelectValueFromDropDownListUsingText(this IWebElement element, string value)
        {
            SelectElement DrpDownList = new SelectElement(element);
            DrpDownList.SelectByText(value);
        }

        //TO SELECT VALUE FROM DROP DOWN LIST BY VALUE
        public static void SelectValueFromDropDownListUsingValue(this IWebElement element, string value)
        {
            SelectElement DrpDownList = new SelectElement(element);
            DrpDownList.SelectByValue(value);
        }

        //TO SELECT VALUE FROM DROP DOWN LIST USING INDEX
        public static void SelectValueFromDropDownListUsingIndex(this IWebElement element, int index)
        {
            SelectElement DrpDownList = new SelectElement(element);
            DrpDownList.SelectByIndex(index);
        }

        //TO SELECT VALUE FROM DROP DOWN LIST USING OPTIONS
        public static void SelectValueFromDropDownListUsingOption(this IWebElement element, IWebDriver driver, string value)
        {
            IWebElement options = element.FindElement(By.XPath("//div[@role='option']/span[text()='" + value + "']"));
            options.Click();
        }

        //TO HIGHLIGHT AN ELEMENT
        public static void Highlight(this IWebElement element, IWebDriver driver)
        {
            //var rc = (RemoteWebElement)element;
            //var driver = (IJavaScriptExecutor)rc.WrappedDriver;
            //var script = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red""; ";
            //driver.ExecuteScript(script, rc);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].setAttribute('style', 'background: yellow; border: 2px solid red;');", element);
        }

        //TO VERIFY IF AN ELEMENT EXISTS OR NOT       
        public static void AssertElementPresent(this IWebElement element)
        {
            if (!IsElementPresent(element))
            {
                throw new Exception(string.Format("Element is not present Exception"));
            }
        }

        private static bool IsElementPresent(IWebElement element)
        {
            try
            {
                bool ele = element.Displayed;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //TO VERIFY PARTIAL TEXT
        public static bool VerifyPartialText(this IWebElement element, string expectedText)
        {
            try
            {
                if (element.Text.Contains(expectedText))
                {
                    Console.WriteLine("Text " + expectedText + " is displayed Partially");
                    return true;
                }
                else
                {
                    Console.WriteLine("Text " + expectedText + " is NOT displayed Partially");
                    return false;
                }
            }
            catch (WebDriverException er)
            {
                Console.WriteLine("WebDriverException : " + er.Message);
            }
            return false;
        }

        //TO VERIFY EXACT TEXT
        public static bool VerifyExactText(this IWebElement element, string expectedText)
        {
            try
            {
                if (element.Text.Equals(expectedText))
                {
                    Console.WriteLine("Text " + expectedText + " is displayed Exactly");
                    return true;
                }
                else
                {
                    Console.WriteLine("Text " + expectedText + " is NOT displayed Exactly");
                    return false;
                }
            }
            catch (WebDriverException er)
            {
                Console.WriteLine("WebDriverException : " + er.Message);
            }
            return false;
        }

        //TO CLEAR AND TYPE TEXT
        public static void ClearAndTypeText(this IWebElement element, string text)
        {
            try
            {
                while (!element.GetAttribute("value").Equals(""))
                {
                    element.SendKeys(Keys.Backspace);
                }
                element.SendKeys(text);
                element.SendKeys(Keys.Tab);
            }
            catch (ElementNotInteractableException)
            {
                Assert.Fail();
                throw new Exception();
            }
        }

        //TO CLEAR TEXT
        public static void ClearText(this IWebElement element)
        {
            try
            {
                while (!element.GetAttribute("value").Equals(""))
                {
                    element.SendKeys(Keys.Backspace);
                }
                Console.WriteLine("Field is cleared");
            }
            catch (ElementNotInteractableException)
            {
                Console.WriteLine("Field is Not Interactable");
                throw new Exception();
            }
        }

        //TO CLICK ON AN ELEMENT USING JAVASCRIPT
        public static void ClickOnElement(this IWebElement element, IWebDriver driver)
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click()", element);
            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine("Field is Not Interactable");
                throw new Exception();
            }
        }

        //TO CLICK MOVE TO AN ELEMENT AND PERFORM DOUBLE CLICK
        public static void MoveToTheElementAndDoubleClick(this IWebElement element, IWebDriver driver)
        {
            //int i = 0;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(condition => element.Displayed);
                Actions act = new Actions(driver);
                act.DoubleClick(element).Build().Perform();
            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine("Field is Not Interactable");
                throw new Exception();
            }
        }

        //TO CLICK MOVE TO AN ELEMENT AND PERFORM  CLICK
        public static void MoveToTheElementAndClick(this IWebElement element, IWebDriver driver)
        {
            //int i = 0;
            try
            {
                System.Threading.Thread.Sleep(1000);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                wait.Until(condition => element.Displayed);
                Actions act = new Actions(driver);
                act.MoveToElement(element);//build,perform added
                System.Threading.Thread.Sleep(3000);
                act.Click(element).Build().Perform();//added

            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine("Field is Not Interactable");
                throw new Exception();
            }
        }

        //TO CLICK MOVE TO AN ELEMENT
        public static void MoveToTheElement(this IWebElement element, IWebDriver driver)
        {
            //int i = 0;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(condition => element.Displayed);
                Actions act = new Actions(driver);
                act.MoveToElement(element);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                act.Perform();
            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine("Field is Not Interactable");
                throw new Exception();
            }
        }

        //TO VERIFY PARTIAL ATTRIBUTE
        public static bool VerifyPartialAttribute(this IWebElement element, string attribute, string value)
        {
            try
            {
                if (element.GetAttribute(attribute).Contains(value))
                {
                    Console.WriteLine("The expected attribute : " + attribute + " value contains the actual " + value);
                    return true;
                }
                else
                {
                    Console.WriteLine("The expected attribute : " + attribute + " value doesnot contains the actual " + value);
                    return false;
                }
            }
            catch (WebDriverException er)
            {
                Console.WriteLine("WebDriverException : " + er.Message);
            }
            return false;
        }

        //TO VERIFY EXACT ATTRIBUTE
        public static bool VerifyExactAttribute(this IWebElement element, string attribute, string value)
        {
            try
            {
                if (element.GetAttribute(attribute).Equals(value))
                {
                    Console.WriteLine("The expected attribute : " + attribute + " value contains the actual " + value);
                    return true;
                }
                else
                {
                    Console.WriteLine("The expected attribute : " + attribute + " value doesnot contains the actual " + value);
                    return false;
                }
            }
            catch (WebDriverException er)
            {
                Console.WriteLine("WebDriverException : " + er.Message);
            }
            return false;
        }
    }
}
