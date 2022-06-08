


DECLARE @ID BIGINT = NULL
DECLARE @TypeName NVARCHAR(50) = 'TypeName d8f9da4dbee3422eb45828ecda25bbc7'
 


IF(EXISTS(SELECT 1 FROM 
					[dbo].[EntityType]
				WHERE 
	(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN [TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM [dbo].[EntityType]
WHERE 
	(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN [TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 

END