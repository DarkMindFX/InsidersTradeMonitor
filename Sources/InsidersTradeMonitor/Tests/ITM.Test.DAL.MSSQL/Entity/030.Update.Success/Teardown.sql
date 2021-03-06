


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @EntityTypeID BIGINT = 2
DECLARE @CIK INT = 258
DECLARE @Name NVARCHAR(250) = 'Name 96d1b6756a174b76ade9ef2b58039d21'
DECLARE @TradingSymbol NVARCHAR(50) = 'TradingSymbol 96d1b6756a174b76ade9ef2b58039d21'
DECLARE @IsMonitored BIT = 0
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updEntityTypeID BIGINT = 1
DECLARE @updCIK INT = 258
DECLARE @updName NVARCHAR(250) = 'Name 7df99239cc0f46e48a6ae2c439012918'
DECLARE @updTradingSymbol NVARCHAR(50) = 'TradingSymbol 7df99239cc0f46e48a6ae2c439012918'
DECLARE @updIsMonitored BIT = 0
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Entity]
				WHERE 
	(CASE WHEN @updEntityTypeID IS NOT NULL THEN (CASE WHEN [EntityTypeID] = @updEntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCIK IS NOT NULL THEN (CASE WHEN [CIK] = @updCIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTradingSymbol IS NOT NULL THEN (CASE WHEN [TradingSymbol] = @updTradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsMonitored IS NOT NULL THEN (CASE WHEN [IsMonitored] = @updIsMonitored THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Entity]
	WHERE 
	(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN [EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN [CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN [TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsMonitored IS NOT NULL THEN (CASE WHEN [IsMonitored] = @IsMonitored THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Entity]
	WHERE 
	(CASE WHEN @updEntityTypeID IS NOT NULL THEN (CASE WHEN [EntityTypeID] = @updEntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCIK IS NOT NULL THEN (CASE WHEN [CIK] = @updCIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updName IS NOT NULL THEN (CASE WHEN [Name] = @updName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTradingSymbol IS NOT NULL THEN (CASE WHEN [TradingSymbol] = @updTradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsMonitored IS NOT NULL THEN (CASE WHEN [IsMonitored] = @updIsMonitored THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Entity was not updated', 1
END