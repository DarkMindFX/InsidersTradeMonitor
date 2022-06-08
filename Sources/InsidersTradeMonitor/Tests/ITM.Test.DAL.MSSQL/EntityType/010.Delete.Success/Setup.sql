


DECLARE @ID BIGINT = NULL
DECLARE @TypeName NVARCHAR(50) = 'TypeName 23d6de606d904bf7b3a1b39e879c73ee'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[EntityType]
				WHERE 
	(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN [TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[EntityType]
		(
	 [TypeName]
		)
	SELECT 		
			 @TypeName
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[EntityType] e
WHERE
	(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN [TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
