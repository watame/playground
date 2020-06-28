using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace WorkTaskApp.Models
{
    public class DateWeather : BindableBase
    {
        public int Id { get; set; }

        /// <summary>
        /// 作業日時
        /// </summary>
        private DateTime workDate;
        public DateTime WorkDate
        {
            get { return workDate; }
            set { SetProperty(ref workDate, value); }
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
        /// コンストラクタ
        /// </summary>
        public DateWeather()
        {
            this.WorkDate = DateTime.Now;
            this.Weather = "晴れ";
        }
    }
}
