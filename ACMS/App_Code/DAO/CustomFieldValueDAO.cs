﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class CustomFieldValueDAO : BaseDAO
    {
        /// <summary>
        /// 取得自訂欄位值的資料
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">報名人</param>
        /// <returns>取得自訂欄位值的資料</returns>
        public List<VO.CustomFieldValueVO> SelectCustomFieldValue(Guid activity_id, string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT  A.field_name,A.field_control,B.* ");
            sb.AppendLine("FROM CustomField A ");
            sb.AppendLine("left join CustomFieldValue B on A.field_id=B.field_id ");
            sb.AppendLine("WHERE A.activity_id=@activity_id and B.emp_id=@emp_id ");
            SqlConnection aconn = MyConn();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

            List<VO.CustomFieldValueVO> myCustomFieldValueVOList = new List<ACMS.VO.CustomFieldValueVO>();

            while (MyDataReader.Read())
            {
                VO.CustomFieldValueVO myCustomFieldValueVO = new ACMS.VO.CustomFieldValueVO();

                myCustomFieldValueVO.field_control = (string)MyDataReader["field_control"];
                myCustomFieldValueVO.id = (Guid)MyDataReader["id"];
                myCustomFieldValueVO.emp_id = (string)MyDataReader["emp_id"];
                myCustomFieldValueVO.field_id = (int)MyDataReader["field_id"];
                myCustomFieldValueVO.field_value = (string)MyDataReader["field_value"];
                myCustomFieldValueVO.field_name = (string)MyDataReader["field_name"];

                myCustomFieldValueVOList.Add(myCustomFieldValueVO);

            }
            MyDataReader.Close();
            aconn.Close();
            if (MyDataReader != null) MyDataReader.Dispose();
            if (aconn != null) aconn.Dispose();
            return myCustomFieldValueVOList;

        }


        public void  DeleteCustomFieldValue(Guid activity_id, string emp_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("delete from CustomFieldValue where field_id in ( ");
            sb.AppendLine("select field_id from  CustomField A  where A.activity_id=@activity_id ) ");
          
            sb.AppendLine(" and emp_id=@emp_id ");
            SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }

    }
}
