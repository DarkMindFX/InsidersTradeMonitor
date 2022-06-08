


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @TypeName NVARCHAR(50) = 'TypeName 7d95d0585ce94d04a335038fe34caafd'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updTypeName NVARCHAR(50) = 'TypeName 4900b709aeed475f9bdd20dc927f2bb1'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[EntityType]
				WHERE 
	(CASE WHEN @updTypeName IS NOT NULL THEN (CASE WHEN [TypeName] = @updTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[EntityType]
	WHERE 
	(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN [TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[EntityType]
	WHERE 
	(CASE WHEN @updTypeName IS NOT NULL THEN (CASE WHEN [TypeName] = @updTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'EntityType was not updated', 1
END