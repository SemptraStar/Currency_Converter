namespace CurrencyConverter.Api.Jobs
{
    public abstract class BackgroundJobProcess : IBackgroundJobProcess
    {
        public abstract void Execute();
    }
}
