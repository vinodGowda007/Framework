
using SeleniumAutoFramework.Helpers;

namespace SeleniumAutoFramework.Base
{
    //Base Class is main class for handling entire framework
    public class Base
    {
        public readonly ParallelConfig _parallelConfig;
        public readonly LoggingStep loggingStep;

        //Method for handling Driver, Logging
        public Base(ParallelConfig parallelConfig, LoggingStep loggingStep)
        {
            _parallelConfig = parallelConfig;
            this.loggingStep = loggingStep;

        }

        //Method for Logging results in text file
        public void Log(string message) => LogHelper.LogFile(loggingStep.FeatureFileName, message);



        //Method to verify whether driver is accessing the correct page or not
        public CurrentPage As<CurrentPage>() where CurrentPage : BasePage
        {
            return (CurrentPage)this;
        }
    }
}
