using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class ActivityRegistBO : BaseBO
    {
        //SELECT
        //取得報名資訊-為了組成個人固定欄位
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public VO.ActivityRegistVO SelectActivityRegistByPK(Guid activity_id, string emp_id)
        {
            DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
            return myActivityRegistDAO.SelectActivityRegistByPK(activity_id, emp_id);
        }

     
        
        //取得該活動所有成功報名者名單
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable SelectEmployeesByID(Guid activity_id)
        {
            DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
            return myActivityRegistDAO.SelectEmployeesByID(activity_id);

        }
    }
}
