using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QAWorks.Int.Test
{
    [Serializable()]
    public class PageMap
    {
        [XmlArray("CSSSelectors")]
        [XmlArrayItem("CSSSelector", typeof(CSSSelector))]
        public List<CSSSelector> CSSSelectors = new List<CSSSelector>();
    }
}
