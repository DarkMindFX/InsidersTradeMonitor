


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OwnershipType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_OwnershipType_Update]
GO

CREATE PROCEDURE [dbo].[p_OwnershipType_Update]
			@ID BIGINT,
			@Code NCHAR(1),
			@Description NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OwnershipType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[OwnershipType]
		SET
									[Code] = IIF( @Code IS NOT NULL, @Code, [Code] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OwnershipType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OwnershipType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN e.[Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO