





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetByReporterID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetByReporterID]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_GetByReporterID]

	@ReporterID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Form4Report] c 
				WHERE
					[ReporterID] = @ReporterID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Form4Report] e
		WHERE 
			[ReporterID] = @ReporterID	

	END
	ELSE
		SET @Found = 0;
END
GO