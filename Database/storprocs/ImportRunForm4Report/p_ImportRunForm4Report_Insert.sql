


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_Insert]
			@ID BIGINT,
			@ImportRunID BIGINT,
			@Form4ReportID BIGINT,
			@TimeStarted DATETIME,
			@TimeCompleted DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImportRunForm4Report]
	SELECT 
		@ImportRunID,
		@Form4ReportID,
		@TimeStarted,
		@TimeCompleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImportRunForm4Report] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImportRunID IS NOT NULL THEN (CASE WHEN e.[ImportRunID] = @ImportRunID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN e.[Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TimeStarted IS NOT NULL THEN (CASE WHEN e.[TimeStarted] = @TimeStarted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TimeCompleted IS NOT NULL THEN (CASE WHEN e.[TimeCompleted] = @TimeCompleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO