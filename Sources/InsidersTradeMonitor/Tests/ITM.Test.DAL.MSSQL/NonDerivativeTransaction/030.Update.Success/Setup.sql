


DECLARE @ID BIGINT = NULL
DECLARE @Form4ReportID BIGINT = 100028
DECLARE @TitleOfSecurity NVARCHAR(250) = 'TitleOfSecurity 4cf0dd55e89a4a33ad5d62de6c21f419'
DECLARE @TransactionDate DATE = '9/13/2020'
DECLARE @DeemedExecDate DATE = '9/13/2020'
DECLARE @TransactionCodeID BIGINT = 5
DECLARE @EarlyVoluntarilyReport BIT = 1
DECLARE @SharesAmount BIGINT = 183928
DECLARE @TransactionTypeID BIGINT = 1
DECLARE @Price DECIMAL(20, 6) = 183927.83412
DECLARE @AmountFollowingReport BIGINT = 183928
DECLARE @OwnershipTypeID BIGINT = 2
DECLARE @NatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership 4cf0dd55e89a4a33ad5d62de6c21f419'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[NonDerivativeTransaction]
				WHERE 
	(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TitleOfSecurity IS NOT NULL THEN (CASE WHEN [TitleOfSecurity] = @TitleOfSecurity THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TransactionDate IS NOT NULL THEN (CASE WHEN [TransactionDate] = @TransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DeemedExecDate IS NOT NULL THEN (CASE WHEN [DeemedExecDate] = @DeemedExecDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TransactionCodeID IS NOT NULL THEN (CASE WHEN [TransactionCodeID] = @TransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @EarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN [EarlyVoluntarilyReport] = @EarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SharesAmount IS NOT NULL THEN (CASE WHEN [SharesAmount] = @SharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TransactionTypeID IS NOT NULL THEN (CASE WHEN [TransactionTypeID] = @TransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Price IS NOT NULL THEN (CASE WHEN [Price] = @Price THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AmountFollowingReport IS NOT NULL THEN (CASE WHEN [AmountFollowingReport] = @AmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OwnershipTypeID IS NOT NULL THEN (CASE WHEN [OwnershipTypeID] = @OwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @NatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN [NatureOfIndirectOwnership] = @NatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[NonDerivativeTransaction]
		(
	 [Form4ReportID],
	 [TitleOfSecurity],
	 [TransactionDate],
	 [DeemedExecDate],
	 [TransactionCodeID],
	 [EarlyVoluntarilyReport],
	 [SharesAmount],
	 [TransactionTypeID],
	 [Price],
	 [AmountFollowingReport],
	 [OwnershipTypeID],
	 [NatureOfIndirectOwnership]
		)
	SELECT 		
			 @Form4ReportID,
	 @TitleOfSecurity,
	 @TransactionDate,
	 @DeemedExecDate,
	 @TransactionCodeID,
	 @EarlyVoluntarilyReport,
	 @SharesAmount,
	 @TransactionTypeID,
	 @Price,
	 @AmountFollowingReport,
	 @OwnershipTypeID,
	 @NatureOfIndirectOwnership
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[NonDerivativeTransaction] e
WHERE
	(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TitleOfSecurity IS NOT NULL THEN (CASE WHEN [TitleOfSecurity] = @TitleOfSecurity THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TransactionDate IS NOT NULL THEN (CASE WHEN [TransactionDate] = @TransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DeemedExecDate IS NOT NULL THEN (CASE WHEN [DeemedExecDate] = @DeemedExecDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TransactionCodeID IS NOT NULL THEN (CASE WHEN [TransactionCodeID] = @TransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @EarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN [EarlyVoluntarilyReport] = @EarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @SharesAmount IS NOT NULL THEN (CASE WHEN [SharesAmount] = @SharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TransactionTypeID IS NOT NULL THEN (CASE WHEN [TransactionTypeID] = @TransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Price IS NOT NULL THEN (CASE WHEN [Price] = @Price THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @AmountFollowingReport IS NOT NULL THEN (CASE WHEN [AmountFollowingReport] = @AmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OwnershipTypeID IS NOT NULL THEN (CASE WHEN [OwnershipTypeID] = @OwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @NatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN [NatureOfIndirectOwnership] = @NatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
