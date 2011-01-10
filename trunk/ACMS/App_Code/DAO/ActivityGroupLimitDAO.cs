using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class ActivityGroupLimitDAO : BaseDAO
    {

        public void UpdateDataSet(DataTable table,Guid activity_id )
        {

            //===========andy 修正為直接加入ActivityGroupLimit,因為只傳入Work_ID
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" insert into ActivityGroupLimit(activity_id,emp_id) select @activity_id,ID  from V_ACSM_USER2 where WORK_ID=@WORK_ID");
            
            foreach (DataRow dr in table.Rows)
            {
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
                sqlParams[0].Value = activity_id;
                sqlParams[1] = new SqlParameter("@WORK_ID", SqlDbType.NVarChar, 50);
                
                sqlParams[1].Value = dr[1].ToString();

                try
                {

                    SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
                }
                catch (Exception ex)
                {

                }
            }
            //===============================================()
         //   StringBuilder sb = new StringBuilder();

         //   sb.AppendFormat("SELECT A.activity_id,B.Work_ID, FROM ActivityGroupLimit A left join V_ACMS_USER2 B on A.emp_id=B.ID  WHERE 1=2");

         //   SqlCommand mySqlCommand = new SqlCommand(sb.ToString());
         //   mySqlCommand.Connection = MyConn();

         //   SqlDataAdapter adapter = new SqlDataAdapter();
         //   adapter.SelectCommand = mySqlCommand;
         //   SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

         ////
         //   DataTable DT = SelectCheckExistEmployeeByActivity_id(activity_id);


         //   if (DT != null && DT.Rows.Count > 0)
         //   {
         //       table = Difference(table, DT);               
         //   }

         //   int n = adapter.Update(table);

        }

        //上傳族群名單 
        public DataTable SelectCheckExistEmployeeByActivity_id(Guid activity_id)
        {
            StringBuilder sb = new StringBuilder();

            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            sb.AppendLine("SELECT activity_id,emp_id ");
            sb.AppendLine("FROM ActivityGroupLimit ");
            sb.AppendLine("WHERE activity_id = @activity_id ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);


            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                return DS.Tables[0];
            }
            else
            {
                return null;
            }

        }


        //匯出族群名單
        public DataTable SelectEmployeeByActivity_id(Guid activity_id)
        {
             StringBuilder sb = new StringBuilder();

           SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            sb.AppendLine("SELECT B.WORK_ID,B.NATIVE_NAME,B.OFFICE_PHONE,B.OFFICE_MAIL,B.DEPT_ID,B.C_DEPT_NAME,B.C_DEPT_ABBR ");
            sb.AppendLine("FROM ActivityGroupLimit A ");
            sb.AppendLine("left join V_ACSM_USER2 B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE activity_id = @activity_id ");

            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);


            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                return DS.Tables[0];
            }
            else
            {
                return null;
            }

        }


        public List<VO.EmployeeVO> SelectByActivity_id(Guid activity_id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.id as keyID,B.ID,B.WORK_ID,B.NATIVE_NAME,B.C_DEPT_NAME,B.C_DEPT_ABBR ");
            sb.AppendLine("FROM ActivityGroupLimit A ");
            sb.AppendLine("inner join V_ACSM_USER2 B on A.emp_id=B.ID ");
            sb.AppendLine("WHERE A.activity_id = @activity_id ");

            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

            List<VO.EmployeeVO> myEmployeeVOList = new List<ACMS.VO.EmployeeVO>();

            while (MyDataReader.Read())
            {
                VO.EmployeeVO myEmployeeVO = new ACMS.VO.EmployeeVO();

                myEmployeeVO.keyID = (int)MyDataReader["keyID"];
                myEmployeeVO.ID = (string)MyDataReader["ID"];
                myEmployeeVO.WORK_ID = (string)MyDataReader["WORK_ID"];
                myEmployeeVO.NATIVE_NAME = (string)MyDataReader["NATIVE_NAME"];
                myEmployeeVO.C_DEPT_ABBR = (string)MyDataReader["C_DEPT_ABBR"];
                myEmployeeVO.C_DEPT_NAME = (string)MyDataReader["C_DEPT_NAME"];
                myEmployeeVOList.Add(myEmployeeVO);

            }

            return myEmployeeVOList;

        }



        public int INSERT(VO.ActivityGroupLimitVO myActivityGroupLimitVO,SqlTransaction trans)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = myActivityGroupLimitVO.activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar,50);
            sqlParams[1].Value = myActivityGroupLimitVO.emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT ActivityGroupLimit ");
            sb.AppendLine("([activity_id],[emp_id]) ");
            sb.AppendLine("VALUES ");
            sb.AppendLine("(@activity_id,@emp_id); ");

            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sb.ToString(), sqlParams);
        }



        public int DELETE(int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = id;
           
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DELETE ActivityGroupLimit WHERE id=@id; ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }




        public DataTable Difference(DataTable First, DataTable Second)
        {
            //Create Empty Table
            DataTable table = new DataTable("Difference");
            //Must use a Dataset to make use of a DataRelation object
            using (DataSet ds = new DataSet())
            {
                //Add tables
                ds.Tables.AddRange(new DataTable[] { First.Copy(), Second.Copy() });
                //Get Columns for DataRelation
                DataColumn[] firstcolumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstcolumns.Length; i++)
                {
                    firstcolumns[i] = ds.Tables[0].Columns[i];
                }
                DataColumn[] secondcolumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondcolumns.Length; i++)
                {
                    secondcolumns[i] = ds.Tables[1].Columns[i];
                }
                //Create DataRelation
                DataRelation r = new DataRelation(string.Empty, firstcolumns, secondcolumns, false);
                ds.Relations.Add(r);
                //Create columns for return table
                for (int i = 0; i < First.Columns.Count; i++)
                {
                    table.Columns.Add(First.Columns[i].ColumnName, First.Columns[i].DataType);
                }
                //If First Row not in Second, Add to return table.
                table.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r);
                    if (childrows == null || childrows.Length == 0)
                        table.LoadDataRow(parentrow.ItemArray, false);
                }
                table.EndLoadData();
            }
            return table;
        }


    }
}
