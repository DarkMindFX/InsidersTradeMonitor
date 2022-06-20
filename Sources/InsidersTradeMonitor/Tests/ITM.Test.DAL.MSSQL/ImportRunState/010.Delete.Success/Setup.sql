


DECLARE @ID BIGINT = NULL
DECLARE @Name NVARCHAR(50) = 'Name 599c9b8811e0404bacebd7f4c42ba93f'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ImportRunState]
				WHERE 
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[ImportRunState]
		(
	 [Name]
		)
	SELECT 		
			 @Name
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[ImportRunState] e
WHERE
	(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN [Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
