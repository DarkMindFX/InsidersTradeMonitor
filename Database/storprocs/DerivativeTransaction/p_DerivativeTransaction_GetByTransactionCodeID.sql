





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_DerivativeTransaction_GetByTransactionCodeID', 'P') IS NOT NULL
DROP PROC [dbo].[p_DerivativeTransaction_GetByTransactionCodeID]
GO

CREATE PROCEDURE [dbo].[p_DerivativeTransaction_GetByTransactionCodeID]

	@TransactionCodeID BIGINT,
	@Found BIT OUTPUT

AS
BEGIN

	SET NOCOUNT ON;

	IF(EXISTS(	SELECT 1 FROM [dbo].[DerivativeTransaction] c 
				WHERE
					[TransactionCodeID] = @TransactionCodeID
	)) 
	BEGIN
		SET @Found = 1; -- notifying that record was found
		
		SELECT
			e.*
		FROM
		[dbo].[DerivativeTransaction] e
		WHERE 
			[TransactionCodeID] = @TransactionCodeID	

	END
	ELSE
		SET @Found = 0;
END
GO