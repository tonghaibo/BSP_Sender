using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;//导入用MySql的包
using System.Data;
using System.Diagnostics;
using BSP_Sender.Util;

namespace BSP_Sender.DB
{
    public class DBHelper_LB
    {
        public string str_DB = "";
        /// <summary>
        /// 得到连接对象
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConn()
        {
            //MySqlConnection mysqlconn = new MySqlConnection("Database='" + str_DB + "';Data Source='" + "192.168.1.175" + "';User Id='" + "ZDZCAPP" + "';Password='" + "zdzc@2016" + "';charset=utf8");
            //内网
            //MySqlConnection mysqlconn = new MySqlConnection("Database='" + str_DB + "';Data Source='" + "rm-bp1d5y2fi1qc79g97.mysql.rds.aliyuncs.com" + "';User Id='" + "zdzcwz" + "';Password='" + "zdzc@2016" + "';charset=utf8");
            //外网
            MySqlConnection mysqlconn = new MySqlConnection("Database='" + str_DB + "';Data Source='" + "rm-bp1d5y2fi1qc79g97o.mysql.rds.aliyuncs.com" + "';User Id='" + "zdzcwz" + "';Password='" + "zdzc@2016" + "';charset=utf8");

            return mysqlconn;
        }
    }


    public class SQLHelper_LB : DBHelper_LB
    {

        public SQLHelper_LB(string strDB)
        {
            //InitializeComponent();
            str_DB = strDB;
        }

        ~SQLHelper_LB()
        {
            //InitializeComponent();
            //str_DB = strDB;
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet Selectinfo(string sql)
        {
            MySqlConnection mysqlconn = null;
            MySqlDataAdapter sda = null;
            DataSet dt = null;
            try
            {
                mysqlconn = base.GetConn();
                sda = new MySqlDataAdapter(sql, mysqlconn);
                dt = new DataSet();
                sda.Fill(dt, "table1");
                mysqlconn.Close();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }

        }
        MySqlConnection conn = null;
        MySqlCommand cmd = null;
        MySqlTransaction tx = null;
        /// <summary>
        /// 增删改操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>执行后的条数</returns>
        public int AddDelUpdate(string sql)
        {

            MySqlCommand cmd = null;

            try
            {
                if (conn == null || conn.State == ConnectionState.Closed)
                {
                    conn = base.GetConn();
                    conn.Open();
                }

                cmd = new MySqlCommand(sql, conn);
                int i = cmd.ExecuteNonQuery();

                conn.Close();
                return i;
            }
            catch (Exception e)
            {
                //Trace.Write(e, "数据插入异常！");
                LogHelper.WriteLog(typeof(FServer), "\r\n数据插入异常！\r\n" + e);
                conn.Close();
                throw;
            }
        }

        public string ExecuteSqlTran(List<string> SQLStringList)
        {
            using (MySqlConnection conn = base.GetConn())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                MySqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                        if (n >= 0 && (n % 2000 == 0 || n == SQLStringList.Count - 1 || SQLStringList.Count == 1))
                        {
                            tx.Commit();
                            tx = conn.BeginTransaction();
                        }
                    }
                    //tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    //throw new Exception(E.Message);
                    return E.Message;

                }
            }

            return "";
        }

        public void ExecuteSqlTran2(List<string> SQLStringList)
        {
            //using (MySqlConnection conn = base.GetConn())
            {
                //conn.Open();
                if (conn == null || conn.State == ConnectionState.Closed)
                {
                    conn = base.GetConn();
                    conn.Open();
                    cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                }


                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                        if (n >= 0 && (n % 2000 == 0 || n == SQLStringList.Count - 1 || SQLStringList.Count == 1))
                        {
                            tx.Commit();
                            tx = conn.BeginTransaction();
                        }
                    }
                    //tx.Commit();
                }
                catch (System.Data.SqlClient.SqlException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }
    }
}
