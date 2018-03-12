using System;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.Api.Jobs
{
    public abstract class BackgroundJobProcess : IBackgroundJobProcess
    {
        public abstract void Execute();
    }
}
