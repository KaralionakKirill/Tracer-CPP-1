using System.IO;
using ConsoleApp.Serializers.Interfaces;
using Newtonsoft.Json;

namespace ConsoleApp.Serializers.Strategies
{
    public class JsonSerializer : ISerializer
    {
        public void Serialize(TextWriter writer, object data)
        {
            writer.WriteLine(
                JsonConvert.SerializeObject(data, Formatting.Indented)
            );
        }
    }
}