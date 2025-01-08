var request = require('request');
var stageurl = 'https://static-stage-tw.248ka.com/init/launchClient.html?';

var key = 1234; //session token
var currency = 'CNY';
var lang = 'zh_hant';
var gameid = '7301';
var org = 'tomseamlesstest'; // setting endpoint url on backend
var channel = 'pc'; 
var home = 'www.google.com'; // mobile onley 主页地址
var fullscreen = 'yes'; // desktop only 全屏模式
var reminderElapsed = 10; // Session超时时间(分钟) 预设不超时
var reminderInterval = 20; // 检查频率 预设60分钟(提醒玩家已经进行多久)
var topOrg = 'TOMGroup'; // only for pff launch client
//var clientHistoryURL = 'url'; //only for UK/GiB integrations
var realityCheckBackURL = 'https://www.google.com/'; //停止遊戲返回的链接
//var redirectType = 'self'; //跳转类型
//var share = 'yes'; // Enables or disables social features
//var leaderboardRewatch = 'yes' // Enables or disables watch functionality in tournaments
//var license = ''; // only for UK/RO/IT/DK/ IT/SCHHOL/AGCC/CZ/ ES integrations
//var skin = 'Casino'; // only for Bingo games (enables UI skin)

var url = stageurl + 'key=' + key + '&currency=' + currency + '&lang=' + lang + '&gameid=' + gameid + '&org=' +org + '&channel=' + channel + '&fullscreen=' + fullscreen

console.log("请求地址:" + "\n" + url + "\n");

//request(url, function (error, response, body) {
//  if (!error && response.statusCode == 200) {
//	console.log(body)
//  }
//});

