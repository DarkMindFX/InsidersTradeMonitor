
/****** Object:  UserDefinedFunction [dbo].[fn_GetEntityIDByCIK]    Script Date: 4/25/2024 3:36:47 PM ******/
DROP FUNCTION IF EXISTS [dbo].[fn_GetEntityIDByCIK]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetEntityIDByCIK]    Script Date: 4/25/2024 3:36:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetEntityIDByCIK]
(
	@CIK NVARCHAR(50)
)
RETURNS BIGINT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result BIGINT

	-- Add the T-SQL statements to compute the return value here
	SELECT @Result = ID FROM dbo.Entity WHERE CIK = @CIK

	-- Return the result of the function
	RETURN @Result

END
GO
