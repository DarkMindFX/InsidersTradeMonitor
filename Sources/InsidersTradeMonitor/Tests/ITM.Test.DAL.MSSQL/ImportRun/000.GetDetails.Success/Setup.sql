


DECLARE @ID BIGINT = NULL
DECLARE @TimeStart DATETIME = '10/9/2019 6:02:20 PM'
DECLARE @TimeEnd DATETIME = '10/9/2019 6:02:20 PM'
DECLARE @RequestJson NVARCHAR(1000) = 'RequestJson 24be0457da26426abb2365356302670b'
DECLARE @StateID BIGINT = 1
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[ImportRun]
				WHERE 
	(CASE WHEN @TimeStart IS NOT NULL THEN (CASE WHEN [TimeStart] = @TimeStart THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TimeEnd IS NOT NULL THEN (CASE WHEN [TimeEnd] = @TimeEnd THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequestJson IS NOT NULL THEN (CASE WHEN [RequestJson] = @RequestJson THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StateID IS NOT NULL THEN (CASE WHEN [StateID] = @StateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[ImportRun]
		(
	 [TimeStart],
	 [TimeEnd],
	 [RequestJson],
	 [StateID]
		)
	SELECT 		
			 @TimeStart,
	 @TimeEnd,
	 @RequestJson,
	 @StateID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[ImportRun] e
WHERE
	(CASE WHEN @TimeStart IS NOT NULL THEN (CASE WHEN [TimeStart] = @TimeStart THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TimeEnd IS NOT NULL THEN (CASE WHEN [TimeEnd] = @TimeEnd THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequestJson IS NOT NULL THEN (CASE WHEN [RequestJson] = @RequestJson THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StateID IS NOT NULL THEN (CASE WHEN [StateID] = @StateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
