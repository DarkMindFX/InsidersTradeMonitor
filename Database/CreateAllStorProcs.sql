USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_TestData_CleanUp]    Script Date: 4/25/2024 4:03:59 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_TestData_CleanUp]
GO
/****** Object:  StoredProcedure [dbo].[p_TestData_CleanUp]    Script Date: 4/25/2024 4:03:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Usage:
EXEC dbo.p_TestData_CleanUp
*/
CREATE PROCEDURE [dbo].[p_TestData_CleanUp] 
	
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @tblParams AS TABLE (
		[Order] INT,
		[Table] NVARCHAR(100),
		[File]  NVARCHAR(100)
	)

	DECLARE @file AS NVARCHAR(100) 
	DECLARE @table AS NVARCHAR(100) 

	INSERT INTO @tblParams	
	
	SELECT 5, 'ImportRunForm4Report', 'ImportRunForm4Report.csv'	UNION
	SELECT 6, 'ImportRun', 'ImportRun.csv'	UNION
	SELECT 7, 'NonDerivativeTransaction', 'NonDerivativeTransaction.csv'	UNION
	SELECT 8, 'DerivativeTransaction', 'DerivativeTransaction.csv'	UNION
	SELECT 9, 'Form4Report', 'Form4Report.csv'	UNION
	SELECT 10, 'Entity', 'Entity.csv' UNION		
	SELECT 11, 'User', 'User.csv' 

	DECLARE paramsCursor CURSOR FOR
	SELECT [File], [Table] FROM @tblParams ORDER BY [Order]

	DECLARE @sql AS NVARCHAR(MAX)
	
	OPEN paramsCursor 

	BEGIN TRY

		BEGIN TRANSACTION

		FETCH NEXT FROM paramsCursor INTO @file, @table

		WHILE @@FETCH_STATUS = 0
		BEGIN

			PRINT('======= ' + @table + ' =======')

			SET @sql = 'DELETE FROM dbo.[' + @table + ']'
			PRINT(@sql)

			EXEC(@sql);

			FETCH NEXT FROM paramsCursor INTO @file, @table

		END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT   
        ERROR_NUMBER() AS ErrorNumber  
       ,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH

	CLOSE paramsCursor
	DEALLOCATE paramsCursor
END
GO
USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_TestData_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_TestData_Populate]
GO
/****** Object:  StoredProcedure [dbo].[p_TestData_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
Usage:
1. From local file
(Optionally) EXEC p_TestData_CleanUp
EXEC p_TestData_Populate 'd:\Projects\InsidersTradeMonitor\Testing\TestData\'

2. From Azure Blob

CREATE MASTER KEY ENCRYPTION BY PASSWORD ='<Password>'

CREATE DATABASE SCOPED CREDENTIAL UploadPhotoPrintTestData
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = '<SAS for blob folder>';

CREATE EXTERNAL DATA SOURCE InsidersTradeMonitor_Azure_TestData
WITH (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://instrademonitor.blob.core.windows.net',
        CREDENTIAL = UploadPhotoPrintTestData
    );
GO 

EXEC p_TestData_Populate 'instrademonitor-test-data/', 'InsidersTradeMonitor_Azure_TestData'

DROP EXTERNAL DATA SOURCE InsidersTradeMonitor_Azure_TestData

DROP DATABASE SCOPED CREDENTIAL UploadPhotoPrintTestData

DROP MASTER KEY
*/
CREATE PROCEDURE [dbo].[p_TestData_Populate]
	@RootFolder NVARCHAR(100),
	@DataSource NVARCHAR(100) = NULL
AS
BEGIN

	SET NOCOUNT ON;
	
	DECLARE @tblParams AS TABLE (
		[Order] INT,
		[Table] NVARCHAR(100),
		[File]  NVARCHAR(100),
		[HasIdentity] BIT
	)

	DECLARE @file AS NVARCHAR(100) 
	DECLARE @table AS NVARCHAR(100) 
	DECLARE @hasIdentity AS BIT 

	INSERT INTO @tblParams
	
	SELECT 0, 'User', 'User.csv', 1			UNION
	SELECT 1, 'Entity', 'Entity.csv', 1			UNION
	SELECT 2, 'Form4Report', 'Form4Report.csv', 1 UNION
	SELECT 3, 'DerivativeTransaction', 'DerivativeTransaction.csv', 1	UNION
	SELECT 4, 'NonDerivativeTransaction', 'NonDerivativeTransaction.csv', 1	UNION
	SELECT 5, 'ImportRun', 'ImportRun.csv', 1	UNION
	SELECT 6, 'ImportRunForm4Report', 'ImportRunForm4Report.csv', 1



	DECLARE paramsCursor CURSOR FOR
	SELECT [File], [Table], [HasIdentity] FROM @tblParams ORDER BY [Order]
	
	DECLARE @Path AS NVARCHAR(MAX)
	DECLARE @sql AS NVARCHAR(MAX)
	
	OPEN paramsCursor 

	
	BEGIN TRY

		BEGIN TRANSACTION

		FETCH NEXT FROM paramsCursor INTO @file, @table, @hasIdentity

		WHILE @@FETCH_STATUS = 0
		BEGIN

			PRINT('======= ' + @file + ' -> ' + @table + ' =======')

			SELECT @Path = CONCAT(@RootFolder, @file)
			IF(@hasIdentity = 1) BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @table + '] ON;'

				PRINT(@sql)
				EXEC(@sql)
			END


			SET @sql = 'BULK INSERT dbo.[' + @table + ']
			FROM ''' + @Path + '''
			WITH (' +
			CASE
				WHEN @DataSource IS NOT NULL THEN 'DATA_SOURCE=''' + @DataSource + ''',' 
				ELSE ''
			END +
			'KEEPIDENTITY,
			FIRSTROW = 2,
			FIELDTERMINATOR = '','',
			ROWTERMINATOR=''0x0d0a'',
			BATCHSIZE=2500000);'

			PRINT(@sql)
			EXEC(@sql)

			IF(@hasIdentity = 1) 
			BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @table + '] OFF;'

				PRINT(@sql)
				EXEC(@sql)
			END

			FETCH NEXT FROM paramsCursor INTO @file, @table, @hasIdentity

		END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT   
        ERROR_NUMBER() AS ErrorNumber  
       ,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH

	CLOSE paramsCursor
	DEALLOCATE paramsCursor
    
END
GO
USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_EntityType_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Util_EntityType_Populate]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_EntityType_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_Util_EntityType_Populate]

AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @tblTemp AS TABLE (
		[ID] bigint,
		[TypeName] nvarchar(50)
	)

	INSERT INTO @tblTemp
	SELECT 1, 'Company' UNION
	SELECT 2, 'Person'


	SET IDENTITY_INSERT dbo.EntityType ON; 

	MERGE dbo.EntityType as t 
	USING @tblTemp as s 
	ON(t.ID = s.ID)
	WHEN MATCHED 
	THEN UPDATE SET t.TypeName = s.TypeName
	WHEN NOT MATCHED BY TARGET
	THEN INSERT (ID, TypeName) VALUES (s.ID, s.TypeName)
	WHEN NOT MATCHED BY SOURCE 
	THEN DELETE 
	;

	SET IDENTITY_INSERT dbo.EntityType OFF; 
END
GO
USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_Entity_Seed]    Script Date: 4/25/2024 4:03:59 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Util_Entity_Seed]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_Entity_Seed]    Script Date: 4/25/2024 4:03:59 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

/*
Usage:
1. From local file
(Optionally) EXEC p_TestData_CleanUp
EXEC dbo.p_Util_Entity_Seed 'd:\Projects\InsidersTradeMonitor\Database\SeedData\'

2. From Azure Blob

CREATE MASTER KEY ENCRYPTION BY PASSWORD ='<Password>'

CREATE DATABASE SCOPED CREDENTIAL UploadPhotoPrintTestData
WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
SECRET = '<SAS for blob folder>';

CREATE EXTERNAL DATA SOURCE InsidersTradeMonitor_Azure_TestData
WITH (
        TYPE = BLOB_STORAGE,
        LOCATION = 'https://instrademonitor.blob.core.windows.net',
        CREDENTIAL = UploadPhotoPrintTestData
    );
GO 

EXEC p_TestData_Populate 'instrademonitor-test-data/', 'InsidersTradeMonitor_Azure_TestData'

DROP EXTERNAL DATA SOURCE InsidersTradeMonitor_Azure_TestData

DROP DATABASE SCOPED CREDENTIAL UploadPhotoPrintTestData

DROP MASTER KEY
*/
CREATE PROCEDURE [dbo].[p_Util_Entity_Seed]
	@RootFolder NVARCHAR(100),
	@DataSource NVARCHAR(100) = NULL
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @file AS NVARCHAR(100) = 'Entity.csv'
	DECLARE @table AS NVARCHAR(100) = 'Entity'
    DECLARE @hasIdentity AS BIT = 1

	DECLARE @Path AS NVARCHAR(MAX)
	DECLARE @sql AS NVARCHAR(MAX)

	BEGIN TRY

		BEGIN TRANSACTION

		PRINT('======= ' + @file + ' -> ' + @table + ' =======')

			SELECT @Path = CONCAT(@RootFolder, @file)
			IF(@hasIdentity = 1) BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @table + '] ON;'

				PRINT(@sql)
				EXEC(@sql)
			END


			SET @sql = 'BULK INSERT dbo.[' + @table + ']
			FROM ''' + @Path + '''
			WITH (' +
			CASE
				WHEN @DataSource IS NOT NULL THEN 'DATA_SOURCE=''' + @DataSource + ''',' 
				ELSE ''
			END +
			'KEEPIDENTITY,
			FIRSTROW = 2,
			FIELDTERMINATOR = '','',
			ROWTERMINATOR=''0x0d0a'',
			BATCHSIZE=2500000);'

			PRINT(@sql)
			EXEC(@sql)

			IF(@hasIdentity = 1) 
			BEGIN
				SET @sql = 'SET IDENTITY_INSERT dbo.[' + @table + '] OFF;'

				PRINT(@sql)
				EXEC(@sql)
			END

		COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT   
        ERROR_NUMBER() AS ErrorNumber  
       ,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
END
GO
USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_ImportRunState_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Util_ImportRunState_Populate]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_ImportRunState_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_Util_ImportRunState_Populate]

AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @tblTemp AS TABLE (
		[ID] bigint,
		[Name] nvarchar(50)
	)

	INSERT INTO @tblTemp
	SELECT 1, 'InProgress' UNION
	SELECT 2, 'Success' UNION
	SELECT 3, 'Fail'


	SET IDENTITY_INSERT dbo.ImportRunState ON; 

	MERGE dbo.ImportRunState as t 
	USING @tblTemp as s 
	ON(t.ID = s.ID)
	WHEN MATCHED 
	THEN UPDATE SET t.Name = s.Name
	WHEN NOT MATCHED BY TARGET
	THEN INSERT (ID, Name) VALUES (s.ID, s.Name)
	WHEN NOT MATCHED BY SOURCE 
	THEN DELETE 
	;

	SET IDENTITY_INSERT dbo.ImportRunState OFF; 
END
GO
USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_OwnershipType_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Util_OwnershipType_Populate]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_OwnershipType_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_Util_OwnershipType_Populate]

AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @tblTemp AS TABLE (
		[ID] bigint,
		[Code] nvarchar(10),
		[Description] nvarchar(50)
	)

	INSERT INTO @tblTemp
	SELECT 1, 'D', 'Direct' UNION
	SELECT 2, 'I', 'Indirect'


	SET IDENTITY_INSERT dbo.OwnershipType ON; 

	MERGE dbo.OwnershipType as t 
	USING @tblTemp as s 
	ON(t.ID = s.ID)
	WHEN MATCHED 
	THEN UPDATE SET t.Code = s.Code, t.Description = s.Description
	WHEN NOT MATCHED BY TARGET
	THEN INSERT (ID, Code, Description) VALUES (s.ID, s.Code, s.Description)
	WHEN NOT MATCHED BY SOURCE 
	THEN DELETE 
	;

	SET IDENTITY_INSERT dbo.OwnershipType OFF; 
END
GO
USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_TransactionCode_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Util_TransactionCode_Populate]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_TransactionCode_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_Util_TransactionCode_Populate]

AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @tblTemp AS TABLE (
		[ID] bigint,
		[Code] nvarchar(10),
		[Description] nvarchar(250)
	)

	INSERT INTO @tblTemp
	SELECT 1, 'P', 'Open market or private purchase of non-derivative or derivative security' UNION
	SELECT 2, 'S', 'Open market or private sale of non-derivative or derivative security' UNION
	SELECT 3, 'V', 'Transaction voluntarily reported earlier than required' UNION
	SELECT 4, 'A', 'Grant, award or other acquisition pursuant to Rule 16b-3(d) ' UNION
	SELECT 5, 'D', 'Disposition to the issuer of issuer equity securities pursuant to Rule 16b-3(e) ' UNION
	SELECT 6, 'F', 'Payment of exercise price or tax liability by delivering or withholding securities incident to the receipt, exercise or vesting of a security issued in accordance with Rule 16b-3' UNION
	SELECT 7, 'I', 'Discretionary transaction in accordance with Rule 16b-3(f) resulting in acquisition or disposition of issuer securities' UNION
	SELECT 8, 'M', 'Exercise or conversion of derivative security exempted pursuant to Rule 16b-3 ' UNION
	SELECT 9, 'C', 'Conversion of derivative security' UNION
	SELECT 10, 'E', 'Expiration of short derivative position' UNION
	SELECT 11, 'H', 'Expiration (or cancellation) of long derivative position with value received' UNION
	SELECT 12, 'O', 'Exercise of out-of-the-money derivative security' UNION
	SELECT 13, 'X', 'Exercise of in-the-money or at-the-money derivative security' UNION
	SELECT 14, 'G', 'Bona fide gift' UNION
	SELECT 15, 'L', 'Small acquisition under Rule 16a-6' UNION
	SELECT 16, 'W', 'Acquisition or disposition by will or the laws of descent and distribution' UNION
	SELECT 17, 'Z', 'Deposit into or withdrawal from voting trust ' UNION
	SELECT 18, 'J', 'Other acquisition or disposition (describe transaction)' UNION
	SELECT 19, 'K', 'Transaction in equity swap or instrument with similar characteristics' UNION
	SELECT 20, 'U', 'Disposition pursuant to a tender of shares in a change of control transaction ' 


	SET IDENTITY_INSERT dbo.TransactionCode ON; 

	MERGE dbo.TransactionCode as t 
	USING @tblTemp as s 
	ON(t.ID = s.ID)
	WHEN MATCHED 
	THEN UPDATE SET t.Code = s.Code, t.Description = s.Description
	WHEN NOT MATCHED BY TARGET
	THEN INSERT (ID, Code, Description) VALUES (s.ID, s.Code, s.Description)
	WHEN NOT MATCHED BY SOURCE 
	THEN DELETE 
	;

	SET IDENTITY_INSERT dbo.TransactionCode OFF; 
END
GO
USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_TransactionType_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Util_TransactionType_Populate]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_TransactionType_Populate]    Script Date: 4/25/2024 4:03:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[p_Util_TransactionType_Populate]

AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @tblTemp AS TABLE (
		[ID] bigint,
		[Code] nvarchar(10),
		[Description] nvarchar(250)
	)

	INSERT INTO @tblTemp
	SELECT 1, 'A', 'Acquired' UNION
	SELECT 2, 'D', 'Disposed'


	SET IDENTITY_INSERT dbo.TransactionType ON; 

	MERGE dbo.TransactionType as t 
	USING @tblTemp as s 
	ON(t.ID = s.ID)
	WHEN MATCHED 
	THEN UPDATE SET t.Code = s.Code, t.Description = s.Description
	WHEN NOT MATCHED BY TARGET
	THEN INSERT (ID, Code, Description) VALUES (s.ID, s.Code, s.Description)
	WHEN NOT MATCHED BY SOURCE 
	THEN DELETE 
	;

	SET IDENTITY_INSERT dbo.TransactionType OFF; 
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_Delete]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[DerivativeTransaction]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[DerivativeTransaction] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_GetAll]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[DerivativeTransaction] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_GetByForm4ReportID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_GetByForm4ReportID]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_GetByForm4ReportID]

	@Form4ReportID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DerivativeTransaction] c 
				WHERE
					[Form4ReportID] = @Form4ReportID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DerivativeTransaction] e
		WHERE 
			[Form4ReportID] = @Form4ReportID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_GetByOwnershipTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_GetByOwnershipTypeID]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_GetByOwnershipTypeID]

	@OwnershipTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DerivativeTransaction] c 
				WHERE
					[OwnershipTypeID] = @OwnershipTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DerivativeTransaction] e
		WHERE 
			[OwnershipTypeID] = @OwnershipTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_GetByTransactionCodeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_GetByTransactionCodeID]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_GetByTransactionCodeID]

	@TransactionCodeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DerivativeTransaction] c 
				WHERE
					[TransactionCodeID] = @TransactionCodeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DerivativeTransaction] e
		WHERE 
			[TransactionCodeID] = @TransactionCodeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_GetByTransactionTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_GetByTransactionTypeID]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_GetByTransactionTypeID]

	@TransactionTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DerivativeTransaction] c 
				WHERE
					[TransactionTypeID] = @TransactionTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DerivativeTransaction] e
		WHERE 
			[TransactionTypeID] = @TransactionTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DerivativeTransaction] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DerivativeTransaction] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_Insert]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_Insert]
			@ID BIGINT,
			@Form4ReportID BIGINT,
			@TitleOfDerivative NVARCHAR(250),
			@ConversionExercisePrice DECIMAL(20, 6),
			@TransactionDate DATE,
			@TransactionCodeID BIGINT,
			@EarlyVoluntarilyReport BIT,
			@SharesAmount BIGINT,
			@DerivativeSecurityPrice DECIMAL(20, 6),
			@TransactionTypeID BIGINT,
			@DateExercisable DATE,
			@ExpirationDate DATE,
			@UnderlyingTitle NVARCHAR(250),
			@UnderlyingSharesAmount BIGINT,
			@AmountFollowingReport BIGINT,
			@OwnershipTypeID BIGINT,
			@NatureOfIndirectOwnership NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[DerivativeTransaction]
	SELECT 
		@Form4ReportID,
		@TitleOfDerivative,
		@ConversionExercisePrice,
		@TransactionDate,
		@TransactionCodeID,
		@EarlyVoluntarilyReport,
		@SharesAmount,
		@DerivativeSecurityPrice,
		@TransactionTypeID,
		@DateExercisable,
		@ExpirationDate,
		@UnderlyingTitle,
		@UnderlyingSharesAmount,
		@AmountFollowingReport,
		@OwnershipTypeID,
		@NatureOfIndirectOwnership
	
	

	SELECT
		e.*
	FROM
		[dbo].[DerivativeTransaction] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN e.[Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TitleOfDerivative IS NOT NULL THEN (CASE WHEN e.[TitleOfDerivative] = @TitleOfDerivative THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConversionExercisePrice IS NOT NULL THEN (CASE WHEN e.[ConversionExercisePrice] = @ConversionExercisePrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionDate IS NOT NULL THEN (CASE WHEN e.[TransactionDate] = @TransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionCodeID IS NOT NULL THEN (CASE WHEN e.[TransactionCodeID] = @TransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN e.[EarlyVoluntarilyReport] = @EarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SharesAmount IS NOT NULL THEN (CASE WHEN e.[SharesAmount] = @SharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DerivativeSecurityPrice IS NOT NULL THEN (CASE WHEN e.[DerivativeSecurityPrice] = @DerivativeSecurityPrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionTypeID IS NOT NULL THEN (CASE WHEN e.[TransactionTypeID] = @TransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DateExercisable IS NOT NULL THEN (CASE WHEN e.[DateExercisable] = @DateExercisable THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ExpirationDate IS NOT NULL THEN (CASE WHEN e.[ExpirationDate] = @ExpirationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnderlyingTitle IS NOT NULL THEN (CASE WHEN e.[UnderlyingTitle] = @UnderlyingTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnderlyingSharesAmount IS NOT NULL THEN (CASE WHEN e.[UnderlyingSharesAmount] = @UnderlyingSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AmountFollowingReport IS NOT NULL THEN (CASE WHEN e.[AmountFollowingReport] = @AmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OwnershipTypeID IS NOT NULL THEN (CASE WHEN e.[OwnershipTypeID] = @OwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN e.[NatureOfIndirectOwnership] = @NatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_Update]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_Update]
			@ID BIGINT,
			@Form4ReportID BIGINT,
			@TitleOfDerivative NVARCHAR(250),
			@ConversionExercisePrice DECIMAL(20, 6),
			@TransactionDate DATE,
			@TransactionCodeID BIGINT,
			@EarlyVoluntarilyReport BIT,
			@SharesAmount BIGINT,
			@DerivativeSecurityPrice DECIMAL(20, 6),
			@TransactionTypeID BIGINT,
			@DateExercisable DATE,
			@ExpirationDate DATE,
			@UnderlyingTitle NVARCHAR(250),
			@UnderlyingSharesAmount BIGINT,
			@AmountFollowingReport BIGINT,
			@OwnershipTypeID BIGINT,
			@NatureOfIndirectOwnership NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[DerivativeTransaction]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[DerivativeTransaction]
		SET
									[Form4ReportID] = IIF( @Form4ReportID IS NOT NULL, @Form4ReportID, [Form4ReportID] ) ,
									[TitleOfDerivative] = IIF( @TitleOfDerivative IS NOT NULL, @TitleOfDerivative, [TitleOfDerivative] ) ,
									[ConversionExercisePrice] = IIF( @ConversionExercisePrice IS NOT NULL, @ConversionExercisePrice, [ConversionExercisePrice] ) ,
									[TransactionDate] = IIF( @TransactionDate IS NOT NULL, @TransactionDate, [TransactionDate] ) ,
									[TransactionCodeID] = IIF( @TransactionCodeID IS NOT NULL, @TransactionCodeID, [TransactionCodeID] ) ,
									[EarlyVoluntarilyReport] = IIF( @EarlyVoluntarilyReport IS NOT NULL, @EarlyVoluntarilyReport, [EarlyVoluntarilyReport] ) ,
									[SharesAmount] = IIF( @SharesAmount IS NOT NULL, @SharesAmount, [SharesAmount] ) ,
									[DerivativeSecurityPrice] = IIF( @DerivativeSecurityPrice IS NOT NULL, @DerivativeSecurityPrice, [DerivativeSecurityPrice] ) ,
									[TransactionTypeID] = IIF( @TransactionTypeID IS NOT NULL, @TransactionTypeID, [TransactionTypeID] ) ,
									[DateExercisable] = IIF( @DateExercisable IS NOT NULL, @DateExercisable, [DateExercisable] ) ,
									[ExpirationDate] = IIF( @ExpirationDate IS NOT NULL, @ExpirationDate, [ExpirationDate] ) ,
									[UnderlyingTitle] = IIF( @UnderlyingTitle IS NOT NULL, @UnderlyingTitle, [UnderlyingTitle] ) ,
									[UnderlyingSharesAmount] = IIF( @UnderlyingSharesAmount IS NOT NULL, @UnderlyingSharesAmount, [UnderlyingSharesAmount] ) ,
									[AmountFollowingReport] = IIF( @AmountFollowingReport IS NOT NULL, @AmountFollowingReport, [AmountFollowingReport] ) ,
									[OwnershipTypeID] = IIF( @OwnershipTypeID IS NOT NULL, @OwnershipTypeID, [OwnershipTypeID] ) ,
									[NatureOfIndirectOwnership] = IIF( @NatureOfIndirectOwnership IS NOT NULL, @NatureOfIndirectOwnership, [NatureOfIndirectOwnership] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'DerivativeTransaction was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[DerivativeTransaction] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN e.[Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TitleOfDerivative IS NOT NULL THEN (CASE WHEN e.[TitleOfDerivative] = @TitleOfDerivative THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ConversionExercisePrice IS NOT NULL THEN (CASE WHEN e.[ConversionExercisePrice] = @ConversionExercisePrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionDate IS NOT NULL THEN (CASE WHEN e.[TransactionDate] = @TransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionCodeID IS NOT NULL THEN (CASE WHEN e.[TransactionCodeID] = @TransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN e.[EarlyVoluntarilyReport] = @EarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SharesAmount IS NOT NULL THEN (CASE WHEN e.[SharesAmount] = @SharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DerivativeSecurityPrice IS NOT NULL THEN (CASE WHEN e.[DerivativeSecurityPrice] = @DerivativeSecurityPrice THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionTypeID IS NOT NULL THEN (CASE WHEN e.[TransactionTypeID] = @TransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DateExercisable IS NOT NULL THEN (CASE WHEN e.[DateExercisable] = @DateExercisable THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ExpirationDate IS NOT NULL THEN (CASE WHEN e.[ExpirationDate] = @ExpirationDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnderlyingTitle IS NOT NULL THEN (CASE WHEN e.[UnderlyingTitle] = @UnderlyingTitle THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @UnderlyingSharesAmount IS NOT NULL THEN (CASE WHEN e.[UnderlyingSharesAmount] = @UnderlyingSharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AmountFollowingReport IS NOT NULL THEN (CASE WHEN e.[AmountFollowingReport] = @AmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OwnershipTypeID IS NOT NULL THEN (CASE WHEN e.[OwnershipTypeID] = @OwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN e.[NatureOfIndirectOwnership] = @NatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Entity_GetMonitoredList]    Script Date: 4/25/2024 4:05:53 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Entity_GetMonitoredList]
GO
/****** Object:  StoredProcedure [dbo].[p_Entity_GetMonitoredList]    Script Date: 4/25/2024 4:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Usage:
EXEC dbo.p_TestData_CleanUp
*/
CREATE PROCEDURE [dbo].[p_Entity_GetMonitoredList] 
	
AS
BEGIN
	SELECT
		e.*
	FROM
		dbo.Entity e
	WHERE e.IsMonitored = 1
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Entity_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Entity_Delete]
GO

CREATE PROCEDURE [dbo].[p_Entity_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Entity]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[Entity] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Entity_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Entity_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Entity_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Entity] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Entity_GetByEntityTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Entity_GetByEntityTypeID]
GO

CREATE PROCEDURE [dbo].[p_Entity_GetByEntityTypeID]

	@EntityTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Entity] c 
				WHERE
					[EntityTypeID] = @EntityTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Entity] e
		WHERE 
			[EntityTypeID] = @EntityTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Entity_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Entity_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Entity_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Entity] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Entity] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Entity_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Entity_Insert]
GO

CREATE PROCEDURE [dbo].[p_Entity_Insert]
			@ID BIGINT,
			@EntityTypeID BIGINT,
			@CIK INT,
			@Name NVARCHAR(250),
			@TradingSymbol NVARCHAR(50),
			@IsMonitored BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Entity]
	SELECT 
		@EntityTypeID,
		@CIK,
		@Name,
		@TradingSymbol,
		@IsMonitored
	
	

	SELECT
		e.*
	FROM
		[dbo].[Entity] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN e.[EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN e.[CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN e.[TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsMonitored IS NOT NULL THEN (CASE WHEN e.[IsMonitored] = @IsMonitored THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Entity_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Entity_Update]
GO

CREATE PROCEDURE [dbo].[p_Entity_Update]
			@ID BIGINT,
			@EntityTypeID BIGINT,
			@CIK INT,
			@Name NVARCHAR(250),
			@TradingSymbol NVARCHAR(50),
			@IsMonitored BIT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Entity]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Entity]
		SET
									[EntityTypeID] = IIF( @EntityTypeID IS NOT NULL, @EntityTypeID, [EntityTypeID] ) ,
									[CIK] = IIF( @CIK IS NOT NULL, @CIK, [CIK] ) ,
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[TradingSymbol] = IIF( @TradingSymbol IS NOT NULL, @TradingSymbol, [TradingSymbol] ) ,
									[IsMonitored] = IIF( @IsMonitored IS NOT NULL, @IsMonitored, [IsMonitored] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Entity was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Entity] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN e.[EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN e.[CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN e.[TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsMonitored IS NOT NULL THEN (CASE WHEN e.[IsMonitored] = @IsMonitored THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_EntityType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_EntityType_Delete]
GO

CREATE PROCEDURE [dbo].[p_EntityType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[EntityType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[EntityType] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_EntityType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_EntityType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_EntityType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[EntityType] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_EntityType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_EntityType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_EntityType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[EntityType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[EntityType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_EntityType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_EntityType_Insert]
GO

CREATE PROCEDURE [dbo].[p_EntityType_Insert]
			@ID BIGINT,
			@TypeName NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[EntityType]
	SELECT 
		@TypeName
	
	

	SELECT
		e.*
	FROM
		[dbo].[EntityType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN e.[TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_EntityType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_EntityType_Update]
GO

CREATE PROCEDURE [dbo].[p_EntityType_Update]
			@ID BIGINT,
			@TypeName NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[EntityType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[EntityType]
		SET
									[TypeName] = IIF( @TypeName IS NOT NULL, @TypeName, [TypeName] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'EntityType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[EntityType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TypeName IS NOT NULL THEN (CASE WHEN e.[TypeName] = @TypeName THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_Delete]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[Form4Report]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[Form4Report] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetAll]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[Form4Report] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetByIssuerID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetByIssuerID]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_GetByIssuerID]

	@IssuerID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Form4Report] c 
				WHERE
					[IssuerID] = @IssuerID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Form4Report] e
		WHERE 
			[IssuerID] = @IssuerID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetByReporterID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetByReporterID]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_GetByReporterID]

	@ReporterID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Form4Report] c 
				WHERE
					[ReporterID] = @ReporterID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Form4Report] e
		WHERE 
			[ReporterID] = @ReporterID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetByReportID', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetByReportID]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_GetByReportID]

	@ReportID NVARCHAR(50),
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Form4Report] c 
				WHERE
					[ReportID] = @ReportID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Form4Report] e
		WHERE 
			[ReportID] = @ReportID	

	END
	ELSE
		SET @Found = 0;
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetComplete', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetComplete]
GO

/*
-- Returns complete form 4 report
*/
CREATE PROCEDURE [dbo].[p_Form4Report_GetComplete]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;

	IF(EXISTS(SELECT 1 FROM dbo.Form4Report WHERE ID = @ID))
	BEGIN
		SET @Found = 1

		-- Selecting report details
		SELECT
			r.ID,
			r.Date,
			r.DateSubmitted,
			r.ReportID,
			r.ReporterID,
			reporter.CIK,
			reporter.Name,
			r.IssuerID,
			issuer.CIK,
			issuer.Name,
			issuer.TradingSymbol,
			r.Is10PctHolder,
			r.IsDirector,
			r.IsOfficer,
			r.OfficerTitle,
			r.IsOther,
			r.OtherText

		FROM dbo.Form4Report r
		INNER JOIN dbo.Entity reporter ON r.ReporterID = reporter.ID
		INNER JOIN dbo.Entity issuer ON r.IssuerID = issuer.ID
		WHERE r.ID = @ID

		-- Non-derivatives
		SELECT
			ndt.ID,
			ndt.Form4ReportID,
			ndt.TransactionDate,
			ndt.TitleOfSecurity,
			ndt.Price,
			ndt.SharesAmount,
			ndt.TransactionTypeID,
			tt.Code as TransactionType,
			ndt.TransactionCodeID,
			tc.Code as TransactionCode,
			tc.Description as TransactionDescription,
			ndt.AmountFollowingReport,
			ndt.DeemedExecDate,
			ndt.EarlyVoluntarilyReport,
			ndt.NatureOfIndirectOwnership,
			ndt.OwnershipTypeID,
			ot.Code as OwnershipType			

		FROM dbo.NonDerivativeTransaction ndt
		INNER JOIN	dbo.Form4Report		r	ON r.ID = ndt.Form4ReportID
		INNER JOIN	dbo.TransactionCode tc	ON tc.ID = ndt.TransactionCodeID
		INNER JOIN	dbo.TransactionType tt	ON tt.ID = ndt.TransactionTypeID
		LEFT JOIN	dbo.OwnershipType	ot	ON ot.ID = ndt.OwnershipTypeID
		WHERE r.ID = @ID

		-- Derivatives
		SELECT
			dt.ID,
			dt.Form4ReportID,
			dt.TransactionDate,
			dt.TitleOfDerivative,
			dt.DerivativeSecurityPrice,
			dt.SharesAmount,
			dt.UnderlyingTitle,
			dt.UnderlyingSharesAmount,
			dt.ConversionExercisePrice,
			
			dt.TransactionTypeID,
			tt.Code as TransactionType,
			dt.TransactionCodeID,
			tc.Code as TransactionCode,
			tc.Description as TransactionDescription,
			dt.AmountFollowingReport,
			dt.DateExercisable,
			dt.ExpirationDate,
			dt.EarlyVoluntarilyReport,
			dt.NatureOfIndirectOwnership,
			dt.OwnershipTypeID,
			ot.Code as OwnershipType			

		FROM dbo.DerivativeTransaction dt
		INNER JOIN	dbo.Form4Report		r	ON r.ID = dt.Form4ReportID
		INNER JOIN	dbo.TransactionCode tc	ON tc.ID = dt.TransactionCodeID
		INNER JOIN	dbo.TransactionType tt	ON tt.ID = dt.TransactionTypeID
		LEFT JOIN	dbo.OwnershipType	ot	ON ot.ID = dt.OwnershipTypeID
		WHERE r.ID = @ID

	END
	ELSE
		SET @Found = 0

		
    
	
END
GO






SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Form4Report_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_Form4Report_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_Form4Report_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[Form4Report] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[Form4Report] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


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


	INSERT INTO [dbo].[Form4Report]
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



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRun_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRun_Delete]
GO

CREATE PROCEDURE [dbo].[p_ImportRun_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ImportRun]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[ImportRun] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRun_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRun_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImportRun_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImportRun] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRun_GetByStateID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRun_GetByStateID]
GO

CREATE PROCEDURE [dbo].[p_ImportRun_GetByStateID]

	@StateID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRun] c 
				WHERE
					[StateID] = @StateID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImportRun] e
		WHERE 
			[StateID] = @StateID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRun_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRun_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_ImportRun_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRun] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImportRun] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRun_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRun_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImportRun_Insert]
			@ID BIGINT,
			@TimeStart DATETIME,
			@TimeEnd DATETIME,
			@RequestJson NVARCHAR(1000),
			@StateID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImportRun]
	SELECT 
		@TimeStart,
		@TimeEnd,
		@RequestJson,
		@StateID
	
	

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



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_Delete]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ImportRunForm4Report]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[ImportRunForm4Report] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImportRunForm4Report] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_GetByForm4ReportID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_GetByForm4ReportID]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_GetByForm4ReportID]

	@Form4ReportID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRunForm4Report] c 
				WHERE
					[Form4ReportID] = @Form4ReportID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImportRunForm4Report] e
		WHERE 
			[Form4ReportID] = @Form4ReportID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_GetByImportRunID', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_GetByImportRunID]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_GetByImportRunID]

	@ImportRunID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRunForm4Report] c 
				WHERE
					[ImportRunID] = @ImportRunID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImportRunForm4Report] e
		WHERE 
			[ImportRunID] = @ImportRunID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRunForm4Report] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImportRunForm4Report] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_Insert]
			@ID BIGINT,
			@ImportRunID BIGINT,
			@Form4ReportID BIGINT,
			@TimeStarted DATETIME,
			@TimeCompleted DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImportRunForm4Report]
	SELECT 
		@ImportRunID,
		@Form4ReportID,
		@TimeStarted,
		@TimeCompleted
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImportRunForm4Report] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImportRunID IS NOT NULL THEN (CASE WHEN e.[ImportRunID] = @ImportRunID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN e.[Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TimeStarted IS NOT NULL THEN (CASE WHEN e.[TimeStarted] = @TimeStarted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TimeCompleted IS NOT NULL THEN (CASE WHEN e.[TimeCompleted] = @TimeCompleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunForm4Report_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunForm4Report_Update]
GO

CREATE PROCEDURE [dbo].[p_ImportRunForm4Report_Update]
			@ID BIGINT,
			@ImportRunID BIGINT,
			@Form4ReportID BIGINT,
			@TimeStarted DATETIME,
			@TimeCompleted DATETIME
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRunForm4Report]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ImportRunForm4Report]
		SET
									[ImportRunID] = IIF( @ImportRunID IS NOT NULL, @ImportRunID, [ImportRunID] ) ,
									[Form4ReportID] = IIF( @Form4ReportID IS NOT NULL, @Form4ReportID, [Form4ReportID] ) ,
									[TimeStarted] = IIF( @TimeStarted IS NOT NULL, @TimeStarted, [TimeStarted] ) ,
									[TimeCompleted] = IIF( @TimeCompleted IS NOT NULL, @TimeCompleted, [TimeCompleted] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImportRunForm4Report was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImportRunForm4Report] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ImportRunID IS NOT NULL THEN (CASE WHEN e.[ImportRunID] = @ImportRunID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN e.[Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TimeStarted IS NOT NULL THEN (CASE WHEN e.[TimeStarted] = @TimeStarted THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TimeCompleted IS NOT NULL THEN (CASE WHEN e.[TimeCompleted] = @TimeCompleted THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunState_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunState_Delete]
GO

CREATE PROCEDURE [dbo].[p_ImportRunState_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[ImportRunState]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[ImportRunState] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunState_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunState_GetAll]
GO

CREATE PROCEDURE [dbo].[p_ImportRunState_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[ImportRunState] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunState_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunState_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_ImportRunState_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRunState] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[ImportRunState] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunState_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunState_Insert]
GO

CREATE PROCEDURE [dbo].[p_ImportRunState_Insert]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[ImportRunState]
	SELECT 
		@Name
	
	

	SELECT
		e.*
	FROM
		[dbo].[ImportRunState] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_ImportRunState_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_ImportRunState_Update]
GO

CREATE PROCEDURE [dbo].[p_ImportRunState_Update]
			@ID BIGINT,
			@Name NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[ImportRunState]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[ImportRunState]
		SET
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'ImportRunState was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[ImportRunState] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_Delete]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[NonDerivativeTransaction]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[NonDerivativeTransaction] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_GetAll]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[NonDerivativeTransaction] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_GetByForm4ReportID', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_GetByForm4ReportID]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_GetByForm4ReportID]

	@Form4ReportID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[NonDerivativeTransaction] c 
				WHERE
					[Form4ReportID] = @Form4ReportID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[NonDerivativeTransaction] e
		WHERE 
			[Form4ReportID] = @Form4ReportID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_GetByOwnershipTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_GetByOwnershipTypeID]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_GetByOwnershipTypeID]

	@OwnershipTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[NonDerivativeTransaction] c 
				WHERE
					[OwnershipTypeID] = @OwnershipTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[NonDerivativeTransaction] e
		WHERE 
			[OwnershipTypeID] = @OwnershipTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_GetByTransactionCodeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_GetByTransactionCodeID]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_GetByTransactionCodeID]

	@TransactionCodeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[NonDerivativeTransaction] c 
				WHERE
					[TransactionCodeID] = @TransactionCodeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[NonDerivativeTransaction] e
		WHERE 
			[TransactionCodeID] = @TransactionCodeID	

	END
	ELSE
		SET @Found = 0;
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_GetByTransactionTypeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_GetByTransactionTypeID]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_GetByTransactionTypeID]

	@TransactionTypeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[NonDerivativeTransaction] c 
				WHERE
					[TransactionTypeID] = @TransactionTypeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[NonDerivativeTransaction] e
		WHERE 
			[TransactionTypeID] = @TransactionTypeID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[NonDerivativeTransaction] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[NonDerivativeTransaction] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_Insert]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_Insert]
			@ID BIGINT,
			@Form4ReportID BIGINT,
			@TitleOfSecurity NVARCHAR(250),
			@TransactionDate DATE,
			@DeemedExecDate DATE,
			@TransactionCodeID BIGINT,
			@EarlyVoluntarilyReport BIT,
			@SharesAmount BIGINT,
			@TransactionTypeID BIGINT,
			@Price DECIMAL(20, 6),
			@AmountFollowingReport BIGINT,
			@OwnershipTypeID BIGINT,
			@NatureOfIndirectOwnership NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[NonDerivativeTransaction]
	SELECT 
		@Form4ReportID,
		@TitleOfSecurity,
		@TransactionDate,
		@DeemedExecDate,
		@TransactionCodeID,
		@EarlyVoluntarilyReport,
		@SharesAmount,
		@TransactionTypeID,
		@Price,
		@AmountFollowingReport,
		@OwnershipTypeID,
		@NatureOfIndirectOwnership
	
	

	SELECT
		e.*
	FROM
		[dbo].[NonDerivativeTransaction] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN e.[Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TitleOfSecurity IS NOT NULL THEN (CASE WHEN e.[TitleOfSecurity] = @TitleOfSecurity THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionDate IS NOT NULL THEN (CASE WHEN e.[TransactionDate] = @TransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeemedExecDate IS NOT NULL THEN (CASE WHEN e.[DeemedExecDate] = @DeemedExecDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionCodeID IS NOT NULL THEN (CASE WHEN e.[TransactionCodeID] = @TransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN e.[EarlyVoluntarilyReport] = @EarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SharesAmount IS NOT NULL THEN (CASE WHEN e.[SharesAmount] = @SharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionTypeID IS NOT NULL THEN (CASE WHEN e.[TransactionTypeID] = @TransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Price IS NOT NULL THEN (CASE WHEN e.[Price] = @Price THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AmountFollowingReport IS NOT NULL THEN (CASE WHEN e.[AmountFollowingReport] = @AmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OwnershipTypeID IS NOT NULL THEN (CASE WHEN e.[OwnershipTypeID] = @OwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN e.[NatureOfIndirectOwnership] = @NatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_NonDerivativeTransaction_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_NonDerivativeTransaction_Update]
GO

CREATE PROCEDURE [dbo].[p_NonDerivativeTransaction_Update]
			@ID BIGINT,
			@Form4ReportID BIGINT,
			@TitleOfSecurity NVARCHAR(250),
			@TransactionDate DATE,
			@DeemedExecDate DATE,
			@TransactionCodeID BIGINT,
			@EarlyVoluntarilyReport BIT,
			@SharesAmount BIGINT,
			@TransactionTypeID BIGINT,
			@Price DECIMAL(20, 6),
			@AmountFollowingReport BIGINT,
			@OwnershipTypeID BIGINT,
			@NatureOfIndirectOwnership NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[NonDerivativeTransaction]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[NonDerivativeTransaction]
		SET
									[Form4ReportID] = IIF( @Form4ReportID IS NOT NULL, @Form4ReportID, [Form4ReportID] ) ,
									[TitleOfSecurity] = IIF( @TitleOfSecurity IS NOT NULL, @TitleOfSecurity, [TitleOfSecurity] ) ,
									[TransactionDate] = IIF( @TransactionDate IS NOT NULL, @TransactionDate, [TransactionDate] ) ,
									[DeemedExecDate] = IIF( @DeemedExecDate IS NOT NULL, @DeemedExecDate, [DeemedExecDate] ) ,
									[TransactionCodeID] = IIF( @TransactionCodeID IS NOT NULL, @TransactionCodeID, [TransactionCodeID] ) ,
									[EarlyVoluntarilyReport] = IIF( @EarlyVoluntarilyReport IS NOT NULL, @EarlyVoluntarilyReport, [EarlyVoluntarilyReport] ) ,
									[SharesAmount] = IIF( @SharesAmount IS NOT NULL, @SharesAmount, [SharesAmount] ) ,
									[TransactionTypeID] = IIF( @TransactionTypeID IS NOT NULL, @TransactionTypeID, [TransactionTypeID] ) ,
									[Price] = IIF( @Price IS NOT NULL, @Price, [Price] ) ,
									[AmountFollowingReport] = IIF( @AmountFollowingReport IS NOT NULL, @AmountFollowingReport, [AmountFollowingReport] ) ,
									[OwnershipTypeID] = IIF( @OwnershipTypeID IS NOT NULL, @OwnershipTypeID, [OwnershipTypeID] ) ,
									[NatureOfIndirectOwnership] = IIF( @NatureOfIndirectOwnership IS NOT NULL, @NatureOfIndirectOwnership, [NatureOfIndirectOwnership] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'NonDerivativeTransaction was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[NonDerivativeTransaction] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Form4ReportID IS NOT NULL THEN (CASE WHEN e.[Form4ReportID] = @Form4ReportID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TitleOfSecurity IS NOT NULL THEN (CASE WHEN e.[TitleOfSecurity] = @TitleOfSecurity THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionDate IS NOT NULL THEN (CASE WHEN e.[TransactionDate] = @TransactionDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @DeemedExecDate IS NOT NULL THEN (CASE WHEN e.[DeemedExecDate] = @DeemedExecDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionCodeID IS NOT NULL THEN (CASE WHEN e.[TransactionCodeID] = @TransactionCodeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EarlyVoluntarilyReport IS NOT NULL THEN (CASE WHEN e.[EarlyVoluntarilyReport] = @EarlyVoluntarilyReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @SharesAmount IS NOT NULL THEN (CASE WHEN e.[SharesAmount] = @SharesAmount THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TransactionTypeID IS NOT NULL THEN (CASE WHEN e.[TransactionTypeID] = @TransactionTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Price IS NOT NULL THEN (CASE WHEN e.[Price] = @Price THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @AmountFollowingReport IS NOT NULL THEN (CASE WHEN e.[AmountFollowingReport] = @AmountFollowingReport THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @OwnershipTypeID IS NOT NULL THEN (CASE WHEN e.[OwnershipTypeID] = @OwnershipTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @NatureOfIndirectOwnership IS NOT NULL THEN (CASE WHEN e.[NatureOfIndirectOwnership] = @NatureOfIndirectOwnership THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OwnershipType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_OwnershipType_Delete]
GO

CREATE PROCEDURE [dbo].[p_OwnershipType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[OwnershipType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[OwnershipType] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OwnershipType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_OwnershipType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_OwnershipType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[OwnershipType] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OwnershipType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_OwnershipType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_OwnershipType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[OwnershipType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[OwnershipType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OwnershipType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_OwnershipType_Insert]
GO

CREATE PROCEDURE [dbo].[p_OwnershipType_Insert]
			@ID BIGINT,
			@Code NCHAR(1),
			@Description NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[OwnershipType]
	SELECT 
		@Code,
		@Description
	
	

	SELECT
		e.*
	FROM
		[dbo].[OwnershipType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN e.[Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_OwnershipType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_OwnershipType_Update]
GO

CREATE PROCEDURE [dbo].[p_OwnershipType_Update]
			@ID BIGINT,
			@Code NCHAR(1),
			@Description NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[OwnershipType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[OwnershipType]
		SET
									[Code] = IIF( @Code IS NOT NULL, @Code, [Code] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'OwnershipType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[OwnershipType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN e.[Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO
/****** Object:  StoredProcedure [dbo].[p_Report_NonDerivAcquiredDisposed]    Script Date: 4/25/2024 4:09:45 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Report_NonDerivAcquiredDisposed]
GO
/****** Object:  StoredProcedure [dbo].[p_Report_NonDerivAcquiredDisposed]    Script Date: 4/25/2024 4:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Returns report showing cummulative acquired/disposed for the given company and for specific date range

*/
CREATE PROCEDURE [dbo].[p_Report_NonDerivAcquiredDisposed]
		@IssuerID		BIGINT,
		@StartDate		DATETIME,
		@EndDate		DATETIME
AS
BEGIN

	SELECT
		ndt.Date,
		SUM(IIF( ndt.TransactionTypeID = 1, ndt.SharesAmount, 0 )) As Acquired,
		SUM(IIF( ndt.TransactionTypeID = 2, ndt.SharesAmount, 0 )) As Disposed,
		SUM(IIF( ndt.TransactionTypeID = 1, ndt.SharesAmount, 0 )) - SUM(IIF( ndt.TransactionTypeID = 2, ndt.SharesAmount, 0 )) As Net,
		COUNT(DISTINCT ndt.Form4ReportID) As ReportsCount
	FROM dbo.v_NonDerivativeTransaction ndt
	WHERE 
		ndt.Date BETWEEN @StartDate AND @EndDate AND
		ndt.IssuerID = @IssuerID
	GROUP BY ndt.Date--, ndt.Form4ReportID

END
SET ANSI_NULLS ON
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Report_NonDerivPurchaseSale', 'P') IS NOT NULL
DROP PROC [dbo].[p_Report_NonDerivPurchaseSale]
GO

/*
Returns report showing cummulative purchases/sales for the given company and for specific date range

*/
CREATE PROCEDURE [dbo].[p_Report_NonDerivPurchaseSale]
		@IssuerID		BIGINT,
		@StartDate		DATETIME,
		@EndDate		DATETIME
AS
BEGIN

	SELECT
		ndt.Date,
		SUM(IIF( ndt.TransactionCodeID = 1, ndt.SharesAmount, 0 )) As Purchased,
		SUM(IIF( ndt.TransactionCodeID = 2, ndt.SharesAmount, 0 )) As Sold,
		SUM(IIF( ndt.TransactionCodeID = 1, ndt.SharesAmount, 0 )) - SUM(IIF( ndt.TransactionCodeID = 2, ndt.SharesAmount, 0 )) As Net,
		COUNT(DISTINCT ndt.Form4ReportID) As ReportsCount
	FROM dbo.v_NonDerivativeTransaction ndt
	WHERE 
		ndt.Date BETWEEN @StartDate AND @EndDate AND
		ndt.IssuerID = @IssuerID
	GROUP BY ndt.Date

END
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[p_Report_NonDerivTransactionsByDate]    Script Date: 4/25/2024 4:09:45 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Report_NonDerivTransactionsByDate]
GO
/****** Object:  StoredProcedure [dbo].[p_Report_NonDerivTransactionsByDate]    Script Date: 4/25/2024 4:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Returns list of all non-derivative transactions for given date for given company

*/
CREATE PROCEDURE [dbo].[p_Report_NonDerivTransactionsByDate]
		@IssuerID	BIGINT,
		@Date		DATETIME
AS
BEGIN

	SELECT
		ndo.ID,
		ndo.Form4ReportID,
		ndo.SECReportID,
		ndo.TitleOfSecurity,
		ndo.TransactionDate,
		ndo.DeemedExecDate,
		ndo.TransactionCodeID,
		ndo.TrasnactionCode,
		ndo.EarlyVoluntarilyReport,
		ndo.SharesAmount,
		ndo.TransactionTypeID,
		ndo.TransactionType,
		ndo.Price,
		ndo.AmountFollowingReport,
		ndo.OwnershipTypeID,
		ndo.OwnershipType,
		ndo.NatureOfIndirectOwnership
	FROM		
		dbo.v_NonDerivativeTransaction ndo
	
	WHERE ndo.IssuerID = @IssuerID AND ndo.Date = @Date

	
	SET NOCOUNT ON;
END



SET ANSI_NULLS ON
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionCode_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionCode_Delete]
GO

CREATE PROCEDURE [dbo].[p_TransactionCode_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[TransactionCode]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[TransactionCode] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionCode_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionCode_GetAll]
GO

CREATE PROCEDURE [dbo].[p_TransactionCode_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[TransactionCode] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionCode_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionCode_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_TransactionCode_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[TransactionCode] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[TransactionCode] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionCode_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionCode_Insert]
GO

CREATE PROCEDURE [dbo].[p_TransactionCode_Insert]
			@ID BIGINT,
			@Code NVARCHAR(10),
			@Description NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[TransactionCode]
	SELECT 
		@Code,
		@Description
	
	

	SELECT
		e.*
	FROM
		[dbo].[TransactionCode] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN e.[Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionCode_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionCode_Update]
GO

CREATE PROCEDURE [dbo].[p_TransactionCode_Update]
			@ID BIGINT,
			@Code NVARCHAR(10),
			@Description NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[TransactionCode]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[TransactionCode]
		SET
									[Code] = IIF( @Code IS NOT NULL, @Code, [Code] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'TransactionCode was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[TransactionCode] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN e.[Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionType_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionType_Delete]
GO

CREATE PROCEDURE [dbo].[p_TransactionType_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[TransactionType]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[TransactionType] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionType_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionType_GetAll]
GO

CREATE PROCEDURE [dbo].[p_TransactionType_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[TransactionType] e
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionType_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionType_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_TransactionType_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[TransactionType] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[TransactionType] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionType_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionType_Insert]
GO

CREATE PROCEDURE [dbo].[p_TransactionType_Insert]
			@ID BIGINT,
			@Code NVARCHAR(10),
			@Description NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[TransactionType]
	SELECT 
		@Code,
		@Description
	
	

	SELECT
		e.*
	FROM
		[dbo].[TransactionType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN e.[Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_TransactionType_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_TransactionType_Update]
GO

CREATE PROCEDURE [dbo].[p_TransactionType_Update]
			@ID BIGINT,
			@Code NVARCHAR(10),
			@Description NVARCHAR(250)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[TransactionType]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[TransactionType]
		SET
									[Code] = IIF( @Code IS NOT NULL, @Code, [Code] ) ,
									[Description] = IIF( @Description IS NOT NULL, @Description, [Description] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'TransactionType was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[TransactionType] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Code IS NOT NULL THEN (CASE WHEN e.[Code] = @Code THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Description IS NOT NULL THEN (CASE WHEN e.[Description] = @Description THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_Delete', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_Delete]
GO

CREATE PROCEDURE [dbo].[p_User_Delete]
		@ID BIGINT,	
		@Removed BIT OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON;
    
	IF( EXISTS( SELECT 1 FROM [dbo].[User]  
				WHERE 
							[ID] = @ID	
				) )
	BEGIN
			DELETE 
		FROM 
			[dbo].[User] 
			WHERE 
						[ID] = @ID	
			
		SET @Removed = 1
	END
	ELSE
		SET @Removed = 0
END
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetAll', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetAll]
GO

CREATE PROCEDURE [dbo].[p_User_GetAll]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		e.*
	FROM
		[dbo].[User] e
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetByModifiedByID', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetByModifiedByID]
GO

CREATE PROCEDURE [dbo].[p_User_GetByModifiedByID]

	@ModifiedByID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[User] c 
				WHERE
					[ModifiedByID] = @ModifiedByID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[User] e
		WHERE 
			[ModifiedByID] = @ModifiedByID	

	END
	ELSE
		SET @Found = 0;
END
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_GetDetails', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_GetDetails]
GO

CREATE PROCEDURE [dbo].[p_User_GetDetails]
		@ID BIGINT,	
		@Found BIT OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[User] c 
				WHERE 
								[ID] = @ID	
				)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[User] e
		WHERE 
								[ID] = @ID	
				END
	ELSE
		SET @Found = 0;
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_Insert]
GO

CREATE PROCEDURE [dbo].[p_User_Insert]
			@ID BIGINT,
			@Login NVARCHAR(250),
			@PwdHash NVARCHAR(250),
			@Salt NVARCHAR(50),
			@FirstName NVARCHAR(50),
			@MiddleName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@FriendlyName NVARCHAR(50),
			@CreatedDate DATETIME,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[User]
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
	
	

	SELECT
		e.*
	FROM
		[dbo].[User] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN e.[Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN e.[PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN e.[Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN e.[MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FriendlyName IS NOT NULL THEN (CASE WHEN e.[FriendlyName] = @FriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_User_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_User_Update]
GO

CREATE PROCEDURE [dbo].[p_User_Update]
			@ID BIGINT,
			@Login NVARCHAR(250),
			@PwdHash NVARCHAR(250),
			@Salt NVARCHAR(50),
			@FirstName NVARCHAR(50),
			@MiddleName NVARCHAR(50),
			@LastName NVARCHAR(50),
			@FriendlyName NVARCHAR(50),
			@CreatedDate DATETIME,
			@ModifiedDate DATETIME,
			@ModifiedByID BIGINT
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[User]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[User]
		SET
									[Login] = IIF( @Login IS NOT NULL, @Login, [Login] ) ,
									[PwdHash] = IIF( @PwdHash IS NOT NULL, @PwdHash, [PwdHash] ) ,
									[Salt] = IIF( @Salt IS NOT NULL, @Salt, [Salt] ) ,
									[FirstName] = IIF( @FirstName IS NOT NULL, @FirstName, [FirstName] ) ,
									[MiddleName] = IIF( @MiddleName IS NOT NULL, @MiddleName, [MiddleName] ) ,
									[LastName] = IIF( @LastName IS NOT NULL, @LastName, [LastName] ) ,
									[FriendlyName] = IIF( @FriendlyName IS NOT NULL, @FriendlyName, [FriendlyName] ) ,
									[CreatedDate] = IIF( @CreatedDate IS NOT NULL, @CreatedDate, [CreatedDate] ) ,
									[ModifiedDate] = IIF( @ModifiedDate IS NOT NULL, @ModifiedDate, [ModifiedDate] ) ,
									[ModifiedByID] = IIF( @ModifiedByID IS NOT NULL, @ModifiedByID, [ModifiedByID] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'User was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[User] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Login IS NOT NULL THEN (CASE WHEN e.[Login] = @Login THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @PwdHash IS NOT NULL THEN (CASE WHEN e.[PwdHash] = @PwdHash THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Salt IS NOT NULL THEN (CASE WHEN e.[Salt] = @Salt THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FirstName IS NOT NULL THEN (CASE WHEN e.[FirstName] = @FirstName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @MiddleName IS NOT NULL THEN (CASE WHEN e.[MiddleName] = @MiddleName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @LastName IS NOT NULL THEN (CASE WHEN e.[LastName] = @LastName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @FriendlyName IS NOT NULL THEN (CASE WHEN e.[FriendlyName] = @FriendlyName THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CreatedDate IS NOT NULL THEN (CASE WHEN e.[CreatedDate] = @CreatedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedDate IS NOT NULL THEN (CASE WHEN e.[ModifiedDate] = @ModifiedDate THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @ModifiedByID IS NOT NULL THEN (CASE WHEN e.[ModifiedByID] = @ModifiedByID THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO