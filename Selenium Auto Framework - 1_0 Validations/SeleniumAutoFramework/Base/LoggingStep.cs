using SeleniumAutoFramework.Helpers;
using System.Threading;

namespace SeleniumAutoFramework.Base
{
    //Class for handling data rows, feature file name, logging results
    public class LoggingStep
    {
        public string FeatureFileName { get; set; }
        public string fileName { get; set; }
        public string rowNo { get; set; }
        public ExcelHelper excelHelper { get; set; }
        //TO SET AND GET FEATURE FILE NAME
        private static readonly ThreadLocal<string> FileName = new();
        public static string GetFeatureFileName() => FileName.Value;
        public static void SetFeatureFileName(string fileName) => FileName.Value = fileName;
    }
}
