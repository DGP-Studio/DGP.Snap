using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DGP.Snap.Service.Weather.Model
{
    public class WeatherHalfDay
    {
        [XmlElement(ElementName = "type")] public string State { get; set; }//天气状况
        [XmlElement(ElementName = "fengxiang")] public string Direction { get; set; }//风向
        [XmlElement(ElementName = "fengli")] public string Power { get; set; } //风力
    }
}
