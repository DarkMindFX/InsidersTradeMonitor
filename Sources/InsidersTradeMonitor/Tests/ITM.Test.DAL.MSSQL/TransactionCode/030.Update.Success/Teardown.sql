


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code c8e60'
DECLARE @Description NVARCHAR(250) = 'Description c8e60273a3ab4942b0a8ff236ca3a785'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updCode NVARCHAR(10) = 'Code 3b956'
DECLARE @updDescription NVARCHAR(250) = 'Description 3b95600d0d1a434c8fd073c2b885b026'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[TransactionCode]
				WHERE 
	(CASE WHEN @updCode IS NOT NULL THEN (CASE WHEN [Code] = @updCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[TransactionCode]
	WHERE 
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[TransactionCode]
	WHERE 
	(CASE WHEN @updCode IS NOT NULL THEN (CASE WHEN [Code] = @updCode THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDescription IS NOT NULL THEN (CASE WHEN [Description] = @updDescription THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'TransactionCode was not updated', 1
END