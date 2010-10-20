﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class CustomFieldItemDAO : BaseDAO
    {

        public int INSERT(VO.CustomFieldItemVO myCustomFieldItemVO)
        {
            //field_item_id欄位自動識別遞增
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@field_id", SqlDbType.Int);
            sqlParams[0].Value = myCustomFieldItemVO.field_id;
            sqlParams[1] = new SqlParameter("@field_item_name", SqlDbType.NText);
            sqlParams[1].Value = myCustomFieldItemVO.field_item_name;
            sqlParams[2] = new SqlParameter("@field_item_text", SqlDbType.NText);
            sqlParams[2].Value = myCustomFieldItemVO.field_item_text;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT CustomFieldItem ");
            sb.AppendLine("([field_id],[field_item_name],[field_item_text]) ");
            sb.AppendLine("VALUES ");
            sb.AppendLine("(@field_id,@field_item_name,@field_item_text); ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }

        public List<VO.CustomFieldItemVO> SelectByField_id(int field_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@field_id", SqlDbType.Int);
            sqlParams[0].Value = field_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT field_id, field_item_id, field_item_name, field_item_text ");
            sb.AppendLine("FROM CustomFieldItem WHERE (field_id = @field_id) ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            List<VO.CustomFieldItemVO> myCustomFieldItemVOList = new List<ACMS.VO.CustomFieldItemVO>();
          
            while (MyDataReader.Read())
            {
                VO.CustomFieldItemVO myCustomFieldItemVO = new ACMS.VO.CustomFieldItemVO();

                myCustomFieldItemVO.field_id = (int)MyDataReader["field_id"];
                myCustomFieldItemVO.field_item_id = (int)MyDataReader["field_item_id"];
                myCustomFieldItemVO.field_item_name = (string)MyDataReader["field_item_name"];
                myCustomFieldItemVO.field_item_text = (string)MyDataReader["field_item_text"];

                myCustomFieldItemVOList.Add(myCustomFieldItemVO);
              
            }

            return myCustomFieldItemVOList;

        }

        public int DELETE(int field_item_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@field_item_id", SqlDbType.Int);
            sqlParams[0].Value = field_item_id;
           
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DELETE CustomFieldItem WHERE field_item_id=@field_item_id; ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }


    }
}
