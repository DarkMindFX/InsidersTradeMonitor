


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @IssuerID BIGINT = 100007
DECLARE @ReporterID BIGINT = 100004
DECLARE @ReportID NVARCHAR(50) = 'ReportID 2eba2b437e83421dad688e2c8f62f5ae'
DECLARE @IsOfficer BIT = 0
DECLARE @IsDirector BIT = 0
DECLARE @Is10PctHolder BIT = 0
DECLARE @IsOther BIT = 0
DECLARE @OtherText NVARCHAR(250) = 'OtherText 2eba2b437e83421dad688e2c8f62f5ae'
DECLARE @OfficerTitle NVARCHAR(50) = 'OfficerTitle 2eba2b437e83421dad688e2c8f62f5ae'
DECLARE @Date DATE = '1/5/2024 2:45:34 AM'
DECLARE @DateSubmitted DATE = '1/5/2024 2:45:34 AM'
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updIssuerID BIGINT = 100005
DECLARE @updReporterID BIGINT = 100006
DECLARE @updReportID NVARCHAR(50) = 'ReportID c843fad9b39b4eabba16844bc734cb6d'
DECLARE @updIsOfficer BIT = 0
DECLARE @updIsDirector BIT = 0
DECLARE @updIs10PctHolder BIT = 0
DECLARE @updIsOther BIT = 0
DECLARE @updOtherText NVARCHAR(250) = 'OtherText c843fad9b39b4eabba16844bc734cb6d'
DECLARE @updOfficerTitle NVARCHAR(50) = 'OfficerTitle c843fad9b39b4eabba16844bc734cb6d'
DECLARE @updDate DATE = '1/5/2024 2:45:34 AM'
DECLARE @updDateSubmitted DATE = '1/5/2024 2:45:34 AM'
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[Form4Report]
				WHERE 
	(CASE WHEN @updIssuerID IS NOT NULL THEN (CASE WHEN [IssuerID] = @updIssuerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updReporterID IS NOT NULL THEN (CASE WHEN [ReporterID] = @updReporterID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updReportID IS NOT NULL THEN (CASE WHEN [ReportID] = @updReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsOfficer IS NOT NULL THEN (CASE WHEN [IsOfficer] = @updIsOfficer THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDirector IS NOT NULL THEN (CASE WHEN [IsDirector] = @updIsDirector THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIs10PctHolder IS NOT NULL THEN (CASE WHEN [Is10PctHolder] = @updIs10PctHolder THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsOther IS NOT NULL THEN (CASE WHEN [IsOther] = @updIsOther THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOtherText IS NOT NULL THEN (CASE WHEN [OtherText] = @updOtherText THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOfficerTitle IS NOT NULL THEN (CASE WHEN [OfficerTitle] = @updOfficerTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDate IS NOT NULL THEN (CASE WHEN [Date] = @updDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDateSubmitted IS NOT NULL THEN (CASE WHEN [DateSubmitted] = @updDateSubmitted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
	[dbo].[Form4Report]
	WHERE 
	(CASE WHEN @IssuerID IS NOT NULL THEN (CASE WHEN [IssuerID] = @IssuerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReporterID IS NOT NULL THEN (CASE WHEN [ReporterID] = @ReporterID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReportID IS NOT NULL THEN (CASE WHEN [ReportID] = @ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsOfficer IS NOT NULL THEN (CASE WHEN [IsOfficer] = @IsOfficer THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDirector IS NOT NULL THEN (CASE WHEN [IsDirector] = @IsDirector THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Is10PctHolder IS NOT NULL THEN (CASE WHEN [Is10PctHolder] = @Is10PctHolder THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsOther IS NOT NULL THEN (CASE WHEN [IsOther] = @IsOther THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OtherText IS NOT NULL THEN (CASE WHEN [OtherText] = @OtherText THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OfficerTitle IS NOT NULL THEN (CASE WHEN [OfficerTitle] = @OfficerTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Date IS NOT NULL THEN (CASE WHEN [Date] = @Date THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @DateSubmitted IS NOT NULL THEN (CASE WHEN [DateSubmitted] = @DateSubmitted THEN 1 ELSE 0 END) ELSE 1 END) = 1 

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[Form4Report]
	WHERE 
	(CASE WHEN @updIssuerID IS NOT NULL THEN (CASE WHEN [IssuerID] = @updIssuerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updReporterID IS NOT NULL THEN (CASE WHEN [ReporterID] = @updReporterID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updReportID IS NOT NULL THEN (CASE WHEN [ReportID] = @updReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsOfficer IS NOT NULL THEN (CASE WHEN [IsOfficer] = @updIsOfficer THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsDirector IS NOT NULL THEN (CASE WHEN [IsDirector] = @updIsDirector THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIs10PctHolder IS NOT NULL THEN (CASE WHEN [Is10PctHolder] = @updIs10PctHolder THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updIsOther IS NOT NULL THEN (CASE WHEN [IsOther] = @updIsOther THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOtherText IS NOT NULL THEN (CASE WHEN [OtherText] = @updOtherText THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updOfficerTitle IS NOT NULL THEN (CASE WHEN [OfficerTitle] = @updOfficerTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDate IS NOT NULL THEN (CASE WHEN [Date] = @updDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updDateSubmitted IS NOT NULL THEN (CASE WHEN [DateSubmitted] = @updDateSubmitted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'Form4Report was not updated', 1
END