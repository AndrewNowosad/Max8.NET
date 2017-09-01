using System.Threading.Tasks;
using System.Timers;

namespace Max8.NET.Helpers
{
    class TplHelpers
    {
        public static Task<T> Delay<T>(T returnValue, int milliseconds)
        {
            var tcs = new TaskCompletionSource<T>();
            var timer = new Timer(milliseconds) { AutoReset = false };
            timer.Elapsed += delegate { timer.Dispose(); tcs.SetResult(returnValue); };
            timer.Start();
            return tcs.Task;
        }

        public static Task<T> Delay<T>(int milliseconds)
            => Delay(default(T), milliseconds);
    }
}