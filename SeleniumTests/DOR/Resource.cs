using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CUST2095.WebTest.DTO
{
    [Serializable]
    public class Resource
    {
        public Resource()
        {
            
        }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Value")]
        public string Value { get; set; }

        [XmlElement("Context")]
        public string Context { get; set; }
    }
}
