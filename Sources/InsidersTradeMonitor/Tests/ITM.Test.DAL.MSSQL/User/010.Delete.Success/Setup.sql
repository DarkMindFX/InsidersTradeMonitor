


DECLARE @ID BIGINT = NULL
DECLARE @Login NVARCHAR(250) = 'Login c64cc4a83510465291b4ad565394630e'
DECLARE @PwdHash NVARCHAR(250) = 'PwdHash c64cc4a83510465291b4ad565394630e'
DECLARE @Salt NVARCHAR(50) = 'Salt c64cc4a83510465291b4ad565394630e'
DECLARE @FirstName NVARCHAR(50) = 'FirstName c64cc4a83510465291b4ad565394630e'
DECLARE @MiddleName NVARCHAR(50) = 'MiddleName c64cc4a83510465291b4ad565394630e'
DECLARE @LastName NVARCHAR(50) = 'LastName c64cc4a83510465291b4ad565394630e'
DECLARE @FriendlyName NVARCHAR(50) = 'FriendlyName c64cc4a83510465291b4ad565394630e'
DECLARE @CreatedDate DATETIME = '11/30/2024 11:20:17 PM'
DECLARE @ModifiedDate DATETIME = '11/30/2024 11:20:17 PM'
DECLARE @ModifiedByID BIGINT = 100007
 


IF(NOT EXISTS(SELECT 1 FROM 
					[dbo].[User]
				WHERE 
	(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN [Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN [Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN [MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN [LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FriendlyName IS NOT NULL THEN (CASE WHEN [FriendlyName] = @FriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN
	INSERT INTO [dbo].[User]
		(
	 [Login],
	 [PwdHash],
	 [Salt],
	 [FirstName],
	 [MiddleName],
	 [LastName],
	 [FriendlyName],
	 [CreatedDate],
	 [ModifiedDate],
	 [ModifiedByID]
		)
	SELECT 		
			 @Login,
	 @PwdHash,
	 @Salt,
	 @FirstName,
	 @MiddleName,
	 @LastName,
	 @FriendlyName,
	 @CreatedDate,
	 @ModifiedDate,
	 @ModifiedByID
END

SELECT TOP 1 
	@ID = [ID]
FROM 
	[dbo].[User] e
WHERE
	(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN [Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN [Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN [MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN [LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @FriendlyName IS NOT NULL THEN (CASE WHEN [FriendlyName] = @FriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 

SELECT 
	@ID
