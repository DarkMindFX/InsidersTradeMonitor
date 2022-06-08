


DECLARE @ID BIGINT = NULL
DECLARE @Form4ReportID BIGINT = 100007
DECLARE @TitleOfDerivative NVARCHAR(250) = 'TitleOfDerivative 735dce9f4ebe44a9a11bd76db27a4745'
DECLARE @ConversionExercisePrice DECIMAL(20, 6) = 181431.783448
DECLARE @TransactionDate DATE = '9/9/2020'
DECLARE @TransactionCodeID BIGINT = 15
DECLARE @EarlyVoluntarilyReport BIT = 1
DECLARE @SharesAmount BIGINT = 226283
DECLARE @DerivativeSecurityPrice DECIMAL(20, 6) = 226282.411826
DECLARE @TransactionTypeID BIGINT = 2
DECLARE @DateExercisable DATE = '10/19/2023'
DECLARE @ExpirationDate DATE = '10/19/2023'
DECLARE @UnderlyingTitle NVARCHAR(250) = 'UnderlyingTitle 735dce9f4ebe44a9a11bd76db27a4745'
DECLARE @UnderlyingSharesAmount BIGINT = 748707
DECLARE @AmountFollowingReport BIGINT = 748707
DECLARE @OwnershipTypeID BIGINT = 2
DECLARE @NatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership 735dce9f4ebe44a9a11bd76db27a4745'
 

DELETE FROM [DerivativeTransaction]
FROM 
	[dbo].[DerivativeTransaction] e
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
