using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace dllGoodCardDicGrp2
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

        public async Task<DataTable> getUniGrp(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getUniGrp]",
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

                    row["cName"] = "Все группы";
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

        #region "Справочник под.групп"

        public async Task<DataTable> setGrp2(int id, string cName, int id_otdel,int id_unigrp, int id_unit,bool specification,bool skoroportovar,decimal NettoMax,int DayMax, bool isActive, bool isDel, int result, bool isAutoIncriments)
        {
            ap.Clear();
            ap.Add(id);
            ap.Add(cName);
            ap.Add(id_otdel);

            ap.Add(id_unigrp);
            ap.Add(id_unit);
            ap.Add(specification);
            ap.Add(skoroportovar);
            ap.Add(NettoMax);
            ap.Add(DayMax);

            ap.Add(isActive);
            ap.Add(result);
            ap.Add(isDel);
            ap.Add(isAutoIncriments);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_setGrp2]",
                 new string[13] { "@id", "@cName", "@id_otdel", "@id_unigrp", "@id_unit", "@specification", "@skoroportovar", "@NettoMax", "@DayMax", "@isActive", "@result", "@isDel","@isAutoIncriments" },
                 new DbType[13] { DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean,DbType.Decimal,DbType.Int32, DbType.Boolean, DbType.Int32, DbType.Boolean, DbType.Boolean }, ap);

            return dtResult;
        }

        public async Task<DataTable> getGrp2()
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getGrp2]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            return dtResult;
        }

        #endregion

    
    }
}
