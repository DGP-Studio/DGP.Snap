using System.Collections.Generic;
using System.Xml.Serialization;

namespace DGP.Snap.Service.Weather.Model
{
    [XmlRoot(ElementName = "forecast")]
    public class Forecast
    {
        [XmlElement(ElementName = "weather")] public List<Weather> Weathers { get; set; }//天气预报
    }
}
