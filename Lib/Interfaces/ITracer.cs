namespace Lib.Interfaces
{
    public interface ITracer
    {
        void StartTrace();

        void StopTrace();
        
        AbstractResult GetTraceResult();
    }
}