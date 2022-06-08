


-- original values --
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
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updForm4ReportID BIGINT = 100007
DECLARE @updTitleOfSecurity NVARCHAR(250) = 'TitleOfSecurity ae48528ef3ce45c7ba52835f59ad6350'
DECLARE @updTransactionDate DATE = '9/13/2020'
DECLARE @updDeemedExecDate DATE = '9/13/2020'
DECLARE @updTransactionCodeID BIGINT = 4
DECLARE @updEarlyVoluntarilyReport BIT = 0
DECLARE @updSharesAmount BIGINT = 706353
DECLARE @updTransactionTypeID BIGINT = 2
DECLARE @updPrice DECIMAL(20, 6) = 706353.148309
DECLARE @updAmountFollowingReport BIGINT = 706353
DECLARE @updOwnershipTypeID BIGINT = 2
DECLARE @updNatureOfIndirectOwnership NVARCHAR(250) = 'NatureOfIndirectOwnership ae48528ef3ce45c7ba52835f59ad6350'
 

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