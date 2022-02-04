IF OBJECT_ID('tblPaymentRequests') IS NOT NULL DROP TABLE tblPaymentRequests


GO

CREATE TABLE tblPaymentRequests (
	    PaymentRequestId INT IDENTITY (1,1),
		TransactionDate DATETIME NOT NULL,
        ReferenceNo VARCHAR(100),
        Merchant VARCHAR(100),
        AccountNo VARCHAR(100),
        AccountName VARCHAR(100),
        OtherDetails VARCHAR(100),
        Amount FLOAT,
        RequestStatus VARCHAR(100),
        FileAttachmentPath VARCHAR(100)
)


GO
