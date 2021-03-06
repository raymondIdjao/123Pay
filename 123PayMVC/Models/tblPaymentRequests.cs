using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _123PayMVC.Models
{
    public class tblPaymentRequests
    {
        [Key]
        public int PaymentRequestId { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Merchant { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string OtherDetails { get; set; }
        public double Amount { get; set; }
        public string RequestStatus { get; set; }
        public string FileAttachmentPath { get; set; }
    }
}
