

namespace SeleniumAutoFramework.Base
{
    // Base Step class is extended by all Step classes, thus inheriting all the base methods
    public class BaseStep : BasePage
    {
        public BaseStep(ParallelConfig parallelConfig, LoggingStep loggingStep) : base(parallelConfig, loggingStep)
        {

        }
    }
}
