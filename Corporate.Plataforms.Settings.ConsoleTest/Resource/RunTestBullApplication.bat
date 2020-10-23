SET "NUMEXECUTE=3"
SET "BASEPATH=C:\Teste\BaseTeste"
SET "EXECUTEPATH=C:\Teste\BullApplication\ExecuteTest"
SET "APLICATION_NAME=BullApplication"


for /l %%x in (1, 1, %numexecute%) do (
  Xcopy /E /I %basepath% %executepath%\%%x
)


for /l %%x in (1, 1, %numexecute%) do (
    cd %executepath%\%%x
    start ConsoleApp1.exe %APLICATION_NAME% %APLICATION_NAME%%%x
	timeout 2
)


pause
