




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_TransactionType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[TransactionType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[TransactionType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO