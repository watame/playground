using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Media.TextFormatting;
using System.Xml.XPath;
using System.Windows.Media.Animation;
using System.Data;

namespace WorkTaskApp.Models
{
    /// <summary>
    /// 参考サイト
    /// http://nprogram.hatenablog.com/entry/2018/06/10/231435
    /// https://jikkenjo.net/391.html
    /// </summary>
    class DataBaseManager : IDisposable
    {
        /// <summary>
        /// DBへの接続のコマンドインスタンス
        /// </summary>
        private SQLiteConnection connection;

        /// <summary>
        /// クエリ実行用のコマンドインスタンス
        /// </summary>
        private SQLiteCommand command;

        /// <summary>
        /// トランザクション管理インスタンス
        /// </summary>
        private SQLiteTransaction transaction;

        /// <summary>
        /// 自身インスタンス（シングルトン用）
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

        #region Utility

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

        #endregion Utility

        #region TRANSACTION

        /// <summary>
        /// トランザクションの開始
        /// </summary>
        private void StartTransaction()
        {
            this.transaction = connection.BeginTransaction();
        }

        /// <summary>
        /// トランザクションのコミット
        /// </summary>
        private void CommitTransaction()
        {
            this.transaction.Commit();
            DisposeTransaction();
        }

        /// <summary>
        /// トランザクションのロールバック
        /// </summary>
        private void RollBackTransaction()
        {
            this.transaction.Rollback();
            DisposeTransaction();
        }

        /// <summary>
        /// トランザクションの解放
        /// </summary>
        private void DisposeTransaction()
        {
            this.transaction.Dispose();
            this.transaction = null;
        }

        #endregion TRANSACTION

        #region PREPARE_STATEMENT
        /// <summary>
        /// パラメータの型をSQLiteの型として取得
        /// </summary>
        /// <param name="param">パラメータ（型は不明）</param>
        /// <returns>SQLiteの型情報</returns>
        private DbType GetDbType(object param)
        {
            if (param is int)
            {
                return DbType.Int32;
            }
            else if (param is string)
            {
                return DbType.String;
            }
            else if (param is double || param is float)
            {
                return DbType.Double;
            }
            else
            {
                throw new ArgumentException("引数に処理できない型が含まれています");
            }

        }

        /// <summary>
        /// 実行するコマンドの用意
        /// </summary>
        /// <param name="query">クエリ</param>
        /// <param name="addParams">登録するパラメータの配列</param>
        private void PrepareCommandParameter(string query, List<object> addParams)
        {
            // クエリの設定
            this.command.CommandText = query;
            // パラメータの設定
            this.command.Parameters.Clear();
            foreach (object param in addParams)
            {
                this.command.Parameters.Add(new SQLiteParameter { DbType = GetDbType(param), Value = param });
            }
            // パラメータを適用した状態を適用
            this.command.Prepare();
        }

        #endregion PREPARE_STATEMENT

        #region SELECT

