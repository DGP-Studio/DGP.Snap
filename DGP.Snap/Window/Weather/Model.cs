using System.Collections.Generic;
using System.Xml.Serialization;

namespace DGP.Snap.Window.Weather
{
    [XmlRoot(ElementName = "resp")]
    public class WeatherModel
    {
        [XmlElement(ElementName = "city")] public string City { get; set; }//城市
        [XmlElement(ElementName = "updatetime")] public string UpdateTime { get; set; }//更新时间
        [XmlElement(ElementName = "wendu")] public string RealTimeTemp { get; set; }//实时温度
        [XmlElement(ElementName = "fengli")] public string Power { get; set; } //风力
        [XmlElement(ElementName = "shidu")] public string Humidity { get; set; }//湿度
        public double Double_Humidity
        {
            get
            {
                if (Humidity != string.Empty && Humidity != null)
                    return double.Parse(Humidity.Replace("%", ""));
                return 0;
            }
        }
        [XmlElement(ElementName = "fengxiang")] public string Direction { get; set; }//风向
        [XmlElement(ElementName = "sunrise_1")] public string Sunrise { get; set; }//日出
        [XmlElement(ElementName = "sunset_1")] public string Sunset { get; set; }//日出

        [XmlElement(ElementName = "yesterday")] public Yesterday Yesterday { get; set; }//昨天
        [XmlElement(ElementName = "forecast")] public Forecast Forecast { get; set; }//天气预报
        [XmlElement(ElementName = "zhishus")] public IndexCollection IndexCollections { get; set; }//指数

    }

    [XmlRoot(ElementName = "yesteday")]
    public class Yesterday//昨天
    {
        [XmlElement(ElementName = "date_1")] public string Date { get; set; }//昨天日期
        [XmlElement(ElementName = "high_1")] public string HighTemp { get; set; }//最高温度
        [XmlElement(ElementName = "low_1")] public string LowTemp { get; set; }//最低温度
        [XmlElement(ElementName = "day_1")] public YesterDayHalf Day { get; set; }
        [XmlElement(ElementName = "night_1")] public YesterDayHalf Night { get; set; }
    }

    public class YesterDayHalf
    {
        [XmlElement(ElementName = "type_1")] public string State { get; set; }//天气状况
        [XmlElement(ElementName = "fx_1")] public string Direction { get; set; }//风向
        [XmlElement(ElementName = "fl_1")] public string Power { get; set; } //风力
    }

    [XmlRoot(ElementName = "forecast")]
    public class Forecast
    {
        [XmlElement(ElementName = "weather")] public List<Weather> Weathers { get; set; }//天气预报
    }

    [XmlRoot(ElementName = "weather")]
    public class Weather
    {
        private string _date;
        [XmlElement(ElementName = "date")] public string Date { get => _date; set => _date = value.Remove(value.IndexOf("星")); }//日期
        [XmlElement(ElementName = "high")] public string HighTemp { get; set; }//最高温度
        public double Double_HighTemp
        {
            get
            {
                return double.Parse(HighTemp.Replace("高温", "").Replace("℃", ""));
            }
            set { Double_HighTemp = value; }
        }//最高温度
        [XmlElement(ElementName = "low")] public string LowTemp { get; set; }//最低温度
        public double Double_LowTemp
        {
            get
            {
                return double.Parse(LowTemp.Replace("低温", "").Replace("℃", ""));
            }
            set { Double_LowTemp = value; }
        }//最高温度
        [XmlElement(ElementName = "day")] public WeatherDayHalf Day { get; set; }//白天
        [XmlElement(ElementName = "night")] public WeatherDayHalf Night { get; set; }//夜间
    }

    public class WeatherDayHalf
    {
        //public ImageSource WeatherImageSource
        //{
        //    get { }
        //    set;
        //}
        [XmlElement(ElementName = "type")] public string State { get; set; }//天气状况
        [XmlElement(ElementName = "fengxiang")] public string Direction { get; set; }//风向
        [XmlElement(ElementName = "fengli")] public string Power { get; set; } //风力
    }

    public class IndexCollection
    {
        [XmlElement(ElementName = "zhishu")] public List<Index> DetailIndexCollection { get; set; }//指数
    }

    public class Index
    {
        [XmlElement(ElementName = "name")] public string Name { get; set; }//指数名称
        [XmlElement(ElementName = "value")] public string Value { get; set; }//值
        [XmlElement(ElementName = "detail")] public string Detail { get; set; }//细节
    }
}
