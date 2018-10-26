using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace MYSQL数据操作
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Database=test007;Data Source=127.0.0.1;port=3306;User Id=root;Password=wa3605228;";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            #region 查询
            //MySqlCommand cmd = new MySqlCommand("select * from user",conn);
            //MySqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{               
            //    string username=reader.GetString("username");
            //    string password = reader.GetString("password");
            //    Console.WriteLine(username+":"+password);
            //}
            //reader.Close();
            #endregion
            #region 插入
            //string username = "liuhengfei003";string password = "tokoy;delete from user;";
            ////string sqlCmd = "insert into user set username='" + username + "'"+",password='" + password+"'";
            ////string sqlCmd = "insert into user set username='"+username+"',password='"+password+"'";//1、'"+username+"'可以在string中加入加引号的变量
            //string sqlCmd = "insert into user set username=@un,password=@pwd";
            //MySqlCommand cmd = new MySqlCommand(sqlCmd,conn);
            //cmd.Parameters.AddWithValue("un",username);
            //cmd.Parameters.AddWithValue("pwd",password);
            //cmd.ExecuteNonQuery();
            #endregion
            #region 删除
            //MySqlCommand cmd = new MySqlCommand("delete from user where id = @id",conn);
            //cmd.Parameters.AddWithValue("id",4);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //Console.ReadKey();
            #endregion
            #region 更新
            MySqlCommand cmd = new MySqlCommand("update user set username =@um where id = 5", conn);
            cmd.Parameters.AddWithValue("um","goubi");
            cmd.ExecuteNonQuery();
            #endregion
            conn.Close();
            Console.ReadKey();
        }
    }
}
