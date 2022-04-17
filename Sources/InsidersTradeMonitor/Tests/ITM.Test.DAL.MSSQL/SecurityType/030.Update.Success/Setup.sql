


DECLARE @ID BIGINT = NULL
DECLARE @SecurityTypeName NVARCHAR(50) = 'SecurityTypeName 3d8f2e3fef6c471999eeded7b599dd80'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[SecurityType]
				WHERE 
	(CASE WHEN @SecurityTypeName IS NOT NULL THEN (CASE WHEN [SecurityTypeName] = @SecurityTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[SecurityType]
		(
	 [SecurityTypeName]
		)
	SELECT 		
			 @SecurityTypeName
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[SecurityType] e
WHERE
	(CASE WHEN @SecurityTypeName IS NOT NULL THEN (CASE WHEN [SecurityTypeName] = @SecurityTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
