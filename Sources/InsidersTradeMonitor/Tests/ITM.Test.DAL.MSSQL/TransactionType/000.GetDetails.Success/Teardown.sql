


DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code 2a3a1'
DECLARE @Description NVARCHAR(250) = 'Description 2a3a13feb5424105b6dabe0873934b8b'
 

DELETE FROM [TransactionType]
FROM 
	[dbo].[TransactionType] e
WHERE
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
