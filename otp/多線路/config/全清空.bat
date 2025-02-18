@echo off
setlocal enabledelayedexpansion

rem 獲取當前腳本的目錄路徑
for %%i in (%0) do set "current_dir=%%~dpi"

rem 清空當前目錄中的所有 .txt 文件
del "!current_dir!*.txt" /q

rem 清空當前目錄中的所有 .json 文件
del "!current_dir!*.json" /q

echo .txt 和 .json 文件已清空: !current_dir!

