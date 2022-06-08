


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code 4a007'
DECLARE @Description NVARCHAR(250) = 'Description 4a00744efbad48a895640cd0f68dd3dd'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updCode NVARCHAR(10) = 'Code 6d517'
DECLARE @updDescription NVARCHAR(250) = 'Description 6d517bbe8f374571ab4ea5dbb6ffdd5b'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[TransactionType]
				WHERE 
	(CASE WHEN @updCode IS NOT NULL THEN (CASE WHEN [Code] = @updCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[TransactionType]
	WHERE 
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[TransactionType]
	WHERE 
	(CASE WHEN @updCode IS NOT NULL THEN (CASE WHEN [Code] = @updCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'TransactionType was not updated', 1
END