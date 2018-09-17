#!/usr/bin/bash

cd bin
cp -sf ../lib/Linux64/*.* .
echo " ===== Compile ===== "
csc -out:vamp.mono -debug -nologo -r:FNA.dll -recurse:../src/*.cs

if [[ $1 == "run" ]] 
then
	echo " ===== Running ===== "
	mono vamp.mono
fi

cd ..
