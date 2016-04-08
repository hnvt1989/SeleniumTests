using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SeleniumTests
{
    [SetUpFixture]
    public class Setup
    {
        [SetUp]
        public void SetupTests()
        {
            TestSettings.SiteUrl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"];
            TestSettings.Username = System.Configuration.ConfigurationManager.AppSettings["Username"];
            TestSettings.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];
        }

        [TearDown]
        public void TeardownTests()
        {
            
        }
    }
}
