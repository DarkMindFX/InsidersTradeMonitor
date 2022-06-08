


DECLARE @ID BIGINT = NULL
DECLARE @Code NCHAR(1) = 'C'
DECLARE @Description NVARCHAR(50) = 'Description 0635c222641c4d3f884406c2c7709082'
 

DELETE FROM [OwnershipType]
FROM 
	[dbo].[OwnershipType] e
WHERE
	(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN [Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN [Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
