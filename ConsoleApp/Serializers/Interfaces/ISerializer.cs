using System.IO;

namespace ConsoleApp.Serializers.Interfaces
{
    public interface ISerializer
    {
        void Serialize(TextWriter writer, object data);
    }
}