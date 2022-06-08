


DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code 926f1'
DECLARE @Description NVARCHAR(250) = 'Description 926f19be56db42dbbc6c83009834c264'
 

DELETE FROM [TransactionType]
FROM 
	[dbo].[TransactionType] e
WHERE
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
