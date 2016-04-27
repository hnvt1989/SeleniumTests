using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace CUST2095.WebTest.DTO
{
    [Serializable]
    public class Product
    {

        private Dictionary<string, Price> priceList;

        public Product(string stockNum, string description)
        {
            StockNum = stockNum;
            Description = description;
            priceList = new Dictionary<string, Price>();
        }

        public Product()
        {
            priceList = new Dictionary<string, Price>();
        }
        [XmlElement("StockNum")]
        public string StockNum { get; set; }

        [XmlElement("Description")]
        public string Description { get; set; }

        [XmlElement("Price")]
        public List<Price> PriceList
        {
            set
            { //if (value != null)
                //{
                foreach (Price price in value)
                {
                    AddPrice(price);
                }
                //}
            }
            get { return priceList.Values.ToList(); }
        }

        public int AddPrice(Price price)
        {
            try
            {
                priceList.Add(price.Type, price);
            }
            catch (ArgumentException)
            {
                return -1;
            }
            return 0;
        }

        public Price GetPriceByPriceType(string type)
        {
            if (priceList.ContainsKey(type))
                return priceList[type];
            return null;
        }

    }

    public class Price
    {
        private Dictionary<string, Volume> volumeList;
        public Price(string type, decimal amount)
        {
            Type = type;
            Amount = amount;
            volumeList = new Dictionary<string, Volume>();
        }

        public Price()
        {
            volumeList = new Dictionary<string, Volume>();
        }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Amount")]
        public decimal Amount { get; set; }

        [XmlElement("Volume")]
        public List<Volume> VolumeList
        {
            set
            {
                foreach (Volume vol in value)
                {
                    AddVolume(vol);
                }
            }
            get { return volumeList.Values.ToList(); }
        }

        public int AddVolume(Volume vol)
        {
            try
            {
                volumeList.Add(vol.Type, vol);
            }
            catch (ArgumentException)
            {
                return -1;
            }
            return 0;
        }

        public Volume GetVolumeByVolumeType(string type)
        {
            if (volumeList.ContainsKey(type))
                return volumeList[type];
            return null;
        }

    }

    public class Volume
    {
        public Volume()
        {
        }

        public Volume(string type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }
        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("Amount")]
        public decimal Amount { get; set; }
    }
}
