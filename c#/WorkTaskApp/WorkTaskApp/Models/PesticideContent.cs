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
        public int Id { get; set; }

        /// <summary>
        /// 農薬名
        /// </summary>
        private PesticideMaster pestcideMaster;
        public PesticideMaster PestcideMaster
        {
            get { return pestcideMaster; }
            set { SetProperty(ref pestcideMaster, value); }
        }

        /// <summary>
        /// 使用量
        /// </summary>
        private double used;
        public double Used
        {
            get { return used; }
            set { SetProperty(ref used, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PesticideContent()
        {
            this.PestcideMaster = new PesticideMaster();
            this.Used = 0;
        }

        /// <summary>
        /// クローン用コンストラクタ
        /// </summary>
        /// <param name="pesticideContent"></param>
        public PesticideContent(PesticideContent pesticideContent)
        {
            this.PestcideMaster = pesticideContent.pestcideMaster;
            this.Used = pesticideContent.Used;

        }

        /// <summary>
        /// ListView用にフォーマットした文字列を戻す
        /// </summary>
        /// <returns>フォーマットされた文字列</returns>
        public override string ToString()
        {
            return String.Format("{0}, {1} {2}", pestcideMaster.Name, Used, pestcideMaster.Unit);
        }
    }
}
