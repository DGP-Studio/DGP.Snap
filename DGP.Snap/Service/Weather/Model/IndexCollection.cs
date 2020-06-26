using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DGP.Snap.Service.Weather.Model
{
    public class IndexCollection
    {
        [XmlElement(ElementName = "zhishu")] public List<Index> DetailIndexCollection { get; set; }//指数
    }
}
