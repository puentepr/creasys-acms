using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class CustomFieldBO : BaseBO
    {
        //SELECT
        /// <summary>
        /// 取得活動的自訂欄位設定
        /// </summary>
        /// <param name="activity_id">活動代號</param>
        /// <returns>取得活動的自訂欄位設定</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.CustomFieldVO> SelectByActivity_id(Guid activity_id)
        {
            DAO.CustomFieldDAO myCustomFieldDAO = new ACMS.DAO.CustomFieldDAO();
            return myCustomFieldDAO.SelectByActivity_id(activity_id);
        }

        //DELETE
        /// <summary>
        /// 刪除自訂欄位設定
        /// </summary>
        /// <param name="field_id">流水號</param>
        /// <returns>刪除自訂欄位設定</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public int DELETE(int field_id)
        {
            DAO.CustomFieldDAO myCustomFieldDAO = new ACMS.DAO.CustomFieldDAO();
            return myCustomFieldDAO.DELETE(field_id);
        }

    }
}
