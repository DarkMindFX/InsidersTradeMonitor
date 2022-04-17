





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