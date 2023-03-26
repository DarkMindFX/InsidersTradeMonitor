SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('v_DerivativeTransaction', 'V') IS NOT NULL
DROP VIEW [dbo].[v_DerivativeTransaction]
GO

CREATE VIEW dbo.v_DerivativeTransaction
AS

SELECT
		do.ID,
		do.Form4ReportID,
		f4r.IssuerID,
		f4r.Date,
		f4r.DateSubmitted,
		f4r.ReportID as SECReportID,
		do.TitleOfDerivative,
		do.ConversionExercisePrice,
		do.TransactionDate,
		do.TransactionCodeID,
		tc.Code as TrasnactionCode,
		tc.Description as TrasnactionCodeDesc,
		do.EarlyVoluntarilyReport,
		do.SharesAmount,
		do.DerivativeSecurityPrice,
		do.TransactionTypeID,
		tt.Code as TransactionType,
		tt.Description as TransactionTypeDesc,
		do.DateExercisable,
		do.ExpirationDate,
		do.UnderlyingTitle,
		do.UnderlyingSharesAmount,
		do.AmountFollowingReport,
		do.OwnershipTypeID,
		ot.Code as OwnershipType,
		ot.Description as OwnershipTypeDesc,
		do.NatureOfIndirectOwnership
	FROM		dbo.DerivativeTransaction do
	INNER JOIN	dbo.Form4Report f4r ON f4r.ID = do.Form4ReportID
	INNER JOIN  dbo.TransactionCode tc ON tc.ID = do.TransactionCodeID
	INNER JOIN	dbo.TransactionType tt ON tt.ID = do.TransactionTypeID
	INNER JOIN	dbo.OwnershipType	ot ON ot.ID = do.OwnershipTypeID
	INNER JOIN	dbo.Entity			e	ON e.ID = f4r.ReporterID
