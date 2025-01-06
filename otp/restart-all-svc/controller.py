# -*- conding:utf-8 -*-
from logging import log
from PyQt5 import QtWidgets, QtGui, QtCore
from jenkins_app import RunAutoBuildMain
from Ui_restart_quote import Ui_MainWindow
import datetime
import sys, os
from tgbot import tgbot_msg
from jenkins import JenkinsException

class MainWindow_controller(QtWidgets.QMainWindow):
    log = []
    def __init__(self):
        super().__init__() # in python3, super(Class, self).xxx = super().xxx
        self.ui = Ui_MainWindow()
        self.ui.setupUi(self)
        self.setup_control()
        
        
        

    #寫Log file    
    def _outputlog(self,log):
        path = "./logs"
        if not os.path.isdir(path):
            os.mkdir(path)

        s = datetime.datetime.now()
        times = s.strftime("%y-%m-%d")
        filepath = "./logs/log-{time}.txt".format(time=times)
        msg = log+list(s.strftime("%y-%m-%d %H:%M:%S"))
        with open(filepath,mode="w",encoding="utf-8") as f:
            f.writelines(msg[0]+"\n")
            
                


    def setup_control(self):
        # TODO 
        self.ui.txt_password.setEchoMode(QtWidgets.QLineEdit.Password)
        self.ui.env.setCurrentIndex(0)
        self.ui.submit.clicked.connect(self.buttonClicked)

    
    def _warning(self, warningstr):
        warning = QtWidgets.QMessageBox()
        warning.setWindowTitle('Warning')
        warning.setIcon(QtWidgets.QMessageBox.Warning)
        warning.setText(warningstr)
        warning.setStandardButtons(QtWidgets.QMessageBox.Ok)
        return warning.exec_()
    
    
                
        
    def buttonClicked(self):
        
        try:
            
 
            username = self.ui.txt_username.text()
            pwd = self.ui.txt_password.text()
            env = self.ui.env.currentText()
            
            results = ""   
            jenkins_url = "http://60.251.26.121:18080/"           
            run = RunAutoBuildMain()
            if env =="OTP-GW" : 
                jenkins_job = r"Restart-OTP-GateWay" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "JGX-GW" :
                jenkins_job = r"Restart-JGX-GateWay" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "TS-GW":
                jenkins_job = r"Restart-TS-GateWay" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "JGX-Risk":
                jenkins_job = r"Restart-JGX-Tomcat-Admin" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "TS-Risk":
                jenkins_job = r"Restart-TS-Tomcat-Admin" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "JGX-Relay":
                jenkins_job = r"Restart-JGX-Relay" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "TS-Relay":
                jenkins_job = r"Restart-TS-Relay" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "OTP-Relay":
                jenkins_job = r"Restart-OTP-Relay" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "PB-Adapter":
                jenkins_job = r"Restart-Ali-PB-Adapter" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "OTP-Quote-All":
                jenkins_job = r"Restart-OTP-Quote-All" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "JGX-Quote-All":
                jenkins_job = r"Restart-JGX-Quote-All" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
            elif env == "TS-Quote-All":
                jenkins_job = r"Restart-TS-Quote-All" 
                results = run._jenkins(jenkins_url, username,  pwd, jenkins_job)
        
            self.ui.txt_username.clear()
            self.ui.txt_password.clear()
            print('print results message :',results)
            # if results == "running" :
            #     self.ui.lab_state.setText("正在執行中...")
            if results == "Failed":
                self._warning("重啟失敗")
                
            elif results == "Success":
                self._warning("重啟成功")
                
        except Exception as ex:
            self.log.append(ex)
            self.ui.txt_username.clear()
            pwd = self.ui.txt_password.clear()
            
            if self.log != None:
                print("print log Message:",log)
                self._warning("請輸入正確登入訊息")
            else:
                print("print log Message:",log)
                self._warning("請輸入正確登入訊息")
                
            