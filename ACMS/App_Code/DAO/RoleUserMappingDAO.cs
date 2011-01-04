using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ACMS.DAO
{
    public class RoleUserMappingDAO : BaseDAO
    {
        public List<VO.RoleUserMappingVO> SelectRoleUserMapping()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT A.*,B.role_name,C.name as unit_name,D.C_DEPT_NAME,D.C_DEPT_ABBR,D.WORK_ID,D.NATIVE_NAME ");
            sb.AppendLine("FROM RoleUserMapping A ");
            sb.AppendLine("left join RoleList B on A.role_id=B.id ");
            sb.AppendLine("left join Unit C on A.unit_id=C.id ");
            sb.AppendLine("left join V_ACSM_USER2 D on A.emp_id=D.ID ");
            sb.AppendLine("order by C.name,B.id  ");  

            IDataReader myIDataReader = SqlHelper.ExecuteReader(MyConn(), CommandType.Text, sb.ToString(), null);

            List<VO.RoleUserMappingVO> myRoleUserMappingVOList = new List<ACMS.VO.RoleUserMappingVO>();

            while (myIDataReader.Read())
            {
                VO.RoleUserMappingVO myRoleUserMappingVO = new ACMS.VO.RoleUserMappingVO();
                myRoleUserMappingVO.id = (int)myIDataReader["id"];
                myRoleUserMappingVO.role_name = (string)myIDataReader["role_name"] ;
                myRoleUserMappingVO.unit_name = clsMyObj.GetStringObject(myIDataReader["unit_name"]);
                myRoleUserMappingVO.emp_id = (string)myIDataReader["emp_id"];
                myRoleUserMappingVO.C_DEPT_ABBR = clsMyObj.GetStringObject(myIDataReader["C_DEPT_ABBR"]);
                
                myRoleUserMappingVO.WORK_ID = clsMyObj.GetStringObject(myIDataReader["WORK_ID"]);
                myRoleUserMappingVO.NATIVE_NAME =clsMyObj.GetStringObject(myIDataReader["NATIVE_NAME"]);           

                myRoleUserMappingVOList.Add(myRoleUserMappingVO);
            }

            return myRoleUserMappingVOList;
        }

        public int InsertRoleUserMapping(VO.RoleUserMappingVO myRoleUserMappingVO)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];

            sqlParams[0] = new SqlParameter("@role_id", SqlDbType.Int);
            sqlParams[0].Value = myRoleUserMappingVO.role_id;
            sqlParams[1] = new SqlParameter("@unit_id", SqlDbType.Int);
            sqlParams[1].Value = myRoleUserMappingVO.unit_id;
            sqlParams[2] = new SqlParameter("@emp_id", SqlDbType.NVarChar, 100);
            sqlParams[2].Value = myRoleUserMappingVO.emp_id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO RoleUserMapping  SELECT @role_id,@unit_id,ID FROM V_ACSM_USER2 WHERE ID=@emp_id and status!=2 ; ");

            int Result=-1;

            try
            {
                Result = SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

                if (Result == 0)
                {
                    clsMyObj.ShowMessage("無法新增此人員!該人員不存在或已離職。");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if ((ex as System.Data.SqlClient.SqlException).Number == 2627)
                    {
                        clsMyObj.ShowMessage("該角色人員已存在!");
                    }
                    else
                    {
                        throw ex;
                    }

                }
                catch
                {
                    throw ex;
                }
            }

            return Result;


        }

        public int DeleteRoleUserMapping(VO.RoleUserMappingVO myRoleUserMappingVO)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = myRoleUserMappingVO.id;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DELETE RoleUserMapping WHERE id=@id;");

            return SqlHelper.ExecuteNonQuery(MyConn(), CommandType.Text, sb.ToString(), sqlParams);

        }

    }
}