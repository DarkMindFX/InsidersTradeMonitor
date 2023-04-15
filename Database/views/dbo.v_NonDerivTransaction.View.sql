SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('v_NonDerivativeTransaction', 'V') IS NOT NULL
DROP VIEW [dbo].[v_NonDerivativeTransaction]
GO

CREATE VIEW dbo.v_NonDerivativeTransaction
AS

SELECT
		ndo.ID,
		ndo.Form4ReportID,
		f4r.IssuerID,
		f4r.Date,
		f4r.DateSubmitted,
		f4r.ReportID as SECReportID,
		ndo.TitleOfSecurity,
		ndo.TransactionDate,
		ndo.DeemedExecDate,
		ndo.TransactionCodeID,
		tc.Code as TrasnactionCode,
		tc.Description as TrasnactionCodeDesc,
		ndo.EarlyVoluntarilyReport,
		ndo.SharesAmount,
		ndo.TransactionTypeID,
		tt.Code as TransactionType,
		tt.Description as TransactionTypeDesc,
		ndo.Price,
		ndo.AmountFollowingReport,
		ndo.OwnershipTypeID,
		ot.Code as OwnershipType,
		ot.Description as OwnershipTypeDesc,
		ndo.NatureOfIndirectOwnership
	FROM		dbo.NonDerivativeTransaction ndo
	INNER JOIN	dbo.Form4Report f4r ON f4r.ID = ndo.Form4ReportID
	LEFT JOIN  dbo.TransactionCode tc ON tc.ID = ndo.TransactionCodeID
	LEFT JOIN	dbo.TransactionType tt ON tt.ID = ndo.TransactionTypeID
	LEFT JOIN	dbo.OwnershipType	ot ON ot.ID = ndo.OwnershipTypeID
	INNER JOIN	dbo.Entity			e	ON e.ID = f4r.ReporterID
