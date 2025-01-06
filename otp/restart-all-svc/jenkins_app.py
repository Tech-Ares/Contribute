import sys
from genericpath import isdir, isfile
from logging import error
import time
import os
import argparse
import jenkins
import datetime
from tgbot import tgbot_msg




class RunAutoBuildMain:
    def __init__(self):
        pass
        
    
    def _jenkins(self, url, username, password, job  ):
        server = jenkins.Jenkins(url, username=username, password=password)
        
        server.build_job(job)
        results = "" 
        while True:
            time.sleep(5)
            #Jenkins API response .
            result, running_number = self._get_jobs_status(job, server)
            
            print("result job state : ",result) #可以得知jenkins狀態
            t = tgbot_msg()
 
            if result =="SUCCESS":
                t._SendMsg(username, result)
                results = "Success"
                break
            elif result =="FAILURE" or result=="UNSTABLE" :
                results = "Failed"
                t._SendMsg(username, result)
                break
            
        return results
            

        
    def _get_jobs_status(self,job_name,server):
        
        try:
            
            server.assert_job_exists(job_name)             
        except Exception as e:
            print(e)
            job_statue = '1'
            
        #判斷job是否處於排隊狀態
        inQueue = server.get_job_info(job_name)['inQueue']
        if str(inQueue) == 'True':
            job_statue = 'pending'
            running_number = server.get_job_info(job_name)['nextBuildNumber']
        else:
            #先假設job處於running狀態，則running_number = nextBuildNumber -1,執行中的job的nextBuildNumber已經更新
            running_number = server.get_job_info(job_name)['nextBuildNumber'] -1
            
            
            try:
                running_status = server.get_build_info(job_name,running_number)['building']
                if str(running_status) == 'True':
                    job_statue = 'running'
                else:
                    
                    #若running_status不是True說明job執行完成
                    job_statue = server.get_build_info(job_name,running_number)['result']
                    #print(job_statue)
                    
            except Exception as e:
                    #上面假设job处于running状态的假设不成立，则job的最新number应该是['lastCompletedBuild']['number']
                    lastCompletedBuild_number = server.get_job_info(job_name)['lastCompletedBuild']['number']
                    job_statue = server.get_build_info(job_name,lastCompletedBuild_number)['result']
        return job_statue, running_number
        
        
        
    
   
    
       

        

     
