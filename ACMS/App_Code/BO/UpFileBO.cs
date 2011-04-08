using System;
using System.Collections.Generic;
using System.Text;

namespace ACMS.BO
{
    [System.ComponentModel.DataObjectAttribute(true)]
    public class UpFileBO : BaseBO
    {

        //SELECT
        /// <summary>
        /// 取得附加檔案資料
        /// </summary>
        /// <param name="dirName">附加檔案目錄</param>
        /// <returns>取得附加檔案資料</returns>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public List<VO.UpFileVO> SELECT(string dirName)
        {
            DAO.UpFilestDAO myUpFilestDAO = new ACMS.DAO.UpFilestDAO();
            return myUpFilestDAO.SELECT(dirName);
        }





    }
}
