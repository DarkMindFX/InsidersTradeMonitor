
/****** Object:  StoredProcedure [dbo].[p_Report_NonDerivTransactionsByDate]    Script Date: 4/25/2024 4:09:45 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Report_NonDerivTransactionsByDate]
GO
/****** Object:  StoredProcedure [dbo].[p_Report_NonDerivTransactionsByDate]    Script Date: 4/25/2024 4:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
		ndo.SECReportID,
		ndo.TitleOfSecurity,
		ndo.TransactionDate,
		ndo.DeemedExecDate,
		ndo.TransactionCodeID,
		ndo.TrasnactionCode,
		ndo.EarlyVoluntarilyReport,
		ndo.SharesAmount,
		ndo.TransactionTypeID,
		ndo.TransactionType,
		ndo.Price,
		ndo.AmountFollowingReport,
		ndo.OwnershipTypeID,
		ndo.OwnershipType,
		ndo.NatureOfIndirectOwnership
	FROM		
		dbo.v_NonDerivativeTransaction ndo
	
	WHERE ndo.IssuerID = @IssuerID AND ndo.Date = @Date

	
	SET NOCOUNT ON;
END



SET ANSI_NULLS ON
GO
