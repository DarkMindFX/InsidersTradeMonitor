


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Form4ReportID BIGINT = NULL
DECLARE @TitleOfDerivative NVARCHAR(250) = 'TitleOfDerivative 18c2ca5e15c749209dd877aa012a993b'
DECLARE @ConversionExercisePrice DECIMAL(20, 6) = 590079.540196
DECLARE @TransactionDate DATE = '10/14/2022 2:39:45 PM'
DECLARE @TransactionCodeID BIGINT = 15
DECLARE @EarlyVoluntarilyReport BIT = 1
DECLARE @SharesAmount BIGINT = 590079
DECLARE @DerivativeSecurityPrice DECIMAL(20, 6) = 590079.540196
DECLARE @TransactionTypeID BIGINT = 2
DECLARE @DateExercisable DATE = '10/14/2022 2:39:45 PM'
DECLARE @ExpirationDate DATE = '10/14/2022 2:39:45 PM'
DECLARE @UnderlyingTitle NVARCHAR(250) = 'UnderlyingTitle 18c2ca5e15c749209dd877aa012a993b'
DECLARE @UnderlyingSharesAmount BIGINT = 590079
DECLARE @AmountFollowingReport BIGINT = 590079
DECLARE @OwnershipTypeID BIGINT = NULL
DECLARE @NatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership 18c2ca5e15c749209dd877aa012a993b'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updForm4ReportID BIGINT = NULL
DECLARE @updTitleOfDerivative NVARCHAR(250) = 'TitleOfDerivative 91450a0a311744e39a8e6fadb41eb6f0'
DECLARE @updConversionExercisePrice DECIMAL(20, 6) = 590079.540196
DECLARE @updTransactionDate DATE = '10/14/2022 2:39:45 PM'
DECLARE @updTransactionCodeID BIGINT = 9
DECLARE @updEarlyVoluntarilyReport BIT = 0
DECLARE @updSharesAmount BIGINT = 112505
DECLARE @updDerivativeSecurityPrice DECIMAL(20, 6) = 112504.854385
DECLARE @updTransactionTypeID BIGINT = 1
DECLARE @updDateExercisable DATE = '3/4/2020 12:26:45 AM'
DECLARE @updExpirationDate DATE = '3/4/2020 12:26:45 AM'
DECLARE @updUnderlyingTitle NVARCHAR(250) = 'UnderlyingTitle 91450a0a311744e39a8e6fadb41eb6f0'
DECLARE @updUnderlyingSharesAmount BIGINT = 112505
DECLARE @updAmountFollowingReport BIGINT = 112505
DECLARE @updOwnershipTypeID BIGINT = NULL
DECLARE @updNatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership 91450a0a311744e39a8e6fadb41eb6f0'
 

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