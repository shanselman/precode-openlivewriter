@echo off
cls

set target=all
if not "%1"=="" set target=%1

tools\NAnt\NAnt.exe -buildfile:default.build %target% ^
-D:build.msbuild.configuration=Release
pause


