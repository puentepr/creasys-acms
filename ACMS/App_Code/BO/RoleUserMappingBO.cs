using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class RoleUserMappingBO : BaseBO
    {
        //SELECT GridView的資料來源
        /// <summary>
        /// 取得角色對應檔資料
        /// </summary>
        /// <returns> 取得角色對應檔資料</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.RoleUserMappingVO> SelectRoleUserMapping()
        {
            DAO.RoleUserMappingDAO myRoleUserMappingDAO = new ACMS.DAO.RoleUserMappingDAO();

            List<VO.RoleUserMappingVO> myRoleUserMappingVOList = new List<ACMS.VO.RoleUserMappingVO>();

            myRoleUserMappingVOList = myRoleUserMappingDAO.SelectRoleUserMapping();

            return myRoleUserMappingVOList;
        }

        //Insert GridView的Insert
        /// <summary>
        /// 新增1筆角色對應資料
        /// </summary>
        /// <param name="myRoleUserMappingVO">角色對應型別物件</param>
        /// <returns>新增1筆角色對應資料</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public int InsertRoleUserMapping(VO.RoleUserMappingVO myRoleUserMappingVO)
        {
            DAO.RoleUserMappingDAO myRoleUserMappingDAO = new ACMS.DAO.RoleUserMappingDAO();
            return myRoleUserMappingDAO.InsertRoleUserMapping(myRoleUserMappingVO);

        }

        //Delete GridView的Delete
        /// <summary>
        /// 刪除1筆角色對應資料
        /// </summary>
        /// <param name="myRoleUserMappingVO">角色對應型別物件</param>
        /// <returns>刪除1筆角色對應資料</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public int DeleteRoleUserMapping(VO.RoleUserMappingVO myRoleUserMappingVO)
        {
            DAO.RoleUserMappingDAO myRoleUserMappingDAO = new ACMS.DAO.RoleUserMappingDAO();
            return myRoleUserMappingDAO.DeleteRoleUserMapping(myRoleUserMappingVO);
        }

    }
}
