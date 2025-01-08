### 1. Login Game(Prod env, request no limit) 登录游戏(正式环境, 调用无限制)
uri: /yggGame/loginGame

POST application/x-www-form-urlencoded

params: 

name | required | type
--- | --- | ---
loginname | true | String(max 20)
topOrg | true | String
org | true | String
gameId | true | String
currency | true | String
language | false | String
channel | true | pc or mobile
returnUrl | false | String(must use in mobile channel, but not need in pc channel)
countryCode | true | String(2 places)
sign | true | String(Player Sign)

return json:

{"code": 0,"data": "https://xxxxxxxxxx"}

----
### 2. Try Game(Free Game, request no limit) 登录游戏(试玩游戏, 调用无限制)
uri: /yggGame/tryGame 

POST application/x-www-form-urlencoded

params: 

name | required | type
--- | --- | ---
gameId | true | String
channel | true | pc or mobile
currency | true | String
language | false | String

return json:

{"code": 0,"data": "https://xxxxxxxxxx"}

----
### 3. Get Balance(request no limit) 获取余额(调用无限制)
uri: /yggGame/getBalance 

POST application/x-www-form-urlencoded

params: 

name | required | type
--- | --- | ---
loginname | true | String(max 20)
topOrg | true | String
org | true | String
currency | true | String
sign | true | String(Player Sign)

return json:

{"code": 0,"data": 0}

----
### 4. credit(request no limit) 充值(调用无限制)
uri: /yggGame/credit 

POST application/x-www-form-urlencoded

params: 

name | required | type
--- | --- | ---
loginname | true | String(max 20)
topOrg | true | String
org | true | String
amount | true | Double(2 decimal places)
billno | true | String(id in merchant system, max 64)
currency | true | String
sign | true | String(Player Sign)

return json:
{"code": 0,"data": "success"}

----
### 5. withdraw(request no limit) 提款(调用无限制)
uri: /yggGame/withdraw 

POST application/x-www-form-urlencoded

params: 

name | required | type
--- | --- | ---
loginname | true | String(max 20)
topOrg | true | String
org | true | String
amount | true | Double(2 decimal places)
billno | true | String(id in merchant system, max 64)
currency | true | String
sign | true | String(Player Sign)

return json:

{"code": 0,"data": "success"}

----
### 6. Get users bet data(max 5k once, request rate 30s/per) 获取玩家投注数据(一次最多5k条, 频率需要少于30s/次)
uri: /yggGame/getUsersBetData 

POST application/x-www-form-urlencoded

params:

name | required | type
--- | --- | ---
topOrg | true | String
org | true | String
loginname | false | String (when loginname not null, only return this player data)
currency | true | String
startTime | true | String(yyyy-MM-dd HH:mm:ss), such as 2019-01-01 00:00:00
endTime | true | String(yyyy-MM-dd HH:mm:ss), such as 2019-01-01 23:59:59
sign | true | String(Merchant Sign)

return json:

{"code":0, "data" : [
{"topOrg" : "topOrg", 
 "org" : "org",
 "loginname" : 
 "user4test",
 "currency" : "CNY",
 "type" : "endWager",
 "amount" : 0.0,
 "beforeAmount" : 980.0,
 "afterAmount" : 980.0,
 "gameName" : "GemRocks",
 "reference" : "1812140129330100001",
 "createTime" : "2019-05-30 01:52:22"},
 {"topOrg" : "topOrg", 
  "org" : "org",
  "loginname" : "user4test",
  "currency" : "CNY",
  "type" : "wager",
  "amount" : 20.0,
  "beforeAmount" : 1000.0,
  "afterAmount" : 980.0,
  "gameName" : "GemRocks",
  "reference" : "1812140129330100001",
  "createTime":"2019-05-30 01:48:37"}]}

----

### 7. Kick Player (踢玩家下线)
uri: /yggGame/kickPlayer 

POST application/x-www-form-urlencoded

params: 

name | required | type
--- | --- | ---
loginname | true | String(max 20)
topOrg | true | String
org | true | String
currency | true | String
sign | true | String(Player Sign)

return json:

{"code": 0,"msg": "success"}

----

### 8. Check Player Last Wager (查询玩家最后一次投注)
uri: /yggGame/checkPlayerLastWager 

POST application/x-www-form-urlencoded

params: 

name | required | type
--- | --- | ---
loginname | true | String(max 20)
topOrg | true | String
org | true | String
currency | true | String
sign | true | String(Player Sign)

return json:

