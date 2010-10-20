using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class UnitDAO : BaseDAO
    {

        public List<VO.UnitVO> SELECT()
        {
  
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT *");
            sb.AppendLine("FROM Unit ");

            IDataReader myIDataReader =  SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.UnitVO> myUnitVOList = new List<ACMS.VO.UnitVO>();

            while (myIDataReader.Read())
            {
                VO.UnitVO myUnitVO = new ACMS.VO.UnitVO();
                myUnitVO.id=(string)myIDataReader["id"];
                myUnitVO.name = (string)myIDataReader["name"];
                myUnitVOList.Add(myUnitVO); 
            }

            return myUnitVOList;
        }

    

    }
}
