


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_EntityType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_EntityType_Insert]
GO

CREATE PROCEDURE [dbo].[p_EntityType_Insert]
			@ID BIGINT,
			@TypeName NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[EntityType]
	SELECT 
		@TypeName
	
	

	SELECT
		e.*
	FROM
		[dbo].[EntityType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN e.[TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO