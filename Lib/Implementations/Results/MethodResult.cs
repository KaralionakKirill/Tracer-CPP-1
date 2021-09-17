using System.Collections.Generic;
using Lib.Interfaces;

namespace Lib.Implementations.Results
{
    public class MethodResult : AbstractResult
    {
        public string MethodName { get; set; }
        
        public string ClassName { get; set; }
        
        public long ExecutionTime { get; set; }
        
        public List<MethodResult> MethodsResults { get; set; }
        
        public override string ToString()
        {
            return $"{{name: {MethodName}, class: {ClassName}, time: {ExecutionTime}, methods: [{string.Join(", ", MethodsResults)}]}}";
        }
    }
}