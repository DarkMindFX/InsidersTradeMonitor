





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_GetByImportRunID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_GetByImportRunID]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_GetByImportRunID]

	@ImportRunID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRunForm4Report] c 
				WHERE
					[ImportRunID] = @ImportRunID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImportRunForm4Report] e
		WHERE 
			[ImportRunID] = @ImportRunID	

	END
	ELSE
		SET @Found = 0;
END
GO