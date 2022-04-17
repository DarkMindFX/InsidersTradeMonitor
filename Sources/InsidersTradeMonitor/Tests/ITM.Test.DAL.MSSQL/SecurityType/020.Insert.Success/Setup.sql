


DECLARE @ID BIGINT = NULL
DECLARE @SecurityTypeName NVARCHAR(50) = 'SecurityTypeName 2e970d006ec34a84a4ffcc21da5a4dbd'
 


IF(EXISTS(SELECT 1 FROM 
					[dbo].[SecurityType]
				WHERE 
	(CASE WHEN @SecurityTypeName IS NOT NULL THEN (CASE WHEN [SecurityTypeName] = @SecurityTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM [dbo].[SecurityType]
WHERE 
	(CASE WHEN @SecurityTypeName IS NOT NULL THEN (CASE WHEN [SecurityTypeName] = @SecurityTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 

END