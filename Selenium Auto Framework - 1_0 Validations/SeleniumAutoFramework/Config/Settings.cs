using SeleniumAutoFramework.Base;

namespace SeleniumAutoFramework.Config
{
    public static class Settings
    {
        // Declaring properties here for Json Config File
        public static string Config_AUT { get; set; }
        public static string Config_AUT_UAT { get; set; }
        public static string Config_AUT_STAGING { get; set; }
        public static string Config_AUT_NonSSO { get; set; }
        public static string Config_AUT_UserName { get; set; }
        public static string Config_AUT_Password { get; set; }
        public static string Config_AUT_SF_Url { get; set; }
        public static string Config_AUT_SF_UserName { get; set; }
        public static string Config_AUT_SF_Password { get; set; }
        public static BrowserType Config_BrowserType { get; set; }
        public static string Config_TestType { get; set; }
        public static string Config_IsLog { get; set; }
        public static string Config_BuildName { get; set; }
        public static string Config_IsReport { get; set; }
        public static string Config_Technology { get; set; }
        public static string Config_Environment { get; set; }

        // Will delete if Parallel Execution didn't work
        public static double Config_WaitTime { get; set; }

    }
}
