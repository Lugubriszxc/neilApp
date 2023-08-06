using System;
using System.Collections.Generic;

namespace neilApp.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public float SubTotal { get; set; }
        public float DiscountPercent { get; set; }
        public float Deduction { get; set; }
        public float TotalAmount { get; set; }
        public float PaidAmount { get; set; }
        public float Sukli { get; set; }
        public string Datepurchased { get; set; }
    }
}
