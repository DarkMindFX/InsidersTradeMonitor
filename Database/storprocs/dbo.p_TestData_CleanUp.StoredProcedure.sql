
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
