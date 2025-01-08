#!/bin/python
# -*- coding: utf-8 -*-
import MySQLdb
import sys
import os
import shutil
import filecmp
import csv
import smtplib 
from email.mime.application import MIMEApplication
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText
from email.utils import COMMASPACE, formatdate

sys.tracebacklimit =1 

user = os.environ["USER"]
user = user

try:
    file = open('/home/'+user+'/.pass', 'r')
except:
    print('There is no .pass file in your home\nPlease invite .pass to you home (づ｡◕‿‿◕｡)づ\n')


with open('/home/'+user+'/.pass') as myfile:
    password=myfile.read().replace('\n', '')

cnwcID = '1912031509140200000'
campaignType = 'cashrace' 
#campaignType = 'tournament'
#campaignType = 'mission'
top_org = 'VoidbridgeGroup'
report_file = '/usr/local/share/support-scripts/campaign_report_sender/reports/report_'+top_org+'_'+cnwcID+'.csv'
temp_file = 'check_temp_'+cnwcID+'.csv'
pid_file = 'check_'+cnwcID+'.pid'
env = {
"tw":"10.203.30.6"
}


def cashrace_check():
    print 'cashrace'
    for key,val in env.iteritems():
        try:
            db = MySQLdb.connect(user=user,
                                    passwd=password,
                                    host=val,
                                    db='game')
        except:
            t = csv.writer(open(temp_file, "a"))
           # t.writerow([key+' error while connecting to DB'])
            t.writerow('')
            continue
        
        cursor = db.cursor()
        check = ("SELECT u.nativeid 'NativeID', COALESCE(u.nickname, '') 'Nickname', u.org 'Brand', h.org_group 'Organization', bc.campaigntype 'PayoutSource', bc.name 'CampaignName', bc.campaignid 'CampaignID', bc.cnwcId 'NetworkCampaignID', tp.wagerid 'Wagerid', tp.bestScoreDate 'Date(UTC)', p.wonamount 'WonAmount', p.woncurrency 'PlayerCurrency', bcp.prize 'PrizeInEUR', '"+key+"' AS 'ENV' FROM boost_campaign bc LEFT JOIN tournament_player tp ON bc.campaignId = tp.tournamentId LEFT JOIN user u ON tp.userid = u.userid LEFT JOIN tournament_result tr ON tp.playerId = tr.playerId AND tr.hasWon = 1 LEFT JOIN boost_campaign_prize bcp ON tr.prizeId = bcp.campaignPrizeId LEFT JOIN prize p ON tp.wagerid = p.wagerid AND p.descr = 'Cashrace' JOIN (SELECT LOWER(c1.Domain) organization, c2.Domain org_group FROM util.organizationclosure c1 JOIN util.organizationclosure c2 ON c1.Parent = c2.Category) h ON LOWER(u.org) = h.organization WHERE bc.cnwcId = "+cnwcID+" AND bc.campaignType = 'CASHRACE' AND u.org IN (SELECT oc.wallet FROM util.organizationclosure oc JOIN util.organizationclosure oc2 ON oc.parent = oc2.category JOIN walletcfg wc ON wc.walletid = oc.wallet WHERE oc2.domain IN ('"+top_org+"')) HAVING PayoutSource IS NOT NULL ORDER BY Prize DESC;")

        cursor.execute(check)
        rows = cursor.fetchall()
        t = csv.writer(open(temp_file, "a"))
        if cursor.rowcount != 0:
            t.writerow(['NativeID', 'Nickname', 'Brand', 'Organization', 'PayoutSource', 'CampaignName', 'CampaignID', 'NetworkCampaignID', 'Wagerid', 'Date(UTC)', 'WonAmount', 'PlayerCurrency', 'PrizeInEUR', 'ENV'])
            for row in rows:
                t.writerow(row)
            t.writerow('')
        else:
            pass

        db.close
    print 'cashrace finished'

