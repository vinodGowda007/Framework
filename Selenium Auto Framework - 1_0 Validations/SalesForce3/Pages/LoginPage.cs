using MongoDB.Driver;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutoFramework.Base;
using SeleniumAutoFramework.Config;
using SeleniumAutoFramework.Extensions;
using SeleniumAutoFramework.Helpers;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections;
using TechTalk.SpecFlow;

namespace SalesForce3.Pages;

public class LoginPage : BasePage
{
    public readonly LoggingStep _loggingStep;
    private readonly ScenarioContext _scenarioContext;
    readonly IWebDriver Driver;
    public static string LooggedInUser;

    //THIS METHOD IS USED TO SIMPLIFY THE LOG WRITING
    public void Log(string message) => LogHelper.LogFile(_loggingStep.FeatureFileName, message);

    //DEPENDENCY INJECTON OR CONTEXT INJECTION
    public LoginPage(ParallelConfig parallelConfig, LoggingStep loggingStep, ScenarioContext scenarioContext) : base(parallelConfig, loggingStep)
    {
        _loggingStep = loggingStep;
        _scenarioContext = scenarioContext;
        Driver = parallelConfig.Driver;
    }



    ////PAGE OBJECTS - Updated
    //readonly By txtUserName = By.Name("username");
    //readonly By txtPassword = By.Name("pw");
    //readonly By btnLogin = By.Name("Login");
    //readonly By btnHome = By.XPath("//a[@title='Home']");


    // SSO Link
    readonly By lnkLoginWithSSO = By.XPath("//div[@id='idp_section_buttons']/button");

    //PAGE OBJECTS - Updated
    readonly By txtUserName = By.Name("loginfmt");
    readonly By txtPassword = By.Name("passwd");

    //readonly By btnLogin = By.Name("Login");
    readonly By btnNextOrSignIn = By.XPath("//input[@id='idSIButton9']");
    readonly By btnHome = By.XPath("//a[@title='Home']");

    readonly By txtUserName_Old = By.Name("username");
    readonly By txtPassword_Old = By.Name("pw");
    readonly By btnNextOrSignIn_Old = By.XPath("//input[@id='Login']");

    // Change User

    //readonly By txtsearchBox = By.XPath("//input[contains(@title,'Search Setup')]");
    readonly By txtsearchBox = By.XPath("//header[@id='oneHeader']//input");

    readonly By displayedUser = By.XPath("//ul/li/a/span[contains(text(),'in Setup')]");
    readonly By linkUser = By.XPath("//ul/li/a/div/span/div");
    readonly By btnLoginToNewUser = By.XPath("(//input[@name='login'])[1]");
    readonly By btnAppLauncher = By.XPath("//div[@class='slds-r5']");
    //readonly By linkViewProfile = By.XPath("(//img[@title='User'])[1]");
    //readonly By linkUserName = By.XPath("(//div/h1/a)[1]");
    readonly By iframeSetup = By.XPath("//iframe[starts-with(@name,'vfFrameId')]");
    readonly By iFrameForSource = By.XPath("//iframe[@id='RLPanelFrame']");



    //CHECK IF LOGIN PAGE IS DISPLAYED
    public void CheckIfLoginPageIsDisplayed()
    {
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        Driver.WaitForElementToPresent(txtUserName);
        Assert.IsTrue(Driver.ElementDisplayed(txtUserName), $"\"LOGIN PAGE\" IS  NOT DISPLAYED");
        Log("\"LOGIN PAGE\" IS DISPLAYED");
    }

    //ENTER USER NAME AND PASSWORD
    public void EnterUserNameAndPassword(string username, string password)
    {
        Driver.SendKeysOrText(txtUserName_Old, username);
        Driver.SendKeysOrText(txtPassword_Old, DecodeString(password));
        Log("Entered UserName & Password");
        Driver.JSClick(btnNextOrSignIn_Old);
    }


