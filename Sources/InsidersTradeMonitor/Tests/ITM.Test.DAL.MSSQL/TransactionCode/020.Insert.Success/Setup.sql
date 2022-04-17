


DECLARE @ID BIGINT = NULL
DECLARE @Code NVARCHAR(10) = 'Code a8a7c'
DECLARE @Description NVARCHAR(250) = 'Description a8a7ceb8af0d40d3b4ba841bfcc1cfbe'
 


IF(EXISTS(SELECT 1 FROM 
					[dbo].[TransactionCode]
				WHERE 
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM [dbo].[TransactionCode]
WHERE 
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 

END