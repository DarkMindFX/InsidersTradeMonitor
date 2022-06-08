


DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code f489c'
DECLARE @Description NVARCHAR(250) = 'Description f489c3b93a114dd8a33af3721c7544d2'
 
DECLARE @Fail AS BIT = 0

IF(EXISTS(SELECT 1 FROM 
				[dbo].[TransactionType]
				WHERE 

	1=1 AND
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[TransactionType]
	WHERE 
	1=1 AND
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND 
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1  

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'TransactionType was not deleted', 1
END