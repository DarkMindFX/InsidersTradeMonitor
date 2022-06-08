


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Entity_Insert', 'P') IS NOT NULL
DROP PROC [dbo].[p_Entity_Insert]
GO

CREATE PROCEDURE [dbo].[p_Entity_Insert]
			@ID BIGINT,
			@EntityTypeID BIGINT,
			@CIK INT,
			@Name NVARCHAR(250),
			@TradingSymbol NVARCHAR(50),
			@IsMonitored BIT
	AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [dbo].[Entity]
	SELECT 
		@EntityTypeID,
		@CIK,
		@Name,
		@TradingSymbol,
		@IsMonitored
	
	

	SELECT
		e.*
	FROM
		[dbo].[Entity] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN e.[EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN e.[CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN e.[TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @IsMonitored IS NOT NULL THEN (CASE WHEN e.[IsMonitored] = @IsMonitored THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO