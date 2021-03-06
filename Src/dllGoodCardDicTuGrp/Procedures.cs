﻿using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace dllGoodCardDicTuGrp
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();

        public async Task<DataTable> getDepartments(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getDepartments]",
                 new string[0] { },
                 new DbType[0] { }, ap);

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

                    row["cName"] = "Все Отделы";
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

        public async Task<DataTable> getNTypeOrg(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getNTypeOrg]",
                 new string[0] { },
                 new DbType[0] { }, ap);

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

                    row["cName"] = "Все ЮЛ";
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

        public async Task<DataTable> getNds(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getNds]",
                 new string[0] { },
                 new DbType[0] { }, ap);

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

                    row["cName"] = "Все НДС";
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

        #region "Справочник Ту групп"

        public async Task<DataTable> setGrp1(int id, string cName, int id_otdel, int id_nds, int? ntypeorg,bool isCredit,bool isWithSubGroups, bool isActive, bool isDel, int result, bool isAutoIncriments)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cName);
            ap.Add(id_otdel);
            ap.Add(id_nds);
            ap.Add(ntypeorg);
            ap.Add(isCredit);
            ap.Add(isWithSubGroups);
            ap.Add(isActive);
            ap.Add(result);
            ap.Add(isDel);
            ap.Add(isAutoIncriments);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_setGrp1]",
                 new string[11] { "@id", "@cName", "@id_otdel", "@id_nds", "@ntypeorg", "@isCredit", "@isWithSubGroups", "@isActive", "@result", "@isDel","@isAutoIncriments" },
                 new DbType[11] { DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Int32, DbType.Boolean, DbType.Boolean }, ap);

            return dtResult;
        }

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
     

        public async Task<DataTable> getGrp1()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getGrp1]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

        public async Task<DataTable> getGrp1VsGrp2(int id_grp1)
        {
            ap.Clear();
            ap.Add(id_grp1);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getGrp1VsGrp2]",
                 new string[1] { "@id_grp1" },
                 new DbType[1] { DbType.Int32 }, ap);

            return dtResult;
        }

        public async Task<DataTable> setGrp1VsGrp2(int id_grp1, int id_grp2, bool isDel,bool isAutoIncriments)
        {
            ap.Clear();
            ap.Add(id_grp1);
            ap.Add(id_grp2);
            ap.Add(isDel);
            ap.Add(isAutoIncriments);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_setGrp1VsGrp2]",
                 new string[4] { "@id_grp1","@id_grp2", "@isDel", "@isAutoIncriments" },
                 new DbType[4] { DbType.Int32, DbType.Int32,DbType.Boolean, DbType.Boolean }, ap);

            return dtResult;
        }

        public async Task<DataTable> getUserVsGrp1(int id_grp1)
        {
            ap.Clear();
            ap.Add(id_grp1);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getUserVsGrp1]",
                 new string[1] { "@id_grp1" },
                 new DbType[1] { DbType.Int32 }, ap);

            return dtResult;
        }

        #endregion

        public async Task<DataTable> getGrp2()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getGrp2]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

    }
}
