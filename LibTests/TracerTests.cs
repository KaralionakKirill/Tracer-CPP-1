using System.Threading;
using Lib.Implementations.Results;
using Lib.Implementations.Tracers;
using Lib.Interfaces;
using NUnit.Framework;

namespace LibTests
{
    public class TracerTests
    {
        private ITracer _tracer;
        [SetUp]
        public void Setup()
        {
            _tracer = new Tracer();
        }

         private void M1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

         private void M2()
         {
             _tracer.StartTrace();
             Thread.Sleep(100);
             M1();
             _tracer.StopTrace(); 
         }
        
        [Test]
        public void Trace_SingleMethod_TimeMore100()
        {
            M1();
            Assert.AreEqual(1, ((TracerResult) _tracer.GetTraceResult()).ThreadsResults.Count);
            Assert.GreaterOrEqual(((TracerResult) _tracer.GetTraceResult()).ThreadsResults[0].ExecutionTime, 100);
        }
        
        [Test]
        public void Trace_TwoMethods_TimeMore200_MethodsAmount2()
        {
            M1();
            M1();
            Assert.AreEqual(1, ((TracerResult) _tracer.GetTraceResult()).ThreadsResults.Count);
            Assert.AreEqual(2, ((TracerResult) _tracer.GetTraceResult()).ThreadsResults[0].MethodsResults.Count);
            Assert.GreaterOrEqual(((TracerResult) _tracer.GetTraceResult()).ThreadsResults[0].ExecutionTime, 200);
        }
        
        [Test]
        public void Trace_TwoThreads()
        {
            var t1 = new Thread(M1);
            var t2 = new Thread(M2);
            t1.Start();
            t1.Join();
            t2.Start();
            t2.Join();
            
            Assert.AreEqual(2, ((TracerResult) _tracer.GetTraceResult()).ThreadsResults.Count);
            Assert.AreEqual(1, ((TracerResult) _tracer.GetTraceResult()).ThreadsResults[0].MethodsResults.Count);
            Assert.AreEqual(1, ((TracerResult) _tracer.GetTraceResult()).ThreadsResults[1].MethodsResults.Count);
            Assert.GreaterOrEqual(((TracerResult) _tracer.GetTraceResult()).ThreadsResults[0].ExecutionTime, 100);
            Assert.GreaterOrEqual(((TracerResult) _tracer.GetTraceResult()).ThreadsResults[1].ExecutionTime, 200);
        }
    }
}