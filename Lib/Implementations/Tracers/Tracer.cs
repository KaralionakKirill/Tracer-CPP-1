using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Lib.Implementations.Results;
using Lib.Interfaces;

namespace Lib.Implementations.Tracers
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, TracedThread> TracedThreads { get; }

        public Tracer()
        {
            TracedThreads = new ConcurrentDictionary<int, TracedThread>();
        }

        public void StartTrace()
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;

            if (TracedThreads.ContainsKey(currentThreadId))
            {
                TracedThreads[currentThreadId].StartTrace();
            }
            else
            {
                var tracedThread = new TracedThread(currentThreadId);
                TracedThreads.TryAdd(currentThreadId, tracedThread);
                tracedThread.StartTrace();
            }
        }

        public void StopTrace()
        {
            TracedThreads[Thread.CurrentThread.ManagedThreadId].StopTrace();
        }

        public AbstractResult GetTraceResult()
        {
            var result = new TracerResult
            {
                ThreadsResults = TracedThreads.Select(thread => thread.Value.GetTraceResult() as ThreadResult).ToList()
            };
            return result;
        }
    }
}