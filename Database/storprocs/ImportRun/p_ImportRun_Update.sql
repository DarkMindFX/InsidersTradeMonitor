


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRun_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRun_Update]
GO

CREATE PROCEDURE [dbo].[p_ImportRun_Update]
			@ID BIGINT,
			@TimeStart DATETIME,
			@TimeEnd DATETIME,
			@RequestJson NVARCHAR(1000),
			@StateID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRun]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ImportRun]
		SET
									[TimeStart] = IIF( @TimeStart IS NOT NULL, @TimeStart, [TimeStart] ) ,
									[TimeEnd] = IIF( @TimeEnd IS NOT NULL, @TimeEnd, [TimeEnd] ) ,
									[RequestJson] = IIF( @RequestJson IS NOT NULL, @RequestJson, [RequestJson] ) ,
									[StateID] = IIF( @StateID IS NOT NULL, @StateID, [StateID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImportRun was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImportRun] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TimeStart IS NOT NULL THEN (CASE WHEN e.[TimeStart] = @TimeStart THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TimeEnd IS NOT NULL THEN (CASE WHEN e.[TimeEnd] = @TimeEnd THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @RequestJson IS NOT NULL THEN (CASE WHEN e.[RequestJson] = @RequestJson THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @StateID IS NOT NULL THEN (CASE WHEN e.[StateID] = @StateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO