using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Prism.Mvvm;

namespace WorkTaskApp.Models
{
    /// <summary>
    /// 作業登録モデル
    /// </summary>
    public class WorkContent : BindableBase, IDataBase
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ユーザID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 作業内容
        /// </summary>
        private string content;
        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        /// <summary>
        /// 天気
        /// </summary>
        private string weather;
        public string Weather
        {
            get { return weather; }
            set { SetProperty(ref weather, value); }
        }

        /// <summary>
        /// 農薬インスタンスリスト
        /// </summary>
        private ObservableCollection<PesticideContent> pesticideContents;
        public ObservableCollection<PesticideContent> PesticideContents
        {
            get { return pesticideContents; }
            set { SetProperty(ref pesticideContents, value); }
        }

        /// <summary>
        /// 作業開始時刻
        /// </summary> 
        private DateTime startWorkTime;
        public DateTime StartWorkTime
        {
            get { return startWorkTime; }
            set { SetProperty(ref startWorkTime, value); }
        }

        /// <summary>
        /// 作業終了時刻
        /// </summary> 
        private DateTime endWorkTime;
        public DateTime EndWorkTime
        {
            get { return endWorkTime; }
            set { SetProperty(ref endWorkTime, value); }
        }

        private WorkerMaster workerMaster;
        public WorkerMaster WorkerMaster
        {
            get { return workerMaster; }
            set { SetProperty(ref workerMaster, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WorkContent()
        {
            this.Id = 0;
            this.UserId = 0;
            this.Content = "";
            this.Weather = "";
            this.PesticideContents = new ObservableCollection<PesticideContent>();
            DateTime nowDate = DateTime.Now;
            this.StartWorkTime = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 6, 0, 0);
            this.EndWorkTime = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 6, 0, 0);
            this.WorkerMaster = new WorkerMaster();
        }

        /// <summary>
        /// クローン用コンストラクタ
        /// </summary>
        public WorkContent(WorkContent workContent)
        {
            this.Id = workContent.Id;
            this.UserId = workContent.UserId;
            this.Content = workContent.Content;
            this.Weather = workContent.Weather;
            this.PesticideContents = new ObservableCollection<PesticideContent>();
            foreach (PesticideContent pestcide in workContent.PesticideContents)
            {
                this.PesticideContents.Add(new PesticideContent(pestcide));
            }
            this.StartWorkTime = workContent.StartWorkTime;
            this.EndWorkTime = workContent.EndWorkTime;
            this.WorkerMaster = workContent.WorkerMaster;
        }

        /// <summary>
        /// 作業開始時間、作業終了時間を設定する
        /// </summary>
        /// <param name="dateTime">指定したいDateTimeインスタンス</param>
        public void SetDateTime(DateTime dateTime)
        {
            this.startWorkTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            this.endWorkTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        /// <summary>
        /// ListView用にフォーマットした文字列を戻す
        /// </summary>
        /// <returns>フォーマットされた文字列</returns>
        public override string ToString()
        {
            // 農薬内容を文字列として取得
            string pestcideStr = "";
            foreach (PesticideContent pc in this.PesticideContents)
            {
                if ("" == pestcideStr)
                {
                    pestcideStr += String.Format("  {0}", pc.ToString());
                    continue;
                }
                pestcideStr += String.Format("\n  {0}", pc.ToString());
            }

            // 農薬内容がある場合にのみ、表示に追加する
            if ("" != pestcideStr)
            {
                return String.Format("天気：{0}, 開始時刻：{1}, 終了時刻：{2}, 作業者：{3}, 作業内容：{4}\n農薬内容：\n{5}",
                    this.Weather, this.StartWorkTime.ToString(), this.EndWorkTime.ToString(), this.WorkerMaster.Name, this.Content, pestcideStr);
            }

            return String.Format("天気：{0}, 開始時刻：{1}, 終了時刻：{2}, 作業者：{3}, 作業内容：{4}",
                this.Weather, this.StartWorkTime.ToString(), this.EndWorkTime.ToString(), this.WorkerMaster.Name, this.Content);
        }

        #region IDataBase実装

        /// <summary>
        /// 登録可能チェック
        /// </summary>
        /// <returns>登録可能かフラグ</returns>
        public bool CanRegister()
        {
            // 登録可能フラグ
            List<bool> canRegisterFlags = new List<bool>
            {
                String.IsNullOrWhiteSpace(this.Content),
                String.IsNullOrWhiteSpace(this.Weather),
                !(this.startWorkTime < this.EndWorkTime)
            };

            // 農薬内容が登録されている場合は、配列内のフラグを取得
            if (this.PesticideContents.Any())
            {
                // 農薬コンテンツが漏れなく登録できているか
                List<bool> pesticideRegisterFlags = this.PesticideContents.Select(pesticide => !(pesticide.CanRegister())).ToList();
                // 登録可能フラグを格納
                canRegisterFlags.AddRange(pesticideRegisterFlags);
            }


            // 登録可能フラグを確認し、登録可能か（フラグにtrueが1つも含まれていない）真偽値を戻す
            return (-1 == canRegisterFlags.FindIndex(canRegisterFlag => true == canRegisterFlag));
        }

        /// <summary>
        /// DBへの登録
        /// </summary>
        public bool RegisterDbRecord()
        {
            if (!CanRegister())
            {
                return false;
            }

            string query = "INSERT INTO T_WorkContent(content, weather, start_datetime, end_datetime, user_id) VALUES(?, ?, ?, ?, ?)";
            List<object> addParams = new List<object>
            {
                this.Content,
                this.Weather,
                this.StartWorkTime.ToString(),
                this.EndWorkTime.ToString(),
                this.UserId
            };
            DataBaseManager.DBManager.ExecuteNonQuery(query, addParams);

            return true;
        }

        /// <summary>
        /// DBの更新
        /// </summary>
        public bool UpdateDbRecord()
        {
            if (0 == this.Id || !CanRegister())
            {
                return false;
            }

            string query = "UPDATE T_WorkContent SET content = ?, weather = ?, start_datetime = ?, end_datetime = ?, user_id = ? WHERE id = ?";
            List<object> addParams = new List<object>
            {
                this.Content,
                this.Weather,
                this.StartWorkTime.ToString(),
                this.EndWorkTime.ToString(),
                this.UserId,
                this.Id
            };

            DataBaseManager.DBManager.ExecuteNonQuery(query, addParams);
            return true;
        }

        /// <summary>
        /// DBの削除
        /// </summary>
        public bool DeleteDbRecord()
        {
            if (0 == this.Id)
            {
                return false;
            }

            string query = "DELETE FROM T_WorkContent WHERE id = ?";
            List<object> addParams = new List<object>
            {
                this.Id
            };

            DataBaseManager.DBManager.ExecuteNonQuery(query, addParams);
            return true;
        }

        #endregion IDataBase実装
    }
}
