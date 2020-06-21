using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace WorkTaskApp.Models
{
    /// <summary>
    /// 農薬散布モデル
    /// </summary>
    public class PesticideContent : BindableBase
    {
        /// <summary>
        /// 農薬名
        /// </summary>
        private string pestsideName;
        public string PestsideName
        {
            get { return pestsideName; }
            set { SetProperty(ref pestsideName, value); }
        }

        /// <summary>
        /// 使用量
        /// </summary>
        private Int16 used;
        public Int16 Used
        {
            get { return used; }
            set { SetProperty(ref used, value); }
        }

        /// <summary>
        /// 単位
        /// </summary>
        private string unit;
        public string Unit
        {
            get { return unit; }
            set { SetProperty(ref unit, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PesticideContent()
        {
            this.PestsideName = "";
            this.Used = 0;
            this.unit = "";
        }
    }
}
