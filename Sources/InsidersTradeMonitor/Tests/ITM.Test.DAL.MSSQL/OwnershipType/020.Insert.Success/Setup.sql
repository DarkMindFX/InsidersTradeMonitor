


DECLARE @ID BIGINT = NULL
DECLARE @Code NCHAR(1) = 'C'
DECLARE @Description NVARCHAR(50) = 'Description 551331c4138e443da5ae6323f444af8f'
 


IF(EXISTS(SELECT 1 FROM 
					[dbo].[OwnershipType]
				WHERE 
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM [dbo].[OwnershipType]
WHERE 
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 

END