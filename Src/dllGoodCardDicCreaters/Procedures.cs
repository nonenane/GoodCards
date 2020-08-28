using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace dllGoodCardDicCreaters
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        #region "Справочник производителей"

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
        public async Task<DataTable> setProizvoditel(int id, string cName, string inn, int id_type_org, bool isActive, bool isDel, int result,bool isAutoIncriments)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cName);
            ap.Add(inn);
            ap.Add(id_type_org);
            ap.Add(isActive);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);
            ap.Add(isAutoIncriments);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_setProizvoditel]",
                 new string[9] { "@id", "@cName", "@inn", "@id_type_org", "@isActive", "@id_user", "@result", "@isDel","@isAutoIncriments" },
                 new DbType[9] { DbType.Int32, DbType.String, DbType.String, DbType.Int32, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean }, ap);

            return dtResult;
        }


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
        public async Task<DataTable> setAdresProizvod(int id, int id_proizvoditel, int id_subject, string cName, bool isActive, bool isDel, int result,bool isAutoIncriments)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(id_proizvoditel);
            ap.Add(id_subject);
            ap.Add(cName);
            ap.Add(isActive);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(result);
            ap.Add(isDel);
            ap.Add(isAutoIncriments);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_setAdresProizvod]",
                 new string[9] { "@id", "@id_proizvoditel", "@id_subject", "@cName", "@isActive", "@id_user", "@result", "@isDel","@isAutoIncriments" },
                 new DbType[9] { DbType.Int32, DbType.Int32, DbType.Int32, DbType.String, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean }, ap);

            return dtResult;
        }

        /// <summary>
        /// Получение списка стран субъектов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getProizvoditel()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getProizvoditel]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }


        /// <summary>
        /// Получение списка стран субъектов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getAdressProizvodVsProizvod(int id_proizvoditel)
        {
            ap.Clear();
            ap.Add(id_proizvoditel);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getAdressProizvodVsProizvod]",
                 new string[1] { "@id_proizvoditel" },
                 new DbType[1] { DbType.Int32 }, ap);

            return dtResult;
        }

        #endregion

        /// <summary>
        /// Получение списка стран субъектов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getTypeOrg()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getTypeOrg]",
                  new string[0] { },
                  new DbType[0] { }, ap);

            if (dtResult != null)
            {
                dtResult.DefaultView.RowFilter = "isActive = 1 and id <> 0 ";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }

            return dtResult;
        }

        /// <summary>
        /// Получение списка стран субъектов
        /// </summary>
        /// <param name=""></param>
        /// <returns>Таблица с данными</returns>        
        public async Task<DataTable> getSubjects()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getSubjects]",
                  new string[0] { },
                  new DbType[0] { }, ap);

            if (dtResult != null)
            {
                dtResult.DefaultView.RowFilter = "isActive = 1 and id <> 0 ";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }

            return dtResult;
        }

    }
}
