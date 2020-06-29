using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkTaskApp.Models
{
    public class WorkerMaster : BindableBase
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
            this.ID = 0;
            this.Name = "";
        }

        /// <summary>
        /// クローン用コンストラクタ
        /// </summary>
        /// <param name="workerMaster"></param>
        public WorkerMaster(WorkerMaster workerMaster)
        {
            this.ID = workerMaster.ID;
            this.Name = workerMaster.Name;
        }
    }
}
