using System;
using System.Xml.Serialization;
using Lib.Implementations.Results;

namespace Lib.Interfaces
{
    [Serializable]
    [XmlInclude(typeof(TracerResult))]
    [XmlInclude(typeof(MethodResult))]
    [XmlInclude(typeof(ThreadResult))]
    public abstract class AbstractResult
    {
        
    }
}