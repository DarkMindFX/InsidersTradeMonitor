


DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code 3e436'
DECLARE @Description NVARCHAR(250) = 'Description 3e436bf05a874c6c997b701e380c8a63'
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[TransactionCode]
				WHERE 

	1=1 AND
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[TransactionCode]
	WHERE 
	1=1 AND
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1  

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'TransactionCode was not deleted', 1
END