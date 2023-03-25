SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Report_NonDerivTransactionsByDate', 'P') IS NOT NULL
DROP PROC [dbo].[p_Report_NonDerivTransactionsByDate]
GO

/*
Returns list of all non-derivative transactions for given date for given company

*/
CREATE PROCEDURE [dbo].[p_Report_NonDerivTransactionsByDate]
		@IssuerID	BIGINT,
		@Date		DATETIME
AS
BEGIN

	SELECT
		ndo.ID,
		ndo.Form4ReportID,
		f4r.ReportID as SECReportID,
		ndo.TitleOfSecurity,
		ndo.TransactionDate,
		ndo.DeemedExecDate,
		ndo.TransactionCodeID,
		tc.Code as TrasnactionCode,
		ndo.EarlyVoluntarilyReport,
		ndo.SharesAmount,
		ndo.TransactionTypeID,
		tt.Code as TransactionType,
		ndo.Price,
		ndo.AmountFollowingReport,
		ndo.OwnershipTypeID,
		ot.Code as OwnershipType,
		ndo.NatureOfIndirectOwnership
	FROM		dbo.NonDerivativeTransaction ndo
	INNER JOIN	dbo.Form4Report f4r ON f4r.ID = ndo.Form4ReportID
	INNER JOIN  dbo.TransactionCode tc ON tc.ID = ndo.TransactionCodeID
	INNER JOIN	dbo.TransactionType tt ON tt.ID = ndo.TransactionTypeID
	INNER JOIN	dbo.OwnershipType	ot ON ot.ID = ndo.OwnershipTypeID
	INNER JOIN	dbo.Entity			e	ON e.ID = f4r.ReporterID
	WHERE f4r.IssuerID = @IssuerID AND f4r.Date = @Date

	
	SET NOCOUNT ON;
END