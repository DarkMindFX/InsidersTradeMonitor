


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_Insert]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_Insert]
			@ID BIGINT,
			@IssuerID BIGINT,
			@ReporterID BIGINT,
			@IsOfficer BIT,
			@IsDirector BIT,
			@Is10PctHolder BIT,
			@IsOther BIT,
			@OtherText BIT,
			@OfficerTitle NVARCHAR(50),
			@Date DATE
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Form4Report]
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
	
	

	SELECT
		e.*
	FROM
		[dbo].[Form4Report] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IssuerID IS NOT NULL THEN (CASE WHEN e.[IssuerID] = @IssuerID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ReporterID IS NOT NULL THEN (CASE WHEN e.[ReporterID] = @ReporterID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsOfficer IS NOT NULL THEN (CASE WHEN e.[IsOfficer] = @IsOfficer THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsDirector IS NOT NULL THEN (CASE WHEN e.[IsDirector] = @IsDirector THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Is10PctHolder IS NOT NULL THEN (CASE WHEN e.[Is10PctHolder] = @Is10PctHolder THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsOther IS NOT NULL THEN (CASE WHEN e.[IsOther] = @IsOther THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OtherText IS NOT NULL THEN (CASE WHEN e.[OtherText] = @OtherText THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OfficerTitle IS NOT NULL THEN (CASE WHEN e.[OfficerTitle] = @OfficerTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Date IS NOT NULL THEN (CASE WHEN e.[Date] = @Date THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO