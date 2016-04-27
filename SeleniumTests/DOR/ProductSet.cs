using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CUST2095.WebTest.DTO
{
    [XmlRoot("PRODUCTS")]
    public class ProductSet
    {
        private Dictionary<string, Product> _products = new Dictionary<string, Product>();
        private const string ProductSetXmlFile = "ProductSet.XML";

        [XmlElement("Product")]
        public List<Product> Products
        {
            set
            {
                if (value != null)
                {
                    foreach (Product prod in value)
                    {
                        AddProduct(prod);
                    }
                }
            }
            get { return _products.Values.ToList(); }
        }

        public Product GetProductByStockNum(string stockNum)
        {
            if (_products.ContainsKey(stockNum))
                return this._products[stockNum];
            return null;
        }

        public int AddProduct(Product product)
        {
            try
            {
                _products.Add(product.StockNum, product);
            }
            catch (ArgumentException)
            {
                return -1;
            }
            return 0;

        }

        public void LoadDataSet()
        {
            //if ProductSet file exists, parse the file
            if (File.Exists(ProductSetXmlFile))
            {
                XDocument xmlDoc = XDocument.Load(ProductSetXmlFile);

                var products = (from product in xmlDoc.Descendants("Product")
                                select new Product()
                                {
                                    StockNum = product.Element("StockNum").Value,
                                    Description = product.Element("Description").Value,
                                    PriceList = (from price in product.Elements("Price")
                                                 select new Price()
                                                 {
                                                     Type = price.Element("Type").Value,
                                                     Amount = Decimal.Parse(price.Element("Amount").Value),
                                                     VolumeList = (from volume in price.Elements("Volume")
                                                                   select new Volume()
                                                                   {
                                                                       Type = volume.Element("Type").Value,
                                                                       Amount =
                                                                           Decimal.Parse(
                                                                               volume.Element("Amount")
                                                                                     .Value)
                                                                   }).ToList<Volume>()

                                                 }
                                                ).ToList<Price>()
                                }).ToList<Product>();

                Products = products;

            }


        }

        public void BuildProductSet()
        {
            XmlSerializer ser = new XmlSerializer(typeof(ProductSet));
            XmlTextWriter writer = new XmlTextWriter(ProductSetXmlFile, null);
            ser.Serialize(writer, this);
        }
    }
}
