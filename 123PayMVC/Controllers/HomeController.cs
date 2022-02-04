
using _123PayMVC.Models;
using _123PayMVC.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _123PayMVC.Controllers
{
    public class HomeController : Controller
    {
        private _123PayDBContext db;
        private readonly IHostingEnvironment hostingEnvironment;
        public HomeController(_123PayDBContext dbContext,
                              IHostingEnvironment hostingEnvironment)
        {
            db = dbContext;
            this.hostingEnvironment = hostingEnvironment;
        }
        public ViewResult Index()
        {
            List<tblPaymentRequests> PaymentRequests = db.tblPaymentRequests.ToList();
            List<AttachPaymentRequest> attachPaymentRequests = attachPaymentRequestsConvert(PaymentRequests);
            
            return View(attachPaymentRequests);
        }

        public ViewResult ProcessRequest(int id)
        {
            var PaymentRequest = db.tblPaymentRequests
                                    .FromSqlRaw<tblPaymentRequests>("spGetPaymentRequest {0}", id)
                                    .ToList()
                                    .FirstOrDefault();
            PaymentRequest.RequestStatus = "PROCESSING";

            db.Entry(PaymentRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["Error"] = "Unexpected error occurred. Contact administrator if issue persists.";
            }

            List<tblPaymentRequests> PaymentRequests = db.tblPaymentRequests.ToList();
            List<AttachPaymentRequest> attachPaymentRequests = attachPaymentRequestsConvert(PaymentRequests);

            return View("Index", attachPaymentRequests);
        }
        public ViewResult DeclineRequest(int id)
        {
            var PaymentRequest = db.tblPaymentRequests
                                    .FromSqlRaw<tblPaymentRequests>("spGetPaymentRequest {0}", id)
                                    .ToList()
                                    .FirstOrDefault();
            PaymentRequest.RequestStatus = "FAILED";

            db.Entry(PaymentRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["Error"] = "Unexpected error occurred. Contact administrator if issue persists.";
            }

            List<tblPaymentRequests> PaymentRequests = db.tblPaymentRequests.ToList();
            List<AttachPaymentRequest> attachPaymentRequests = attachPaymentRequestsConvert(PaymentRequests);

            return View("Index", attachPaymentRequests);
        }

        [HttpPost]
        public ViewResult UploadFile(int id, IFormFile photo)
        {
            string newFileName = null;
            if (ModelState.IsValid)
            {
                if(photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    newFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, newFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
            }
            tblPaymentRequests PaymentRequest = db.tblPaymentRequests.Where(x => x.PaymentRequestId == id).FirstOrDefault();
            PaymentRequest.RequestStatus = "COMPLETE";
            PaymentRequest.FileAttachmentPath = newFileName;

            db.Entry(PaymentRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["Error"] = "Unexpected error occurred. Contact administrator if issue persists.";
            }


            List<tblPaymentRequests> PaymentRequests = db.tblPaymentRequests.ToList();
            List<AttachPaymentRequest> attachPaymentRequests = attachPaymentRequestsConvert(PaymentRequests);

            return View("Index", attachPaymentRequests);
        }
        [HttpPost]
        public ViewResult UploadFailedFile(int id, IFormFile photo)
        {
            string newFileName = null;
            if (ModelState.IsValid)
            {
                if(photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    newFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, newFileName);
                    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
            }
            tblPaymentRequests PaymentRequest = db.tblPaymentRequests.Where(x => x.PaymentRequestId == id).FirstOrDefault();
            PaymentRequest.FileAttachmentPath = newFileName;

            db.Entry(PaymentRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                TempData["Error"] = "Unexpected error occurred. Contact administrator if issue persists.";
            }


            List<tblPaymentRequests> PaymentRequests = db.tblPaymentRequests.ToList();
            List<AttachPaymentRequest> attachPaymentRequests = attachPaymentRequestsConvert(PaymentRequests);

            return View("Index", attachPaymentRequests);
        }

        public List<AttachPaymentRequest> attachPaymentRequestsConvert(List<tblPaymentRequests> tblPaymentRequests)
        {
            List<AttachPaymentRequest> attachPaymentRequests = new List<AttachPaymentRequest>();
            foreach (var paymentRequest in tblPaymentRequests)
            {
                AttachPaymentRequest attachPaymentRequest = new AttachPaymentRequest
                {
                    PaymentRequestId = paymentRequest.PaymentRequestId = paymentRequest.PaymentRequestId,
                    ReferenceNo = paymentRequest.ReferenceNo = paymentRequest.ReferenceNo,
                    TransactionDate = paymentRequest.TransactionDate = paymentRequest.TransactionDate,
                    Merchant = paymentRequest.Merchant = paymentRequest.Merchant,
                    AccountNo = paymentRequest.AccountNo = paymentRequest.AccountNo,
                    AccountName = paymentRequest.AccountName = paymentRequest.AccountName,
                    OtherDetails = paymentRequest.OtherDetails = paymentRequest.OtherDetails,
                    Amount = paymentRequest.Amount = paymentRequest.Amount,
                    RequestStatus = paymentRequest.RequestStatus = paymentRequest.RequestStatus,
                    FileAttachmentPath = paymentRequest.FileAttachmentPath = paymentRequest.FileAttachmentPath
                };
                attachPaymentRequests.Add(attachPaymentRequest);
            }
            return attachPaymentRequests;
        }
    }
}
