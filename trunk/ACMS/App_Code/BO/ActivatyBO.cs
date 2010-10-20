using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class ActivatyBO:BaseBO
    {



        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public VO.ActivatyVO SelectActivatyByActivatyID(Guid id)
        {
            DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
            return myActivatyDAO.SelectActivatyByID(id);
        }

        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable SelectActivatyDTByID(Guid id)
        {
            DAO.ActivatyDAO myActivatyDAO = new ACMS.DAO.ActivatyDAO();
            return myActivatyDAO.SelectActivatyDTByID(id);
        }


    }
}
