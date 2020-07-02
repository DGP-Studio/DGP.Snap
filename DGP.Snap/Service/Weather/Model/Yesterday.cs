using System.Xml.Serialization;

namespace DGP.Snap.Service.Weather.Model
{
    [XmlRoot(ElementName = "yesteday")]
    public class Yesterday//昨天
    {
        [XmlElement(ElementName = "date_1")] public string Date { get; set; }//昨天日期
        [XmlElement(ElementName = "high_1")] public string HighTemp { get; set; }//最高温度
        [XmlElement(ElementName = "low_1")] public string LowTemp { get; set; }//最低温度
        [XmlElement(ElementName = "day_1")] public YesterHalfDay Day { get; set; }
        [XmlElement(ElementName = "night_1")] public YesterHalfDay Night { get; set; }
    }
}
