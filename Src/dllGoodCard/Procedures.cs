using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace dllGoodCardDicTypeOwnership
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        #region "Справочник форм собственности"

        /// <summary>
        /// Запись справочника форм собственности
        /// </summary>
        /// <param name="id">Код записи</param>
        /// <param name="cName">Наименование </param>
        /// <param name="Abbreviation">Аббревиатура</param>
        /// <param name="isActive">признак активности записи</param>  
        /// <param name="isDel">Признак удаления записи</param>
        /// <param name="result">Результирующая для проверки</param>
        /// <returns>Таблица с данными</returns>
        /// <param name="id">код созданной записи</param>
        public async Task<DataTable> setTypeOrg(int id, string cName, bool isActive, bool isDel, int result,bool isAutoIncriments)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cName);
            ap.Add(isActive);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);
            ap.Add(isAutoIncriments);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_setTypeOrg]",
                 new string[7] { "@id", "@cName", "@isActive", "@id_user", "@result", "@isDel","@isAutoIncriments" },
                 new DbType[7] { DbType.Int32, DbType.String, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean }, ap);

            return dtResult;
        }

        /// <summary>
        /// Получение списка форм собственности
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getTypeOrg()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getTypeOrg]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

        #endregion
    }
}
