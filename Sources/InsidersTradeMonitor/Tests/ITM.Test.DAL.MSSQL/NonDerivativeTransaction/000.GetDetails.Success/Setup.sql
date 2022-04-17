


DECLARE @ID BIGINT = NULL
DECLARE @Form4ReportID BIGINT = NULL
DECLARE @TitleOfSecurity NVARCHAR(250) = 'TitleOfSecurity 80b9e2b0657c499fb4b00e444b82162a'
DECLARE @TransactionDate DATE = '10/13/2019 8:14:45 AM'
DECLARE @DeemedExecDate DATE = '10/13/2019 8:14:45 AM'
DECLARE @TransactionCodeID BIGINT = 2
DECLARE @EarlyVoluntarilyReport BIT = 1
DECLARE @SharesAmount BIGINT = 41082
DECLARE @TransactionTypeID BIGINT = 2
DECLARE @Price DECIMAL(20, 6) = 563507.18884
DECLARE @AmountFollowingReport BIGINT = 563507
DECLARE @OwnershipTypeID BIGINT = NULL
DECLARE @NatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership 80b9e2b0657c499fb4b00e444b82162a'
 


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
