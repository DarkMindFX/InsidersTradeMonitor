
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Report_NonDerivPurchaseSale', 'P') IS NOT NULL
DROP PROC [dbo].[p_Report_NonDerivPurchaseSale]
GO

/*
Returns report showing cummulative purchases/sales for the given company and for specific date range

*/
CREATE PROCEDURE [dbo].[p_Report_NonDerivPurchaseSale]
		@IssuerID		BIGINT,
		@StartDate		DATETIME,
		@EndDate		DATETIME
AS
BEGIN

	SELECT
		ndt.Date,
		SUM(IIF( ndt.TransactionCodeID = 1, ndt.SharesAmount, 0 )) As Purchased,
		SUM(IIF( ndt.TransactionCodeID = 2, ndt.SharesAmount, 0 )) As Sold,
		SUM(IIF( ndt.TransactionCodeID = 1, ndt.SharesAmount, 0 )) - SUM(IIF( ndt.TransactionCodeID = 2, ndt.SharesAmount, 0 )) As Net,
		COUNT(DISTINCT ndt.Form4ReportID) As ReportsCount
	FROM dbo.v_NonDerivativeTransaction ndt
	WHERE 
		ndt.Date BETWEEN @StartDate AND @EndDate AND
		ndt.IssuerID = @IssuerID
	GROUP BY ndt.Date

END
SET ANSI_NULLS ON
GO