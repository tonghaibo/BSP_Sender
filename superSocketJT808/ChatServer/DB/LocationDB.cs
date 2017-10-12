using ChatServer.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ChatServer.DB
{
    class LocationDB
    {
        SQLHelper_LB helper_lb = new SQLHelper_LB("gpsdb");

        private string GetNewGuid()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString("N");
        }
        private List<string> sqlList = new List<string>();

        /// <summary>
        /// 获取TCP接入点信息
        /// </summary>
        /// <returns></returns>
        public DataSet GET_t_bm_access_sensor()
        {
            string sql = string.Format("SELECT * FROM t_gps_access WHERE status=4");
            return helper_lb.Selectinfo(sql);
        }

        /// <summary>
        /// 插入JT808数据到DB
        /// </summary>
        /// <param name="aJT808_PackageData"></param>
        /// <returns></returns>
        public string GetSql_INSERT_JT808_PackageData(JT808_PackageData aJT808_PackageData)
        {
            string sql = "";
            sql = string.Format("INSERT INTO t_gps_jt808 VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}');",
               // GetNewGuid(),
                aJT808_PackageData.msgHeader.terminalPhone,
                aJT808_PackageData.locationInfo.alc,
                aJT808_PackageData.locationInfo.bst,
                aJT808_PackageData.locationInfo.lon.ToString(),
                aJT808_PackageData.locationInfo.lat.ToString(),
                aJT808_PackageData.locationInfo.hgt.ToString(),
                aJT808_PackageData.locationInfo.spd.ToString(),
                aJT808_PackageData.locationInfo.agl.ToString(),
                aJT808_PackageData.locationInfo.gtm.ToString(),
                aJT808_PackageData.locationInfo.mlg.ToString(),
                aJT808_PackageData.locationInfo.oil.ToString(),
                aJT808_PackageData.locationInfo.spd2.ToString(),
                aJT808_PackageData.locationInfo.est,
                aJT808_PackageData.locationInfo.io,
                aJT808_PackageData.locationInfo.ad1.ToString(),
                aJT808_PackageData.locationInfo.yte.ToString(),
                aJT808_PackageData.locationInfo.gnss.ToString(),
                "NOW()"
                );
            //sqlList.Add(sql);
            //if (sqlList.Count == 2000)
            //{
            //    helper_lb.ExecuteSqlTran(sqlList);
            //    sqlList.RemoveRange(0, 2000);
            //}
            //else if (sqlList.Count < 1000)
            //{

            //}
            return sql;

            //return helper_lb.AddDelUpdate(sql);
        }

        public string ExecuteSqlTran_JT808_PackageData(List<string> sqlList)
        {
            return helper_lb.ExecuteSqlTran(sqlList);
        }

        /// <summary>
        /// 插入JT808数据到DB
        /// </summary>
        /// <param name="aJT808_PackageData"></param>
        /// <returns></returns>
        public int INSERT2(JT808_PackageData aJT808_PackageData)
        {
            string sql = "";
            //sql = string.Format("INSERT INTO `locationdb`.`t_gps_jt808` (`message_id`, `vehicle_sim`, `alc`, `bst`, `lon`, `lat`, `hgt`, `spd`, `agl`, `gtm`, `mlg`, `oil`, `spd2`, `est`, `io`, `ad1`, `yte`, `gnss`, `db_time`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', {18});",
              sql = string.Format("INSERT INTO `gpsdb`.`t_gps` ( `device_id`, `alarm_status`, `vehicle_status`, `longitude`, `latitude`, `height`, `speed`, `direction`, `time`, `mile`, `oil`, `speed2`, `signal_status`, `io_status`, `analog`, `wifi`, `satellite_num`, `create_time`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}',{17});",
                //GetNewGuid(),
                aJT808_PackageData.msgHeader.terminalPhone,
                aJT808_PackageData.locationInfo.alc,
                aJT808_PackageData.locationInfo.bst,
                aJT808_PackageData.locationInfo.lon.ToString(),
                aJT808_PackageData.locationInfo.lat.ToString(),
                aJT808_PackageData.locationInfo.hgt.ToString(),
                aJT808_PackageData.locationInfo.spd.ToString(),
                aJT808_PackageData.locationInfo.agl.ToString(),
                aJT808_PackageData.locationInfo.gtm.ToString(),
                aJT808_PackageData.locationInfo.mlg.ToString(),
                aJT808_PackageData.locationInfo.oil.ToString(),
                aJT808_PackageData.locationInfo.spd2.ToString(),
                aJT808_PackageData.locationInfo.est,
                aJT808_PackageData.locationInfo.io,
                aJT808_PackageData.locationInfo.ad1.ToString(),
                aJT808_PackageData.locationInfo.yte.ToString(),
                aJT808_PackageData.locationInfo.gnss.ToString(),
                "NOW()"
                );

            return helper_lb.AddDelUpdate(sql);
        }


        /// <summary>
        /// 插入报警数据
        /// </summary>
        /// <param name="aJT808_PackageData"></param>
        /// <returns></returns>
        public int insertAlarm(JT808_PackageData aJT808_PackageData)
        {
            string sql = "";
            sql = string.Format("INSERT INTO `gpsdb`.`t_gps_alarm` ( `device_id`, `alarm_status`, `vehicle_status`, `longitude`, `latitude`, `height`, `speed`, `direction`, `time`, `mile`, `oil`, `speed2`, `signal_status`, `io_status`, `analog`, `wifi`, `satellite_num`, `create_time`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}',{17});",
              //GetNewGuid(),
              aJT808_PackageData.msgHeader.terminalPhone,
              aJT808_PackageData.locationInfo.alc,
              aJT808_PackageData.locationInfo.bst,
              aJT808_PackageData.locationInfo.lon.ToString(),
              aJT808_PackageData.locationInfo.lat.ToString(),
              aJT808_PackageData.locationInfo.hgt.ToString(),
              aJT808_PackageData.locationInfo.spd.ToString(),
              aJT808_PackageData.locationInfo.agl.ToString(),
              aJT808_PackageData.locationInfo.gtm.ToString(),
              aJT808_PackageData.locationInfo.mlg.ToString(),
              aJT808_PackageData.locationInfo.oil.ToString(),
              aJT808_PackageData.locationInfo.spd2.ToString(),
              aJT808_PackageData.locationInfo.est,
              aJT808_PackageData.locationInfo.io,
              aJT808_PackageData.locationInfo.ad1.ToString(),
              aJT808_PackageData.locationInfo.yte.ToString(),
              aJT808_PackageData.locationInfo.gnss.ToString(),
              "NOW()"
              );

            return helper_lb.AddDelUpdate(sql);
        }



    }
}
