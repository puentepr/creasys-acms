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

            IDataReader myIDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.UnitVO> myUnitVOList = new List<ACMS.VO.UnitVO>();

            while (myIDataReader.Read())
            {
                VO.UnitVO myUnitVO = new ACMS.VO.UnitVO();
                myUnitVO.id = (int)myIDataReader["id"];
                myUnitVO.name = (string)myIDataReader["name"];
                myUnitVO.active = (string)myIDataReader["active"];
                myUnitVOList.Add(myUnitVO);
            }

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

    }
}