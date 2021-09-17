using System;
using System.Collections.Generic;
using System.Linq;
using Lib.Implementations.Results;
using Lib.Interfaces;

namespace Lib.Implementations.Tracers
{
    [Serializable]
    public class TracedThread : ITracer
    {
        private readonly ThreadResult _threadResult;
        private bool Active { get; set; }

        private List<TracedMethod> TracedMethods { get; }

        public TracedThread(int id)
        {
            _threadResult = new ThreadResult
            {
                Id = id
            };
            TracedMethods = new List<TracedMethod>();
        }

        public void StartTrace()
        {
            if (!Active)
            {
                Active = true;
                var tracedMethod = new TracedMethod();
                TracedMethods.Add(tracedMethod);
                tracedMethod.StartTrace();
            }
            else
            {
                TracedMethods.Last().StartTrace();
            }
        }

        public void StopTrace()
        {
            TracedMethods.Last().StopTrace();
            Active = TracedMethods.Last().Active;
        }

        public AbstractResult GetTraceResult()
        {
            _threadResult.ExecutionTime = TracedMethods.Select(method => method.GetTraceResult() as MethodResult)
                .Sum(result => result.ExecutionTime);
            _threadResult.MethodsResults =
                TracedMethods.Select(method => method.GetTraceResult() as MethodResult).ToList();
            return _threadResult;
        }
    }
}