


DECLARE @ID BIGINT = NULL
DECLARE @TypeName NVARCHAR(50) = 'TypeName 23d6de606d904bf7b3a1b39e879c73ee'
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[EntityType]
				WHERE 

	1=1 AND
	(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN [TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[EntityType]
	WHERE 
	1=1 AND
	(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN [TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1  

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'EntityType was not deleted', 1
END