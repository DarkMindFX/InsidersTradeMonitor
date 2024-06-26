
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
