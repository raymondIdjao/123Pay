using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _123PayMVC.ViewModels
{
    public class PaymentRequest
    {
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public string Merchant { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string OtherDetails { get; set; }
        public double Amount { get; set; }
    }
}
