@echo off
setlocal enabledelayedexpansion

rem 获取当前批处理脚本所在目录
set "script_dir=%~dp0"

rem 进入批处理脚本所在目录
cd "%script_dir%"

rem 循环处理所有.json文件
for %%f in (*.json) do (
    otp encrypt "%%~f"
)

echo 所有.json文件已加密。

endlocal
