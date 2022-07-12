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
		@IssuerID	 BIGINT,
		@Date		DATETIME
AS
BEGIN
	
	SET NOCOUNT ON;
END