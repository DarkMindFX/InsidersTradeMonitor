


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
 


IF(NOT EXISTS(SELECT 1 FROM 
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
 ))
					
BEGIN
	INSERT INTO [dbo].[DerivativeTransaction]
		(
	 [Form4ReportID],
	 [TitleOfDerivative],
	 [ConversionExercisePrice],
	 [TransactionDate],
	 [TransactionCodeID],
	 [EarlyVoluntarilyReport],
	 [SharesAmount],
	 [DerivativeSecurityPrice],
	 [TransactionTypeID],
	 [DateExercisable],
	 [ExpirationDate],
	 [UnderlyingTitle],
	 [UnderlyingSharesAmount],
	 [AmountFollowingReport],
	 [OwnershipTypeID],
	 [NatureOfIndirectOwnership]
		)
	SELECT 		
			 @Form4ReportID,
	 @TitleOfDerivative,
	 @ConversionExercisePrice,
	 @TransactionDate,
	 @TransactionCodeID,
	 @EarlyVoluntarilyReport,
	 @SharesAmount,
	 @DerivativeSecurityPrice,
	 @TransactionTypeID,
	 @DateExercisable,
	 @ExpirationDate,
	 @UnderlyingTitle,
	 @UnderlyingSharesAmount,
	 @AmountFollowingReport,
	 @OwnershipTypeID,
	 @NatureOfIndirectOwnership
END

SELECT TOP 1 
	@ID = [ID]
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

SELECT 
	@ID
