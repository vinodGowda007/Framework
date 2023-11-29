using Microsoft.Extensions.Configuration;
using System.IO;



namespace SeleniumAutoFramework.Config
{
    public static class ConfigReader
    {
        public static void SetFrameWorkSettings()
        {
            //For Json Config File
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("GlobalConfig.json");

            IConfigurationRoot configurationRoot = builder.Build();
            Settings.Config_AUT = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().AUT;
            Settings.Config_AUT_UAT = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().AUT_UAT;
            Settings.Config_AUT_STAGING = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().AUT_STAGING;
            Settings.Config_AUT_NonSSO = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().AUT_NonSSO;
            Settings.Config_AUT_UserName = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().AUT_UserName;
            Settings.Config_AUT_Password = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().AUT_Password;
            Settings.Config_AUT_SF_Url = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().AUT_SF_Url;
            Settings.Config_AUT_SF_UserName = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().AUT_SF_UserName;
            Settings.Config_AUT_SF_Password = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().AUT_SF_Password;
            Settings.Config_BrowserType = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().Browser;
            Settings.Config_TestType = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().TestType;
            Settings.Config_IsLog = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().IsLog;
            Settings.Config_BuildName = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().BuildName;
            Settings.Config_IsReport = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().IsReport;
            Settings.Config_Technology = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().Technology;
            Settings.Config_Environment = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().Environment;

            // Adding wait time
            Settings.Config_WaitTime = configurationRoot.GetSection("testRunSettings").Get<TestRunSettings>().WaitTime;
        }
    }
}
