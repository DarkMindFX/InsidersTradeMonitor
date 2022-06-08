


DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code b4702'
DECLARE @Description NVARCHAR(250) = 'Description b47026094d5c4eb28d4e22f7bcd2f2cd'
 

DELETE FROM [TransactionCode]
FROM 
	[dbo].[TransactionCode] e
WHERE
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
