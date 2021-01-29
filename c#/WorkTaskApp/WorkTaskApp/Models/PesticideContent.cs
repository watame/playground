using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace WorkTaskApp.Models
{
    /// <summary>
    /// 農薬散布モデル
    /// </summary>
    public class PesticideContent : BindableBase, IDataBase
    {
        public int Id { get; set; }

        public int PesticideId { get; set; }

        public int WorkContentId { get; set; }

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
            this.Id = 0;
            this.PestcideMaster = new PesticideMaster();
            this.PesticideId = 0;
            this.Used = 0;
        }

        /// <summary>
        /// クローン用コンストラクタ
        /// </summary>
        /// <param name="pesticideContent"></param>
        public PesticideContent(PesticideContent pesticideContent)
        {
            this.Id = pesticideContent.Id;
            this.PestcideMaster = pesticideContent.pestcideMaster;
            this.PesticideId = pesticideContent.PesticideId;
            this.Used = pesticideContent.Used;
        }

        /// <summary>
        /// ListView用にフォーマットした文字列を戻す
        /// </summary>
        /// <returns>フォーマットされた文字列</returns>
        public override string ToString()
        {
            return String.Format("{0}, {1} {2}", PestcideMaster.Name, Used, PestcideMaster.Unit);
        }

        #region IDataBase実装

        /// <summary>
        /// 登録可能チェック
        /// </summary>
        /// <returns>登録可能かフラグ</returns>
        public bool CanRegister()
        {
            // 必須項目の値を確認
            return 0 != this.Used;
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

            string query = "INSERT INTO T_PesticideContent(used, pesticide_id, work_content_id) VALUES(?, ?, ?)";
            List<object> addParams = new List<object>
            {
                this.Used,
                this.PesticideId,
                this.WorkContentId
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

            string query = "UPDATE T_PesticideContent SET used = ?, pesticide_id =? WHERE id = ?";
            List<object> addParams = new List<object>
            {
                this.Used,
                this.PesticideId,
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
            string query = "DELETE FROM T_PesticideContent WHERE id = ?";
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
