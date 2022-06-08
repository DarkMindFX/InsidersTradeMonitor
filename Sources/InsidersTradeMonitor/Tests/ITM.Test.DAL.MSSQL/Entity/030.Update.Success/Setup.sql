


DECLARE @ID BIGINT = NULL
DECLARE @EntityTypeID BIGINT = 2
DECLARE @CIK INT = 258
DECLARE @Name NVARCHAR(250) = 'Name 96d1b6756a174b76ade9ef2b58039d21'
DECLARE @TradingSymbol NVARCHAR(50) = 'TradingSymbol 96d1b6756a174b76ade9ef2b58039d21'
DECLARE @IsMonitored BIT = 0
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Entity]
				WHERE 
	(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN [EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN [CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN [TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsMonitored IS NOT NULL THEN (CASE WHEN [IsMonitored] = @IsMonitored THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Entity]
		(
	 [EntityTypeID],
	 [CIK],
	 [Name],
	 [TradingSymbol],
	 [IsMonitored]
		)
	SELECT 		
			 @EntityTypeID,
	 @CIK,
	 @Name,
	 @TradingSymbol,
	 @IsMonitored
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Entity] e
WHERE
	(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN [EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN [CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN [TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsMonitored IS NOT NULL THEN (CASE WHEN [IsMonitored] = @IsMonitored THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
