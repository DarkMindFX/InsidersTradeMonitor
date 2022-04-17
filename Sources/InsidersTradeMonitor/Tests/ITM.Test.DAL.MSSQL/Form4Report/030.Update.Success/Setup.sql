


DECLARE @ID BIGINT = NULL
DECLARE @IssuerID BIGINT = NULL
DECLARE @ReporterID BIGINT = NULL
DECLARE @IsOfficer BIT = 0
DECLARE @IsDirector BIT = 0
DECLARE @Is10PctHolder BIT = 0
DECLARE @IsOther BIT = 0
DECLARE @OtherText BIT = 0
DECLARE @OfficerTitle NVARCHAR(50) = 'OfficerTitle 4a2750f825214cc89e67b7c702f0c6b8'
DECLARE @Date DATE = '4/9/2024 8:39:45 PM'
 


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
