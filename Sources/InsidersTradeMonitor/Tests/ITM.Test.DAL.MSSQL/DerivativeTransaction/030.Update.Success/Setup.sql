


DECLARE @ID BIGINT = NULL
DECLARE @Form4ReportID BIGINT = 100012
DECLARE @TitleOfDerivative NVARCHAR(250) = 'TitleOfDerivative c5cf2326258b4b7fb281908cb32b6619'
DECLARE @ConversionExercisePrice DECIMAL(20, 6) = 315983.668583
DECLARE @TransactionDate DATE = '6/5/2021'
DECLARE @TransactionCodeID BIGINT = 5
DECLARE @EarlyVoluntarilyReport BIT = 0
DECLARE @SharesAmount BIGINT = 315984
DECLARE @DerivativeSecurityPrice DECIMAL(20, 6) = 315983.668583
DECLARE @TransactionTypeID BIGINT = 1
DECLARE @DateExercisable DATE = '6/5/2021'
DECLARE @ExpirationDate DATE = '6/5/2021'
DECLARE @UnderlyingTitle NVARCHAR(250) = 'UnderlyingTitle c5cf2326258b4b7fb281908cb32b6619'
DECLARE @UnderlyingSharesAmount BIGINT = 315984
DECLARE @AmountFollowingReport BIGINT = 315984
DECLARE @OwnershipTypeID BIGINT = 2
DECLARE @NatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership c5cf2326258b4b7fb281908cb32b6619'
 


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
