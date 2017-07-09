namespace QAWorks.Int.Test
{
    [System.Serializable()]
    public class CSSSelector
    {
        [System.Xml.Serialization.XmlAttribute("FriendlyFieldName")]
        public string FriendlyFieldName { get; set; }

        [System.Xml.Serialization.XmlAttribute("CSSSelectorType")]
        public string CSSSelectorType { get; set; }

        [System.Xml.Serialization.XmlAttribute("CSSSelectorValue")]
        public string CSSSelectorValue { get; set; }
    }
}
