@echo off
setlocal enabledelayedexpansion

rem �����e�}�����ؿ����|
for %%i in (%0) do set "current_dir=%%~dpi"

rem �M�ŷ�e�ؿ������Ҧ� .txt ���
del "!current_dir!*.txt" /q

rem �M�ŷ�e�ؿ������Ҧ� .json ���
del "!current_dir!*.json" /q

echo .txt �M .json ���w�M��: !current_dir!

