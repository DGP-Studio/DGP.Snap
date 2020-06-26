using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DGP.Snap.Service.Weather.Model
{
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
        [XmlElement(ElementName = "day")] public WeatherHalfDay Day { get; set; }//白天
        [XmlElement(ElementName = "night")] public WeatherHalfDay Night { get; set; }//夜间
    }
}
