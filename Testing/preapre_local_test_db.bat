@ECHO OFF

SET SQLCMD="c:\Program Files\Microsoft SQL Server\Client SDK\ODBC\170\Tools\Binn\SQLCMD.EXE"

SET SERVER="127.0.0.1,1433"

SET MASTER_DB="master"

SET TEST_DB="InsidersTradeMonitor_Tests"

SET LOGIN="itm_test_account"

SET PASSWORD="ITMTestAccountService2022!"

SET SQLS_PATH="d:\Projects\InsidersTradeMonitor\Database\"

SET TESTINGROOT_PATH="d:\Projects\InsidersTradeMonitor\Testing\"

SET TESTDATA_PATH="d:\Projects\InsidersTradeMonitor\Testing\TestData\"

cd %TESTINGROOT_PATH%

%SQLCMD% -S %SERVER% -d %MASTER_DB% -U %LOGIN% -P %PASSWORD% -i "DropTestDB.sql"

%SQLCMD% -S %SERVER% -d %MASTER_DB% -U %LOGIN% -P %PASSWORD% -i "CreateTestDB.sql"

CD %SQLS_PATH%

call tables.concat.bat

call views.concat.bat

call storprocs.concat.bat

call functions.concat.bat

%SQLCMD% -S %SERVER% -d %TEST_DB% -U %LOGIN% -P %PASSWORD% -i "CreateAllTables.sql"

%SQLCMD% -S %SERVER% -d %TEST_DB% -U %LOGIN% -P %PASSWORD% -i "CreateAllViews.sql"

%SQLCMD% -S %SERVER% -d %TEST_DB% -U %LOGIN% -P %PASSWORD% -i "CreateAllFunctions.sql"

%SQLCMD% -S %SERVER% -d %TEST_DB% -U %LOGIN% -P %PASSWORD% -i "CreateAllStorProcs.sql"

cd %TESTINGROOT_PATH%