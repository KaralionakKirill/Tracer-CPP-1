using System;
using ConsoleApp.Serializers.Interfaces;

namespace ConsoleApp.Serializers.Strategies
{
    public class XmlSerializer : System.Xml.Serialization.XmlSerializer, ISerializer
    {
        public XmlSerializer(Type t)
            : base(t)
        {
            
        }
    }
}