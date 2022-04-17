


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code 9d71d'
DECLARE @Description NVARCHAR(250) = 'Description 9d71da0295da46bc8e5ed3e4be24f9b3'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updCode NVARCHAR(10) = 'Code 235d0'
DECLARE @updDescription NVARCHAR(250) = 'Description 235d01e9705f4e059f295a874cdc0642'
 

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