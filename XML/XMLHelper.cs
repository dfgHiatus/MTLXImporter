using System.IO;
using System.Xml.Serialization;

namespace MTLXImporter;

public static class XMLHelper
{
    public static T Deserialize<T>(string path)
    {
        using (FileStream fs = new(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            var xs = new XmlSerializer(typeof(T));
            var obj = (T)xs.Deserialize(fs)!;
            return obj;
        }
    }
}
