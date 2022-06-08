


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Form4ReportID BIGINT = 100012
DECLARE @TitleOfDerivative NVARCHAR(250) = 'TitleOfDerivative c5cf2326258b4b7fb281908cb32b6619'
DECLARE @ConversionExercisePrice DECIMAL(20, 6) = 315983.668583
DECLARE @TransactionDate DATE = '6/5/2021 1:34:26 AM'
DECLARE @TransactionCodeID BIGINT = 5
DECLARE @EarlyVoluntarilyReport BIT = 0
DECLARE @SharesAmount BIGINT = 315984
DECLARE @DerivativeSecurityPrice DECIMAL(20, 6) = 315983.668583
DECLARE @TransactionTypeID BIGINT = 1
DECLARE @DateExercisable DATE = '6/5/2021 1:34:26 AM'
DECLARE @ExpirationDate DATE = '6/5/2021 1:34:26 AM'
DECLARE @UnderlyingTitle NVARCHAR(250) = 'UnderlyingTitle c5cf2326258b4b7fb281908cb32b6619'
DECLARE @UnderlyingSharesAmount BIGINT = 315984
DECLARE @AmountFollowingReport BIGINT = 315984
DECLARE @OwnershipTypeID BIGINT = 2
DECLARE @NatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership c5cf2326258b4b7fb281908cb32b6619'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updForm4ReportID BIGINT = 100019
DECLARE @updTitleOfDerivative NVARCHAR(250) = 'TitleOfDerivative 664806d526eb4758ac9d975bb2ca7419'
DECLARE @updConversionExercisePrice DECIMAL(20, 6) = 315983.668583
DECLARE @updTransactionDate DATE = '6/5/2021 1:34:26 AM'
DECLARE @updTransactionCodeID BIGINT = 2
DECLARE @updEarlyVoluntarilyReport BIT = 0
DECLARE @updSharesAmount BIGINT = 838409
DECLARE @updDerivativeSecurityPrice DECIMAL(20, 6) = 838408.982772
DECLARE @updTransactionTypeID BIGINT = 1
DECLARE @updDateExercisable DATE = '4/15/2024 11:20:26 AM'
DECLARE @updExpirationDate DATE = '4/15/2024 11:20:26 AM'
DECLARE @updUnderlyingTitle NVARCHAR(250) = 'UnderlyingTitle 664806d526eb4758ac9d975bb2ca7419'
DECLARE @updUnderlyingSharesAmount BIGINT = 838409
DECLARE @updAmountFollowingReport BIGINT = 838409
DECLARE @updOwnershipTypeID BIGINT = 1
DECLARE @updNatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership 664806d526eb4758ac9d975bb2ca7419'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[DerivativeTransaction]
				WHERE 
	(CASE WHEN @updForm4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @updForm4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitleOfDerivative IS NOT NULL THEN (CASE WHEN [TitleOfDerivative] = @updTitleOfDerivative THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updConversionExercisePrice IS NOT NULL THEN (CASE WHEN [ConversionExercisePrice] = @updConversionExercisePrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionDate IS NOT NULL THEN (CASE WHEN [TransactionDate] = @updTransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionCodeID IS NOT NULL THEN (CASE WHEN [TransactionCodeID] = @updTransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updEarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN [EarlyVoluntarilyReport] = @updEarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSharesAmount IS NOT NULL THEN (CASE WHEN [SharesAmount] = @updSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDerivativeSecurityPrice IS NOT NULL THEN (CASE WHEN [DerivativeSecurityPrice] = @updDerivativeSecurityPrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionTypeID IS NOT NULL THEN (CASE WHEN [TransactionTypeID] = @updTransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDateExercisable IS NOT NULL THEN (CASE WHEN [DateExercisable] = @updDateExercisable THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updExpirationDate IS NOT NULL THEN (CASE WHEN [ExpirationDate] = @updExpirationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUnderlyingTitle IS NOT NULL THEN (CASE WHEN [UnderlyingTitle] = @updUnderlyingTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUnderlyingSharesAmount IS NOT NULL THEN (CASE WHEN [UnderlyingSharesAmount] = @updUnderlyingSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAmountFollowingReport IS NOT NULL THEN (CASE WHEN [AmountFollowingReport] = @updAmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOwnershipTypeID IS NOT NULL THEN (CASE WHEN [OwnershipTypeID] = @updOwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updNatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN [NatureOfIndirectOwnership] = @updNatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[DerivativeTransaction]
	WHERE 
	(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TitleOfDerivative IS NOT NULL THEN (CASE WHEN [TitleOfDerivative] = @TitleOfDerivative THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ConversionExercisePrice IS NOT NULL THEN (CASE WHEN [ConversionExercisePrice] = @ConversionExercisePrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TransactionDate IS NOT NULL THEN (CASE WHEN [TransactionDate] = @TransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TransactionCodeID IS NOT NULL THEN (CASE WHEN [TransactionCodeID] = @TransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @EarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN [EarlyVoluntarilyReport] = @EarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SharesAmount IS NOT NULL THEN (CASE WHEN [SharesAmount] = @SharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DerivativeSecurityPrice IS NOT NULL THEN (CASE WHEN [DerivativeSecurityPrice] = @DerivativeSecurityPrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TransactionTypeID IS NOT NULL THEN (CASE WHEN [TransactionTypeID] = @TransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DateExercisable IS NOT NULL THEN (CASE WHEN [DateExercisable] = @DateExercisable THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ExpirationDate IS NOT NULL THEN (CASE WHEN [ExpirationDate] = @ExpirationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UnderlyingTitle IS NOT NULL THEN (CASE WHEN [UnderlyingTitle] = @UnderlyingTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @UnderlyingSharesAmount IS NOT NULL THEN (CASE WHEN [UnderlyingSharesAmount] = @UnderlyingSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AmountFollowingReport IS NOT NULL THEN (CASE WHEN [AmountFollowingReport] = @AmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OwnershipTypeID IS NOT NULL THEN (CASE WHEN [OwnershipTypeID] = @OwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @NatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN [NatureOfIndirectOwnership] = @NatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[DerivativeTransaction]
	WHERE 
	(CASE WHEN @updForm4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @updForm4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitleOfDerivative IS NOT NULL THEN (CASE WHEN [TitleOfDerivative] = @updTitleOfDerivative THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updConversionExercisePrice IS NOT NULL THEN (CASE WHEN [ConversionExercisePrice] = @updConversionExercisePrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionDate IS NOT NULL THEN (CASE WHEN [TransactionDate] = @updTransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionCodeID IS NOT NULL THEN (CASE WHEN [TransactionCodeID] = @updTransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updEarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN [EarlyVoluntarilyReport] = @updEarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSharesAmount IS NOT NULL THEN (CASE WHEN [SharesAmount] = @updSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDerivativeSecurityPrice IS NOT NULL THEN (CASE WHEN [DerivativeSecurityPrice] = @updDerivativeSecurityPrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionTypeID IS NOT NULL THEN (CASE WHEN [TransactionTypeID] = @updTransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDateExercisable IS NOT NULL THEN (CASE WHEN [DateExercisable] = @updDateExercisable THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updExpirationDate IS NOT NULL THEN (CASE WHEN [ExpirationDate] = @updExpirationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUnderlyingTitle IS NOT NULL THEN (CASE WHEN [UnderlyingTitle] = @updUnderlyingTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updUnderlyingSharesAmount IS NOT NULL THEN (CASE WHEN [UnderlyingSharesAmount] = @updUnderlyingSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAmountFollowingReport IS NOT NULL THEN (CASE WHEN [AmountFollowingReport] = @updAmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOwnershipTypeID IS NOT NULL THEN (CASE WHEN [OwnershipTypeID] = @updOwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updNatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN [NatureOfIndirectOwnership] = @updNatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'DerivativeTransaction was not updated', 1
END