


DECLARE @ID BIGINT = NULL
DECLARE @ImportRunID BIGINT = 100004
DECLARE @Form4ReportID BIGINT = 100023
DECLARE @TimeStarted DATETIME = '7/4/2020 10:03:20 AM'
DECLARE @TimeCompleted DATETIME = '7/4/2020 10:03:20 AM'
 

DELETE FROM [ImportRunForm4Report]
FROM 
	[dbo].[ImportRunForm4Report] e
WHERE
	(CASE WHEN @ImportRunID IS NOT NULL THEN (CASE WHEN [ImportRunID] = @ImportRunID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TimeStarted IS NOT NULL THEN (CASE WHEN [TimeStarted] = @TimeStarted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TimeCompleted IS NOT NULL THEN (CASE WHEN [TimeCompleted] = @TimeCompleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 