


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunState_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunState_Update]
GO

CREATE PROCEDURE [dbo].[p_ImportRunState_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRunState]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ImportRunState]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImportRunState was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImportRunState] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO