﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class CustomFieldDAO : BaseDAO
    {
        /// <summary>
        /// 新增1筆自訂欄資料
        /// </summary>
        /// <param name="myCustomFieldVO">自訂欄資料型別物件</param>
        /// <returns>新增1筆自訂欄資料</returns>

        public int INSERT(VO.CustomFieldVO myCustomFieldVO)
        {

            //field_id欄位自動識別遞增
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = myCustomFieldVO.activity_id;
            sqlParams[1] = new SqlParameter("@field_name", SqlDbType.NText);
            sqlParams[1].Value = myCustomFieldVO.field_name;
            sqlParams[2] = new SqlParameter("@field_control", SqlDbType.NText);
            sqlParams[2].Value = myCustomFieldVO.field_control;


            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT CustomField ");
            sb.AppendLine("([activity_id],[field_name],[field_control]) ");
            sb.AppendLine("VALUES ");
            sb.AppendLine("(@activity_id,@field_name,@field_control); ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }
        /// <summary>
        /// 取得活動的自訂欄位設定
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <returns>取得活動的自訂欄位設定</returns>
        public List<VO.CustomFieldVO> SelectByActivity_id(Guid activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT activity_id, field_id, field_name, field_control,CASE field_control WHEN 'textbox' THEN 'false' ELSE 'true' END as IsShowEdit ");
            sb.AppendLine("FROM CustomField WHERE (activity_id = @activity_id) ");
            SqlConnection aconn = MyConn();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            List<VO.CustomFieldVO> myCustomFieldVOList = new List<ACMS.VO.CustomFieldVO>();

            while (MyDataReader.Read())
            {
                VO.CustomFieldVO myCustomFieldVO = new ACMS.VO.CustomFieldVO();

                myCustomFieldVO.activity_id = (Guid)MyDataReader["activity_id"];
                myCustomFieldVO.field_id = (int)MyDataReader["field_id"];
                myCustomFieldVO.field_name = (string)MyDataReader["field_name"];
                myCustomFieldVO.field_control = (string)MyDataReader["field_control"];
                myCustomFieldVO.isShowEdit = Convert.ToBoolean(MyDataReader["IsShowEdit"]);

                myCustomFieldVOList.Add(myCustomFieldVO);

            }
            MyDataReader.Close();
            aconn.Close();
            if (MyDataReader != null) MyDataReader.Dispose();
            if (aconn != null) aconn.Dispose();
            return myCustomFieldVOList;

        }
        /// <summary>
        /// 刪除自訂欄位設定
        /// </summary>
        /// <param name="field_id">流水號</param>
        /// <returns>刪除自訂欄位設定</returns>
        public int DELETE(int field_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@field_id", SqlDbType.Int);
            sqlParams[0].Value = field_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DELETE CustomField WHERE field_id=@field_id; ");
            sb.AppendLine("DELETE CustomFieldItem WHERE field_id=@field_id; ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }
        /// <summary>
        /// 選出某一活動有選項的欄位但是尚未編輯選項的資料有多少個欄位
        /// </summary>
        /// <param name="activity_id">活動編號</param>
        /// <returns>選出某一活動有選項的欄位但是尚未編輯選項的資料有多少個欄位</returns>
        public DataTable CheckCustFieldItemEmpty(Guid activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" Select *  from CustomField A ");
            sb.AppendLine(" left join CustomFieldItem B on A.field_id =B.field_id ");
            sb.AppendLine(" where A.activity_id =@activity_id  and A.field_control like '%list%'");
            sb.AppendLine(" and B.field_id  is null  ");

            return SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams).Tables[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        public DataTable CheckCustFieldItemOutOfRangInt(Guid activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select B.field_id ,B.field_name ,COUNT(A.field_id ) ,SUM(convert(int,A.field_item_text )) as Ttl  from dbo.CustomFieldItem A left join CustomField B on A.field_id =b.field_id ");
            sb.AppendLine("where A.field_id in (select field_id  from dbo.CustomField  where activity_id =@activity_id and field_control ='textboxlist')");
            sb.AppendLine("group by  B.field_id ,B.field_name ");
            sb.AppendLine("having SUM(convert(int,A.field_item_text )) >2000000000  or SUM(convert(int,A.field_item_text )) <-2000000000 ");

            return SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams).Tables[0];
        }
        /// <summary>
        /// 檢查是否有選項重覆的名稱
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <returns>檢查是否有選項重覆的名稱</returns>
        public DataTable CheckCustFieldItemNameDuplicate(Guid activity_id)
        {

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" Select a.field_name+'_'+b.field_item_name as field_item_name ,COUNT(*) as aa  from CustomField A ");
            sb.AppendLine("  left join CustomFieldItem B on A.field_id =B.field_id ");
            sb.AppendLine("    where A.activity_id =@activity_id  and A.field_control like '%list%'");
            sb.AppendLine("    group by b.field_item_name, a.field_name");
            sb.AppendLine("       having COUNT(*)>1");
            return SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams).Tables[0];
        }

        /// <summary>
        /// 檢查是否有重覆的名稱
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <returns>檢查是否有重覆的名稱</returns>
        public DataTable CheckCustFieldNameDuplicate(Guid activity_id)
        {

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" Select a.field_name,COUNT(*) as aa  from CustomField A ");
            sb.AppendLine("    where A.activity_id =@activity_id  ");
            sb.AppendLine("    group by  a.field_name");
            sb.AppendLine("       having COUNT(*)>1");
            return SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams).Tables[0];
        }
    }
}
