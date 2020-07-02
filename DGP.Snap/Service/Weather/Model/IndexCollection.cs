using System.Collections.Generic;
using System.Xml.Serialization;

namespace DGP.Snap.Service.Weather.Model
{
    public class IndexCollection
    {
        [XmlElement(ElementName = "zhishu")] public List<Index> Details { get; set; }//指数
    }
}
