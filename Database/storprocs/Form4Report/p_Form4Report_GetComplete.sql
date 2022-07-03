



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetComplete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetComplete]
GO

/*
-- Returns complete form 4 report
*/
CREATE PROCEDURE [dbo].[p_Form4Report_GetComplete]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;

	IF(EXISTS(SELECT 1 FROM dbo.Form4Report WHERE ID = @ID))
	BEGIN
		SET @Found = 1

		-- Selecting report details
		SELECT
			r.ID,
			r.Date,
			r.DateSubmitted,
			r.ReportID,
			r.ReporterID,
			reporter.CIK,
			reporter.Name,
			r.IssuerID,
			issuer.CIK,
			issuer.Name,
			issuer.TradingSymbol,
			r.Is10PctHolder,
			r.IsDirector,
			r.IsOfficer,
			r.OfficerTitle,
			r.IsOther,
			r.OtherText

		FROM dbo.Form4Report r
		INNER JOIN dbo.Entity reporter ON r.ReporterID = reporter.ID
		INNER JOIN dbo.Entity issuer ON r.IssuerID = issuer.ID
		WHERE r.ID = @ID

		-- Non-derivatives
		SELECT
			ndt.ID,
			ndt.Form4ReportID,
			ndt.TransactionDate,
			ndt.TitleOfSecurity,
			ndt.Price,
			ndt.SharesAmount,
			ndt.TransactionTypeID,
			tt.Code as TransactionType,
			ndt.TransactionCodeID,
			tc.Code as TransactionCode,
			tc.Description as TransactionDescription,
			ndt.AmountFollowingReport,
			ndt.DeemedExecDate,
			ndt.EarlyVoluntarilyReport,
			ndt.NatureOfIndirectOwnership,
			ndt.OwnershipTypeID,
			ot.Code as OwnershipType			

		FROM dbo.NonDerivativeTransaction ndt
		INNER JOIN	dbo.Form4Report		r	ON r.ID = ndt.Form4ReportID
		INNER JOIN	dbo.TransactionCode tc	ON tc.ID = ndt.TransactionCodeID
		INNER JOIN	dbo.TransactionType tt	ON tt.ID = ndt.TransactionTypeID
		LEFT JOIN	dbo.OwnershipType	ot	ON ot.ID = ndt.OwnershipTypeID
		WHERE r.ID = @ID

		-- Derivatives
		SELECT
			dt.ID,
			dt.Form4ReportID,
			dt.TransactionDate,
			dt.TitleOfDerivative,
			dt.DerivativeSecurityPrice,
			dt.SharesAmount,
			dt.UnderlyingTitle,
			dt.UnderlyingSharesAmount,
			dt.ConversionExercisePrice,
			
			dt.TransactionTypeID,
			tt.Code as TransactionType,
			dt.TransactionCodeID,
			tc.Code as TransactionCode,
			tc.Description as TransactionDescription,
			dt.AmountFollowingReport,
			dt.DateExercisable,
			dt.ExpirationDate,
			dt.EarlyVoluntarilyReport,
			dt.NatureOfIndirectOwnership,
			dt.OwnershipTypeID,
			ot.Code as OwnershipType			

		FROM dbo.DerivativeTransaction dt
		INNER JOIN	dbo.Form4Report		r	ON r.ID = dt.Form4ReportID
		INNER JOIN	dbo.TransactionCode tc	ON tc.ID = dt.TransactionCodeID
		INNER JOIN	dbo.TransactionType tt	ON tt.ID = dt.TransactionTypeID
		LEFT JOIN	dbo.OwnershipType	ot	ON ot.ID = dt.OwnershipTypeID
		WHERE r.ID = @ID

	END
	ELSE
		SET @Found = 0

		
    
	
END
GO

DECLARE @ReportID AS BIGINT = 100001
DECLARE @Found AS BIT = 0

EXEC [dbo].[p_Form4Report_GetComplete] @ReportID, @Found OUT
