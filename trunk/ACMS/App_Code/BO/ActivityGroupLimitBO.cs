using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class ActivityGroupLimitBO : BaseBO
    {

        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> SelectByActivity_id(Guid activity_id)
        {
            DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();
            return myActivityGroupLimitDAO.SelectByActivity_id(activity_id);
        }

        //DELETE
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public int DELETE(int id)
        {
            DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();
            return myActivityGroupLimitDAO.DELETE(id);
        }

    





    }
}
