using System.Xml.Serialization;

namespace DGP.Snap.Service.Weather.Model
{
    [XmlRoot(ElementName = "resp")]
    public class WeatherModel
    {
        [XmlElement(ElementName = "city")] public string City { get; set; }//城市
        [XmlElement(ElementName = "updatetime")] public string UpdateTime { get; set; }//更新时间
        [XmlElement(ElementName = "wendu")] public string RealTimeTemp { get; set; }//实时温度
        [XmlElement(ElementName = "fengli")] public string Power { get; set; } //风力
        [XmlElement(ElementName = "shidu")] public string Humidity { get; set; }//湿度
        [XmlElement(ElementName = "fengxiang")] public string Direction { get; set; }//风向
        [XmlElement(ElementName = "sunrise_1")] public string Sunrise { get; set; }//日出
        [XmlElement(ElementName = "sunset_1")] public string Sunset { get; set; }//日出
        [XmlElement(ElementName = "yesterday")] public Yesterday Yesterday { get; set; }//昨天
        [XmlElement(ElementName = "forecast")] public Forecast Forecast { get; set; }//天气预报
        [XmlElement(ElementName = "zhishus")] public IndexCollection IndexCollections { get; set; }//指数
    }
}
