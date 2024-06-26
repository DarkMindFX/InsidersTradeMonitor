
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
