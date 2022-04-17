


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_SecurityType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_SecurityType_Insert]
GO

CREATE PROCEDURE [dbo].[p_SecurityType_Insert]
			@ID BIGINT,
			@SecurityTypeName NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[SecurityType]
	SELECT 
		@SecurityTypeName
	
	

	SELECT
		e.*
	FROM
		[dbo].[SecurityType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SecurityTypeName IS NOT NULL THEN (CASE WHEN e.[SecurityTypeName] = @SecurityTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO