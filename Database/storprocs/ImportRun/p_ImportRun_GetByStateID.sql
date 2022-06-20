





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRun_GetByStateID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRun_GetByStateID]
GO

CREATE PROCEDURE [dbo].[p_ImportRun_GetByStateID]

	@StateID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRun] c 
				WHERE
					[StateID] = @StateID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImportRun] e
		WHERE 
			[StateID] = @StateID	

	END
	ELSE
		SET @Found = 0;
END
GO