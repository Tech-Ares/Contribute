@echo off
setlocal enabledelayedexpansion

rem �n�U�������URL�C��
set "file_urls=https://otp-1.otp888.com/serverListConfig.txt"

rem �����e�}�����ؿ����|
for %%i in (%0) do set "current_dir=%%~dpi"

rem �M�����URL�C��äU�����
for %%u in (%file_urls%) do (
    set "file_url=%%u"
    
    rem �������W����
    for /f %%f in ("!file_url!") do set "file_name=%%~nxf"

    rem �ϥ�curl��wget�U�����A�z�i�H��ܨ䤤�@�Ӥu��
    rem �ϥ�curl�U��
    curl -o "!current_dir!!file_name!" "!file_url!"
    
    rem �ϥ�wget�U���]�p�G�z��wget�^
    rem wget -O "!current_dir!!file_name!" "!file_url!"
)

echo ���w�U����: !current_dir!

