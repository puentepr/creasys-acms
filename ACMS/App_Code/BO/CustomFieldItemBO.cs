using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class CustomFieldItemBO : BaseBO
    {
        //SELECT
        /// <summary>
        /// 取得欄位選項的資料
        /// </summary>
        /// <param name="field_id">欄位代號流水號</param>
        /// <returns>取得欄位選項的資料</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.CustomFieldItemVO> SelectByField_id(int field_id)
        {
            DAO.CustomFieldItemDAO myCustomFieldItemDAO = new ACMS.DAO.CustomFieldItemDAO();
            return myCustomFieldItemDAO.SelectByField_id(field_id);
        }

        //DELETE
        /// <summary>
        /// 刪除一個自訂欄位的選項資料
        /// </summary>
        /// <param name="field_item_id">選項代號流水號</param>
        /// <returns>刪除一個自訂欄位的選項資料</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public int DELETE(int field_item_id)
        {
            DAO.CustomFieldItemDAO myCustomFieldItemDAO = new ACMS.DAO.CustomFieldItemDAO();
            return myCustomFieldItemDAO.DELETE(field_item_id);
        }
    }
}
