using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class UnitBO : BaseBO
    {
        //SELECT GridView的資料來源
        /// <summary>
        /// 主辦單位資料
        /// </summary>
        /// <returns>主辦單位資料</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.UnitVO> SelectUnit()
        {
            DAO.UnitDAO myUnitDAO = new ACMS.DAO.UnitDAO();

            List<VO.UnitVO> myUnitVOList = new List<ACMS.VO.UnitVO>();

            myUnitVOList = myUnitDAO.SelectUnit();

            return myUnitVOList;
        }

        //Update GridView的Update
        /// <summary>
        /// 修改主辦單位資料
        /// </summary>
        /// <param name="myUnitVO">主辦單位資料型別物件</param>
        /// <returns>主辦單位資料</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public int UpdateUnit(VO.UnitVO myUnitVO)
        {
            DAO.UnitDAO myUnitDAO = new ACMS.DAO.UnitDAO();
            return myUnitDAO.UpdateUnit(myUnitVO);
        }

         /// <summary>
        /// 檢查主辦單位是否已在活動及角色table中已使用
        /// </summary>
        /// <param name="id">主辦單位代號</param>
        /// <returns>true=已使用,false=未使用 </returns>
        public bool isStart(int id)
        {
            DAO.UnitDAO myUnitDAO = new ACMS.DAO.UnitDAO();
            return myUnitDAO.isStart(id);
        }

         /// <summary>
        /// 檢查名稱是否重覆
        /// </summary>
        /// <param name="id">主辦單位代號</param>
        /// <param name="name">主辦單位名稱</param>
        /// <returns>true=重覆,false=不重覆</returns>
        public bool chkDuplicateName(int id, string name)
        {
             DAO.UnitDAO myUnitDAO = new ACMS.DAO.UnitDAO();
             return myUnitDAO.chkDuplicateName(id, name);
        }
           /// <summary>
        /// 刪除主辦單位
        /// </summary>
        /// <param name="id">主辦單位代號</param>
        public void Delete(int id)
        {
            DAO.UnitDAO myUnitDAO = new ACMS.DAO.UnitDAO();
            myUnitDAO.Delete(id);
        }

    }
}
