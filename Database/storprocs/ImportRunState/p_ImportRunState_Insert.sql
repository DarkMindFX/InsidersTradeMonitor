


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunState_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunState_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImportRunState_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImportRunState]
	SELECT 
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImportRunState] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO