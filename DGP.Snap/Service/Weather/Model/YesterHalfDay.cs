using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DGP.Snap.Service.Weather.Model
{
    public class YesterHalfDay
    {
        [XmlElement(ElementName = "type_1")] public string State { get; set; }//天气状况
        [XmlElement(ElementName = "fx_1")] public string Direction { get; set; }//风向
        [XmlElement(ElementName = "fl_1")] public string Power { get; set; } //风力
    }
}
