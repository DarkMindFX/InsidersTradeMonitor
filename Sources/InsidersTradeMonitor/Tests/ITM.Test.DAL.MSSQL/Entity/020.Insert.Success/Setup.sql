


DECLARE @ID BIGINT = 890747
DECLARE @EntityTypeID BIGINT = NULL
DECLARE @CIK NVARCHAR(250) = 'CIK 6040a272638844719fda814739096578'
DECLARE @Name NVARCHAR(250) = 'Name 6040a272638844719fda814739096578'
DECLARE @TradingSymbol NVARCHAR(50) = 'TradingSymbol 6040a272638844719fda814739096578'
 


IF(EXISTS(SELECT 1 FROM 
					[dbo].[Entity]
				WHERE 
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN [EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN [CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN [TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM [dbo].[Entity]
WHERE 
	(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN [ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN [EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN [CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN [TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 

END