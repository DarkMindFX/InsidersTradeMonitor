


DECLARE @ID BIGINT = NULL
DECLARE @TypeName NVARCHAR(50) = 'TypeName 8d93df8644334861be00419a0c830837'
 


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
