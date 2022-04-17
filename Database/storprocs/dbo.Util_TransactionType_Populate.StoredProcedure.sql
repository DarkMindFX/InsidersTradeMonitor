USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[Util_TransactionType_Populate]    Script Date: 4/17/2022 1:30:31 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[Util_TransactionType_Populate]
GO
/****** Object:  StoredProcedure [dbo].[Util_TransactionType_Populate]    Script Date: 4/17/2022 1:30:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Util_TransactionType_Populate]

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
