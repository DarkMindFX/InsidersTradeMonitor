


DECLARE @ID BIGINT = NULL
DECLARE @Code NCHAR(1) = 'C'
DECLARE @Description NVARCHAR(50) = 'Description 0050e553593c4847a42c34644222bfc6'
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[OwnershipType]
				WHERE 

	1=1 AND
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[OwnershipType]
	WHERE 
	1=1 AND
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1  

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OwnershipType was not deleted', 1
END