using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.TextFormatting;
using System.Xml.XPath;

namespace WorkTaskApp.Models
{
    /// <summary>
    /// 参考サイト
    /// http://nprogram.hatenablog.com/entry/2018/06/10/231435
    /// https://jikkenjo.net/391.html
    /// </summary>
    class DataBaseManager : IDisposable
    {
        private readonly string dbFileName = @"test.db";
        private readonly string connectionString = null;
        private SQLiteConnection connection;
        private SQLiteCommand command;
        /// <summary>
        /// プロパティ
        /// </summary>
        private static DataBaseManager dbManager;
        public static DataBaseManager DBManager
        {
            get
            {
                if (null == dbManager)
                {
                    throw new NullReferenceException("データベースがすでに開かれています");
                }
                return dbManager;
            }
        }

        /// <summary>
        /// データベースへ接続する
        /// </summary>
        /// <param name="dbFileName"></param>
        public static void ConnectDB(string dbFileName)
        {
            dbManager = new DataBaseManager(dbFileName);
        }

        /// <summary>
        /// データベースから切断する
        /// </summary>
        /// <param name="dbFileName"></param>
        public static void DisConnectDB()
        {
            dbManager.Dispose();
            dbManager = null;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private DataBaseManager(string dbFileName)
        {
            // DBファイル存在チェック
            if (!System.IO.File.Exists(dbFileName))
            {
                throw new System.IO.FileNotFoundException("指定されたDBが見つかりません");
            }

            // DBへの接続
            var builder = new SQLiteConnectionStringBuilder() { DataSource = dbFileName };
            if (null == (this.connection = new SQLiteConnection(builder.ToString())))
            {
                throw new SQLiteException("DB接続に失敗しました");
            }

            // コマンドの作成
            if (null == (this.command = new SQLiteCommand(this.connection)))
            {
                throw new SQLiteException("SQLの実行ができません");
            }

            // データベースを展開する
            this.connection.Open();
        }

        public bool IsExistDBFile()
        {
            return System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + this.dbFileName);
        }

        /// <summary>
        /// 農薬マスタリスト取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PesticideContent> GetPesticideMaster(int id = -1)
        {
            List<PesticideContent> result = new List<PesticideContent>();
            connection.Open();
            if (-1 == id)
            {
                command.CommandText = @"SELECT * FROM M_Pesticide WHERE id = ?";
                command.Parameters.Add(id);
            }
            else
            {
                command.CommandText = @"SELECT * FROM M_Pesticide";
            }

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    PesticideContent tmp = new PesticideContent()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        PestcideName = reader["name"].ToString(),
                        Unit = reader["unit"].ToString()
                    };

                    result.Add(tmp);
                }
            }
            this.command.Parameters.Clear();
            return result;
        }

        /// <summary>
        /// 日付・天気テーブルの取得
        /// </summary>
        /// <returns></returns>
        public List<DateWeather> GetDateWeather()
        {
            List<DateWeather> result = new List<DateWeather>();
            command.CommandText = @"SELECT * FROM T_DateWeather";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    DateWeather tmp = new DateWeather()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        WorkDate = DateTime.Parse(reader["date"].ToString()),
                        Weather = reader["weather"].ToString()
                    };
                    result.Add(tmp);
                }
            }
            return result;
        }

        /// <summary>
        /// 作業内容リスト取得
        /// </summary>
        /// <param name="date_id"></param>
        /// <returns></returns>
        public List<WorkContent> GetWorkContent(int date_id)
        {
            List<WorkContent> result = new List<WorkContent>();
            this.command.CommandText = @"SELECT * FROM T_WorkContent WHERE date_id = ?";
            this.command.Parameters.Add(date_id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    WorkContent tmp = new WorkContent()
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Content = reader["content"].ToString(),
                        StartWorkTime = DateTime.Parse(reader["start_datetime"].ToString()),
                        EndWorkTime = DateTime.Parse(reader["end_datetime"].ToString()),
                        UserId = int.Parse(reader["user_id"].ToString()),
                        DateId = int.Parse(reader["date_id"].ToString())
                    };
                }
            }
            this.command.Parameters.Clear();
            return result;
        }

        /// <summary>
        /// 農薬内容リスト取得
        /// </summary>
        /// <param name="work_content_id"></param>
        /// <returns></returns>
        public List<PesticideContent> GetPesticideContent(int work_content_id)
        {
            List<PesticideContent> result = new List<PesticideContent>();
            connection.Open();
            command.CommandText = @"SELECT * FROM T_PesticideContent WHERE work_content_id = ?";
            command.Parameters.Add(work_content_id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int pestside_id = int.Parse(reader["pestcide_id"].ToString());
                    List<PesticideContent> master_list = GetPesticideMaster(pestside_id);
                    PesticideContent tmp = new PesticideContent
                    {
                        Id = int.Parse(reader["id"].ToString()),
                        Used = double.Parse(reader["used"].ToString()),
                        PestcideName = master_list.First().PestcideName,
                        Unit = master_list.First().Unit,
                        PestcideId = pestside_id,
                    };

                    result.Add(tmp);
                }
            }
            this.command.Parameters.Clear();
            return result;
        }

        public void Dispose()
        {
            this.command.Dispose();
            this.connection.Close();
        }
    }
}
