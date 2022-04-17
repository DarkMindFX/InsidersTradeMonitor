


DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code c12fb'
DECLARE @Description NVARCHAR(250) = 'Description c12fb2a8b1f2434ca516a48ee1e5534e'
 

DELETE FROM [TransactionCode]
FROM 
	[dbo].[TransactionCode] e
WHERE
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