        /// <summary>
        /// 農薬マスタリスト取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<WorkerMaster> GetWorkerMaster(int id = -1)
        {
            List<WorkerMaster> result = new List<WorkerMaster>();
            try
            {
                if (-1 != id)
                {
                    command.CommandText = @"SELECT * FROM M_Worker WHERE id = ?";
                    this.command.Parameters.Clear();
                    command.Parameters.Add(id);
                }
                else
                {
                    command.CommandText = @"SELECT * FROM M_Worker";
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WorkerMaster tmp = new WorkerMaster()
                        {
                            ID = int.Parse(reader["id"].ToString()),
                            Name = reader["name"].ToString(),
                        };

                        result.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SQLiteException(ex.ToString());
            }

            return result;
        }

        /// <summary>
        /// 農薬マスタリスト取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PesticideMaster> GetPesticideMaster(int id = -1)
        {
            List<PesticideMaster> result = new List<PesticideMaster>();
            try
            {
                if (-1 != id)
                {
                    command.CommandText = @"SELECT * FROM M_Pesticide WHERE id = ?";
                    this.command.Parameters.Clear();
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
                        PesticideMaster tmp = new PesticideMaster()
                        {
                            ID = int.Parse(reader["id"].ToString()),
                            Name = reader["name"].ToString(),
                            Unit = reader["unit"].ToString(),
                            URI = reader["uri"].ToString(),
                            Description = reader["description"].ToString()
                        };

                        result.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SQLiteException(ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// 日付・天気テーブルの取得
        /// </summary>
        /// <returns></returns>
        public List<DateWeather> GetDateWeather()
        {
            List<DateWeather> result = new List<DateWeather>();
            try
            {
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
            }
            catch (Exception ex)
            {
                throw new SQLiteException(ex.ToString());
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
            try
            {
                this.command.CommandText = @"SELECT * FROM T_WorkContent WHERE date_id = ?";
                this.command.Parameters.Clear();
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
            }
            catch (Exception ex)
            {
                throw new SQLiteException(ex.ToString());
            }
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
            command.CommandText = @"SELECT * FROM T_PesticideContent WHERE work_content_id = ?";
            try
            {
                this.command.Parameters.Clear();
                command.Parameters.Add(work_content_id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int pestside_id = int.Parse(reader["pestcide_id"].ToString());
                        List<PesticideMaster> master_list = GetPesticideMaster(pestside_id);
                        PesticideContent tmp = new PesticideContent
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            PestcideMaster = master_list[0],
                            Used = double.Parse(reader["used"].ToString()),
                        };

                        result.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SQLiteException(ex.ToString());
            }
            return result;
        }
        #endregion SELECT

        #region EXECUTE_NONQUERY

        /// <summary>
        /// 戻り値のないクエリ(INSERT, UPDATE, DELETE)を実行する
        /// </summary>
        /// <param name="query">クエリ</param>
        /// <param name="addParams">登録するパラメータの配列</param>
        public void ExecuteNonQuery(string query, List<object> addParams)
        {
            try
            {
                StartTransaction();
                PrepareCommandParameter(query, addParams);
                this.command.ExecuteNonQuery();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw new SQLiteException(ex.ToString());
            }

        }
        #endregion EXECUTE_NONQUERY

        #region INSERT

        /// <summary>
        /// 労働者マスタ登録
        /// </summary>
        /// <param name="workerMaster"></param>
        public void RegisterWorkerMaster(WorkerMaster workerMaster)
        {
            try
            {
                StartTransaction();
                this.command.CommandText = "INSERT INTO M_Worker(name) VALUES(?)";
                this.command.Parameters.Clear();
                this.command.Parameters.Add(new SQLiteParameter { Value = workerMaster.Name });
                this.command.ExecuteNonQuery();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw new SQLiteException(ex.ToString());
            }
        }

        /// <summary>
        /// 労働者マスタ更新
        /// </summary>
        /// <param name="workerMaster"></param>
        public void UpdateWorkerMaster(WorkerMaster workerMaster)
        {
            try
            {
                StartTransaction();
                this.command.CommandText = "UPDATE M_Worker SET name = ? WHERE id = ?";
                this.command.Parameters.Clear();
                this.command.Parameters.Add(new SQLiteParameter { Value = workerMaster.Name });
                this.command.Parameters.Add(new SQLiteParameter { Value = workerMaster.ID });
                this.command.ExecuteNonQuery();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw new SQLiteException(ex.ToString());
            }
        }

        /// <summary>
        /// 労働者マスタ更新
        /// </summary>
        /// <param name="workerMaster"></param>
        public void DeleteWorkerMaster(WorkerMaster workerMaster)
        {
            try
            {
                StartTransaction();
                PrepareCommandParameter("DELETE FROM M_Worker WHERE id = ?", new List<object> { workerMaster.ID });
                this.command.ExecuteNonQuery();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw new SQLiteException(ex.ToString());
            }
        }



        /// <summary>
        /// 日付・天気内容登録
        /// </summary>
        /// <param name="dateWeather"></param>
        public void RegisterDateWeather(DateWeather dateWeather)
        {
            try
            {
                StartTransaction();
                this.command.CommandText = "INSERT INTO T_DateWeather(weather, date) VALUES(?, ?)";
                this.command.Parameters.Clear();
                this.command.Parameters.Add(new SQLiteParameter { Value = dateWeather.Weather });
                this.command.Parameters.Add(new SQLiteParameter { Value = dateWeather.WorkDate.ToString() });
                this.command.ExecuteNonQuery();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw new SQLiteException(ex.ToString());
            }
        }

        /// <summary>
        /// 作業内容登録
        /// </summary>
        /// <param name="workContent"></param>
        /// <param name="date_id"></param>
        public void RegisterWorkContent(WorkContent workContent, int date_id)
        {
            try
            {
                StartTransaction();
                this.command.CommandText = "INSERT INTO T_WorkContent(content, start_datetime, end_datetime, user_id, date_id) VALUES(?, ?, ?, ?, ?)";
                this.command.Parameters.Clear();
                this.command.Parameters.Add(new SQLiteParameter { Value = workContent.Content });
                this.command.Parameters.Add(new SQLiteParameter { Value = workContent.StartWorkTime.ToString() });
                this.command.Parameters.Add(new SQLiteParameter { Value = workContent.EndWorkTime.ToString() });
                this.command.Parameters.Add(new SQLiteParameter { Value = workContent.UserId });
                this.command.Parameters.Add(date_id);
                this.command.ExecuteNonQuery();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw new SQLiteException(ex.ToString());
            }
        }

        /// <summary>
        /// 農薬内容登録
        /// </summary>
        /// <param name="pesticideContent"></param>
        /// <param name="work_content_id"></param>
        public void RegisterPesticideContent(PesticideContent pesticideContent, int work_content_id)
        {
            try
            {
                StartTransaction();
                this.command.CommandText = "INSERT INTO T_PesticideContent(used, pesticide_id, work_content_id) VALUES(?, ?, ?)";
                this.command.Parameters.Clear();
                this.command.Parameters.Add(new SQLiteParameter { Value = pesticideContent.Used });
                this.command.Parameters.Add(new SQLiteParameter { Value = pesticideContent.PestcideMaster.ID });
                this.command.Parameters.Add(new SQLiteParameter { Value = work_content_id });
                this.command.ExecuteNonQuery();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw new SQLiteException(ex.ToString());
            }
        }
        #endregion INSERT

        #region IDisposableの実装

        public void Dispose()
        {
            DisposeTransaction();
            this.command.Dispose();
            this.connection.Close();
            this.connection.Dispose();
        }

        #endregion IDisposableの実装
    }
}
