using System.Collections.Generic;
using System.Xml.Serialization;
using Lib.Interfaces;

namespace Lib.Implementations.Results
{
    public class TracerResult : AbstractResult
    {
        [XmlIgnore]
        public List<ThreadResult> ThreadsResults { get; set; }
    }
}