SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Report_NonDerivAcquiredDisposed', 'P') IS NOT NULL
DROP PROC [dbo].[p_Report_NonDerivAcquiredDisposed]
GO

/*
Returns report showing cummulative acquired/disposed for the given company and for specific date range

*/
CREATE PROCEDURE [dbo].[p_Report_NonDerivAcquiredDisposed]
		@IssuerID		BIGINT,
		@StartDate		DATETIME,
		@EndDate		DATETIME
AS
BEGIN

	SELECT
		ndt.Date,
		SUM(IIF( ndt.TransactionTypeID = 1, ndt.SharesAmount, 0 )) As Acquired,
		SUM(IIF( ndt.TransactionTypeID = 2, ndt.SharesAmount, 0 )) As Disposed
	FROM dbo.v_NonDerivativeTransaction ndt
	WHERE 
		ndt.Date BETWEEN @StartDate AND @EndDate AND
		ndt.IssuerID = @IssuerID
	GROUP BY ndt.Date

END