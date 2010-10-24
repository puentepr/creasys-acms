using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class CustomFieldDAO : BaseDAO
    {

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

        public List<VO.CustomFieldVO> SelectByActivity_id(Guid activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT activity_id, field_id, field_name, field_control,CASE field_control WHEN 'textbox' THEN 'false' ELSE 'true' END as IsShowEdit ");
            sb.AppendLine("FROM CustomField WHERE (activity_id = @activity_id) ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

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

            return myCustomFieldVOList;

        }

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


    }
}
