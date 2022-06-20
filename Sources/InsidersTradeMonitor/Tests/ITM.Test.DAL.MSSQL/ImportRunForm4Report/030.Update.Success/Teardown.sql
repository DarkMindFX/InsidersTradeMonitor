


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @ImportRunID BIGINT = 100006
DECLARE @Form4ReportID BIGINT = 100029
DECLARE @TimeStarted DATETIME = '5/15/2023 7:50:20 PM'
DECLARE @TimeCompleted DATETIME = '5/15/2023 7:50:20 PM'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updImportRunID BIGINT = 100009
DECLARE @updForm4ReportID BIGINT = 100030
DECLARE @updTimeStarted DATETIME = '5/15/2023 7:50:20 PM'
DECLARE @updTimeCompleted DATETIME = '5/15/2023 7:50:20 PM'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ImportRunForm4Report]
				WHERE 
	(CASE WHEN @updImportRunID IS NOT NULL THEN (CASE WHEN [ImportRunID] = @updImportRunID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updForm4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @updForm4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTimeStarted IS NOT NULL THEN (CASE WHEN [TimeStarted] = @updTimeStarted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTimeCompleted IS NOT NULL THEN (CASE WHEN [TimeCompleted] = @updTimeCompleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[ImportRunForm4Report]
	WHERE 
	(CASE WHEN @ImportRunID IS NOT NULL THEN (CASE WHEN [ImportRunID] = @ImportRunID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TimeStarted IS NOT NULL THEN (CASE WHEN [TimeStarted] = @TimeStarted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TimeCompleted IS NOT NULL THEN (CASE WHEN [TimeCompleted] = @TimeCompleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[ImportRunForm4Report]
	WHERE 
	(CASE WHEN @updImportRunID IS NOT NULL THEN (CASE WHEN [ImportRunID] = @updImportRunID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updForm4ReportID IS NOT NULL THEN (CASE WHEN [Form4ReportID] = @updForm4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTimeStarted IS NOT NULL THEN (CASE WHEN [TimeStarted] = @updTimeStarted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updTimeCompleted IS NOT NULL THEN (CASE WHEN [TimeCompleted] = @updTimeCompleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ImportRunForm4Report was not updated', 1
END