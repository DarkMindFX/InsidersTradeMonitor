


DECLARE @ID BIGINT = NULL
DECLARE @IssuerID BIGINT = 100007
DECLARE @ReporterID BIGINT = 100004
DECLARE @ReportID NVARCHAR(50) = 'ReportID f796b6fa507e4fa9a827ce60d43ad38c'
DECLARE @IsOfficer BIT = 0
DECLARE @IsDirector BIT = 0
DECLARE @Is10PctHolder BIT = 0
DECLARE @IsOther BIT = 0
DECLARE @OtherText NVARCHAR(250) = 'OtherText f796b6fa507e4fa9a827ce60d43ad38c'
DECLARE @OfficerTitle NVARCHAR(50) = 'OfficerTitle f796b6fa507e4fa9a827ce60d43ad38c'
DECLARE @Date DATE = '2/23/2021 4:58:34 PM'
DECLARE @DateSubmitted DATE = '2/23/2021 4:58:34 PM'
 


IF(NOT EXISTS(SELECT 1 FROM 
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
 ))
					
BEGIN
	INSERT INTO [dbo].[Form4Report]
		(
	 [IssuerID],
	 [ReporterID],
	 [ReportID],
	 [IsOfficer],
	 [IsDirector],
	 [Is10PctHolder],
	 [IsOther],
	 [OtherText],
	 [OfficerTitle],
	 [Date],
	 [DateSubmitted]
		)
	SELECT 		
			 @IssuerID,
	 @ReporterID,
	 @ReportID,
	 @IsOfficer,
	 @IsDirector,
	 @Is10PctHolder,
	 @IsOther,
	 @OtherText,
	 @OfficerTitle,
	 @Date,
	 @DateSubmitted
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Form4Report] e
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

SELECT 
	@ID
