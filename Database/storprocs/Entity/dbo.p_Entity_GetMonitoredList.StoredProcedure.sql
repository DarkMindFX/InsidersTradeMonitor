
/****** Object:  StoredProcedure [dbo].[p_Entity_GetMonitoredList]    Script Date: 4/25/2024 4:05:53 PM ******/
DROP PROCEDURE IF EXISTS [dbo].[p_Entity_GetMonitoredList]
GO
/****** Object:  StoredProcedure [dbo].[p_Entity_GetMonitoredList]    Script Date: 4/25/2024 4:05:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
Usage:
EXEC dbo.p_TestData_CleanUp
*/
CREATE PROCEDURE [dbo].[p_Entity_GetMonitoredList] 
	
AS
BEGIN
	SELECT
		e.*
	FROM
		dbo.Entity e
	WHERE e.IsMonitored = 1
END
GO
