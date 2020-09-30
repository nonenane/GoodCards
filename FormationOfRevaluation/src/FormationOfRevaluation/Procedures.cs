using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.User;
using Nwuram.Framework.Settings.Connection;
using System.Collections;
using System.Runtime.InteropServices;

namespace FormationOfRevaluation
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
            : base(server, database, username, password, appName)
        {
        }

        ArrayList ap = new ArrayList();
        
        /// <summary>
        /// Процедура получения текущей даты с сервера
        /// </summary>
        /// <returns>Текущая дата</returns>
        public DateTime getdate()
        {
            DataTable dt = new DataTable();

            ap.Clear();
            
            dt = executeProcedure("[Goods_Card].[getDate]",
                    new string[] { },
                    new DbType[] { }, ap);

            if ((dt != null) && (dt.Rows.Count != 0))
            {
                return DateTime.Parse(dt.Rows[0][0].ToString()).Date;
            }
            else
            {
                return DateTime.Now.Date;
            }
        }

        /// <summary>
        /// Получение списка отделов
        /// </summary>        
        /// <returns>Список отделов</returns>
        public DataTable GetDeps()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card].[GetDeps]",
                    new string[] { },
                    new DbType[] { }, ap);

            return dtResult;
        }

        public DataTable GetTypRequest()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card].[GetTypRequest]",
                    new string[] { },
                    new DbType[] { }, ap);

            return dtResult;
        }

        /// <summary>
        /// Процедура получения списка заявок за период по отделу
        /// </summary>
        /// <param name="d1">дата начала выборки</param>
        /// <param name="d2">дата конца выборки</param>
        /// <param name="id_dep">id отдела</param>
        /// <param name="id_typeProperties">id типа заявок</param>
        /// <returns>список заявок</returns>
        public DataTable GetReq(DateTime d1, DateTime d2, int id_dep, int id_typeProperties)
        {
            ap.Clear();
            ap.Add(d1);
            ap.Add(d2);
            ap.Add(id_dep);
            ap.Add(id_typeProperties);

            DataTable dtResult = executeProcedure("[Goods_Card].[GetRequestsForRevaluation]",
                    new string[] { "@d1", "@d2", "@id_dep", "@id_typeProperties" },
                    new DbType[] { DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.Int32}, ap);

            return dtResult;
        }

        /// <summary>
        /// Процедура получения содержимого заявки
        /// </summary>
        /// <param name="id_trequest">id заявки</param>
        /// <returns>таблица с содержимым заявки</returns>
        public DataTable GetReqDetails(int id_trequest)
        {
            ap.Clear();
            ap.Add(id_trequest);            

            DataTable dtResult = executeProcedure("[Goods_Card].[GetRequestDetailsForRevaluation]",
                    new string[] { "@id_trequest" },
                    new DbType[] { DbType.Int32 }, ap);

            return dtResult;
        }

        /// <summary>
        /// Процедура добавления / обновления переоценки (записи в s_rcena_future)
        /// </summary>
        /// <param name="date">дата заявки</param>
        /// <param name="id_tovar">id товара</param>
        /// <param name="rcena">будущая цена</param>
        /// <returns>таблица: new - признак добавления (1), обновления (0) записи
        /// old_rcena - старая цена переоценки на дату,
        /// id - id добавленной / обновленной записи
        /// </returns>
        public DataTable AddEditRcena(DateTime date, int id_tovar, decimal rcena)
        {
            ap.Clear();
            ap.Add(date);
            ap.Add(id_tovar);
            ap.Add(rcena);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);

            DataTable dtResult = executeProcedure("[Goods_Card].[AddEditRcena]",
                    new string[] { "@date", "@id_tovar", "@rcena", "@id_User" },
                    new DbType[] { DbType.DateTime, DbType.Int32, DbType.Decimal, DbType.Int32 }, ap);

            return dtResult;
        }

        /// <summary>
        /// Процедура проставления у заявки признака наличия переоценки
        /// </summary>
        /// <param name="id_treq">id заявки</param>        
        public void UpdateTreq(int id_treq)
        {
            ap.Clear();
            ap.Add(id_treq);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            

            executeProcedure("[Goods_Card].[UpdateTreq]",
                    new string[] { "@id_treq", "@id_User" },
                    new DbType[] { DbType.Int32, DbType.Int32 }, ap);                        
        }

        /// <summary>
        /// Обновление акта переоценки
        /// </summary>
        /// <param name="id_request">id записи</param>
        /// <param name="zcena">Цена закупки</param>
        /// <param name="delete">Признак удаления</param>
        /// <returns></returns>
        public DataTable UpdateReq(int id_request, decimal zcena, int delete)
        {
            ap.Clear();
            ap.Add(id_request);
            ap.Add(zcena);
            ap.Add(delete);            

            DataTable dtResult = executeProcedure("[Goods_Card].[UpdateReq]",
                    new string[] { "@id_request", "@zcena", "@delete" },
                    new DbType[] { DbType.Int32, DbType.Decimal, DbType.Int32 }, ap);

            return dtResult;
        }

        /// <summary>
        /// Обновление акта переоценки (добавление товара)
        /// </summary>
        /// <param name="id_request">id записи</param>
        /// <param name="zcenabnds">цена закупки без НДС</param>
        /// <param name="id_trequest">id акта</param>
        /// <param name="id_tovar">id товара</param>
        /// <param name="netto">кол-во</param>
        /// <param name="rcena">цена</param>
        public void UpdateReq(int id_request, decimal zcenabnds, int id_trequest, int id_tovar, decimal netto, decimal rcena)
        {
            ap.Clear();
            ap.AddRange(new object[7] { id_request, zcenabnds, 0, id_trequest, id_tovar, netto, rcena });

            executeProcedure("[Goods_Card].[UpdateReq]",
                   new string[7] { "@id_request", "@zcena", "@delete", "@id_trequest", "@id_tovar", "@netto", "@rcena" },
                   new DbType[7] { DbType.Int32, DbType.Decimal, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Decimal, DbType.Decimal }, 
                   ap);       
        }

        /// <summary>
        /// Получение информации о товаре
        /// </summary>
        /// <param name="ean">Код товара</param>
        /// <param name="idTrequest">ID акта</param>
        /// <returns>Таблица с информацией о товаре</returns>
        public DataTable GetGoodByEan(string ean, int idTrequest)
        {
            ap.Clear();
            ap.Add(ean);
            ap.Add(idTrequest);

            return executeProcedure("[Goods_Card].[GetGoodInformation]",
                                    new string[2] { "@ean", "@id_trequest" }, 
                                    new DbType[2] { DbType.String, DbType.Int32 }, ap);
        }
        
        /// <summary>
        /// Процедура добавления шапки в j_allprihod 
        /// </summary>
        /// <param name="date">дата накладной</param>
        /// <param name="idTrequest">id заявки</param>
        /// <param name="ntypeorg">номер юл</param>
        /// <returns>id добавленной накладной</returns>
        public int AddAllprihod(DateTime date, int idTrequest, int ntypeorg)
        {
            int res = 0;

            ap.Clear();
            ap.Add(date);
            ap.Add(ConnectionSettings.GetIdProgram());
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(idTrequest);
            ap.Add(ntypeorg);            

            DataTable dt = new DataTable();
            dt = executeProcedure("[Goods_Card].[AddAllprihod]",
                 new string[] { "@date", "@id_prog", "@id_user", "@id_treq", "@ntypeorg" },
                 new DbType[] { DbType.DateTime, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32 }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                res = int.Parse(dt.Rows[0][0].ToString());
                Config.TTN = dt.Rows[0][1].ToString();
            }

            return res;
        }
        
        /// <summary>
        /// Процедура проверки наличия товара в актах переоценки за дату с той же ценой
        /// </summary>
        /// <param name="id_tovar">id товара</param>
        /// <param name="date">дата</param>
        /// <param name="curprice">текущая цена</param>
        /// <param name="newprice">новая цена</param>
        /// <returns>признак наличия товара за дату с той же ценой в актах переоценки</returns>
        public bool AnotherGoodPereocExists(int id_tovar, DateTime date, decimal curprice, decimal newprice)
        {
            int res = 0;

            ap.Clear();
            ap.Add(id_tovar);
            ap.Add(date);
            ap.Add(curprice);
            ap.Add(newprice);

            DataTable dt = new DataTable();
            dt = executeProcedure("[Goods_Card].[CheckAnotherPereocWGoods]",
                 new string[] { "@id_tovar", "@date", "@curprice", "@newprice" },
                 new DbType[] { DbType.Int32, DbType.DateTime, DbType.Decimal, DbType.Decimal }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                res = int.Parse(dt.Rows[0][0].ToString());
            }

            return (res>0);
        }

        /// <summary>
        /// Процедура добавления тела накладной переоценки
        /// </summary>
        /// <param name="id_all">id шапки накладной</param>
        /// <param name="id_tovar">id товара</param>
        /// <param name="newprice">новая цена</param>
        /// <param name="curprice">текущая цена</param>
        /// <param name="id_dep">id отдела</param>
        /// <param name="npp">номер по порядку</param>
        /// <param name="id_req">id заявки</param>
        /// <returns>id добавленной записи в j_pereoc</returns>
        public int AddPereoc(int id_all, int id_tovar, decimal newprice, decimal curprice, int id_dep, string npp, int id_req)
        {
            int res = 0;

            ap.Clear();
            ap.Add(id_all);
            ap.Add(id_tovar);
            ap.Add(newprice);
            ap.Add(curprice);
            ap.Add(id_dep);
            ap.Add(npp);
            ap.Add(Nwuram.Framework.Settings.User.UserSettings.User.Id);
            ap.Add(id_req);
            

            DataTable dt = new DataTable();
            dt = executeProcedure("[Goods_Card].[AddPereoc]",
                 new string[] { 
                     "@id_all", "@id_tovar", "@newprice", 
                     "@curprice", "@id_dep", "@npp", 
                     "@id_user", "@id_req" },
                 new DbType[] { 
                     DbType.Int32, DbType.Int32, DbType.Decimal, 
                     DbType.Decimal, DbType.Int32, DbType.String, 
                     DbType.Int32, DbType.Int32 }, ap);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                res = int.Parse(dt.Rows[0][0].ToString());
            }

            return res;
        }

        /// <summary>
        /// Процедура удаления шапки накладной
        /// </summary>
        /// <param name="id_all">id накладной</param>
        public void DelAllprihod(int id_all)
        {
            ap.Clear();
            ap.Add(id_all);
                        
            executeProcedure("[Goods_Card].[DelAllprihod]",
                 new string[] { 
                     "@id" },
                 new DbType[] { 
                     DbType.Int32 }, ap);
        }

        /// <summary>
        /// Процедура взятия настроек
        /// </summary>
        /// <param name="id_prog">id программы</param>
        /// <param name="id_value">код настройки</param>
        public int getSettings(int id_prog, string id_value)
        {
            ap.Clear();
            ap.Add(id_prog);
            ap.Add(id_value);

            DataTable result = executeProcedure("[Goods_Card].[getSettings]",
                 new string[] {
                     "@id_prog", "@id_value" },
                 new DbType[] {
                     DbType.Int32, DbType.String }, ap);
            if (result.Rows.Count > 0)
                return int.Parse(result.Rows[0][0].ToString());
            else return 0;
        }

        public DataTable getShop(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[getShop]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (dtResult == null) return null;

            if (withAllDeps)
            {
                if (dtResult != null)
                {
                    if (!dtResult.Columns.Contains("isMain"))
                    {
                        DataColumn col = new DataColumn("isMain", typeof(int));
                        col.DefaultValue = 1;
                        dtResult.Columns.Add(col);
                        dtResult.AcceptChanges();
                    }

                    DataRow row = dtResult.NewRow();

                    row["cName"] = "Все магазины";
                    row["id"] = 0;
                    row["isMain"] = 0;
                    dtResult.Rows.Add(row);
                    dtResult.AcceptChanges();
                    dtResult.DefaultView.Sort = "isMain asc, id asc";
                    dtResult = dtResult.DefaultView.ToTable().Copy();
                }
            }
            else
            {
                dtResult.DefaultView.Sort = "id asc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }

            return dtResult;
        }

        public DataTable getRcenaFuture(DateTime date)
        {
            ap.Clear();
            ap.Add(date);

            return executeProcedure("[Goods_Card_New].[getRcenaFuture]",
                 new string[1] {
                     "@date" },
                 new DbType[1] {
                     DbType.Date}, ap);
        }

        public DataTable setRcenaFuture(int id_tovar, DateTime date,decimal rcena,bool isDel)
        {
            ap.Clear();
            ap.Add(id_tovar);
            ap.Add(date);
            ap.Add(rcena);
            ap.Add(UserSettings.User.Id);
            ap.Add(isDel);

            return executeProcedure("[Goods_Card_New].[setRcenaFuture]",
                 new string[5] {
                     "@id_tovar",
                     "@date",
                     "@rcena",
                     "@id_user",
                     "@isDel"},
                 new DbType[5] {
                     DbType.Int32,
                     DbType.Date,
                     DbType.Decimal,
                     DbType.Int32,
                     DbType.Boolean},
                 ap);
        }

        public DataTable setGoodsUpdate(int id_departments, int tax, int grp, int dpt, int price, string name, string ean)
        {
            ap.Clear();
            ap.Add(id_departments);
            ap.Add(UserSettings.User.Id);
            ap.Add(tax);
            ap.Add(grp);
            ap.Add(dpt);
            ap.Add(price);
            ap.Add(name);
            ap.Add(ean);




            return executeProcedure("[Goods_Card_New].[setGoodsUpdate]",
                 new string[8] {
                     "@id_departments",
                     "@sender",
                     "@tax",
                     "@grp",
                     "@dpt",
                     "@price",
                     "@name",
                     "@ean"},
                 new DbType[8] {
                     DbType.Int32,
                     DbType.String,
                     DbType.Int16,
                     DbType.Int32,
                     DbType.Int32,
                     DbType.Int32,
                     DbType.String,
                     DbType.String},
                 ap);
        }
    }
}