def tournament_mission_check():
    print 'tournament'
    for key,val in env.iteritems():
        try:
            db = MySQLdb.connect(user=user,
                                    passwd=password,
                                    host=val,
                                    db='game')
        except:
            t = csv.writer(open(temp_file, "a"))
           # t.writerow([key+' error while connecting to DB'])
            t.writerow('') 
            continue
        
        cursor = db.cursor()
        check = ("SELECT u.nativeid 'NativeID', COALESCE(u.nickname, '') 'Nickname', tp.amount 'Score', u.org 'Brand', h.org_group 'Organization', bc.name 'CampaignName', bc.rangetype 'Range', bc.campaigntype 'Type', bc.campaignid 'CampaignID', bc.cnwcID 'NetworkCampaignID', bp.prizeInPlayersCurrency 'WonAmount', u.currency 'PlayerCurrency', bcp.prize 'PrizeInEUR',  bp.type 'PrizeType', bp.status 'Status', bp.processed 'Date(UTC)', bp.wagerid 'Wagerid', '"+key+"' as 'Env' from tournament_player tp join tournament_result tr on tr.playerid=tp.playerid  join user u on u.userid =tp.userid join boost_campaign bc on bc.campaignid=tp.tournamentid left JOIN prize p ON tp.wagerid = p.wagerid LEFT JOIN boost_campaign_prize bcp ON tr.prizeId = bcp.campaignPrizeId right join boost_payment bp on bp.prizeId=tr.prizeId  JOIN (SELECT LOWER(c1.Domain) organization, c2.Domain org_group FROM util.organizationclosure c1 JOIN util.organizationclosure c2 ON c1.Parent = c2.Category) h ON LOWER(u.org) = h.organization WHERE bc.cnwcId = "+cnwcID+" AND u.org IN (SELECT oc.wallet FROM util.organizationclosure oc JOIN util.organizationclosure oc2 ON oc.parent = oc2.category JOIN walletcfg wc ON wc.walletid = oc.wallet WHERE oc2.domain IN ('"+top_org+"')) and tr.haswon=1 and tp.amount !='0.0' group by bp.wagerid order by bcp.prize DESC;")

        cursor.execute(check)
        rows = cursor.fetchall()
        t = csv.writer(open(temp_file, "a"))
        if cursor.rowcount != 0:
            t.writerow(['NativeID', 'Nickname', 'Score', 'Brand', 'Organization', 'CampaignName,', 'Range', 'Type', 'CampaignID', 'NetworkCampaignID', 'WonAmount', 'PlayerCurrency', 'PrizeInEUR', 'PrizeType', 'Status', 'Date(UTC)', 'Wagerid', 'ENV'])
            for row in rows:
                t.writerow(row)
            t.writerow('')
        else:
            pass
        db.close
    print 'tournament finished'

def send_mail():
    print 'mail'
    server = 'sender.yggdrasilgaming.com'
    send_from  = 'network_campaign_winners@sender.yggdrasilgaming.com'
    send_to = ['adam@azuretech.tw','mars@azuretech.tw','cora@azuretech.tw','sarahann.cruz@asianbge.com','alexandra.manansala@asianbge.com','jason.morris@voidbridge.com','martin.elliott@voidbridge.com']
    subject = 'Yggdrasil Network Campaign report'
    text = 'Dear Parteners,\r\nPlease find attached the newest winners list from the ongoing Network Prize Drop.\r\nBest regards,\r\nYggdrasil team'
    to_send = MIMEText(text,'plain')
    mailer = smtplib.SMTP()

    if os.stat(temp_file).st_size != 0:
        msg = MIMEMultipart()

        msg['From'] = send_from
        msg['To'] = ', '.join(send_to)
        msg['Date'] = formatdate(localtime=True)
        msg['Subject'] = subject
        msg['Message'] = text

        msg.attach(to_send)
        msg.attach(MIMEText(open(temp_file, 'rb').read()))

        mailer.connect(server)
        mailer.sendmail(send_from, send_to, msg.as_string())
        mailer.close()

    else:
        pass
    print 'mail finished'

def main():
    check_temp()
    pid = str(os.getpid())
    if os.path.isfile(pid_file):
        try:
            os.kill(int(open(pid_file, 'r').read()), 9)
        except:
            pass
    open(pid_file, 'w').write(pid)
    try:
        execute_scenario()
    finally:
        os.unlink(pid_file)


def check_temp():
    print 'check temp'
    if os.path.isfile(temp_file):
        os.remove(temp_file)
    else:
        pass
    print 'check temp finished'

def copy_temp():
    print 'copy temp'
    shutil.copy(temp_file, report_file)
    print 'copy temp finished'

def compare_results():
    print 'compare'
    try:
        filecmp.cmp(temp_file, report_file)
    except:
        copy_temp()
        send_mail()
    if filecmp.cmp(temp_file, report_file):
        print 'True'
        check_temp()
    else:
        print 'NO FILE'
        copy_temp()
        send_mail()
        check_temp()
    print 'compare finished'

def execute_scenario():
    if campaignType is 'cashrace':
        cashrace_check()
    if campaignType in ('tournament','mission'):
        tournament_mission_check()
    compare_results()

main()