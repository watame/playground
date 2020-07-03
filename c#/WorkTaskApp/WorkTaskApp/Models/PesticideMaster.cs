using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace WorkTaskApp.Models
{
    public class PesticideMaster : BindableBase, IDataBase
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
        /// 登録可能チェック
        /// </summary>
        /// <returns>登録可能かフラグ</returns>
        public bool CanRegister()
        {
            // 登録可能フラグ
            List<bool> canRegisterFlags = new List<bool>
            {
                String.IsNullOrWhiteSpace(this.Name),
                String.IsNullOrWhiteSpace(this.Unit)
            };

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

            string query = "INSERT INTO M_Pesticide(name, unit, uri, description) VALUES(?, ?, ?, ?)";
            List<object> addParams = new List<object>
            {
                this.Name,
                this.Unit,
                this.URI,
                this.Description,
            };
            DataBaseManager.DBManager.ExecuteNonQuery(query, addParams);

            return true;
        }

        /// <summary>
        /// DBの更新
        /// </summary>
        public bool UpdateDbRecord()
        {
            if (0 == this.ID || !CanRegister())
            {
                return false;
            }

            string query = "UPDATE M_Pesticide SET name = ? , unit = ?, uri = ?, description = ? WHERE id = ?";
            List<object> addParams = new List<object>
            {
                this.Name,
                this.Unit,
                this.URI,
                this.Description,
                this.ID
            };
            DataBaseManager.DBManager.ExecuteNonQuery(query, addParams);

            return true;
        }

        /// <summary>
        /// DBの削除
        /// </summary>
        public bool DeleteDbRecord()
        {
            if (0 == this.ID)
            {
                return false;
            }

            string query = "DELETE FROM M_Pesticide WHERE id = ?";
            List<object> addParams = new List<object>
            {
                this.ID
            };
            DataBaseManager.DBManager.ExecuteNonQuery(query, addParams);

            return true;
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
