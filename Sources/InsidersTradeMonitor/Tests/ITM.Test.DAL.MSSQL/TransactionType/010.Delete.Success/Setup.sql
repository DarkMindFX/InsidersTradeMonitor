


DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code f489c'
DECLARE @Description NVARCHAR(250) = 'Description f489c3b93a114dd8a33af3721c7544d2'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[TransactionType]
				WHERE 
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[TransactionType]
		(
	 [Code],
	 [Description]
		)
	SELECT 		
			 @Code,
	 @Description
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[TransactionType] e
WHERE
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
