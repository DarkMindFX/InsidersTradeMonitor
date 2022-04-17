


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_EntityType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_EntityType_Update]
GO

CREATE PROCEDURE [dbo].[p_EntityType_Update]
			@ID BIGINT,
			@TypeName NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[EntityType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[EntityType]
		SET
									[TypeName] = IIF( @TypeName IS NOT NULL, @TypeName, [TypeName] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'EntityType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[EntityType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN e.[TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO