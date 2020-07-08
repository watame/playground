using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkTaskApp.Models
{
    public class WorkerMaster : BindableBase, IDataBase
    {
        /// <summary>
        /// ID
        /// </summary>
        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        /// <summary>
        /// 作業者名
        /// </summary>
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WorkerMaster()
        {
            this.Id = 0;
            this.Name = "";
        }

        /// <summary>
        /// クローン用コンストラクタ
        /// </summary>
        /// <param name="workerMaster"></param>
        public WorkerMaster(WorkerMaster workerMaster)
        {
            this.Id = workerMaster.Id;
            this.Name = workerMaster.Name;
        }

        #region IDataBase実装

        public bool CanRegister()
        {
            // 登録可能フラグ
            List<bool> canRegisterFlags = new List<bool>
            {
                String.IsNullOrWhiteSpace(this.Name),
            };

            // 登録可能フラグを確認し、登録可能か（フラグにtrueが1つも含まれていない）真偽値を戻す
            return (-1 == canRegisterFlags.FindIndex(canRegisterFlag => true == canRegisterFlag));
        }

        public bool RegisterDbRecord()
        {
            if (!CanRegister())
            {
                return false;
            }

            string query = "INSERT INTO M_Worker(name) VALUES(?)";
            List<object> addParams = new List<object>
            {
                this.Name
            };
            DataBaseManager.DBManager.ExecuteNonQuery(query, addParams);

            return true;
        }

        public bool UpdateDbRecord()
        {
            if (0 == this.Id || !CanRegister())
            {
                return false;
            }

            string query = "UPDATE M_Worker SET name = ? WHERE id = ?";
            List<object> addParams = new List<object>
            {
                this.Name,
                this.Id
            };
            DataBaseManager.DBManager.ExecuteNonQuery(query, addParams);

            return true;
        }

        public bool DeleteDbRecord()
        {
            if (0 == this.Id)
            {
                return false;
            }

            string query = "DELETE FROM M_Worker WHERE id = ?";
            List<object> addParams = new List<object>
            {
                this.Id
            };
            DataBaseManager.DBManager.ExecuteNonQuery(query, addParams);

            return true;
        }

        #endregion IDataBase実装

        /// <summary>
        /// ListView用にフォーマットした文字列を戻す
        /// </summary>
        /// <returns>フォーマットされた文字列</returns>
        public override string ToString()
        {
            return String.Format("作業者：{0}", this.Name);
        }

    }
}
