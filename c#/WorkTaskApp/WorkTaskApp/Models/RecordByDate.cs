using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace WorkTaskApp.Models
{
    /// <summary>
    /// 日付単位レコードクラス
    /// </summary>
    class RecordByDate : BindableBase
    {
        /// <summary>
        /// 作業日時
        /// </summary>
        private DateWeather dateWeather;
        public DateWeather DateWeather
        {
            get { return dateWeather; }
            set { SetProperty(ref dateWeather, value); }
        }

        /// <summary>
        /// 作業内容インスタンスリスト
        /// </summary>
        private ObservableCollection<WorkContent> workContents;
        public ObservableCollection<WorkContent> WorkContents
        {
            get { return workContents; }
            set { SetProperty(ref workContents, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RecordByDate()
        {
            this.DateWeather = new DateWeather();
            this.WorkContents = new ObservableCollection<WorkContent>();
        }
    }
}
