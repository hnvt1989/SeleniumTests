using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CUST2095.WebTest.DTO
{
    public class ProductOrder
    {

        private Product _product;

        public ProductOrder(Product product)
        {
            _product = product;
            Editable = true;
            Discount = new ItemDiscount()
                {
                    Value = 0m,
                    Description = string.Empty
                };
        }

        public Product Product { set { _product = value; } get { return _product; } }
        public ItemDiscount Discount { get; set; }
        public Boolean HasDiscount
        {
            get { return (Discount.Value > 0m) && (Discount.Description == ""); }
        }
        //can user edit this line item, or it's read-only
        public Boolean Editable { get; set; }

        public decimal PercentDiscount 
        { get 
            { 
                return (GetSubTotal() > 0) 
                    ?LineItemTotalDiscount() / GetSubTotal()
                    : 0m ; 
            } 
        }

        public int Qty { set; get; }
        public string PriceType { set; get; }

        public decimal GetSubtotal(string priceType)
        {
            return Qty * _product.GetPriceByPriceType(priceType).Amount;
        }

        public decimal GetSubTotalAfterDiscount()
        {
            //if (HasDiscount)
            return GetSubTotal() - LineItemTotalDiscount();
            //return GetSubTotal();
        }

        public decimal LineItemTotalDiscount()
        {
            return (Discount.Value * Qty);
        }

        public decimal GetSubTotal()
        {
            return Qty * _product.GetPriceByPriceType(PriceType).Amount;
        }

        public decimal GetSubtotalCv(string priceType)
        {
            return Qty * _product.GetPriceByPriceType(priceType).GetVolumeByVolumeType("CV").Amount;
        }

        public decimal GetSubtotalCv()
        {
            return Qty * _product.GetPriceByPriceType(PriceType).GetVolumeByVolumeType("CV").Amount;
        }

        public decimal GetSubtotalQv(string priceType)
        {
            return Qty * _product.GetPriceByPriceType(priceType).GetVolumeByVolumeType("QV").Amount;
        }

        public decimal GetSubtotalQv()
        {
            return Qty * _product.GetPriceByPriceType(PriceType).GetVolumeByVolumeType("QV").Amount;
        }

        public decimal GetSubtotalQvAfterDiscount()
        {
            var baseVolume = (GetSubtotalQv()/Qty);
            var discountAmount = Math.Round(baseVolume *PercentDiscount, 2, MidpointRounding.AwayFromZero);
            return Convert.ToDecimal(Qty * (baseVolume - discountAmount));
        }

        public decimal GetSubtotalCvAfterDiscount()
        {
            var baseVolume = (GetSubtotalCv() / Qty);
            var discountAmount = Math.Round(baseVolume * PercentDiscount, 2, MidpointRounding.AwayFromZero);
            return Convert.ToDecimal(Qty * (baseVolume - discountAmount));
        }
    }

    public class ItemDiscount
    {
        public ItemDiscount()
        {
        }

        public string Description { get; set; }
        public string PriceType { get; set; }
        public decimal Value { get; set; }
    }
}
