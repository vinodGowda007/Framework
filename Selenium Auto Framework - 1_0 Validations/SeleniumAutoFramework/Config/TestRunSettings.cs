using Newtonsoft.Json;
using SeleniumAutoFramework.Base;

namespace SeleniumAutoFramework.Config
{
    [JsonObject("testRunSettings")]
    public class TestRunSettings
    {
        // Declaring properties here 
        [JsonProperty("AUT")]
        public string AUT { get; set; }

        [JsonProperty("AUT_UAT")]
        public string AUT_UAT { get; set; }

        [JsonProperty("AUT_STAGING")]
        public string AUT_STAGING { get; set; }

        [JsonProperty("AUT_NonSSO")]
        public string AUT_NonSSO { get; set; }

        [JsonProperty("AUT_UserName")]
        public string AUT_UserName { get; set; }

        [JsonProperty("AUT_Password")]
        public string AUT_Password { get; set; }

        [JsonProperty("AUT_SF_Url")]
        public string AUT_SF_Url { get; set; }

        [JsonProperty("AUT_SF_UserName")]
        public string AUT_SF_UserName { get; set; }

        [JsonProperty("AUT_SF_Password")]
        public string AUT_SF_Password { get; set; }

        [JsonProperty("Browser")]
        public BrowserType Browser { get; set; }

        [JsonProperty("TestType")]
        public string TestType { get; set; }

        [JsonProperty("IsLog")]
        public string IsLog { get; set; }

        [JsonProperty("BuildName")]
        public string BuildName { get; set; }

        [JsonProperty("IsReport")]
        public string IsReport { get; set; }

        [JsonProperty("Technology")]
        public string Technology { get; set; }

        [JsonProperty("Environment")]
        public string Environment { get; set; }

        // Wait time
        [JsonProperty("WaitTime")]
        public double WaitTime { get; set; }

    }

}
