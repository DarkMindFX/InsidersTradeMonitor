


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code efe69'
DECLARE @Description NVARCHAR(250) = 'Description efe69490bfd346f58c6c2342a248c6ed'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updCode NVARCHAR(10) = 'Code e7c33'
DECLARE @updDescription NVARCHAR(250) = 'Description e7c337dda94b46c5bea5d7730fc8eda5'
 

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