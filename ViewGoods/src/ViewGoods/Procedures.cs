using System.Text;
using System.Collections;
using Nwuram.Framework.Data;
using Nwuram.Framework.Settings.Connection;
using System.Data;
using System;
using Nwuram.Framework.Settings.User;
using System.Threading.Tasks;
using System.Threading;

namespace ViewGoods 
{
    class Procedures : SqlProvider
    {
        public Procedures(string server, string database, string username, string password, string appName)
              : base(server, database, username, password, appName)
        {
        }
        ArrayList ap = new ArrayList();


        public async Task<DataTable> getShop(bool withAllDeps = false)
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

        public async Task<DataTable> getDepartments(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getDepartments]",
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

        public async Task<DataTable> getGrp1(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getGrp1]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (dtResult == null) return null;

            delColumn(dtResult, "id_nds");
            delColumn(dtResult, "nds");
            delColumn(dtResult, "nameDeps");
            delColumn(dtResult, "ntypeorg");
            delColumn(dtResult, "Abbriviation");
            delColumn(dtResult, "isCredit");
            delColumn(dtResult, "isWithSubGroups");


            if (withAllDeps)
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
                row["id_otdel"] = 0;
                row["isActive"] = true;
                row["isMain"] = 0;
                dtResult.Rows.Add(row);
                dtResult.AcceptChanges();
                dtResult.DefaultView.RowFilter = "isActive = 1";
                dtResult.DefaultView.Sort = "isMain asc, cName asc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }
            else
            {
                dtResult.DefaultView.RowFilter = "isActive = 1";
                dtResult.DefaultView.Sort = "cName asc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }

            return dtResult;
        }

        public async Task<DataTable> getGrp2(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getGrp2]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (dtResult == null) return null;

            delColumn(dtResult, "id_unigrp");
            delColumn(dtResult, "id_unit");
            delColumn(dtResult, "nameDeps");
            delColumn(dtResult, "nameUniGrp");
            delColumn(dtResult, "nameUnit");
            delColumn(dtResult, "specification");
            delColumn(dtResult, "skoroportovar");
            delColumn(dtResult, "NettoMax");
            delColumn(dtResult, "DayMax");


            if (withAllDeps)
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
                row["id_otdel"] = 0;
                row["isActive"] = true;
                row["isMain"] = 0;
                dtResult.Rows.Add(row);
                dtResult.AcceptChanges();
                dtResult.DefaultView.RowFilter = "isActive = 1";
                dtResult.DefaultView.Sort = "isMain asc, cName asc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }
            else
            {
                dtResult.DefaultView.RowFilter = "isActive = 1";
                dtResult.DefaultView.Sort = "cName asc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }

            return dtResult;
        }

        public async Task<DataTable> getGrp3(bool withAllDeps = false)
        {
            ap.Clear();

            DataTable dtResult = executeProcedure("[Goods_Card_New].[spg_getGrp3]",
                 new string[0] { },
                 new DbType[0] { }, ap);

            if (dtResult == null) return null;

            delColumn(dtResult, "nameDeps");

            if (withAllDeps)
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
                row["id_otdel"] = 0;
                row["isActive"] = true;
                row["isMain"] = 0;
                dtResult.Rows.Add(row);
                dtResult.AcceptChanges();
                dtResult.DefaultView.RowFilter = "isActive = 1";
                dtResult.DefaultView.Sort = "isMain asc, cName asc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }
            else
            {
                dtResult.DefaultView.RowFilter = "isActive = 1";
                dtResult.DefaultView.Sort = "cName asc";
                dtResult = dtResult.DefaultView.ToTable().Copy();
            }

            return dtResult;
        }

        private void delColumn(DataTable dt, string name)
        {
            if (dt.Columns.Contains(name)) dt.Columns.Remove(name);
        }

        public async Task<DataTable> getFindTovar(string ean,string cName, bool isNewGoods)
        {
            ap.Clear();
            ap.Add(ean);
            ap.Add(cName);
            ap.Add(Nwuram.Framework.Settings.Connection.ConnectionSettings.GetIdProgram());
            ap.Add(isNewGoods);

            DataTable dtResult = executeProcedure("[Goods_Card_New].[getFindTovar]",
                 new string[4] { "@ean", "@cName", "@id_prog", "@isNewGoods" },
                 new DbType[4] { DbType.String, DbType.String, DbType.Int32, DbType.Boolean }, ap);

            return dtResult;
        }


    }
}
