
/****** Object:  StoredProcedure [dbo].[p_Report_NonDerivAcquiredDisposed]    Script Date: 4/25/2024 4:09:45 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Report_NonDerivAcquiredDisposed]
GO
/****** Object:  StoredProcedure [dbo].[p_Report_NonDerivAcquiredDisposed]    Script Date: 4/25/2024 4:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
		SUM(IIF( ndt.TransactionTypeID = 2, ndt.SharesAmount, 0 )) As Disposed,
		SUM(IIF( ndt.TransactionTypeID = 1, ndt.SharesAmount, 0 )) - SUM(IIF( ndt.TransactionTypeID = 2, ndt.SharesAmount, 0 )) As Net,
		COUNT(DISTINCT ndt.Form4ReportID) As ReportsCount
	FROM dbo.v_NonDerivativeTransaction ndt
	WHERE 
		ndt.Date BETWEEN @StartDate AND @EndDate AND
		ndt.IssuerID = @IssuerID
	GROUP BY ndt.Date--, ndt.Form4ReportID

END
SET ANSI_NULLS ON
GO
