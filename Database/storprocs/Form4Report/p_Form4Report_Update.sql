


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_Update]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_Update]
			@ID BIGINT,
			@IssuerID BIGINT,
			@ReporterID BIGINT,
			@ReportID NVARCHAR(50),
			@IsOfficer BIT,
			@IsDirector BIT,
			@Is10PctHolder BIT,
			@IsOther BIT,
			@OtherText NVARCHAR(250),
			@OfficerTitle NVARCHAR(50),
			@Date DATE,
			@DateSubmitted DATE
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Form4Report]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Form4Report]
		SET
									[IssuerID] = IIF( @IssuerID IS NOT NULL, @IssuerID, [IssuerID] ) ,
									[ReporterID] = IIF( @ReporterID IS NOT NULL, @ReporterID, [ReporterID] ) ,
									[ReportID] = IIF( @ReportID IS NOT NULL, @ReportID, [ReportID] ) ,
									[IsOfficer] = IIF( @IsOfficer IS NOT NULL, @IsOfficer, [IsOfficer] ) ,
									[IsDirector] = IIF( @IsDirector IS NOT NULL, @IsDirector, [IsDirector] ) ,
									[Is10PctHolder] = IIF( @Is10PctHolder IS NOT NULL, @Is10PctHolder, [Is10PctHolder] ) ,
									[IsOther] = IIF( @IsOther IS NOT NULL, @IsOther, [IsOther] ) ,
									[OtherText] = IIF( @OtherText IS NOT NULL, @OtherText, [OtherText] ) ,
									[OfficerTitle] = IIF( @OfficerTitle IS NOT NULL, @OfficerTitle, [OfficerTitle] ) ,
									[Date] = IIF( @Date IS NOT NULL, @Date, [Date] ) ,
									[DateSubmitted] = IIF( @DateSubmitted IS NOT NULL, @DateSubmitted, [DateSubmitted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Form4Report was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Form4Report] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IssuerID IS NOT NULL THEN (CASE WHEN e.[IssuerID] = @IssuerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ReporterID IS NOT NULL THEN (CASE WHEN e.[ReporterID] = @ReporterID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ReportID IS NOT NULL THEN (CASE WHEN e.[ReportID] = @ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsOfficer IS NOT NULL THEN (CASE WHEN e.[IsOfficer] = @IsOfficer THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDirector IS NOT NULL THEN (CASE WHEN e.[IsDirector] = @IsDirector THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Is10PctHolder IS NOT NULL THEN (CASE WHEN e.[Is10PctHolder] = @Is10PctHolder THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsOther IS NOT NULL THEN (CASE WHEN e.[IsOther] = @IsOther THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OtherText IS NOT NULL THEN (CASE WHEN e.[OtherText] = @OtherText THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OfficerTitle IS NOT NULL THEN (CASE WHEN e.[OfficerTitle] = @OfficerTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Date IS NOT NULL THEN (CASE WHEN e.[Date] = @Date THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DateSubmitted IS NOT NULL THEN (CASE WHEN e.[DateSubmitted] = @DateSubmitted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO