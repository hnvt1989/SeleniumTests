using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using SeleniumTests;

namespace CUST2095.WebTest.DTO
{
    [XmlRoot("Resources")]
    public class ResourceSet
    {
        private List<Resource> _resources = new List<Resource>();
        private const string ResourceSetXmlFile = "ResourceSet.XML";

        [XmlElement("Resource")]
        public List<Resource> Resources
        {
            set
            {
                if (value != null)
                {
                    _resources.AddRange(value);
                }
            }
            get { return _resources; }
        }

        public void LoadDataSet()
        {
            if (File.Exists(ResourceSetXmlFile))
            {
                while (!Utils.IsFileReady(ResourceSetXmlFile))
                {
                    Thread.Sleep(500);
                }

                XDocument resourceSetDoc = XDocument.Load(ResourceSetXmlFile);

                var resources = (from resource in resourceSetDoc.Descendants("Resource")
                                select new Resource()
                                {
                                    Name = resource.Element("Name").Value,
                                    Value = resource.Element("Value").Value,
                                    Context = resource.Element("Context").Value,
                                }
                              ).ToList<Resource>();

                this.Resources = resources;
            }
            else
            {
                SaveResourcesToFile();
            }


        }

        public void CreateResourceSet()
        {
            _resources.Add(new Resource() { Context = "Address", Name = "ShipToName" , Value = "Name"});
            _resources.Add(new Resource() { Context = "Address", Name = "Postcodelookup", Value = "PostCode Lookup" });
            _resources.Add(new Resource() { Context = "Address", Name = "Country", Value = "Country" });
            _resources.Add(new Resource() { Context = "Address", Name = "Line1", Value = "House No & Street" });
            _resources.Add(new Resource() { Context = "Address", Name = "Line2", Value = "Address Line 2" });
            _resources.Add(new Resource() { Context = "Address", Name = "City", Value = "Town/City" });
            _resources.Add(new Resource() { Context = "Address", Name = "Postcode", Value = "Postcode" });
            _resources.Add(new Resource() { Context = "Address", Name = "State", Value = "County" });
            _resources.Add(new Resource() { Context = "Address", Name = "Line3", Value = "Ship To Mobile" });
            _resources.Add(new Resource() { Context = "Address", Name = "Line4", Value = "Ship To Email" });
            _resources.Add(new Resource() { Context = "Address", Name = "Line5", Value = "Special Delivery Instructions" });

        }
        public void SaveResourcesToFile()
        {
            CreateResourceSet();

            XmlSerializer ser = new XmlSerializer(typeof(ResourceSet));
            XmlTextWriter writer = new XmlTextWriter(ResourceSetXmlFile, null);
            ser.Serialize(writer, this);
        }

        public string GetResourceValue(string id)
        {
            var context = id.Split('.')[0];
            var name = id.Split('.')[1];
            return _resources.SingleOrDefault(r => r.Context == context && r.Name == name).Value;
        }
    }
}
