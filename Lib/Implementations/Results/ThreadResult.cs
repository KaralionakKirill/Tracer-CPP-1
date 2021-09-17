using System.Collections.Generic;
using Lib.Interfaces;

namespace Lib.Implementations.Results
{
    public class ThreadResult : AbstractResult
    {
        public int Id { get; set; }

        public long ExecutionTime { get; set; }
        
        public List<MethodResult> MethodsResults { get; set; }

        public override string ToString()
        {
            return $"{{id: {Id}, time: {ExecutionTime}, methods: [{string.Join(", ", MethodsResults)}]}}";
        }
    }
}