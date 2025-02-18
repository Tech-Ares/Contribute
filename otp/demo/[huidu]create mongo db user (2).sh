
#use admin;
#db.runCommand(
#{
#    createUser: "myuser",
#    pwd : "mypwd",
#    roles: 
#    [
#        { role: "readWrite", db: "db1" } ,
#        { role: "readWrite", db: "db2" } 
#    ]
#});
use admin
db.createUser(
    {
      user: "admin",
      pwd: "jj3jVUxkPKJTFw2",
      roles: [ "root" ]
    }
)

use huidu_gateway
db.huidu_gateway.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_gateway"}]
        }
)

use huidu_gateway_sl
db.huidu_gateway_sl.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_gateway_sl"}]
        }
)

use huidu_java_db_fsr
db.huidu_java_db_fsr.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_java_db_fsr"}]
        }
)

use huidu_java_db_gateway_support
db.huidu_java_db_gateway_support.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_java_db_gateway_support"}]
        }
)

use huidu_java_db_riskServer
db.huidu_java_db_riskServer.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_java_db_riskServer"}]
        }
)

use huidu_java_db_support
db.huidu_java_db_support.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_java_db_support"}]
        }
)

use huidu_routine
db.huidu_routine.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_routine"}]
        }
)
use huidu_routine_sl
db.huidu_routine_sl.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_routine_sl"}]
        }
)

use huidu_ste
db.huidu_ste.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_ste"}]
        }
)

use huidu_trade_simulator
db.huidu_trade_simulator.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_trade_simulator"}]
        }
)

use huidu_trade_simulator_sl
db.huidu_trade_simulator_sl.insert({"name":"tutorials point"})
db.createUser(

        {
            user:"admin",
            pwd:"jj3jVUxkPKJTFw2",
            roles:[
			{role:"readWrite",db:"huidu_trade_simulator_sl"}]
        }
)
