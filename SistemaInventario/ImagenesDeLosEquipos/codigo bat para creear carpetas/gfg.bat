@echo off 
setlocal enableDelayedExpansion 
FOR /l %%N in (1000,1,7000) do (
    set "NUM=0%%N"
    set "DIRNAME=!NUM:~-4!"
    md !DIRNAME! 
)
