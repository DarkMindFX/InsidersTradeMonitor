


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Code NCHAR(1) = 'C'
DECLARE @Description NVARCHAR(50) = 'Description 790d313d67cb4b6b885b4353d21ceff5'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updCode NCHAR(1) = 'C'
DECLARE @updDescription NVARCHAR(50) = 'Description fab82e3caa4f4bf9aaf114a58bf57025'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[OwnershipType]
				WHERE 
	(CASE WHEN @updCode IS NOT NULL THEN (CASE WHEN [Code] = @updCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[OwnershipType]
	WHERE 
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[OwnershipType]
	WHERE 
	(CASE WHEN @updCode IS NOT NULL THEN (CASE WHEN [Code] = @updCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'OwnershipType was not updated', 1
END