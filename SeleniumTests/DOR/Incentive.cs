using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUST2095.WebTest.DTO
{
    public class Incentive
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Earned { get; set; }
        public decimal Used { get; set;}
        public decimal Remaining { get { return Earned - Used; } }
        public string PriceType { get; set; }
    }
}
