@echo off
setlocal enabledelayedexpansion

rem 要下載的文件URL列表
set "file_urls=https://otp-1.otp888.com/serverListConfig.txt"

rem 獲取當前腳本的目錄路徑
for %%i in (%0) do set "current_dir=%%~dpi"

rem 遍歷文件URL列表並下載文件
for %%u in (%file_urls%) do (
    set "file_url=%%u"
    
    rem 提取文件名部分
    for /f %%f in ("!file_url!") do set "file_name=%%~nxf"

    rem 使用curl或wget下載文件，您可以選擇其中一個工具
    rem 使用curl下載
    curl -o "!current_dir!!file_name!" "!file_url!"
    
    rem 使用wget下載（如果您有wget）
    rem wget -O "!current_dir!!file_name!" "!file_url!"
)

echo 文件已下載到: !current_dir!

