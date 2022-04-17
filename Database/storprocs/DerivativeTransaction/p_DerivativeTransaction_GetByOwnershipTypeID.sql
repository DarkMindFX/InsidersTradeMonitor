





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