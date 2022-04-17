


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @SecurityTypeName NVARCHAR(50) = 'SecurityTypeName 3d8f2e3fef6c471999eeded7b599dd80'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updSecurityTypeName NVARCHAR(50) = 'SecurityTypeName b01565710db6487b823f567474335ba7'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[SecurityType]
				WHERE 
	(CASE WHEN @updSecurityTypeName IS NOT NULL THEN (CASE WHEN [SecurityTypeName] = @updSecurityTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[SecurityType]
	WHERE 
	(CASE WHEN @SecurityTypeName IS NOT NULL THEN (CASE WHEN [SecurityTypeName] = @SecurityTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[SecurityType]
	WHERE 
	(CASE WHEN @updSecurityTypeName IS NOT NULL THEN (CASE WHEN [SecurityTypeName] = @updSecurityTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'SecurityType was not updated', 1
END