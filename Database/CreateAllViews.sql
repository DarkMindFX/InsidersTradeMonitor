
/****** Object:  View [dbo].[v_DerivativeTransaction]    Script Date: 4/25/2024 3:44:17 PM ******/
DROP VIEW IF EXISTS [dbo].[v_DerivativeTransaction]
GO
/****** Object:  View [dbo].[v_DerivativeTransaction]    Script Date: 4/25/2024 3:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_DerivativeTransaction]
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
GO

/****** Object:  View [dbo].[v_NonDerivativeTransaction]    Script Date: 4/25/2024 3:44:17 PM ******/
DROP VIEW IF EXISTS [dbo].[v_NonDerivativeTransaction]
GO
/****** Object:  View [dbo].[v_NonDerivativeTransaction]    Script Date: 4/25/2024 3:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[v_NonDerivativeTransaction]
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
GO
