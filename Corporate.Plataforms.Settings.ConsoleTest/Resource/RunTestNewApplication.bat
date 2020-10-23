SET "NUMEXECUTE=2"
SET "BASEPATH=C:\Teste\BaseTeste"
SET "EXECUTEPATH=C:\Teste\NewApplication\ExecuteTest"
SET "APLICATION_NAME=NewApplication"


for /l %%x in (1, 1, %numexecute%) do (
  Xcopy /E /I %basepath% %executepath%\%%x
)


for /l %%x in (1, 1, %numexecute%) do (
    cd %executepath%\%%x
    start ConsoleApp1.exe %APLICATION_NAME% %APLICATION_NAME%%%x
	timeout 2
)


pause
