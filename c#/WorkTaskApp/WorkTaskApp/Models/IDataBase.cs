using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkTaskApp.Models
{
    /// <summary>
    /// データベース利用インターフェース
    /// </summary>
    interface IDataBase
    {
        /// <summary>
        /// DB登録可能か
        /// </summary>
        bool CanRegister();

        /// <summary>
        /// DBレコードの登録
        /// </summary>
        bool RegisterDbRecord();

        /// <summary>
        /// DBレコードの更新
        /// </summary>
        bool UpdateDbRecord();

        /// <summary>
        /// DBレコードの削除
        /// </summary>
        bool DeleteDbRecord();
    }
}
