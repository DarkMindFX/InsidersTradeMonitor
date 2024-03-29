USE [InsidersTradeMonitor]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetEntityIDByTradingSymbol]    Script Date: 11/22/2022 10:33:18 AM ******/
DROP FUNCTION [dbo].[fn_GetEntityIDByTradingSymbol]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetEntityIDByTradingSymbol]    Script Date: 11/22/2022 10:33:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fn_GetEntityIDByTradingSymbol] 
(
	@TradingSymbol NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = ID FROM dbo.Entity WHERE TradingSymbol = @TradingSymbol

	-- Return the result of the function
	RETURN @Result

END
GO
