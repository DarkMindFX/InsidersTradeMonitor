


-- original values --
DECLARE @ID BIGINT = NULL
DECLARE @Login NVARCHAR(250) = 'Login 844f50aab10c40d5af460242e6c97255'
DECLARE @PwdHash NVARCHAR(250) = 'PwdHash 844f50aab10c40d5af460242e6c97255'
DECLARE @Salt NVARCHAR(50) = 'Salt 844f50aab10c40d5af460242e6c97255'
DECLARE @FirstName NVARCHAR(50) = 'FirstName 844f50aab10c40d5af460242e6c97255'
DECLARE @MiddleName NVARCHAR(50) = 'MiddleName 844f50aab10c40d5af460242e6c97255'
DECLARE @LastName NVARCHAR(50) = 'LastName 844f50aab10c40d5af460242e6c97255'
DECLARE @FriendlyName NVARCHAR(50) = 'FriendlyName 844f50aab10c40d5af460242e6c97255'
DECLARE @CreatedDate DATETIME = '4/21/2022 9:07:17 AM'
DECLARE @ModifiedDate DATETIME = '4/21/2022 9:07:17 AM'
DECLARE @ModifiedByID BIGINT = 100011
 
-- updated values --

DECLARE @updID BIGINT = NULL
DECLARE @updLogin NVARCHAR(250) = 'Login e8a38a804bb54b4d8108341fb6572d91'
DECLARE @updPwdHash NVARCHAR(250) = 'PwdHash e8a38a804bb54b4d8108341fb6572d91'
DECLARE @updSalt NVARCHAR(50) = 'Salt e8a38a804bb54b4d8108341fb6572d91'
DECLARE @updFirstName NVARCHAR(50) = 'FirstName e8a38a804bb54b4d8108341fb6572d91'
DECLARE @updMiddleName NVARCHAR(50) = 'MiddleName e8a38a804bb54b4d8108341fb6572d91'
DECLARE @updLastName NVARCHAR(50) = 'LastName e8a38a804bb54b4d8108341fb6572d91'
DECLARE @updFriendlyName NVARCHAR(50) = 'FriendlyName e8a38a804bb54b4d8108341fb6572d91'
DECLARE @updCreatedDate DATETIME = '3/1/2025 6:54:17 PM'
DECLARE @updModifiedDate DATETIME = '3/1/2025 6:54:17 PM'
DECLARE @updModifiedByID BIGINT = 100009
 

DECLARE @Fail AS BIT = 0

IF(NOT EXISTS(SELECT 1 FROM 
				[dbo].[User]
				WHERE 
	(CASE WHEN @updLogin IS NOT NULL THEN (CASE WHEN [Login] = @updLogin THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @updPwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSalt IS NOT NULL THEN (CASE WHEN [Salt] = @updSalt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updFirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @updFirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updMiddleName IS NOT NULL THEN (CASE WHEN [MiddleName] = @updMiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updLastName IS NOT NULL THEN (CASE WHEN [LastName] = @updLastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updFriendlyName IS NOT NULL THEN (CASE WHEN [FriendlyName] = @updFriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
 ))
					
BEGIN

DELETE FROM 
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

	SET @Fail = 1
END
ELSE
BEGIN
DELETE FROM 
	[dbo].[User]
	WHERE 
	(CASE WHEN @updLogin IS NOT NULL THEN (CASE WHEN [Login] = @updLogin THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updPwdHash IS NOT NULL THEN (CASE WHEN [PwdHash] = @updPwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updSalt IS NOT NULL THEN (CASE WHEN [Salt] = @updSalt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updFirstName IS NOT NULL THEN (CASE WHEN [FirstName] = @updFirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updMiddleName IS NOT NULL THEN (CASE WHEN [MiddleName] = @updMiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updLastName IS NOT NULL THEN (CASE WHEN [LastName] = @updLastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updFriendlyName IS NOT NULL THEN (CASE WHEN [FriendlyName] = @updFriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updCreatedDate IS NOT NULL THEN (CASE WHEN [CreatedDate] = @updCreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedDate IS NOT NULL THEN (CASE WHEN [ModifiedDate] = @updModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
	(CASE WHEN @updModifiedByID IS NOT NULL THEN (CASE WHEN [ModifiedByID] = @updModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
END


IF(@Fail = 1) 
BEGIN
	THROW 51001, 'User was not updated', 1
END