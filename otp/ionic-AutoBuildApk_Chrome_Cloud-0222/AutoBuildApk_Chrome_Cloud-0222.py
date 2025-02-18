import datetime
import sys
import time
import os
from selenium import webdriver
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By
import glob
import chromedriver_autoinstaller

# -*- coding:utf-8 -*-


class RunAutoBuildMain:

    def __init__(self):
        pass

    # 寫Log file
    def _outputlog(self, log):
        path = "/src/log"
        if not os.path.isdir(path):
            os.mkdir(path)

        s = datetime.datetime.now()
        times = s.strftime("%y-%m-%d %H_%M")
        filepath = "/src/log/log-{time}.txt".format(time=times)
        with open(filepath, mode="a", encoding="utf-8") as f:
            for i in log:
                f.writelines(i+"\n")

    def _browser_cfg(self, driver="chrome"):
        driverPath = chromedriver_autoinstaller.install()
        options = webdriver.ChromeOptions()
        options.add_argument("--no-sandbox")  # Linux 必須加
        options.add_argument("--disable-infobars")
        options.add_argument("--disable-dev-shm-usage")  # Linux 必須加
        options.add_argument("--headless")  # 啟動Headless 無頭
        options.add_argument('--disable-gpu')  # 關閉GPU 避免某些系統或是網頁出錯
        # 打啟動selenium 務必確認driver 檔案跟python 檔案要在同個資料夾中
        driver = webdriver.Chrome(options=options, executable_path=driverPath)
        params = {'behavior': 'allow', 'downloadPath': os.getcwd()}
        driver.execute_cdp_cmd('Page.setDownloadBehavior', params)
        return driver


if __name__ == "__main__":

    try:
        # 清除舊的apk檔案
        apks = os.path.join("/src/apk/", "*.apk")
        for i in glob.glob(apks):
            os.remove(i)

        log = []
        ionic_url = "https://ionicframework.com/login"
        ionic_username = "it@otp.com.tw"
        ionic_password = "Password!23"

        run = RunAutoBuildMain()
        log.append("Android dev build Apk start ")
        driver = run._browser_cfg()
        driver.get(ionic_url)
        log.append("connect Ionic website")
        WebDriverWait(driver, 10).until(
            EC.element_to_be_clickable((By.ID, "bitbucket"))).click()

        # 輸入email
        context = driver.find_element_by_id("username")
        context.send_keys(ionic_username)
        btn_continue = driver.find_element_by_id("login-submit")
        btn_continue.click()
        time.sleep(2.5)

        # 輸入password
        WebDriverWait(driver, 10).until(
            EC.element_to_be_clickable((By.NAME, "password"))).click()
        txt_password = driver.find_element_by_name("password")
        txt_password.send_keys(ionic_password)

        # Login
        WebDriverWait(driver, 10).until(
            EC.element_to_be_clickable((By.ID, "login-submit"))).click()
        log.append("Login Ionic")

        WebDriverWait(driver, 50, 1).until(EC.element_to_be_clickable(
            (By.XPATH, "//body/div[@id='root']/div[1]/div[2]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/ul[1]/li[1]/div[1]"))).click()
        log.append("Click tta app project")

        WebDriverWait(driver, 20, 1).until(EC.element_to_be_clickable(
            (By.XPATH, "//body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/ul[1]/li[5]/div[1]/a[1]/div[1]"))).click()
        log.append("click Build option")

        WebDriverWait(driver, 20, 1).until(EC.element_to_be_clickable(
            (By.XPATH, "//button[contains(text(),'＋ New build')]"))).click()
        log.append("click +New Build option")

        WebDriverWait(driver, 10).until(EC.element_to_be_clickable(
            (By.XPATH, "//body/div[@id='root']/div[1]/div[2]/div[2]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/ul[1]/li[1]"))).click()
        log.append("select Commit")

        WebDriverWait(driver, 10).until(EC.element_to_be_clickable(
            (By.XPATH, "//body/div[@id='root']/div[1]/div[2]/div[2]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/form[1]/div[2]/div[2]/div[1]/div[1]/div[3]"))).click()
        log.append("select Android environment")

        WebDriverWait(driver, 10).until(EC.element_to_be_clickable(
            (By.XPATH, "//body/div[@id='root']/div[1]/div[2]/div[2]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/form[1]/div[3]/div[2]/div[1]/div[1]"))).click()
        WebDriverWait(driver, 10).until(EC.element_to_be_clickable(
            (By.XPATH, "//span[contains(text(),'Linux - 2021.09')]"))).click()

        WebDriverWait(driver, 20).until(EC.element_to_be_clickable(
            (By.XPATH, "//button[contains(text(),'Build')]"))).click()
        starttime = datetime.datetime.now()  # 開始計算時間
        log.append("start build Android APK  ")

        BuildStatus = WebDriverWait(driver, 20).until(
            EC.element_to_be_clickable((By.XPATH, "//header/div[2]/div[2]/div[1]/div[1]")))
        time.sleep(8)

        # Build 狀態
        spendtime = ""
        if BuildStatus.text != "Success":
            while True:

                if BuildStatus.text == "Success":
                    log.append("Build Apk Success")
                    break
                if BuildStatus.text == "Failed":
                    log.append("Build Apk Filed")
                    run._outputlog(log)
                    print("Failed")
                    sys.exit()

        endtime = datetime.datetime.now()

        # 下載APK
        WebDriverWait(driver, 20).until(EC.element_to_be_clickable(
            (By.XPATH, "//body/div[@id='root']/div[1]/div[2]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[4]/div[2]/div[1]"))).click()
        log.append("Downloading Android APK")
        time.sleep(10)
        # 搜尋APK檔名
        apkFileName = WebDriverWait(driver, 20).until(EC.element_to_be_clickable(
            (By.XPATH, "//body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[4]/div[2]/div[1]/div[1]"))).text
        old_file = os.path.join(os.getcwd(), apkFileName.replace("/", "_"))
        log.append("Download completed.")

        # Rename file
        if os.path.isfile(old_file):
            new_file = os.path.join(os.getcwd(), "tta.apk")
            os.replace(old_file, new_file)
            os.system("mv %s /src/apk/" % new_file)
            log.append("Android build end")
        run._outputlog(log)
        print("Success")
    except Exception as ex:
        log.append("except error: {}, Failed".format(ex))
        run._outputlog(log)
        print("Failed")
    finally:
        driver.quit()
        sys.exit()
