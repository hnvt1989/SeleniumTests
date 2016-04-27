using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumTests.DOR;

namespace CUST2095.WebTest.DTO
{
    public class SaleOrderStruct
    {

        private List<ProductOrder> _productsList;

        private decimal _orderNet = 0m; //order net
        private decimal _subTotal = 0m;
        private decimal _totalDiscount = 0m;
        private decimal _freightAmt = 0m;
        private decimal _totalAmt = 0m; //order total (including freight and _orderNet)

        private decimal _orderQv = 0m;
        private decimal _orderCv = 0m;

        //discount
        private double _freightDiscount = 4.00;

        private Account _customer;
        private string _carrierMethod = string.Empty;

        public Payment Payment { get; set; }

        private Address shipAddress;

        private List<Incentive> _incentives;
        public List<Incentive> Incentives
        {
            get
            {
                if (_incentives == null)
                    _incentives = new List<Incentive>();
                return _incentives;
            }
            set { }
        }

        public void AddIncentives(IEnumerable<Incentive> incentives)
        {
            if (_incentives == null)
                _incentives = new List<Incentive>();
            _incentives.AddRange(incentives);
        }




        public string OrderType { get; set; }
        public string OrderNumber { get; set; }
        public string SiebelNumber { get; set; }
        public string Comments { get; set; }

        public Address ShippingAddress
        {
            get
            {
                if (this.shipAddress == null)
                {
                    this.shipAddress = this._customer.Address.SingleOrDefault(a=>a.Type == "PRIMARY");
                }
                return this.shipAddress;
            }
            set { this.shipAddress = value; }
        }

        public decimal FreightAmt { get { return _freightAmt; } set { _freightAmt = value; } }
        public decimal TotalAmt { get { return _totalAmt; } set { _totalAmt = value; } }
        public decimal SubTotal { get { return _subTotal; } }
        public decimal SubTotalAfterDiscount {get { return SubTotal - TotalDiscount; }}
        public decimal TotalDiscount { get { return _totalDiscount; } }
        public decimal OrderNet { get { return _orderNet; } set { _orderNet = value; } }
        public string CarrierMethod { get { return _carrierMethod; } set { _carrierMethod = value; } }
        public Account Customer { get { return _customer; } set { _customer = value; } }
        public decimal BalanceDue { get { return TotalAmt - Payment.PaymentAmount; } }
        public string OrderStatus { get; set; }

        public List<ProductOrder> ProductsList { get { return _productsList; } set { _productsList = value; } }

        //is this Direct Ship order
        public bool DirectShip { get; set; }

        //the event that this sales order belong to
        public string Event { get; set; }

        public decimal OrderQv { get { return _orderQv; } set { _orderQv = value; } }
        public decimal OrderCv { get { return _orderCv; } set { _orderCv = value; } }


        public SaleOrderStruct()
        {
            _productsList = new List<ProductOrder>();
            SiebelNumber = string.Empty;
            Comments = string.Empty;
            
        }

        public void AddToProductList(ProductOrder po)
        {
            _productsList.Add(po);
        }

        /// <summary>
        /// Update Order Total
        /// </summary>
        public void UpdateOrderTotal()
        {
            //debug info
            var totalQv = 0m;
            var totalCv = 0m;

            //reset the value, in case other place can call this multiple time resulting in the amounts updated more than needed
            _subTotal = 0m;
            _orderNet = 0m;
            _totalDiscount = 0m;
            _orderQv = 0m;
            _orderCv = 0m;
            _totalAmt = 0m;

            if (this._productsList.Count > 0)
            {
                for (int i = 0; i < this._productsList.Count; i++)
                {
                    ProductOrder po = (ProductOrder)_productsList[i];
                    _subTotal += po.GetSubtotal(po.PriceType);
                    _orderNet += po.GetSubTotalAfterDiscount();
                    _totalDiscount += po.LineItemTotalDiscount();
                    _orderQv += po.GetSubtotalQvAfterDiscount();
                    //cart plan only discount QV
                    _orderCv += po.GetSubtotalCv();

                    //debug info
                    totalQv += po.GetSubtotalQv();
                    totalCv += po.GetSubtotalCv();

                }
            }

            _totalAmt = _orderNet + _freightAmt;

            Console.WriteLine("Subtotal : " + _subTotal);
            Console.WriteLine("Order Net : " + _orderNet);
            Console.WriteLine("Total Discount " + _totalDiscount);
            Console.WriteLine("Total " + _totalAmt);
            Console.WriteLine("Total discounted Qv: " + _orderQv);
            Console.WriteLine("Total discounted Cv: " + _orderCv);
            Console.WriteLine("Total Qv : " + totalQv);
            Console.WriteLine("Total Cv : " + totalCv);
        }
    }
}
