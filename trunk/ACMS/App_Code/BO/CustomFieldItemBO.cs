using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class CustomFieldItemBO : BaseBO
    {
        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.CustomFieldItemVO> SelectByField_id(int field_id)
        {
            DAO.CustomFieldItemDAO myCustomFieldItemDAO = new ACMS.DAO.CustomFieldItemDAO();
            return myCustomFieldItemDAO.SelectByField_id(field_id);
        }

        //DELETE
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public int DELETE(int field_item_id)
        {
            DAO.CustomFieldItemDAO myCustomFieldItemDAO = new ACMS.DAO.CustomFieldItemDAO();
            return myCustomFieldItemDAO.DELETE(field_item_id);
        }
    }
}
