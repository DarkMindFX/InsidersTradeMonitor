





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