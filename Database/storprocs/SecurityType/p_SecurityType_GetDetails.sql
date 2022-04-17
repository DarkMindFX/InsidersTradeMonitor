




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_SecurityType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_SecurityType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_SecurityType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[SecurityType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[SecurityType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO