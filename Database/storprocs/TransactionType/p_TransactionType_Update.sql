


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionType_Update]
GO

CREATE PROCEDURE [dbo].[p_TransactionType_Update]
			@ID BIGINT,
			@Code NVARCHAR(10),
			@Description NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[TransactionType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[TransactionType]
		SET
									[Code] = IIF( @Code IS NOT NULL, @Code, [Code] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'TransactionType was not found', 1;
	END	

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