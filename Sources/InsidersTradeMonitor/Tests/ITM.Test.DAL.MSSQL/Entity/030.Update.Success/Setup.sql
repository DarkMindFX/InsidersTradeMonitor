


DECLARE @ID BIGINT = 890747
DECLARE @EntityTypeID BIGINT = NULL
DECLARE @CIK NVARCHAR(250) = 'CIK 52ea2c70817c4844911a712e85e0e33e'
DECLARE @Name NVARCHAR(250) = 'Name 52ea2c70817c4844911a712e85e0e33e'
DECLARE @TradingSymbol NVARCHAR(50) = 'TradingSymbol 52ea2c70817c4844911a712e85e0e33e'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Entity]
				WHERE 
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN [EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN [CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN [TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Entity]
		(
	 [ID],
	 [EntityTypeID],
	 [CIK],
	 [Name],
	 [TradingSymbol]
		)
	SELECT 		
			 @ID,
	 @EntityTypeID,
	 @CIK,
	 @Name,
	 @TradingSymbol
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Entity] e
WHERE
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN [EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN [CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN [TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
