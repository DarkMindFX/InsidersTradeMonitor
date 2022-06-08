


DECLARE @ID BIGINT = NULL
DECLARE @IssuerID BIGINT = 100005
DECLARE @ReporterID BIGINT = 100005
DECLARE @IsOfficer BIT = 0
DECLARE @IsDirector BIT = 0
DECLARE @Is10PctHolder BIT = 0
DECLARE @IsOther BIT = 0
DECLARE @OtherText NVARCHAR(250) = 'OtherText f4a24a9cc4a74b1ba07260fbbfc02381'
DECLARE @OfficerTitle NVARCHAR(50) = 'OfficerTitle f4a24a9cc4a74b1ba07260fbbfc02381'
DECLARE @Date DATE = '5/4/2022 10:09:26 AM'
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[Form4Report]
				WHERE 
	(CASE WHEN @IssuerID IS NOT NULL THEN (CASE WHEN [IssuerID] = @IssuerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReporterID IS NOT NULL THEN (CASE WHEN [ReporterID] = @ReporterID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsOfficer IS NOT NULL THEN (CASE WHEN [IsOfficer] = @IsOfficer THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDirector IS NOT NULL THEN (CASE WHEN [IsDirector] = @IsDirector THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Is10PctHolder IS NOT NULL THEN (CASE WHEN [Is10PctHolder] = @Is10PctHolder THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsOther IS NOT NULL THEN (CASE WHEN [IsOther] = @IsOther THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OtherText IS NOT NULL THEN (CASE WHEN [OtherText] = @OtherText THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OfficerTitle IS NOT NULL THEN (CASE WHEN [OfficerTitle] = @OfficerTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Date IS NOT NULL THEN (CASE WHEN [Date] = @Date THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[Form4Report]
		(
	 [IssuerID],
	 [ReporterID],
	 [IsOfficer],
	 [IsDirector],
	 [Is10PctHolder],
	 [IsOther],
	 [OtherText],
	 [OfficerTitle],
	 [Date]
		)
	SELECT 		
			 @IssuerID,
	 @ReporterID,
	 @IsOfficer,
	 @IsDirector,
	 @Is10PctHolder,
	 @IsOther,
	 @OtherText,
	 @OfficerTitle,
	 @Date
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[Form4Report] e
WHERE
	(CASE WHEN @IssuerID IS NOT NULL THEN (CASE WHEN [IssuerID] = @IssuerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ReporterID IS NOT NULL THEN (CASE WHEN [ReporterID] = @ReporterID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsOfficer IS NOT NULL THEN (CASE WHEN [IsOfficer] = @IsOfficer THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsDirector IS NOT NULL THEN (CASE WHEN [IsDirector] = @IsDirector THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Is10PctHolder IS NOT NULL THEN (CASE WHEN [Is10PctHolder] = @Is10PctHolder THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @IsOther IS NOT NULL THEN (CASE WHEN [IsOther] = @IsOther THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OtherText IS NOT NULL THEN (CASE WHEN [OtherText] = @OtherText THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @OfficerTitle IS NOT NULL THEN (CASE WHEN [OfficerTitle] = @OfficerTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Date IS NOT NULL THEN (CASE WHEN [Date] = @Date THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
