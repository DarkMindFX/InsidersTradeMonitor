del CreateAllFunctions.sql
for /r ".\functions" %%F in (*.sql) do @type "%%F" >> CreateAllFunctions.sql
