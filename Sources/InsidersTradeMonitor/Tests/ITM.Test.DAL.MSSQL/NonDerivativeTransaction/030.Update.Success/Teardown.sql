


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Form4ReportID BIGINT = NULL
DECLARE @TitleOfSecurity NVARCHAR(250) = 'TitleOfSecurity 6339133fe5084d4f83e519b230414185'
DECLARE @TransactionDate DATE = '7/8/2020 12:15:45 AM'
DECLARE @DeemedExecDate DATE = '7/8/2020 12:15:45 AM'
DECLARE @TransactionCodeID BIGINT = 12
DECLARE @EarlyVoluntarilyReport BIT = 0
DECLARE @SharesAmount BIGINT = 698059
DECLARE @TransactionTypeID BIGINT = 1
DECLARE @Price DECIMAL(20, 6) = 698059.073974
DECLARE @AmountFollowingReport BIGINT = 698059
DECLARE @OwnershipTypeID BIGINT = NULL
DECLARE @NatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership 6339133fe5084d4f83e519b230414185'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updForm4ReportID BIGINT = NULL
DECLARE @updTitleOfSecurity NVARCHAR(250) = 'TitleOfSecurity a783b1b8ad8243e182246b297d158e51'
DECLARE @updTransactionDate DATE = '10/4/2020 10:28:45 AM'
DECLARE @updDeemedExecDate DATE = '10/4/2020 10:28:45 AM'
DECLARE @updTransactionCodeID BIGINT = 5
DECLARE @updEarlyVoluntarilyReport BIT = 0
DECLARE @updSharesAmount BIGINT = 220485
DECLARE @updTransactionTypeID BIGINT = 1
DECLARE @updPrice DECIMAL(20, 6) = 220484.388164
DECLARE @updAmountFollowingReport BIGINT = 220485
DECLARE @updOwnershipTypeID BIGINT = NULL
DECLARE @updNatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership a783b1b8ad8243e182246b297d158e51'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[NonDerivativeTransaction]
				WHERE 
	(CASE WHEN @updForm4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @updForm4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitleOfSecurity IS NOT NULL THEN (CASE WHEN [TitleOfSecurity] = @updTitleOfSecurity THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionDate IS NOT NULL THEN (CASE WHEN [TransactionDate] = @updTransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDeemedExecDate IS NOT NULL THEN (CASE WHEN [DeemedExecDate] = @updDeemedExecDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionCodeID IS NOT NULL THEN (CASE WHEN [TransactionCodeID] = @updTransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updEarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN [EarlyVoluntarilyReport] = @updEarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSharesAmount IS NOT NULL THEN (CASE WHEN [SharesAmount] = @updSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionTypeID IS NOT NULL THEN (CASE WHEN [TransactionTypeID] = @updTransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPrice IS NOT NULL THEN (CASE WHEN [Price] = @updPrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAmountFollowingReport IS NOT NULL THEN (CASE WHEN [AmountFollowingReport] = @updAmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOwnershipTypeID IS NOT NULL THEN (CASE WHEN [OwnershipTypeID] = @updOwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updNatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN [NatureOfIndirectOwnership] = @updNatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
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

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[NonDerivativeTransaction]
	WHERE 
	(CASE WHEN @updForm4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @updForm4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTitleOfSecurity IS NOT NULL THEN (CASE WHEN [TitleOfSecurity] = @updTitleOfSecurity THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionDate IS NOT NULL THEN (CASE WHEN [TransactionDate] = @updTransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDeemedExecDate IS NOT NULL THEN (CASE WHEN [DeemedExecDate] = @updDeemedExecDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionCodeID IS NOT NULL THEN (CASE WHEN [TransactionCodeID] = @updTransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updEarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN [EarlyVoluntarilyReport] = @updEarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSharesAmount IS NOT NULL THEN (CASE WHEN [SharesAmount] = @updSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTransactionTypeID IS NOT NULL THEN (CASE WHEN [TransactionTypeID] = @updTransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPrice IS NOT NULL THEN (CASE WHEN [Price] = @updPrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updAmountFollowingReport IS NOT NULL THEN (CASE WHEN [AmountFollowingReport] = @updAmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOwnershipTypeID IS NOT NULL THEN (CASE WHEN [OwnershipTypeID] = @updOwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updNatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN [NatureOfIndirectOwnership] = @updNatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'NonDerivativeTransaction was not updated', 1
END