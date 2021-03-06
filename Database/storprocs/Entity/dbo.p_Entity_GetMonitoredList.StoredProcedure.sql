USE [InsidersTradeMonitor]
GO
/****** Object:  StoredProcedure [dbo].[p_Entity_GetMonitoredList]    Script Date: 6/6/2022 10:06:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('p_Entity_GetMonitoredList', 'P') IS NOT NULL
DROP PROC [dbo].[p_TestData_CleanUp]
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