    public void LoginToStarrInsurance(string username, string password)
    {
        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));

        // Can be removed if login page is changed again [SINCE LOGIN PAGE IS CHANGED WRITING BELOW CODE]---------------------------------------------

        //Driver.CaptureScreen(_scenarioContext);
        //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(lnkLoginWithSSO));
        //Driver.ScrollToCenter(lnkLoginWithSSO);
        //Driver.JSClick(lnkLoginWithSSO);
        //-----------------------------------------------------------------------------------------

        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(txtUserName));
        Driver.CaptureScreen(_scenarioContext);
        Driver.SendKeysOrText(txtUserName, username);
        Driver.JSClick(btnNextOrSignIn);
        Log("CLICKED ON NEXT BUTTON");
        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(txtPassword));
        Console.WriteLine(password);
        Driver.SendKeysOrText(txtPassword, password);
        Driver.JSClick(btnNextOrSignIn);
        Log("CLICKED ON SIGN-IN BUTTON");
    }


    ////CLICK LOGIN
    //public void ClickLogin()
    //{
    //    Assert.IsTrue(Driver.IsDisplayed(btnLogin));
    //    Driver.WaitAndClick(btnLogin);
    //    Log("Clicked on 'Login' button");
    //}

    //CHANGE USER PROFILE
    public void ChangeUserProfile(string ProfileData)
    {
        Console.WriteLine(Settings.Config_WaitTime);
        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        LooggedInUser = ProfileData;
        try
        {
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(txtsearchBox));
            System.Threading.Thread.Sleep(1000);
            Driver.SendKeysOrText(txtsearchBox, ProfileData);
            System.Threading.Thread.Sleep(1000);
            Driver.WaitAndClick(txtsearchBox);
            System.Threading.Thread.Sleep(500);
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(displayedUser));
                System.Threading.Thread.Sleep(3000);
            }
            catch(Exception e)
            {
                Log("FILTERED VALUES ARE NOT DISPLAYED IN SEARCHBOX");
            }            
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(linkUser));
                Console.WriteLine("User is + " + ProfileData);
                if (Driver.SelectFromListUsingJS(linkUser, ProfileData) == true)
                {
                    try
                    {
                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(iframeSetup));
                        Driver.SwitchToFrame(0);
                        try
                        {
                            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(btnLoginToNewUser));

                            Driver.CaptureScreen(_scenarioContext);
                            Driver.WaitAndClick(btnLoginToNewUser);
                            System.Threading.Thread.Sleep(2000);
                            Console.WriteLine("CLICKED ON LOGIN BUTTON");
                        }
                        catch (Exception e)
                        {
                            Driver.CaptureScreen(_scenarioContext);
                            Assert.Fail(ProfileData + " USER IS NOT ACTIVE ");
                        }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("COULD NOT FIND THE FRAME IN THIS PAGE");
                        Log("COULD NOT FIND THE FRAME IN THIS PAGE");
                    }
                }
                else
                {
                    Log("COULD NOT FIND THE USER IN THE ORG");
                    Assert.Fail("COULD NOT FIND THE USER IN THE ORG");
                }
            }

            catch (Exception e)
            {
                Log("USER IS NOT DISPLAYED IN THE LIST");
                Console.WriteLine("USER IS NOT DISPLAYED IN THE LIST");
            }
    }
        catch (Exception e)
        {
            Log("COULD NOT FIND THE SEARCH BOX IN THE PAGE");
            Console.WriteLine("COULD NOT FIND THE SEARCH BOX IN THE PAGE");
        }
    }


    //ENCRYPT THE PASSWORD
    public void EncryptString(string pwdString)
    {
        byte[] encode = new byte[pwdString.Length];
        encode = System.Text.Encoding.UTF8.GetBytes(pwdString);
        string encodedString = Convert.ToBase64String(encode);
        Console.WriteLine(encodedString);
    }

    //DECODE PASSWORD
    public string DecodeString(string encodedString)
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
        System.Text.Decoder DecodeData = encoder.GetDecoder();
        byte[] todecode = Convert.FromBase64String(encodedString);
        int charcount = DecodeData.GetCharCount(todecode, 0, todecode.Length);
        char[] decoded_char = new char[charcount];
        DecodeData.GetChars(todecode, 0, todecode.Length, decoded_char, 0);
        string result = new string(decoded_char);
        return result;
    }


    public void ThenUserEnterTheUsername(string ProfileData)
    {
        LooggedInUser = ProfileData;
        Driver.WaitForElementToPresent(txtsearchBox);
        Driver.ClearAndSend(txtsearchBox, ProfileData);
        System.Threading.Thread.Sleep(2000);
        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Settings.Config_WaitTime));
        wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(linkUser));
        if (Driver.IsDisplayed(linkUser))
        {
            if (Driver.GetTextFromElement(linkUser).Equals(ProfileData.Trim().ToString()))
            {
                Driver.WaitAndClick(linkUser);
                Driver.WaitForNextPage();
                System.Threading.Thread.Sleep(4000);
                Driver.WaitForElementToPresent(iframeSetup);
                Driver.SwitchToFrame(0);
                Driver.ScrollByPixel(1200);
                Driver.CaptureScreen(_scenarioContext);
            }
            else
            {
                Assert.Fail("COULD NOT FIND THE USER IN THE ORG");
            }
        }
        else
        {
            Assert.Fail("COULD NOT FIND THE USER IN THE ORG");
        }
    }






}
