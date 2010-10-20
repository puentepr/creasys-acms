using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class UpFileBO : BaseBO
    {

        //SELECT
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.UpFileVO> SELECT(string dirName)
        {
            DAO.UpFilestDAO myUpFilestDAO = new ACMS.DAO.UpFilestDAO();
            return myUpFilestDAO.SELECT(dirName);
        }





    }
}
