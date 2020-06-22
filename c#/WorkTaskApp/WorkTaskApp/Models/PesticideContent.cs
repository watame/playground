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
        private string pestcideName;
        public string PestcideName
        {
            get { return pestcideName; }
            set { SetProperty(ref pestcideName, value); }
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
            this.PestcideName = "";
            this.Used = 0;
            this.unit = "";
        }

        /// <summary>
        /// ListView用にフォーマットした文字列を戻す
        /// </summary>
        /// <returns>フォーマットされた文字列</returns>
        public override string ToString()
        {
            return String.Format("{0}, {1} {2}", PestcideName, Used, Unit);
        }
    }
}
