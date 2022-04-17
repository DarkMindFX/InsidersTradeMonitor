


DECLARE @ID BIGINT = NULL
DECLARE @Code NCHAR(1) = 'C'
DECLARE @Description NVARCHAR(50) = 'Description 52f0d9604a8b46ae98e382f3b66c2513'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[OwnershipType]
				WHERE 
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[OwnershipType]
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
	[dbo].[OwnershipType] e
WHERE
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
