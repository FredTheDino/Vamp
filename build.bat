@echo off
SET _FLAGS=-debug -nologo -r:FNA.dll -appconfig:FNA.dll.config
SET _SRC=-recurse:..\src\*.cs
SET _NAME=vamp.exe

REM For 32 bit, change the line below to end with x32
SET _LIB=../lib/Winx64

PUSHD bin
PUSHD "%_LIB%"
COPY /Y * "../../bin" >nul 2>nul
POPD
ECHO  ====== Compile ====== 
CSC -out:"%_NAME%" %_FLAGS% -lib:"%_LIB%" "%_SRC%"

if "%1" == "run" (
ECHO  ====== Running ======
	%_NAME%
)

POPD

