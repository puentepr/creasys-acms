using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class CustomFieldValueDAO : BaseDAO
    {

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

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

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

            return myCustomFieldValueVOList;

        }

   

    }
}
