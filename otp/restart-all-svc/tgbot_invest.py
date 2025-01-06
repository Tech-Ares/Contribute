import telegram
import os 
import datetime
class tgbot_msg :
    def __init__(self,results) :
        self.results = results
       
    
    
    def _SendMsg(self):
        bot = telegram.Bot(token=('5018488597:AAFwUJo5EF9dYAsfhaNSHf4SblcMC96YU_E'))
        bot.send_message(chat_id='-1001633905508', text=self.results)
        
            
        

if __name__ =="__main__"  :
    s = datetime.datetime.now()
    times = s.strftime("%Y%m%d")
    filepath = os.path.join(os.getcwd(),"today-{time}.txt".format(time=times))
    print("filepath :",filepath)
    results = """"""
    with open(filepath,mode="r+",encoding="utf-8") as f:
        tmp = f.readlines()
        
        for i in range(len(tmp)):
            tmp[i] = tmp[i]
        for i in tmp:
            results += i
    print(results)
    

    # tg=tgbot_msg(results)
    # tg._SendMsg() 