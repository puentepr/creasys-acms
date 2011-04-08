using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class ActivityGroupLimitBO : BaseBO
    {

        //SELECT
        /// <summary>
        /// 取得活動限制人員清單
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <returns>取得活動限制人員清單</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.EmployeeVO> SelectByActivity_id(Guid activity_id)
        {
            DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();
            return myActivityGroupLimitDAO.SelectByActivity_id(activity_id);
        }

        //DELETE
        /// <summary>
        /// 刪除活動限制名冊
        /// </summary>
        /// <param name="id">流水編號</param>
        /// <returns>刪除活動限制名冊</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public int DELETE(int id)
        {
            DAO.ActivityGroupLimitDAO myActivityGroupLimitDAO = new ACMS.DAO.ActivityGroupLimitDAO();
            return myActivityGroupLimitDAO.DELETE(id);
        }

    





    }
}
