





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetByReportID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetByReportID]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_GetByReportID]

	@ReportID NVARCHAR(50),
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Form4Report] c 
				WHERE
					[ReportID] = @ReportID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Form4Report] e
		WHERE 
			[ReportID] = @ReportID	

	END
	ELSE
		SET @Found = 0;
END
GO