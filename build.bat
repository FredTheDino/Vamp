@echo off
SET _FLAGS=-debug -nologo -r:FNA.dll -appconfig:FNA.dll.config
SET _SRC=-recurse:..\src\*.cs
SET _NAME=vamp.exe

REM For 32 bit, change the line below to end with x32
SET _LIB=../lib/Winx64

SET _PATH=""
SET _RUN=""

FOR %%A IN (%*) DO (
    IF "%%A"=="/r" ( 
		SET _RUN=YES 
	)
    IF "%%A"=="/p" ( 
		SET _PATH=YES 
	)
)

if %_PATH% == YES (
	call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\vsdevcmd"
)

if not exist bin mkdir bin

PUSHD bin
PUSHD "%_LIB%"
COPY /Y * "../../bin" >nul 2>nul
POPD
ECHO  ====== Compile ====== 
CSC -out:"%_NAME%" %_FLAGS% -lib:"%_LIB%" "%_SRC%"

if %_RUN% == YES (
ECHO  ====== Running ======
	%_NAME%
)

POPD

