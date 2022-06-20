


DECLARE @ID BIGINT = NULL
DECLARE @TimeStart DATETIME = '10/9/2019 6:02:20 PM'
DECLARE @TimeEnd DATETIME = '10/9/2019 6:02:20 PM'
DECLARE @RequestJson NVARCHAR(1000) = 'RequestJson a387175cb12b4b619f24b3ab62e5c6b7'
DECLARE @StateID BIGINT = 1
 
DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[ImportRun]
				WHERE 
	(CASE WHEN @TimeStart IS NOT NULL THEN (CASE WHEN [TimeStart] = @TimeStart THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TimeEnd IS NOT NULL THEN (CASE WHEN [TimeEnd] = @TimeEnd THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequestJson IS NOT NULL THEN (CASE WHEN [RequestJson] = @RequestJson THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StateID IS NOT NULL THEN (CASE WHEN [StateID] = @StateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	SET @Fail = 1
END

DELETE FROM 
	[dbo].[ImportRun]
	WHERE 
	(CASE WHEN @TimeStart IS NOT NULL THEN (CASE WHEN [TimeStart] = @TimeStart THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @TimeEnd IS NOT NULL THEN (CASE WHEN [TimeEnd] = @TimeEnd THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @RequestJson IS NOT NULL THEN (CASE WHEN [RequestJson] = @RequestJson THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @StateID IS NOT NULL THEN (CASE WHEN [StateID] = @StateID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

IF(@Fail = 1) 
BEGIN
	THROW 51001, 'ImportRun was not inserted', 1
END