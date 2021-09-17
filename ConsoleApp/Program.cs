using System.IO;
using System.Threading;
using ConsoleApp.Serializers.Interfaces;
using ConsoleApp.Serializers.Strategies;
using Lib.Implementations.Tracers;
using Lib.Interfaces;


namespace ConsoleApp
{
    class Program
    {
        private class Test
        {
            private ITracer tracer;

            public Test(ITracer tracer)
            {
                this.tracer = tracer;
            }

            public void M1()
            {
                tracer.StartTrace();
                Thread.Sleep(100);
                tracer.StopTrace();
            }
            
            public void M2()
            {
                tracer.StartTrace();
                Thread.Sleep(100);
                M1();
                tracer.StopTrace();
            }

            public void M3()
            {
                tracer.StartTrace();
                M1();
                M2();
                tracer.StopTrace();
            }
        }
        static void Main()
        {
            var tracer = new Tracer();
            var test = new Test(tracer);

            var t1 = new Thread(() =>
            {
                test.M1();
                test.M2();
                test.M3();
            });
            t1.Start();
            
            var t2 = new Thread(() =>
            {
                test.M1();
                test.M2();
                test.M3();
            });
            t2.Start();
            
            t1.Join();
            t2.Join();

            ISerializer jsonSerializer = new JsonSerializer();
            ISerializer xmlSerializer = new XmlSerializer(typeof(AbstractResult));
            using (var file = new FileStream("C:/Users/Asus/Desktop/jsonResult.json", FileMode.Create))
            {
                using (var writer = new StreamWriter(file))
                {
                    jsonSerializer.Serialize(writer, tracer.GetTraceResult());
                }
                
            }
            using (var file = new FileStream("C:/Users/Asus/Desktop/xmlResult.xml", FileMode.Create))
            {
                using (var writer = new StreamWriter(file))
                {
                    xmlSerializer.Serialize(writer, tracer.GetTraceResult());
                }
                
            }
        }
    }
}