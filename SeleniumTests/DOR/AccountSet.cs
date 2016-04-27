using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using SeleniumTests;
using SeleniumTests.DOR;

namespace CUST2095.WebTest.DTO
{
    [XmlRoot("ACCOUNTS")]
    public class AccountSet
    {
        private Dictionary<string, Account> _accounts = new Dictionary<string, Account>();
        private const string AccountSetXmlFile = "AccountSet.XML";
        private const string NewAccountsXmlFile = "NewAccounts.XML";

        [XmlElement("Account")]
        public List<Account> Accounts
        {
            set
            {
                if (value != null)
                {
                    foreach (Account acc in value)
                    {
                        AddAccount(acc);
                    }
                }
            }
            get { return _accounts.Values.ToList(); }
        }

        public Account GetAccountByUserCode(string userCode)
        {
            if (_accounts.ContainsKey(userCode))
                return _accounts[userCode];
            return null;
        }

        public int AddAccount(Account acc)
        {
            try
            {
                _accounts.Add(acc.UserName, acc);
            }
            catch (ArgumentException)
            {
                return -1;
            }
            return 0;

        }


        public void LoadDataSet()
        {
            if (File.Exists(NewAccountsXmlFile))
            {
                while (!Utils.IsFileReady(NewAccountsXmlFile))
                {
                    Thread.Sleep(500);
                }

                XDocument accountSetDoc = XDocument.Load(NewAccountsXmlFile);

                var accounts = (from account in accountSetDoc.Descendants("Account")
                                select new Account()
                                {
                                    AccountType = account.Element("AccountType").Value,
                                    UserName = account.Element("UserName").Value,
                                    FirstName = account.Element("FirstName").Value,
                                    LastName = account.Element("LastName").Value,
                                    Email = account.Element("Email").Value,
                                    Phone = account.Element("Phone").Value,
                                    MobilePhone = account.Element("MobilePhone").Value,
                                    CompanyName = account.Element("CompanyName").Value,
                                    PreferredFirstName = account.Element("PreferredFirstName").Value,
                                    FullName = account.Element("FullName").Value,
                                    Password = account.Element("Password").Value,
                                    EnrollingCountry = account.Element("EnrollingCountry").Value,
                                    EnrollingSponsor = account.Element("EnrollingSponsor").Value,
                                    Optin = Boolean.Parse(account.Element("Optin").Value),
                                    VatNumber = account.Element("VatNumber").Value,
                                    IdNumber = account.Element("IdNumber").Value,
                                    BirthDay = account.Element("BirthDay").Value,
                                    NameOnAccount = account.Element("NameOnAccount").Value,
                                    SortCode = account.Element("SortCode").Value,
                                    AccountNumber = account.Element("AccountNumber").Value,
                                    Address = (from address in account.Elements("Address")
                                               select new Address()
                                               {
                                                   Type = address.Element("Type").Value,
                                                   AddrLine1 = address.Element("AddrLine1").Value,
                                                   AddrLine2 = address.Element("AddrLine2").Value,
                                                   AddrLine3 = address.Element("AddrLine3").Value,
                                                   AddrLine4 = address.Element("AddrLine4").Value,
                                                   AddrLine5 = address.Element("AddrLine5").Value,
                                                   PostalCode = address.Element("PostalCode").Value,
                                                   City = address.Element("City").Value,
                                                   State = address.Element("State").Value,
                                                   Country = address.Element("Country").Value

                                               }
                                ).ToList<Address>()
                                }
                              ).ToList<Account>();

                this.Accounts = accounts;
            }

            foreach (var acc in this.Accounts)
            {
                foreach (var addr in acc.Address)
                {
                    if (addr.AddrLine2 == "None")
                        addr.AddrLine2 = string.Empty;
                    if (addr.AddrLine3 == "None")
                        addr.AddrLine3 = string.Empty;
                    if (addr.AddrLine4 == "None")
                        addr.AddrLine4 = string.Empty;
                    if (addr.AddrLine5 == "None")
                        addr.AddrLine5 = string.Empty;
                    if (addr.Country == "GB")
                        addr.Country = "United Kingdom";
                    if (addr.Type == "HOME")
                        addr.Type = "PRIMARY";
                }
            }
            //else
            //{
            //    BuildAccountSet();

            //    XmlSerializer ser = new XmlSerializer(typeof(AccountSet));
            //    XmlTextWriter writer = new XmlTextWriter(AccountSetXmlFile, null);
            //    ser.Serialize(writer, this);
            //}
        }


