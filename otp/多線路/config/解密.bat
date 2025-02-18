@echo off
setlocal enabledelayedexpansion

rem 获取当前批处理脚本所在目录
set "script_dir=%~dp0"

rem 进入批处理脚本所在目录
cd "%script_dir%"

rem 循环处理所有.txt文件
for %%f in (*.txt) do (
    otp decrypt "%%~f"
)

echo 所有.txt文件已解密。

endlocal
