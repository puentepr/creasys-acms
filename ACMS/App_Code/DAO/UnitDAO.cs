using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class UnitDAO : BaseDAO
    {
        public List<VO.UnitVO> SelectUnit()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT *");
            sb.AppendLine("FROM Unit A ");
            SqlConnection aconn = MyConn();
            IDataReader myIDataReader = SqlHelper.ExecuteReader(aconn, CommandType.Text, sb.ToString(), null);

            List<VO.UnitVO> myUnitVOList = new List<ACMS.VO.UnitVO>();

            while (myIDataReader.Read())
            {
                VO.UnitVO myUnitVO = new ACMS.VO.UnitVO();
                myUnitVO.id = (int)myIDataReader["id"];
                myUnitVO.name = (string)myIDataReader["name"];
                myUnitVO.active = (string)myIDataReader["active"];
                myUnitVOList.Add(myUnitVO);
            }
            myIDataReader.Close();
            aconn.Close();
            return myUnitVOList;
        }

        public int InsertUnit(VO.UnitVO myUnitVO)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
            sqlParams[0].Value = myUnitVO.name;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT Unit (name,active) Values (@name,'Y'); ");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }


        public int UpdateUnit(VO.UnitVO myUnitVO)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = myUnitVO.id;
            sqlParams[1] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
            sqlParams[1].Value = myUnitVO.name;
            sqlParams[2] = new SqlParameter("@active", SqlDbType.NChar, 1);
            sqlParams[2].Value = myUnitVO.active;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("UPDATE Unit SET name=@name,active=@active WHERE id=@id;");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }
        /// <summary>
        /// 檢查名稱是否重覆
        /// </summary>
        /// <param name="id">主辦單位代號</param>
        /// <param name="name">主辦單位名稱</param>
        /// <returns>true=重覆,false=不重覆</returns>
        public bool chkDuplicateName(int id, string name)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = id;
            sqlParams[1] = new SqlParameter("@name", SqlDbType.NVarChar, 50);
            sqlParams[1].Value = name;


            StringBuilder sb = new StringBuilder();

            sb.AppendLine("declare @aa int select @aa=count(*) from Unit WHERE id<>@id and name=@name select isNull(@aa,0) ");

            return SqlHelper.ExecuteScalar(MyConn(), CommandType.Text, sb.ToString(), sqlParams).ToString() != "0";
        }
        /// <summary>
        /// 檢查主辦單位是否已在活動及角色table中已使用
        /// </summary>
        /// <param name="id">主辦單位代號</param>
        /// <returns>true=已使用,false=未使用 </returns>
        public bool isStart(int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = id;



            StringBuilder sb = new StringBuilder();

            sb.AppendLine("declare @aa int select @aa=count(*) from RoleUserMapping WHERE unit_id=@id select isNull(@aa,0) ");

            string start;
            start = SqlHelper.ExecuteScalar(MyConn(), CommandType.Text, sb.ToString(), sqlParams).ToString();
            if (start != "0")
            {
                return true;
            }

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = id;
            sb = new StringBuilder();
            sb.AppendLine("declare @aa int select @aa=count(*) from Activity WHERE org_id=@id  select isNull(@aa,0)");


            return SqlHelper.ExecuteScalar(MyConn(), CommandType.Text, sb.ToString(), sqlParams).ToString() != "0";


        }
        /// <summary>
        /// 刪除主辦單位
        /// </summary>
        /// <param name="id">主辦單位代號</param>
        public void Delete(int id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = id;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("delete from Unit WHERE id=@id  ");
            SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);          
        }

    }
}