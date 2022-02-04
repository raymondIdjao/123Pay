IF OBJECT_ID('spGetPaymentRequest') IS NOT NULL DROP PROCEDURE spGetPaymentRequest


GO

CREATE PROCEDURE spGetPaymentRequest (@Id Int)
AS

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
WHERE PaymentRequestId = @Id

GO
