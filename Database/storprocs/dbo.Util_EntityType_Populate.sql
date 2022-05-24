
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Util_EntityType_Populate]

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
