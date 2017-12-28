using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile_Locator_App.Database
{
    class GetFriendLocation
    {
        // can use the pub/sub feature of redis to get data when it changes
        // but this may be to intensive as the data will be streamed often if the location
        // is updated often so might be better getting location every few seconds instead.


        

        int xLocation = (int)DBSupervisor.RedisDB.StringGet("usernameLocation");
        int yLocation = (int)DBSupervisor.RedisDB.StringGet("usernameLocation");

        private void DeleteKeys()
        {
            DBSupervisor.RedisDB.KeyDelete("abc");
            DBSupervisor.RedisDB.HashSet("user:user1", new HashEntry[] { new HashEntry("12", "13"), new HashEntry("14", "15") });

        }

        private void ifKeyExists()
        {
            var value = DBSupervisor.RedisDB.StringGet("abc");
            bool isNil = value.IsNull; // this is true

        }
    }
}
