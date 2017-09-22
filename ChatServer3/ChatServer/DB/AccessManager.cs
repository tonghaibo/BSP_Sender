using ChatServer.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChatServer.DB
{
    class AccessManager
    {
        LocationDB locationDB = new LocationDB();


        public void query_access_sensor(JT808_PackageData packageData) {
            DataSet accessSensorSet = locationDB.GET_t_bm_access_sensor();
            foreach (DataRow mDr in accessSensorSet.Tables[0].Rows)
            {
              int  sensor_port = int.Parse(mDr["sensor_port"].ToString());// int(6) DEFAULT NULL COMMENT '接入点端口',
                Console.WriteLine("ip====="+ sensor_port);
            }

        }


        public void inster_JT808data(JT808_PackageData packageData) {
            locationDB.INSERT2(packageData);
        }

        public void inster_JT808alarmData(JT808_PackageData packageData)
        {
            locationDB.insertAlarm(packageData);
        }


    }
}
