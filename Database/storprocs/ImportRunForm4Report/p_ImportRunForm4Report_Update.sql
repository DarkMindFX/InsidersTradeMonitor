


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_Update]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_Update]
			@ID BIGINT,
			@ImportRunID BIGINT,
			@Form4ReportID BIGINT,
			@TimeStarted DATETIME,
			@TimeCompleted DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRunForm4Report]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ImportRunForm4Report]
		SET
									[ImportRunID] = IIF( @ImportRunID IS NOT NULL, @ImportRunID, [ImportRunID] ) ,
									[Form4ReportID] = IIF( @Form4ReportID IS NOT NULL, @Form4ReportID, [Form4ReportID] ) ,
									[TimeStarted] = IIF( @TimeStarted IS NOT NULL, @TimeStarted, [TimeStarted] ) ,
									[TimeCompleted] = IIF( @TimeCompleted IS NOT NULL, @TimeCompleted, [TimeCompleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImportRunForm4Report was not found', 1;
	END	

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