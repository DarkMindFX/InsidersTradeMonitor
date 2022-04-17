



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_SecurityType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_SecurityType_Delete]
GO

CREATE PROCEDURE [dbo].[p_SecurityType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[SecurityType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[SecurityType] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO
