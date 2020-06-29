using Prism.Mvvm;
using System;

namespace WorkTaskApp.Models
{
    public class PesticideMaster : BindableBase
    {
        /// <summary>
        /// ID
        /// </summary>
        private int id;
        public int ID
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        /// <summary>
        /// 農薬名
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
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
        /// URI
        /// </summary>
        private string uri;
        public string URI
        {
            get { return uri; }
            set { SetProperty(ref uri, value); }
        }

        /// <summary>
        /// 説明
        /// </summary>
        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PesticideMaster()
        {
            this.ID = 0;
            this.Name = "";
            this.Unit = "";
            this.URI = "";
            this.Description = "";
        }

        /// <summary>
        /// クローン用コンストラクタ
        /// </summary>
        /// <param name="pesticideMaster"></param>
        public PesticideMaster(PesticideMaster pesticideMaster)
        {
            this.ID = pesticideMaster.ID;
            this.Name = pesticideMaster.Name;
            this.Unit = pesticideMaster.Unit;
            this.URI = pesticideMaster.URI;
            this.Description = pesticideMaster.Description;
        }

        /// <summary>
        /// ListView用にフォーマットした文字列を戻す
        /// </summary>
        /// <returns>フォーマットされた文字列</returns>
        public override string ToString()
        {
            return String.Format("名称：{0}, 単位：{1} URL：{2} 説明：{3}", this.Name, this.Unit, this.URI, this.Description);
        }
    }
}
