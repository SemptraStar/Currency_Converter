using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.Api.Jobs
{
    public interface IBackgroundJobProcess
    {
        void Execute();
    }
}
