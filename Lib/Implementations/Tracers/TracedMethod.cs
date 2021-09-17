using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lib.Implementations.Results;
using Lib.Interfaces;

namespace Lib.Implementations.Tracers
{
    [Serializable]
    public class TracedMethod : ITracer
    {
        private readonly int _skip;
        public bool Active { get; set; }

        private readonly MethodResult _methodResult;
        
        private Stopwatch ExecutorStopwatch { get; }
        
        private List<TracedMethod> TracedMethods { get; }
        
        public TracedMethod(int skip = 3)
        {
            ExecutorStopwatch = new Stopwatch();
            TracedMethods = new List<TracedMethod>();
            _skip = skip;
            _methodResult = new MethodResult();
        }

        public void StartTrace()
        {
            if (!Active)
            {
                Active = true;
                var method = new StackFrame(_skip).GetMethod();
                _methodResult.MethodName = method?.Name;
                _methodResult.ClassName = method?.DeclaringType?.Name;

                ExecutorStopwatch.Start();
            }
            else
            {
                if (TracedMethods.Any() && TracedMethods.Last().Active)
                {
                    TracedMethods.Last().StopTrace();
                }
                else
                {
                    var tracer = new TracedMethod(_skip + 1);
                    TracedMethods.Add(tracer);
                    tracer.StartTrace();
                }
            }
        }

        public void StopTrace()
        {
            if (TracedMethods.Any() && TracedMethods.Last().Active)
            {
                TracedMethods.Last().StopTrace();
            }
            else
            {
                ExecutorStopwatch.Stop();
                Active = false;
                _methodResult.ExecutionTime = ExecutorStopwatch.ElapsedMilliseconds;
            }
        }

        public AbstractResult GetTraceResult()
        {
             _methodResult.MethodsResults = TracedMethods.Select(method => method._methodResult).ToList();
             return _methodResult;
        }
    }
}