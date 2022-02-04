IF OBJECT_ID('viwPaymentRequests') IS NOT NULL DROP View viwPaymentRequests


GO

CREATE VIEW viwPaymentRequests AS

SELECT PaymentRequestId,
		TransactionDate,
		ReferenceNo,
		Merchant,
		AccountNo,
		AccountName,
		OtherDetails,
		Amount,
		RequestStatus,
		FileAttachmentPath
FROM tblPaymentRequests


GO