        public void SaveAccountToFile(Account acc)
        {
            if (File.Exists(NewAccountsXmlFile))
            {
                XDocument accountSetDoc = XDocument.Load(NewAccountsXmlFile);

                var accounts = (from account in accountSetDoc.Descendants("Account")
                                select new Account()
                                {
                                    UserName = account.Element("UserName").Value,
                                    AccountType = account.Element("AccountType").Value,
                                    FirstName = account.Element("FirstName").Value,
                                    LastName = account.Element("LastName").Value,
                                    Email = account.Element("Email").Value,
                                    Phone = account.Element("Phone").Value,
                                    MobilePhone = account.Element("MobilePhone").Value,
                                    CompanyName = account.Element("CompanyName").Value,
                                    PreferredFirstName = account.Element("PreferredFirstName").Value,
                                    FullName = account.Element("FullName").Value,
                                    Password = account.Element("Password").Value,
                                    EnrollingCountry = account.Element("EnrollingCountry").Value,
                                    EnrollingSponsor = account.Element("EnrollingSponsor").Value,
                                    Optin = Boolean.Parse(account.Element("Optin").Value),
                                    VatNumber = account.Element("VatNumber").Value,
                                    IdNumber = account.Element("IdNumber").Value,
                                    BirthDay = account.Element("BirthDay").Value,
                                    NameOnAccount = account.Element("NameOnAccount").Value,
                                    SortCode = account.Element("SortCode").Value,
                                    AccountNumber = account.Element("AccountNumber").Value,
                                    JoinDate = account.Element("JoinDate").Value,
                                    Address = (from address in account.Elements("Address")
                                               select new Address()
                                               {
                                                   Type = address.Element("Type").Value,
                                                   AddrLine1 = address.Element("AddrLine1").Value,
                                                   AddrLine2 = address.Element("AddrLine2").Value,
                                                   AddrLine3 = address.Element("AddrLine3").Value,
                                                   AddrLine4 = address.Element("AddrLine4").Value,
                                                   AddrLine5 = address.Element("AddrLine5").Value,
                                                   PostalCode = address.Element("PostalCode").Value,
                                                   City = address.Element("City").Value,
                                                   State = address.Element("State").Value,
                                                   Country = address.Element("Country").Value

                                               }
                                ).ToList<Address>()
                                }
                              ).ToList<Account>();

                this.Accounts = accounts;
            }

            AddAccount(acc);

            foreach (var acccount in this.Accounts)
            {
                foreach (var addr in acccount.Address)
                {
                    if (addr.Type == "HOME")
                        addr.Type = "PRIMARY";
                    if (addr.AddrLine2 == string.Empty)
                        addr.AddrLine2 = "None";
                    if (addr.AddrLine3 == string.Empty)
                        addr.AddrLine3 = "None";
                    if (addr.AddrLine4 == string.Empty)
                        addr.AddrLine4 = "None";
                    if (addr.AddrLine5 == string.Empty)
                        addr.AddrLine5 = "None";
                }
            }

            XmlSerializer ser = new XmlSerializer(typeof(AccountSet));
            XmlTextWriter writer = new XmlTextWriter(NewAccountsXmlFile, null);
            ser.Serialize(writer, this);
        }

        public void SaveAccountsToFileBatch(IEnumerable<Account> accounts)
        {
            foreach (var account in accounts)
            {
                AddAccount(account);
            }
            XmlSerializer ser = new XmlSerializer(typeof(AccountSet));
            XmlTextWriter writer = new XmlTextWriter(NewAccountsXmlFile, null);
            ser.Serialize(writer, this);

        }


        //TODO
        public List<Account> ReadNewAccounts()
        {
            return null;
        }

        public void UpdateAccount(Account account)
        {
            _accounts.Remove(account.UserName);
            AddAccount(account);

            XmlSerializer ser = new XmlSerializer(typeof(AccountSet));
            XmlTextWriter writer = new XmlTextWriter(NewAccountsXmlFile, null);
            ser.Serialize(writer, this);
        }

    }
}
