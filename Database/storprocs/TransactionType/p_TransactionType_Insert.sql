


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionType_Insert]
GO

CREATE PROCEDURE [dbo].[p_TransactionType_Insert]
			@ID BIGINT,
			@Code NVARCHAR(10),
			@Description NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[TransactionType]
	SELECT 
		@Code,
		@Description
	
	

	SELECT
		e.*
	FROM
		[dbo].[TransactionType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN e.[Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO