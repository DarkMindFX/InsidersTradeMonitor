


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_SecurityType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_SecurityType_Update]
GO

CREATE PROCEDURE [dbo].[p_SecurityType_Update]
			@ID BIGINT,
			@SecurityTypeName NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[SecurityType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[SecurityType]
		SET
									[SecurityTypeName] = IIF( @SecurityTypeName IS NOT NULL, @SecurityTypeName, [SecurityTypeName] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'SecurityType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[SecurityType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SecurityTypeName IS NOT NULL THEN (CASE WHEN e.[SecurityTypeName] = @SecurityTypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO