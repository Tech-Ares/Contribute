import telegram

class tgbot_msg :
    def __init__(self) :
       pass
    
    
    def _SendMsg(self, userid, result):
        bot = telegram.Bot(token=('1968294824:AAFQ4A1_XaNH8tRgCcbRR9RHPW40Z3psa5I'))
        #由使用者 {} 手動重啟網關行情
        if result == "SUCCESS":
            #由使用者 {} 手動重啟網關行情，啟動成功
            bot.send_message(chat_id='-1001566592084', text="由使用者 {} 手動重啟網關行情，啟動成功.".format(userid))
        else:
            ##由使用者 {} 手動重啟網關行情，啟動失敗
            bot.send_message(chat_id='-1001566592084', text="由使用者 {} 手動重啟網關行情，啟動失敗.".format(userid))

# if __name__ =="__main__"  :
#     tg=tgbot()
#     tg._SendMsg("genos") 