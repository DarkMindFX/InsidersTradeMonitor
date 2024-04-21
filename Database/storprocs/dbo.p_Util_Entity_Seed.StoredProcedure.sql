
USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_Util_EntityType_Populate]    Script Date: 6/20/2022 1:33:41 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

IF OBJECT_ID('p_Util_Entity_Seed', 'P') IS NOT NULL
DROP PROC [dbo].[p_Util_Entity_Seed]
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