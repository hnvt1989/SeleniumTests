using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUST2095.WebTest.DTO
{
    public class WebTestException : Exception
    {
        public WebTestException(string message, Exception innerException)
			: base(message, innerException) {}

        public WebTestException(string message)
            : base(message) { }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
