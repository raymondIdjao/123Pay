using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _123PayMVC.ViewModels;
using _123PayMVC.Models;

namespace _123PayMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Payment : ControllerBase
    {
        private _123PayDBContext __123PayDBContext;
        public Payment(_123PayDBContext dbContext)
        {
            __123PayDBContext = dbContext;
        }

        //[HttpGet("GetSample")]
        //public IActionResult GetSample()
        //{
        //    var paymentRequests = GetPaymentRequests();
        //    return Ok(paymentRequests);
        //}

        //[HttpGet("GetList")]
        //public IActionResult GetList()
        //{
        //    var paymentRequests = __123PayDBContext.tblPaymentRequests.ToList();
        //    return Ok(paymentRequests);
        //}

        //private List<PaymentRequest> GetPaymentRequests()
        //{
        //    return new List<PaymentRequest>{
        //        new PaymentRequest { ReferenceNo = "101", Merchant = "Netflix", AccountNo = "1001", AccountName = "Test1", Amount = 999.99 },
        //        new PaymentRequest { ReferenceNo = "102", Merchant = "TELEPLAY", AccountNo = "1002", AccountName = "Test2", Amount = 888.99 },
        //        new PaymentRequest { ReferenceNo = "103", Merchant = "AT&T", AccountNo = "1003", AccountName = "Test3", Amount = 1099.99 }
        //    };
        //}

        [HttpPost("Submit")]
        public IActionResult Create([FromBody] PaymentRequest request)
        {
            //validation
            if( (request.ReferenceNo == null || request.ReferenceNo.Trim() == "") &&
                (request.Merchant == null || request.Merchant.Trim() == "") &&
                (request.AccountNo == null || request.AccountNo.Trim() == "") &&
                (request.AccountName == null || request.AccountName.Trim() == "") &&
                (request.Amount <= 0)
              )
            {
                return StatusCode(500, "some data are invalid.");
            }


            tblPaymentRequests paymentRequest = new tblPaymentRequests();
            paymentRequest.ReferenceNo = request.ReferenceNo;
            paymentRequest.TransactionDate = DateTime.Now;
            paymentRequest.Merchant = request.Merchant;
            paymentRequest.AccountNo = request.AccountNo;
            paymentRequest.AccountName = request.AccountName;
            paymentRequest.OtherDetails = request.OtherDetails;
            paymentRequest.Amount = request.Amount;
            paymentRequest.RequestStatus = "PENDING";

            try
            {
                __123PayDBContext.tblPaymentRequests.Add(paymentRequest);
                __123PayDBContext.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500, "an internal error has occured");
            }

            var paymentRequests = __123PayDBContext.tblPaymentRequests.ToList();
            return Ok(paymentRequests);
        }
    }
}
