


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Entity_Update', 'P') IS NOT NULL
DROP PROC [dbo].[p_Entity_Update]
GO

CREATE PROCEDURE [dbo].[p_Entity_Update]
			@ID BIGINT,
			@EntityTypeID BIGINT,
			@CIK NVARCHAR(250),
			@Name NVARCHAR(250),
			@TradingSymbol NVARCHAR(50)
	AS
BEGIN

	SET NOCOUNT ON;


		
	IF(EXISTS(	SELECT 1 FROM [dbo].[Entity]
					WHERE 
												[ID] = @ID	
							))
	BEGIN
		UPDATE [dbo].[Entity]
		SET
									[ID] = IIF( @ID IS NOT NULL, @ID, [ID] ) ,
									[EntityTypeID] = IIF( @EntityTypeID IS NOT NULL, @EntityTypeID, [EntityTypeID] ) ,
									[CIK] = IIF( @CIK IS NOT NULL, @CIK, [CIK] ) ,
									[Name] = IIF( @Name IS NOT NULL, @Name, [Name] ) ,
									[TradingSymbol] = IIF( @TradingSymbol IS NOT NULL, @TradingSymbol, [TradingSymbol] ) 
						WHERE 
												[ID] = @ID	
					
 		
			
	END
	ELSE
	BEGIN
		THROW 51001, 'Entity was not found', 1;
	END	

	SELECT
		e.*
	FROM
		[dbo].[Entity] e
	WHERE
				(CASE WHEN @ID IS NOT NULL THEN (CASE WHEN e.[ID] = @ID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @EntityTypeID IS NOT NULL THEN (CASE WHEN e.[EntityTypeID] = @EntityTypeID THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @CIK IS NOT NULL THEN (CASE WHEN e.[CIK] = @CIK THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @Name IS NOT NULL THEN (CASE WHEN e.[Name] = @Name THEN 1 ELSE 0 END) ELSE 1 END) = 1 AND
				(CASE WHEN @TradingSymbol IS NOT NULL THEN (CASE WHEN e.[TradingSymbol] = @TradingSymbol THEN 1 ELSE 0 END) ELSE 1 END) = 1 
		END
GO