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
        /// <summary>
        /// 取得報名資訊-為了組成個人固定欄位
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="emp_id">員工</param>
        /// <returns>取得報名資訊-為了組成個人固定欄位</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public VO.ActivityRegistVO SelectActivityRegistByPK(Guid activity_id, string emp_id)
        {
            DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
            return myActivityRegistDAO.SelectActivityRegistByPK(activity_id, emp_id);
        }

     
        
        //取得該活動所有成功報名者名單
        /// <summary>
        /// 取得該活動所有成功報名者名單
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="activity_type">活動類別</param>
        /// <returns>取得該活動所有成功報名者名單</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public DataTable SelectEmployeesByID(Guid activity_id, string activity_type)
        {
            DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
            return myActivityRegistDAO.SelectEmployeesByID(activity_id, activity_type);
        }

        /// <summary>
        /// 取得報名後的報名順序
        /// </summary>
        /// <param name="activity">活動代號</param>
        /// <returns>取得報名後的報名順序</returns>
        public string getSNByActivity(Guid activity_id, string emp_id)
        {  
            DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
            return myActivityRegistDAO.getSNByActivity(activity_id, emp_id);
        
        }

        /// <summary>
        /// 取得取消名單清冊
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <param name="name">員工或隊長的中英文名字</param>
        /// <returns>取得取消名單清冊</returns>
        public DataTable GetCancelRegist(Guid activity_id, string name)
        {
            DAO.ActivityRegistDAO myActivityRegistDAO = new ACMS.DAO.ActivityRegistDAO();
            return myActivityRegistDAO.GetCancelRegist(activity_id, name);
        }
    }
}
