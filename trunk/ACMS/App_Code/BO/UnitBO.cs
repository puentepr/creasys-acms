using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class UnitBO : BaseBO
    {
        //SELECT GridView的資料來源
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.UnitVO> SelectUnit()
        {
            DAO.UnitDAO myUnitDAO = new ACMS.DAO.UnitDAO();

            List<VO.UnitVO> myUnitVOList = new List<ACMS.VO.UnitVO>();

            myUnitVOList = myUnitDAO.SelectUnit();

            return myUnitVOList;
        }

        //Update GridView的Update
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public int UpdateUnit(VO.UnitVO myUnitVO)
        {
            DAO.UnitDAO myUnitDAO = new ACMS.DAO.UnitDAO();
            return myUnitDAO.UpdateUnit(myUnitVO);
        }

    }
}
