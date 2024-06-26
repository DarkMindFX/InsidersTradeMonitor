
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
