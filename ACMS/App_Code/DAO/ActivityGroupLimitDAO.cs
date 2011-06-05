using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class ActivityGroupLimitDAO : BaseDAO
    {
        /// <summary>
        /// 檢查是否可以報名(若存在限制名單中或不用限制人員的活動則傳回true.)
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">員工</param>
        /// <returns>檢查是否可以報名(若存在限制名單中或不用限制人員的活動則傳回true.)</returns>
        public Boolean GroupLimitIsExist(string  activity_id, string emp_id)
        {
            StringBuilder sb = new StringBuilder();

            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.NVarChar );
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar);
            sqlParams[1].Value = emp_id;
            sb.AppendLine("SELECT id from Activity where id =@activity_id and is_grouplimit='N'");
            sb.AppendLine("SELECT activity_id,emp_id ");
            sb.AppendLine("FROM ActivityGroupLimit ");
            sb.AppendLine("WHERE activity_id = @activity_id  and emp_id=@emp_id ");

            sb.AppendLine("SELECT activity_id,B.id ");
            sb.AppendLine("FROM ActivityLimitCompany  A inner join  V_ACSM_USER2 B on A.COMPANY_CODE =B.COMPANY_CODE   ");
            sb.AppendLine("WHERE A.activity_id = @activity_id  and B.id=@emp_id ");


            DataSet DS = SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
            if (DS.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            if (DS.Tables[1].Rows.Count > 0)
            {
                return true;
            }
            if (DS.Tables[2].Rows.Count > 0)
            {
                return true;
            }

            return false;

        }
        /// <summary>
        /// 新增限制人員清單
        /// </summary>
        /// <param name="table">限制人員清單</param>
        /// <param name="activity_id">活動代號</param>
        public void UpdateDataSet(DataTable table, Guid activity_id)
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
        /// <summary>
        /// 上傳族群名單 
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <returns>上傳族群名單 </returns>
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
                try
                {
                    return DS.Tables[0];
                }
                finally
                {
                    if (DS != null) DS.Dispose();

                }
            }
            else
            {
                return null;
            }

        }


        //匯出族群名單
        /// <summary>
        /// 匯出族群名單
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <returns>匯出族群名單</returns>
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
                try
                {
                    return DS.Tables[0];
                }
                finally
                {
                    if (DS != null) DS.Dispose();
                }
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 取得活動限制人員清單
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <returns>取得活動限制人員清單</returns>
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
            SqlConnection aconn = MyConn();
            SqlDataReader MyDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), sqlParams);

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
            MyDataReader.Close();
            aconn.Close();
            if (MyDataReader != null) MyDataReader.Dispose();
            if (aconn != null) aconn.Dispose();
            return myEmployeeVOList;

        }


        /// <summary>
        /// 新增限制人員清單
        /// </summary>
        /// <param name="myActivityGroupLimitVO">限制人員清單</param>
        /// <param name="trans">資料庫交易物件</param>
        /// <returns>新增限制人員清單</returns>
        public int INSERT(VO.ActivityGroupLimitVO myActivityGroupLimitVO, SqlTransaction trans)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = myActivityGroupLimitVO.activity_id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 50);
            sqlParams[1].Value = myActivityGroupLimitVO.emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT ActivityGroupLimit ");
            sb.AppendLine("([activity_id],[emp_id]) ");
            sb.AppendLine("VALUES ");
            sb.AppendLine("(@activity_id,@emp_id); ");

            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sb.ToString(), sqlParams);
        }


        /// <summary>
        /// 刪除活動限制名冊
        /// </summary>
        /// <param name="id">流水編號</param>
        /// <returns>刪除活動限制名冊</returns>
        public int DELETE(int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DELETE ActivityGroupLimit WHERE id=@id; ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }

        /// <summary>
        /// 刪除活動限制名冊
        /// </summary>
        /// <param name="emp_id">員工</param>
        /// <param name="id">活動代號</param>
        /// <returns></returns>
        public int DELETE(string emp_id, Guid id)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = id;
            sqlParams[1] = new SqlParameter("@emp_id", SqlDbType.NVarChar, emp_id.Length);
            sqlParams[1].Value = emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DELETE ActivityGroupLimit WHERE activity_id=@id and emp_id=@emp_id  ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }
        /// <summary>
        /// 取得公司限制名單
        /// </summary>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        public DataTable GetLimitCompany(Guid activity_id)
        {

            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
           

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Select * from  ActivityLimitCompany WHERE activity_id=@activity_id   ");

            return SqlHelper.ExecuteDataset(MyConn(), CommandType.Text, sb.ToString(), sqlParams).Tables [0];
        }

         public void InsertLimitCompany(Guid activity_id,string COMPANY_CODE,string C_NAME)
        {

            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@activity_id", SqlDbType.UniqueIdentifier);
            sqlParams[0].Value = activity_id;
            sqlParams[1] = new SqlParameter("@COMPANY_CODE", SqlDbType.NVarChar, 36);
            sqlParams[1].Value = COMPANY_CODE;
            sqlParams[2] = new SqlParameter("@C_NAME", SqlDbType.NVarChar, 200);
            sqlParams[2].Value = C_NAME;
           

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("insert into   ActivityLimitCompany(activity_id,COMPANY_CODE,C_NAME) values (@activity_id,@COMPANY_CODE,@C_NAME)   ");

            SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
        }

         public void DeleteLimitCompany(int id )
         {

             SqlParameter[] sqlParams = new SqlParameter[1];

             sqlParams[0] = new SqlParameter("@id", SqlDbType.Int );
             sqlParams[0].Value = id;
           

             StringBuilder sb = new StringBuilder();

             sb.AppendLine("delete from  ActivityLimitCompany where id=@id");

             SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);
         }


        /// <summary>
        /// 比較不同的資料
        /// </summary>
        /// <param name="First">舊資料</param>
        /// <param name="Second">新資料</param>
        /// <returns>比較不同的資料</returns>

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
            try
            {
                return table;
            }
            finally
            {
                if (table != null) table.Dispose();


            }

        }
      
    }
}