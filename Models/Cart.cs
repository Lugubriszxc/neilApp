using System;
using System.Collections.Generic;

namespace neilApp.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int ProdId { get; set; }
        public int CQuantity { get; set; }
        public int CMockTotal { get; set; }
        public int CMockStock { get; set; }
    }
}
