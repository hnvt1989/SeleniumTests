using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;


namespace SeleniumTests.DOR
{
    [Serializable]
    public class Account
    {

        private Dictionary<string, Address> address;

        private Dictionary<TestBase.AccountTypeIndex, string> typeDescription = new Dictionary<TestBase.AccountTypeIndex, string>()
            {
                {TestBase.AccountTypeIndex.CON, "Consultant"},
                {TestBase.AccountTypeIndex.CUS, "Customer"},
                {TestBase.AccountTypeIndex.KIT, "Kit Consultant"},
                {TestBase.AccountTypeIndex.KIT25, "Kit25 Consultant"}
            };


        public Account()
        {
            address = new Dictionary<string, Address>();

        }

        public string TypeDescription(TestBase.AccountTypeIndex index)
        {
            return typeDescription[index];
        }

        [XmlElement("AccNum")]
        public string AccNum { set; get; }

        [XmlElement("JoinDate")]
        public string JoinDate { get; set; }

        [XmlElement("AccountType")]
        public string AccountType { set; get; }

        [XmlElement("UserName")]
        public string UserName { set; get; }

        [XmlElement("EnrollingSponsor")]
        public string EnrollingSponsor { set; get; }

        [XmlElement("FirstName")]
        public string FirstName { set; get; }

        [XmlElement("LastName")]
        public string LastName { set; get; }

        [XmlElement("PreferredFirstName")]
        public string PreferredFirstName { set; get; }

        [XmlElement("FullName")]
        public string FullName { set { } get { return FirstName + " " + LastName; } }


        [XmlElement("Address")]
        public List<Address> Address
        {
            set
            {
                if (value != null)
                {
                    foreach (Address addr in value)
                    {
                        address.Add(addr.Type, addr);
                    }
                }
            }
            get { return address.Values.ToList(); }
        }


        public int AddAddress(Address addr)
        {
            try
            {
                address.Add(addr.Type, addr);
            }
            catch (ArgumentException)
            {
                return -1;
            }
            return 0;
        }

        [XmlElement("Email")]
        public string Email { set; get; }

        [XmlElement("MobileEmail")]
        public string MobileEmail { set; get; }

        [XmlElement("Password")]
        public string Password { set; get; }

        [XmlElement("EnrollingCountry")]
        public string EnrollingCountry { set; get; }

        [XmlElement("Optin")]
        public bool Optin { set; get; }

        [XmlElement("CompanyName")]
        public string CompanyName { set; get; }

        [XmlElement("VatNumber")]
        public string VatNumber { set; get; }

        [XmlElement("IdNumber")]
        public string IdNumber { set; get; }

        [XmlElement("BirthDay")]
        public string BirthDay { set; get; }

        [XmlElement("Phone")]
        public string Phone { set; get; }

        [XmlElement("MobilePhone")]
        public string MobilePhone { set; get; }

        [XmlElement("NameOnAccount")]
        public string NameOnAccount { set; get; }

        [XmlElement("SortCode")]
        public string SortCode { set; get; }

        [XmlElement("AccountNumber")]
        public string AccountNumber { set; get; }
    }

    public class Address
    {

        public Address()
        {
            Name = string.Empty;
            AddrLine1 = string.Empty;
            AddrLine2 = string.Empty;
            AddrLine3 = string.Empty;
            AddrLine4 = string.Empty;
            AddrLine5 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Country = string.Empty;
        }

        public Address(string type)
        {
            Type = type;
        }

        //used for shipping address
        public string Name { get; set; }

        [XmlElement("Type")]
        public string Type { get; set; }

        [XmlElement("AddrLine1")]
        public string AddrLine1 { get; set; }

        [XmlElement("AddrLine2")]
        public string AddrLine2 { get; set; }

        [XmlElement("AddrLine3")]
        public string AddrLine3 { get; set; }

        [XmlElement("AddrLine4")]
        public string AddrLine4 { get; set; }

        [XmlElement("AddrLine5")]
        public string AddrLine5 { get; set; }

        [XmlElement("PostalCode")]
        public string PostalCode { get; set; }

        [XmlElement("City")]
        public string City { get; set; }

        [XmlElement("State")]
        public string State { get; set; }

        [XmlElement("Country")]
        public string Country { get; set; }

        private List<string> MultipleAddrPostcodeList = new List<string>() { "M1 1AA", "E20 2ST", };

        public string GetRandomMultipleAddrPostCode()
        {
            return MultipleAddrPostcodeList.ElementAt(new Random().Next(0, MultipleAddrPostcodeList.Count() - 1));
        }

        private List<string> SingleAddrPostCodeList = new List<string>() { "G58 1SB", "E20 3HB", "N81 1ER" };

        public string GetRandomSingleAddrPostCode()
        {
            return SingleAddrPostCodeList.ElementAt(new Random().Next(0, SingleAddrPostCodeList.Count() - 1));
        }

    }

}
