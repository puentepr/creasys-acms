using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class UnitBO : BaseBO
    {

        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.UnitVO> SelectActivatyByActivatyID()
        {
            DAO.UnitDAO myUnitDAO = new ACMS.DAO.UnitDAO();


            List<VO.UnitVO> myUnitVOList = new List<ACMS.VO.UnitVO>();

            myUnitVOList = myUnitDAO.SELECT();

            VO.UnitVO myUnitVO=new ACMS.VO.UnitVO();

            myUnitVO.id = "";
            myUnitVO.name="請選擇";
            myUnitVOList.Insert(0,myUnitVO);

            return myUnitVOList;
        }





    }
}
