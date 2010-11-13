using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class CustomFieldBO : BaseBO
    {
        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.CustomFieldVO> SelectByActivity_id(Guid activity_id)
        {
            DAO.CustomFieldDAO myCustomFieldDAO = new ACMS.DAO.CustomFieldDAO();
            return myCustomFieldDAO.SelectByActivity_id(activity_id);
        }

        //DELETE
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public int DELETE(int field_id)
        {
            DAO.CustomFieldDAO myCustomFieldDAO = new ACMS.DAO.CustomFieldDAO();
            return myCustomFieldDAO.DELETE(field_id);
        }

    }
}
