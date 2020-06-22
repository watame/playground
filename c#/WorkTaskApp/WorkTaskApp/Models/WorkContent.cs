using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Prism.Mvvm;

namespace WorkTaskApp.Models
{
    /// <summary>
    /// 作業登録モデル
    /// </summary>
    public class WorkContent : BindableBase
    {
        /// <summary>
        /// ユーザID
        /// </summary>
        private int userId;
        public int UserId
        {
            get { return userId; }
            set { SetProperty(ref userId, value); }
        }

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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WorkContent()
        {
            this.Content = "";
            this.PesticideContents = new ObservableCollection<PesticideContent>();
            DateTime nowDate = DateTime.Now;
            this.startWorkTime = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 6, 0, 0);
            this.endWorkTime = new DateTime(nowDate.Year, nowDate.Month, nowDate.Day, 6, 0, 0);
        }
    }
}
