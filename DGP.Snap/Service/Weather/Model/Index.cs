using System.Xml.Serialization;

namespace DGP.Snap.Service.Weather.Model
{
    public class Index
    {
        [XmlElement(ElementName = "name")] public string Name { get; set; }//指数名称
        [XmlElement(ElementName = "value")] public string Value { get; set; }//值
        [XmlElement(ElementName = "detail")] public string Detail { get; set; }//细节
    }
}
