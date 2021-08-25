using Cake.Common.Xml;
using Cake.Core.IO;

namespace Aero.Cake.Wrappers
{
    public interface IXmlWrapper
    {
        string XmlPeek(FilePath filePath, string xpath);

        string XmlPeek(FilePath filePath, string xpath, XmlPeekSettings settings);

        void XmlPoke(FilePath filePath, string xpath, string value);

        void XmlPoke(FilePath filePath, string xpath, string value, XmlPokeSettings settings);

        string XmlPokeString(string sourceXml, string xpath, string value);

        string XmlPokeString(string sourceXml, string xpath, string value, XmlPokeSettings settings);
    }

    public class XmlWrapper : AbstractWrapper, IXmlWrapper
    {
        public XmlWrapper(IAeroContext aeroContext) : base(aeroContext)
        {
        }

        public string XmlPeek(FilePath filePath, string xpath)
        {
            return AeroContext.XmlPeek(filePath, xpath);
        }

        public string XmlPeek(FilePath filePath, string xpath, XmlPeekSettings settings)
        {
            return AeroContext.XmlPeek(filePath, xpath, settings);
        }

        public void XmlPoke(FilePath filePath, string xpath, string value)
        {
            AeroContext.XmlPoke(filePath, xpath, value);
        }

        public void XmlPoke(FilePath filePath, string xpath, string value, XmlPokeSettings settings)
        {
            AeroContext.XmlPoke(filePath, xpath, value, settings);
        }

        public string XmlPokeString(string sourceXml, string xpath, string value)
        {
            return AeroContext.XmlPokeString(sourceXml, xpath, value);
        }

        public string XmlPokeString(string sourceXml, string xpath, string value, XmlPokeSettings settings)
        {
            return AeroContext.XmlPokeString(sourceXml, xpath, value, settings);
        }
    }
}
