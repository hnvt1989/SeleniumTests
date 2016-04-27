using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUST2095.WebTest.DTO
{
    public class Payment
    {
        public Payment()
        {
        }
        /// <summary>
        /// Cash, Credit Card, COA
        /// </summary>
        public string PaymentType { set; get; }

        /// <summary>
        /// Payment amount, full or partial
        /// </summary>
        public decimal PaymentAmount { get; set; }
        public string CardHolder { set; get; }
        public string CardType { set; get; }
        public string CardNum { set; get; }
        public string MaskedCardNum { get; set; }
        public string NameOnCard { set; get; }
        public string ExpMonth { set; get; }
        public string ExpYear { set; get; }
        public string PaymentStatus { set; get; }
    }
}