{"code":0,"data" : 
{"topOrg" : "topOrg", 
 "org" : "org",
 "loginname":"user4test",
 "currency" : "CNY",
 "type" : "wager",
 "amount" : 20.0,
 "beforeAmount" : 1000.0,
 "afterAmount" : 980.0,
 "gameName" : "GemRocks",
 "reference" : "1812140129330100001",
 "createTime" : "2019-05-30 01:48:37"}}

----

### 9. Check Transfer Status (查询玩家转账)
uri: /yggGame/checkTransferStatus 

POST application/x-www-form-urlencoded

params: 

name | required | type
--- | --- | ---
topOrg | true | String
org | true | String
billno | false | String
loginname | false | String(max 20)
currency | false | String
startTime | false | String (yyyy-MM-dd HH:mm:ss) (required when billno is blank)
endTime | false | String (yyyy-MM-dd HH:mm:ss) (required when billno is blank)
sign | true | String(Merchant Sign)

return json:

{"code":0, "data" : [
 {"topOrg" : "topOrg", 
  "org" : "org",
  "loginname" : "user4test",
  "currency" : "CNY",
  "amount" : 50,
  "type" : "in",
  "transferTime" : "2019-05-30 03:37:58",
  "billno":"sasda01"}]}

----
### 10. Get users bet data(max 5k once, request rate 30s/per) 获取玩家投注数据(一次最多5k条, 频率需要少于30s/次)
uri: /yggGame/getUsersBetDataV2 

POST application/x-www-form-urlencoded

params:

name | required | type
--- | --- | ---
topOrg | true | String
org | true | String
lastId | true | Integer (please send 0 when null)
loginname | false | String (when loginname not null, only return this player data)
currency | true | String
startTime | true | String(yyyy-MM-dd HH:mm:ss), such as 2019-01-01 00:00:00
endTime | true | String(yyyy-MM-dd HH:mm:ss), such as 2019-01-01 23:59:59
sign | true | String(Merchant Sign)

return json:

{"code":0, "data" : [
{"id" : 1,
 "topOrg" : "topOrg", 
 "org" : "org",
 "loginname" : 
 "user4test",
 "currency" : "CNY",
 "type" : "endWager",
 "amount" : 0.0,
 "beforeAmount" : 980.0,
 "afterAmount" : 980.0,
 "gameName" : "GemRocks",
 "reference" : "1812140129330100001",
 "createTime" : "2019-05-30 01:52:22"},
 {"id" : 2,
  "topOrg" : "topOrg", 
  "org" : "org",
  "loginname" : "user4test",
  "currency" : "CNY",
  "type" : "wager",
  "amount" : 20.0,
  "beforeAmount" : 1000.0,
  "afterAmount" : 980.0,
  "gameName" : "GemRocks",
  "reference" : "1812140129330100001",
  "createTime":"2019-05-30 01:48:37"}]}
  
----  

### sign method (Player Sign)
MD5(loginname + key) 

for example: 

playerName = playerTestName (in your system)

then: 

key = kjan*$kajwn)1 

sign = MD5(loginname + key) = MD5(playerTestNamekjan*$kajwn)1)

#### key will supported from game side
----

### sign method (Merchant Sign)

for example:

topOrg = TOP01

org = org03

key = kjan*$kajwn)1

sign = MD5(topOrg + org + key) = MD5(TOP01org03kjan*$kajwn)1)

----


### Response Code List

code | remark
--- | ---
0 | success
1 | system error
2 | account frozen
3 | balance insufficient
4 | sign error
5 | amount can't less than 0
6 | merchant not exist
7 | request param error
8 | billno already exist
9 | channel param error
10 | currency not support
11 | countryCode error
12 | player not exist
13 | player no wager data
14 | startTime and endTime are required when billno is blank

----

### Currency Code List

code | remark
--- | ---
CNY | Chinese Yuan
KRW | Korea Won
USD | United States dollar
HKD | Hong Kong Dollar
EUR | Euro
GBP | Great Britain Pound
JPY | Japanese Yen
TWD | New Taiwan dollar
THB | Thai Baht

----

### Language Code List

code | language
--- | ---
zh_hans | Chinese
ko | Korean
en | English

#### default langauge use en

----

### Country Code List

code | remark
--- | ---
CN | China
HK | Hong Kong
TW | Tai Wan
KR | Korean
JP | Japan
TH | Thailand

----

### Bet type in Get users bet data

type | remark
--- | ---
wager | bet
endWager | end bet
cancelWager | end bet
appendWagerResult | appeared in jackpot